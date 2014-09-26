using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace 高雄縣轉檔__省教育廳.DAO
{
    class DomainSemScores
    {
        //                                        stud_no,           schoolyear,                semester_grade ,
        private static Dictionary<string, Dictionary<string, Dictionary<string, List<VO.DomainSemScoreInfo>>>> allDomainSemScores;
        public static void Load(int fromYear, int toYear)
        {
            allDomainSemScores = new Dictionary<string, Dictionary<string, Dictionary<string, List<VO.DomainSemScoreInfo>>>>();

            for (int i = fromYear; i <= toYear; i++)
            {
                OleDbConnection cn = ConnectionHelper.GetStageConnection(i);

                for (int semester = 1; semester < 3; semester++)
                {
                    for (int grade = 7; grade < 10; grade++)
                    {
                        string sql = string.Format("select * from y91t{0}{1}{2}.dbf", i.ToString(), semester.ToString(), grade.ToString());
                        cn.Open();
                        OleDbCommand cmd = new OleDbCommand(sql, cn);
                        IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        while (dr.Read())
                        {
                            string stud_no = dr["stud_no"].ToString();
                            if (!allDomainSemScores.ContainsKey(stud_no))
                                allDomainSemScores.Add(stud_no, new Dictionary<string, Dictionary<string, List<VO.DomainSemScoreInfo>>>());
                            if (!allDomainSemScores[stud_no].ContainsKey(i.ToString()))
                                allDomainSemScores[stud_no].Add(i.ToString(), new Dictionary<string, List<VO.DomainSemScoreInfo>>());
                            if (!allDomainSemScores[stud_no][i.ToString()].ContainsKey(semester.ToString()))
                                allDomainSemScores[stud_no][i.ToString()].Add(semester.ToString(), new List<VO.DomainSemScoreInfo>());

                            VO.DomainSemScoreInfo domainScore = new VO.DomainSemScoreInfo(dr);

                            domainScore.SchoolYear = i;
                            domainScore.Semester = semester;
                            domainScore.GradeYear = grade;
                            allDomainSemScores[stud_no][i.ToString()][semester.ToString()].Add(domainScore);
                        }
                        dr.Close();
                        cn.Close();
                    } // end for grade
                } //end for semester
            } // end for schoolyear
        } //end for function


        public static Dictionary<string, Dictionary<string, List<VO.DomainSemScoreInfo>>> GetDomainSemScoresByStudentNo(string stud_no)
        {
            Dictionary<string, Dictionary<string, List<VO.DomainSemScoreInfo>>> result = null;
            if (allDomainSemScores.ContainsKey(stud_no))
                result = allDomainSemScores[stud_no];

            return result;
        }

        public static List<string> GetAllStudentNos()
        {
            return new List<string>(allDomainSemScores.Keys);
        }
    }
}
