using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace 成德高中國中部轉檔.VO
{
    class AbsenceInfo
    {
        public string StudentNo { get; set; }
        public string AbsentDate { get; set; }
        public string SchoolYear { get; set; }
        public string Semester { get; set; }
        public string Grade { get; set; }
        
        private Dictionary<string, string> absenceDetail;

        public AbsenceInfo(DataRow dr)
        {
            this.absenceDetail = new Dictionary<string,string>();

            this.StudentNo = dr["學號"].ToString().Trim();
            this.AbsentDate = DateTime.Parse(dr["日期"].ToString()).ToString("yyyy/MM/dd");
            this.SchoolYear = DAO.Util.ParseSchoolYear(dr["學年度"].ToString().Trim());
            
            this.Semester = DAO.Util.ParseSemester(dr["學年度"].ToString().Trim());
            this.Grade = DAO.Util.ParseGrade(dr["班號"].ToString().Trim());

            if (dr["abs0"].ToString().Trim() != "0")
                this.absenceDetail.Add("升旗", GetAbsenceType(dr["abs0"].ToString().Trim()));

            for (int i = 1; i < 9; i++)
            {
                if (dr["abs" + i.ToString()].ToString().Trim() != "0")
                    this.absenceDetail.Add(GetChiChar(i), GetAbsenceType(dr["abs" + i.ToString()].ToString().Trim()));
            }            
        }

        public Dictionary<string, string> GetAbsenceDetail()
        {
            return this.absenceDetail;
        }

        private string GetChiChar(int num)
        {
            string result = "";
            if (num == 1) result = "一";
            if (num == 2) result = "二";
            if (num == 3) result = "三";
            if (num == 4) result = "四";
            if (num == 5) result = "五";
            if (num == 6) result = "六";
            if (num == 7) result = "七";
            if (num == 8) result = "八";

            return result;
        }

        private string GetAbsenceType(string absCode)
        {
            string result = "";
            if (absCode.ToUpper() == "4") result = "公假";
            if (absCode.ToUpper() == "1") result = "曠課";
            if (absCode.ToUpper() == "2") result = "事假";
            if (absCode.ToUpper() == "5") result = "喪假";
            if (absCode.ToUpper() == "3") result = "病假";
            
            return result;
        }
    }
}
