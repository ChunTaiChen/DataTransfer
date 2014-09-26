using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace 高雄縣轉檔__省教育廳.DAO
{
    class Subjects
    {
        //                    schoolyear_semester_grade ,  subj_no,  
        private static Dictionary<string, Dictionary<string, VO.SubjectInfo>> allSubjects;

        public static void Load(int fromYear, int toYear)
        {
            allSubjects = new Dictionary<string, Dictionary<string, VO.SubjectInfo>>();

            for (int i = fromYear; i <= toYear; i++)
            {
                OleDbConnection cn = ConnectionHelper.GetCourseConnection(i);

                for (int semester = 1; semester < 3; semester++)
                {
                    for (int grade = 7; grade < 9; grade++)
                    {
                        string key = string.Format("{0}_{1}_{2}", i.ToString(), semester.ToString(), grade.ToString());
                        if (!allSubjects.ContainsKey(key))
                            allSubjects.Add(key, new Dictionary<string, VO.SubjectInfo>());

                        string sql = string.Format("select * from y91s{0}{1}{2}.dbf", i.ToString(), semester.ToString(), grade.ToString());
                        cn.Open();
                        OleDbCommand cmd = new OleDbCommand(sql, cn);
                        IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        while (dr.Read())
                        {
                            VO.SubjectInfo subj = new VO.SubjectInfo(dr);
                            subj.SchoolYear = i.ToString();
                            subj.Semester = semester.ToString();
                            subj.GradeYear = grade.ToString();
                            allSubjects[key].Add(subj.SubjNo, subj);
                        }
                        dr.Close();
                        cn.Close();                        
                    } // end for grade
                } //end for semester
            } // end for schoolyear
        } //end for function

        /// <summary>
        /// 取得科目資料。因為科目是按照學年度、學期、年級開設的，所以要分開儲存。
        /// </summary>
        /// <param name="schoolyear"></param>
        /// <param name="semester"></param>
        /// <param name="grade"></param>
        /// <param name="subj_no"></param>
        /// <returns></returns>
        public static VO.SubjectInfo GetSubject(int schoolyear, int semester, int grade, string subj_no)
        {
            VO.SubjectInfo result = null;
            string key = string.Format("{0}_{1}_{2}", schoolyear.ToString(), semester.ToString(), grade.ToString());
            if (allSubjects.ContainsKey(key))
            {
                if (allSubjects[key].ContainsKey(subj_no))
                    result = allSubjects[key][subj_no];
            }
            return result;
        }
    }
}
