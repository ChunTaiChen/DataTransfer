using System;
using System.Collections.Generic;

using System.Text;

namespace 高雄縣轉檔__省教育廳.VO
{
    class SemesterHistoryInfo
    {
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
        public string GetHomeTeacherName()
        {
            string result = "";
            HomeTeacherInfo tea =  DAO.HomeTeachers.GetHomeTeacher(this.SchoolYear, this.Semester, this.Grade + this.ClassName);
            if (tea != null)
                result = tea.TeacherName;
            return result;
        }
    }
}
