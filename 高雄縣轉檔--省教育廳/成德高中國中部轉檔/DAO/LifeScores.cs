using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace 成德高中國中部轉檔.DAO
{
    /// <summary>
    /// 日常生活表現成績
    /// </summary>
    class LifeScores
    {
        /*
        //                                        stud_no,           schoolyear,                semester_grade ,
        private static Dictionary<string, Dictionary<string, Dictionary<string, List<VO.LifeScoreInfo>>>> allLifeScores;
        public static void Load(int fromYear, int toYear)
        {
            allLifeScores = new Dictionary<string, Dictionary<string, Dictionary<string, List<VO.LifeScoreInfo>>>>();

            for (int i = fromYear; i <= toYear; i++)
            {
                OleDbConnection cn = ConnectionHelper.GetLifeCheckConnection(i);

                for (int semester = 1; semester < 3; semester++)
                {
                    for (int grade = 7; grade < 10; grade++)
                    {
                        string sql = string.Format("select * from ydch{0}{1}{2}.dbf", i.ToString(), semester.ToString(), grade.ToString());
                        cn.Open();
                        OleDbCommand cmd = new OleDbCommand(sql, cn);
                        IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        while (dr.Read())
                        {
                            string stud_no = dr["stud_no"].ToString().Trim();
                            if (!allLifeScores.ContainsKey(stud_no))
                                allLifeScores.Add(stud_no, new Dictionary<string, Dictionary<string, List<VO.LifeScoreInfo>>>());
                            if (!allLifeScores[stud_no].ContainsKey(i.ToString()))
                                allLifeScores[stud_no].Add(i.ToString(), new Dictionary<string, List<VO.LifeScoreInfo>>());
                            if (!allLifeScores[stud_no][i.ToString()].ContainsKey(semester.ToString()))
                                allLifeScores[stud_no][i.ToString()].Add(semester.ToString(), new List<VO.LifeScoreInfo>());

                            VO.LifeScoreInfo lifeScore = new VO.LifeScoreInfo(dr);

                            lifeScore.SchoolYear = i;
                            lifeScore.Semester = semester;
                            lifeScore.GradeYear = grade;

                            allLifeScores[stud_no][i.ToString()][semester.ToString()].Add(lifeScore);
                        }
                        dr.Close();
                        cn.Close();
                    } // end for grade
                } //end for semester
            } // end for schoolyear
        } //end for function


        public static Dictionary<string, Dictionary<string, List<VO.LifeScoreInfo>>> GetLifeScoresByStudentNo(string stud_no)
        {
            Dictionary<string, Dictionary<string, List<VO.LifeScoreInfo>>> result = null;
            if (allLifeScores.ContainsKey(stud_no))
                result = allLifeScores[stud_no];

            return result;
        }

        public static List<string> GetAllStudentNos()
        {
            return new List<string>(allLifeScores.Keys);
        }
         * */
    }
}
