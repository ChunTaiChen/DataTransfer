using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;

namespace 成德高中國中部轉檔.DAO
{
    /// <summary>
    /// 學期科目成績
    /// </summary>
    class SubjectScores
    {
        //                                        stud_no,           schoolyear,                semester_grade ,
        private static Dictionary<string, Dictionary<string, Dictionary<string, List<VO.SubjectScoreInfo>>>> allSubjectSemScores;
        private static List<VO.SubjectScoreInfo> subjectScores;

        public static void Load()
        {
            subjectScores = new List<VO.SubjectScoreInfo>();

            OdbcConnection cn = ConnectionHelper.GetConnection();
            string sql = "select * from mainh.dbf ";
            // string sql = "select * from mainh.dbf where 學年度='001'  "; //只轉100上學期資料
            OdbcDataAdapter adp = new OdbcDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                subjectScores.Add(new VO.SubjectScoreInfo(row));
            }

            dispatchRecords();
        }
        private static void dispatchRecords()
        {
            allSubjectSemScores = new Dictionary<string, Dictionary<string, Dictionary<string, List<VO.SubjectScoreInfo>>>>();
            foreach (VO.SubjectScoreInfo score in subjectScores)
            {
                if (!allSubjectSemScores.ContainsKey(score.StudentNumber))
                    allSubjectSemScores.Add(score.StudentNumber, new Dictionary<string, Dictionary<string, List<VO.SubjectScoreInfo>>>());
                if (!allSubjectSemScores[score.StudentNumber].ContainsKey(score.SchoolYear))
                    allSubjectSemScores[score.StudentNumber].Add(score.SchoolYear, new Dictionary<string, List<VO.SubjectScoreInfo>>());
                if (!allSubjectSemScores[score.StudentNumber][score.SchoolYear].ContainsKey(score.Semester))
                    allSubjectSemScores[score.StudentNumber][score.SchoolYear].Add(score.Semester, new List<VO.SubjectScoreInfo>());

                allSubjectSemScores[score.StudentNumber][score.SchoolYear][score.Semester].Add(score);                            

            }
        }

        public static List<VO.SubjectScoreInfo> GetSubjectScoreInfos()
        {
            return subjectScores;
        }

        public static Dictionary<string, Dictionary<string, List<VO.SubjectScoreInfo>>> GetSubjectSemScoresByStudentNo(string stud_no)
        {
            Dictionary<string, Dictionary<string, List<VO.SubjectScoreInfo>>> result = new Dictionary<string,Dictionary<string,List<VO.SubjectScoreInfo>>>();
            if (allSubjectSemScores.ContainsKey(stud_no))
                result = allSubjectSemScores[stud_no];

            return result;
        }

        public static List<VO.SubjectScoreInfo> GetSubjectScoresByStudNoSchoolyearSemester(string studentNo, string schoolyear, string semester)
        {
            List<VO.SubjectScoreInfo> result = new List<VO.SubjectScoreInfo>();
            Dictionary<string, Dictionary<string, List<VO.SubjectScoreInfo>>> schoolyear_semester_subjscores = GetSubjectSemScoresByStudentNo(studentNo);
            if (schoolyear_semester_subjscores.ContainsKey(schoolyear))
            {
                if (schoolyear_semester_subjscores[schoolyear].ContainsKey(semester))
                    result = schoolyear_semester_subjscores[schoolyear][semester];
            }
            return result ;
        }

        public static List<string> GetAllStudentNos()
        {
            return new List<string>(allSubjectSemScores.Keys);
        }

    }
}
