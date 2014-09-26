using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace 高雄縣轉檔__省教育廳.VO
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

            this.StudentNo = dr["stud_no"].ToString().Trim();
            this.AbsentDate = DateTime.Parse(dr["abs_date"].ToString()).ToString("yyyy/MM/dd");

            if (!string.IsNullOrEmpty(dr["abs0"].ToString().Trim()))
                this.absenceDetail.Add("升旗", GetAbsenceType(dr["abs0"].ToString().Trim()));

            for (int i = 1; i < 9; i++)
            {
                if (!string.IsNullOrEmpty(dr["abs" + i.ToString()].ToString().Trim()))
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
            if (absCode.ToUpper() == "B") result = "公假";
            if (absCode.ToUpper() == "C") result = "曠課";
            if (absCode.ToUpper() == "V") result = "事假";
            if (absCode.ToUpper() == "M") result = "喪假";
            if (absCode.ToUpper() == "S") result = "病假";
            
            return result;
        }
    }
}
