using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace 高雄縣轉檔__省教育廳.DAO
{
    //科目學期成績
    class SubjectSemScores
    {
        //                                        stud_no,           schoolyear,                semester_grade ,
        private static Dictionary<string, Dictionary<string, Dictionary<string, List<VO.SubjectSemScoreInfo>>>> allSubjectSemScores;
        public static void Load(int fromYear, int toYear)
        {
            allSubjectSemScores = new Dictionary<string, Dictionary<string, Dictionary<string, List<VO.SubjectSemScoreInfo>>>>();

            for (int i = fromYear; i <= toYear; i++)
            {
                OleDbConnection cn = ConnectionHelper.GetStageConnection(i);

                for (int semester = 1; semester < 3; semester++)
                {
                    for (int grade = 7; grade < 10; grade++)
                    {                      
                        string sql = string.Format("select * from y91g{0}{1}{2}.dbf", i.ToString(), semester.ToString(), grade.ToString());
                        cn.Open();
                        OleDbCommand cmd = new OleDbCommand(sql, cn);
                        IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        while (dr.Read())
                        {
                            string stud_no = dr["stud_no"].ToString().Trim();
                            if (!allSubjectSemScores.ContainsKey(stud_no))
                                allSubjectSemScores.Add(stud_no, new Dictionary<string, Dictionary<string, List<VO.SubjectSemScoreInfo>>>());
                            if (!allSubjectSemScores[stud_no].ContainsKey(i.ToString()))
                                allSubjectSemScores[stud_no].Add(i.ToString(), new Dictionary<string, List<VO.SubjectSemScoreInfo>>());
                            if (!allSubjectSemScores[stud_no][i.ToString()].ContainsKey(semester.ToString()))
                                allSubjectSemScores[stud_no][i.ToString()].Add(semester.ToString(), new List<VO.SubjectSemScoreInfo>());

                            VO.SubjectSemScoreInfo subjScore = new VO.SubjectSemScoreInfo(dr);

                            subjScore.SchoolYear = i;
                            subjScore.Semester = semester;
                            subjScore.GradeYear = grade;
                            allSubjectSemScores[stud_no][i.ToString()][semester.ToString()].Add(subjScore);                            
                        }
                        dr.Close();
                        cn.Close();
                    } // end for grade
                } //end for semester
            } // end for schoolyear
        } //end for function


        public static Dictionary<string, Dictionary<string, List<VO.SubjectSemScoreInfo>>> GetSubjectSemScoresByStudentNo( string stud_no)
        {
            Dictionary<string, Dictionary<string, List<VO.SubjectSemScoreInfo>>> result = null;
            if (allSubjectSemScores.ContainsKey(stud_no))
                result = allSubjectSemScores[stud_no];

            return result;
        }

        public static List<string> GetAllStudentNos()
        {
            return new List<string>(allSubjectSemScores.Keys);
        }

    }
}
