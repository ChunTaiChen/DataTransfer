using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

namespace 成德高中國中部轉檔.VO
{
    class SemesterHistoryInfo
    {
        public SemesterHistoryInfo(DataRow dr)
        {
            this.SchoolYear = dr["學年度"].ToString().Substring(0,2);
            this.Semester = dr["學年度"].ToString().Substring(2, 1);
            this.Grade = dr["班號"].ToString().Substring(0, 1);
            this.ClassName = dr["班號"].ToString().Substring(1, 2);
            this.SeatNo = dr["班號"].ToString().Substring(3);
        }
        public SemesterHistoryInfo(string schoolyear, string semester, string grade, string className, string seatNo)
        {
            this.SchoolYear = schoolyear;
            this.Semester = semester;
            this.Grade = grade;
            this.ClassName = className;
            this.SeatNo = seatNo;
        }
        public string SchoolYear { get; set; }
        public string Semester { get; set; }
        public string Grade { get; set; }
        public string ClassName { get; set; }
        public string SeatNo { get; set; }
        /*
        public string GetHomeTeacherName()
        {
           
            string result = "";
            HomeTeacherInfo tea =  DAO.HomeTeachers.GetHomeTeacher(this.SchoolYear, this.Semester, this.Grade + this.ClassName);
            if (tea != null)
                result = tea.TeacherName;
            return result;
        }
         * */
    }
}
