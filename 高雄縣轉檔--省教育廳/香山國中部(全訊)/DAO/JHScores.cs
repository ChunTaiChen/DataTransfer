using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace 香山國中部_全訊_.DAO
{
    class JHScores
    {
        //                    Dictionary<stud_no, List<VO.JHSubjectSemesterScore>>  
        private static Dictionary<string, List<VO.JHSubjectSemesterScore>> subjSemsScores = new Dictionary<string, List<VO.JHSubjectSemesterScore>>();

        //                     Dictionary<stud_no, Dictionary<domain_name, score>>
        private static Dictionary<string, List<VO.DomainScoreInfo>> domainSemsScores = new Dictionary<string, List<VO.DomainScoreInfo>>();


        public static List<string> GetAllStudentNos()
        {
            return new List<string>(subjSemsScores.Keys);
        }

        public static List<VO.JHSubjectSemesterScore> GetSubjectSemsScores(string studNo)
        {
            List<VO.JHSubjectSemesterScore> result = new List<VO.JHSubjectSemesterScore>();
            if (subjSemsScores.ContainsKey(studNo))
                result = subjSemsScores[studNo];

            return result;
        }

        public static List<VO.DomainScoreInfo> GetDomainSemsScores(string studNo)
        {
            List<VO.DomainScoreInfo> result = new List<VO.DomainScoreInfo>();
            if (domainSemsScores.ContainsKey(studNo))
                result = domainSemsScores[studNo];

            return result;
        }


        public static void Load()
        {
            OleDbConnection cn = ConnectionHelper.GetConnection1();
            //string sql = "select * from stus";
            string sql = "select * from stusn";     //實中國小部

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
                    //for (int gradeyear = 1; gradeyear < 4; gradeyear++)
                    for (int gradeyear = 1; gradeyear < 7; gradeyear++)
                    {
                        for(int semester =1; semester<3; semester ++)
                        {
                            VO.JHSubjectSemesterScore score = new VO.JHSubjectSemesterScore(row, gradeyear.ToString(), semester.ToString());
                            if (score.IsValidScore)
                            {
                                if (!subjSemsScores.ContainsKey(stud_no))
                                    subjSemsScores.Add(stud_no, new List<VO.JHSubjectSemesterScore>());

                                subjSemsScores[stud_no].Add(score);
                            }
                        }
                    } 
                } // end if

            }// end foreach


            //解析領域成績
            domainSemsScores = new Dictionary<string, List<VO.DomainScoreInfo>>();
            foreach (string stud_no in subjSemsScores.Keys)
            {
                if (!domainSemsScores.ContainsKey(stud_no))
                {
                    domainSemsScores.Add(stud_no, new List<VO.DomainScoreInfo>());
                }

                //                schoolyear             semester                domainName, credit
                Dictionary<string, Dictionary<string, Dictionary<string, int>>> credits = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>();
                //                schoolyear             semester                domainName, score
                Dictionary<string, Dictionary<string, Dictionary<string, decimal>>> scores = new Dictionary<string, Dictionary<string, Dictionary<string, decimal>>>();
                
                foreach (VO.JHSubjectSemesterScore score in subjSemsScores[stud_no])
                {
                    VO.CourseInfo ci = Courses.GetCourseByCourseCode(score.CourseNo);
                    if (!credits.ContainsKey(score.SchoolYear))
                    {
                        credits.Add(score.SchoolYear, new Dictionary<string,Dictionary<string,int>>());
                        scores.Add(score.SchoolYear, new Dictionary<string,Dictionary<string,decimal>>());
                    }
                    if (!credits[score.SchoolYear].ContainsKey(score.Semester))
                    {
                        credits[score.SchoolYear].Add(score.Semester, new Dictionary<string, int>());
                        scores[score.SchoolYear].Add(score.Semester, new Dictionary<string, decimal>());
                    }
                    if (!credits[score.SchoolYear][score.Semester].ContainsKey(ci.Domain))
                    {
                        credits[score.SchoolYear][score.Semester].Add(ci.Domain, 0); ;
                        scores[score.SchoolYear][score.Semester].Add(ci.Domain, 0m);
                    }
                    if (stud_no == "80001" && score.SchoolYear == "099" && score.Semester == "2" && ci.Domain == "自然與生活科技")
                        Console.WriteLine("stop");

                    credits[score.SchoolYear][score.Semester][ci.Domain] += int.Parse(score.Credit);
                    scores[score.SchoolYear][score.Semester][ci.Domain] += decimal.Parse(score.Score) * int.Parse(score.Credit);
                }

                foreach (string schoolYear in credits.Keys)
                {
                    foreach (string semester in credits[schoolYear].Keys)
                    {
                        foreach (string domainName in credits[schoolYear][semester].Keys)
                        {
                            if (stud_no == "80001" && schoolYear == "099" && schoolYear == "2" && domainName == "自然與生活科技")
                                Console.WriteLine("stop");

                            decimal finalScore = (scores[schoolYear][semester][domainName] / credits[schoolYear][semester][domainName]) + 0.5m;
                            string domainScore = Math.Floor(finalScore).ToString();
                            VO.DomainScoreInfo dmScore = new VO.DomainScoreInfo();
                            dmScore.StudentNo = stud_no;
                            dmScore.SchoolYear = schoolYear;
                            dmScore.Semester = semester;
                            dmScore.Hour = credits[schoolYear][semester][domainName].ToString();
                            dmScore.DomainName = domainName;
                            dmScore.Score = domainScore;                            
                            domainSemsScores[stud_no].Add(dmScore);
                        } // end foreach domainName
                    } // end foreach semester
                } // end foreach schoolyear
            }
        }
        
    }
}

