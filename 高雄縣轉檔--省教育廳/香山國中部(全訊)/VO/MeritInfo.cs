using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace 香山國中部_全訊_.VO
{
    class MeritInfo
    {
        public string StudentNo { get; set; }
        public string MeritDate { get; set; }
        public string SchoolYear { get; set; }
        public string Semester { get; set; }
        public string Reason { get; set; }
        public string M1 { get; set; }
        public string M2 { get; set; }
        public string M3 { get; set; }
        public string D1 { get; set; }
        public string D2 { get; set; }
        public string D3 { get; set; }
        public bool IsCanceled { get; set; }
        public string CancelDate { get; set; }
        
        public MeritInfo(DataRow dr)
        {
            this.StudentNo = dr["s_no"].ToString().Trim();
            this.MeritDate = Util.FormatDate(dr["date2"].ToString());            
            this.SchoolYear = DAO.Util.ParseSchoolYear(dr["year"].ToString().Trim());
            this.Semester = DAO.Util.ParseSemester(dr["year"].ToString().Trim());  //1,2,3,4,5,6            
            this.Reason = dr["thing"].ToString().Trim();
            string code = dr["code"].ToString().Trim().Substring(0, 1);
            
            string amt = dr["amt"].ToString();

            if (code == "1") this.M1 = amt;
            if (code == "2") this.M2 = amt;
            if (code == "3") this.M3 = amt;
            if (code == "5") this.D1 = amt;
            if (code == "6") this.D2 = amt;
            if (code == "7") this.D3 = amt;


            this.IsCanceled = (dr["mark"].ToString().Trim()=="2");
            //if (this.IsCanceled)
                this.CancelDate = Util.FormatDate(dr["date3"].ToString());          
        }

    }
}
