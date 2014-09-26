using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

namespace 香山國中部_全訊_.VO
{
    class CourseInfo
    {
        public string CourseNo = "";
        public string CourseName = "";
        
        private DataRow row ;

        private Dictionary<string, string> vols;    //冊別

        public CourseInfo(DataRow row  )
        {             
            this.CourseNo = row["c_no"].ToString().Trim();
            this.CourseName = row["c_na"].ToString().Trim();
            this.row = row;

            decideVolumns();  //計算冊別
        }

        private void decideVolumns()
        {
            int volNo = 1;
            vols = new Dictionary<string, string>();
            //年級
            for (int grade = 1; grade < 4; grade++)
            {
                string wt1 = row["wt" + grade].ToString().Trim();
                if (!string.IsNullOrWhiteSpace(wt1))
                {
                    string[] credits = wt1.Split(new char[] { '/' });
                    int semester = 1;   //上學期
                    if (!string.IsNullOrWhiteSpace(credits[0]))
                    {
                        vols.Add(grade.ToString() + "_" + semester.ToString(), volNo.ToString());
                        volNo += 1;
                    }

                    if (credits.Length > 1)
                    {
                        semester = 2;  //下學期
                        if (!string.IsNullOrWhiteSpace(credits[1]))
                        {
                            vols.Add(grade.ToString() + "_" + semester.ToString(), volNo.ToString());
                            volNo += 1;
                        }
                    }
                }
              
            }
        }

        public string Domain
        {
            get
            {
                return CourseInfo.GetDomainByCourseNo(this.CourseNo);
            }
        }

        public string GetSubjectLevel(string gradeYear, string semester)
        {
            string result = "";
            string key = string.Format("{0}_{1}", gradeYear, semester) ;
            if (this.vols.ContainsKey(key))
                result = this.vols[key];

            return result;
        }

        public static String GetDomainByCourseNo(string courseNo)
        {
            return DAO.Courses.GetDomainNameByCourseCode(courseNo);
            /*
            string result = "";
            if (courseNo.Length > 0)
            {
                if (courseNo == "101") 
                    result = "本國語文";
                else if (courseNo == "102")
                    result = "英語";
                else
                {
                    string domain_code = courseNo.Substring(0, 1);
                    if (domain_code == "2") result = "體育與健康教育";
                    if (domain_code == "3") result = "社會";
                    if (domain_code == "4") result = "藝術與人文";
                    if (domain_code == "5") result = "自然與生活科技";
                    if (domain_code == "6") result = "數學";
                    if (domain_code == "7") result = "綜合活動";
                }
            }
            return result;
             
             * */
        }
    }
}
