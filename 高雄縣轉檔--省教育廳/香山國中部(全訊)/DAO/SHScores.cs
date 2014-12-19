using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.OleDb;

namespace 香山國中部_全訊_.DAO
{
    /// <summary>
    /// 高中學期和學年科目成績，以及分項成績
    /// </summary>
    class SHScores
    {
        private static List<VO.SHSubjectSemesterScore> subjSemsScores = new List<VO.SHSubjectSemesterScore>(); //科目學期成績
        private static List<VO.SHLearnSemesterScore> learnSemsScores = new List<VO.SHLearnSemesterScore>();    //分項成績
        private static List<VO.SHSubjectSemesterScore> subjYearScores = new List<VO.SHSubjectSemesterScore>(); //科目學年成績
        private static List<VO.SHLearnSemesterScore> learnYearScores = new List<VO.SHLearnSemesterScore>();    //分項成績

        public static List<VO.SHSubjectSemesterScore> GetSubjectSemsScores()
        {
            return subjSemsScores;
        }

        public static List<VO.SHLearnSemesterScore> GetLearnSemsScores()
        {
            return learnSemsScores;
        }

        public static List<VO.SHSubjectSemesterScore> GetSubjectYearScores()
        {
            return subjYearScores;
        }

        public static List<VO.SHLearnSemesterScore> GetLearnYearScores()
        {
            return learnYearScores;
        }

        public static void Load()
        {
            OleDbConnection cn = ConnectionHelper.GetConnection1();
            string sql = "select * from stusn";
            OleDbDataAdapter adp = new OleDbDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                string stud_no = row["s_no"].ToString().Trim();
                VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(stud_no);
                if (!香山國中部_全訊_.Util.IsValidStudent(stud))
                    continue;

                string course_no = row["c_no"].ToString().Trim();
                VO.CourseInfo ci = Courses.GetCourseByCourseCode(course_no);
                  if (ci != null) //解析課程成績
                {
                      string[] credits = row["wt"].ToString().Split(new char[] { '/' });
                      //if (string.IsNullOrWhiteSpace(row["u_mka"].ToString().Trim()))
                      //{
                          //a. 上學期
                          VO.SHSubjectSemesterScore subjSemScore = new VO.SHSubjectSemesterScore(row, "1");
                          subjSemScore.Credit = credits[0];
                          subjSemsScores.Add(subjSemScore);
                      //}
                      //if (string.IsNullOrWhiteSpace(row["u_mkb"].ToString().Trim()))
                      //{
                        //b. 下學期
                        VO.SHSubjectSemesterScore subjSemScore2 = new VO.SHSubjectSemesterScore(row, "2");
                        if (credits.Length>1)
                            subjSemScore2.Credit = credits[1];
                        subjSemsScores.Add(subjSemScore2);
                      //}                   
                    //c. 科目學年成績
                      VO.SHSubjectSemesterScore subjYearScore = new VO.SHSubjectSemesterScore(row, "0");
                      subjYearScores.Add(subjYearScore);
                }
                else    //是否學業成績(分項成績)
                {
                    if (course_no == "A1")
                    {
                        string[] credits = row["wt"].ToString().Split(new char[] { '/' });
                        VO.SHLearnSemesterScore subjSemScore1 = new VO.SHLearnSemesterScore(row, "1");
                        learnSemsScores.Add(subjSemScore1);

                        VO.SHLearnSemesterScore subjSemScore2 = new VO.SHLearnSemesterScore(row, "2");
                        learnSemsScores.Add(subjSemScore2);

                        VO.SHLearnSemesterScore subjSemScore0 = new VO.SHLearnSemesterScore(row, "0");
                        learnYearScores.Add(subjSemScore0);
                    }
                }
            }
        }

    }
}
