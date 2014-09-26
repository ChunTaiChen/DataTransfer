using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace 香山國中部_全訊_.VO
{
    class JHSubjectSemesterScore
    {
        public string StudentNo { get; set; }
        public string SchoolYear { get; set; }
        public string Semester { get; set; }
        public string GradeYear { get; set; }
        public string Credit { get; set; }
        public string Score { get; set; }  //原始成績
        public string CourseNo { get; set; }  //科目代碼

        public bool IsValidScore { get; set; }

        public JHSubjectSemesterScore(DataRow dr, string gradeYear, string semester)
        {
            this.StudentNo = dr["s_no"].ToString().Trim();
            VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(StudentNo);
            this.Semester = semester;
            this.GradeYear = gradeYear;
            //this.SchoolYear = (GradeYear == "1" ? stud.SchoolYear1 : (GradeYear == "2" ? stud.SchoolYear2 : (GradeYear == "3" ? stud.SchoolYear3 : "")));
            switch (GradeYear)
            {
                case "1" :
                    this.SchoolYear = stud.SchoolYear1;
                    break;
                case "2":
                    this.SchoolYear = stud.SchoolYear2;
                    break;
                case "3":
                    this.SchoolYear = stud.SchoolYear3;
                    break;
                case "4":
                    this.SchoolYear = stud.SchoolYear4;
                    break;
                case "5":
                    this.SchoolYear = stud.SchoolYear5;
                    break;
                case "6":
                    this.SchoolYear = stud.SchoolYear6;
                    break;
            }
            
            this.CourseNo = dr["c_no"].ToString().Trim();

            if (this.StudentNo == "80001" && this.SchoolYear == "099" && this.Semester == "2" && this.CourseNo == "504")
                Console.WriteLine("stop");

            //判斷是否有效的科目成績(根據是否有學分數來判斷)
            //string wt = dr["wt" + gradeYear].ToString().Trim();
            if (dr["grade"].ToString() != this.GradeYear)   //如果年級不對，就不處理
            {
                this.IsValidScore = false;
            }
            else
            {
                string wt = dr["wt"].ToString().Trim(); //實中國中部只有 wt 欄位
                if (string.IsNullOrWhiteSpace(wt))
                    this.IsValidScore = false;
                else
                {
                    //判斷是否有學分數                
                    string[] credits = wt.Split(new char[] { '/' });
                    if (this.Semester == "1")
                    {
                        if (string.IsNullOrWhiteSpace(credits[0]))
                            this.IsValidScore = false;
                        else
                        {
                            this.IsValidScore = true;
                            this.Credit = credits[0];
                        }
                    }
                    else if (this.Semester == "2")
                    {
                        if (string.IsNullOrWhiteSpace(credits[1]))
                            this.IsValidScore = false;
                        else
                        {
                            this.IsValidScore = true;
                            this.Credit = credits[1];
                        }
                    }

                }

            }
            //取得成績
            //string scoreField = string.Format("s{0}{1}", (this.Semester == "1" ? "a" : "b"), this.GradeYear);
            string scoreField = string.Format("s{0}", (this.Semester == "1" ? "a" : "b"));  //實中國中部只有 sa, sb 欄位
            this.Score = dr[scoreField].ToString().Trim();

        }

    }
}
