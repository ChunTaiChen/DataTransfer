using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace 成德高中國中部轉檔.VO
{
    class AbsenceSummaryInfo
    {
        public string StudentNo { get; set; }
        public string SchoolYear { get; set; }
        public string Semester { get; set; }

        public string FlagCount { get; set; }      //集會曠課

        public string MCount { get; set; }   //喪假
        public string SCount { get; set; }  //病假
        public string VCount { get; set; }  //事假
        public string BCount { get; set; }  //公假
        public string CCount { get; set; }  //曠課

        private List<string[]> summaries;

        public AbsenceSummaryInfo(DataRow dr, int entranceSchoolYear)
        {
            this.StudentNo = dr["stud_no"].ToString();
            
            this.Semester = dr["term"].ToString();  //1,2,3,4,5,6
            //計算確實的學年度學期
            int sm = int.Parse(this.Semester);
            int x = (sm - 1) / 2;
            this.SchoolYear = (entranceSchoolYear + x).ToString();
            this.Semester = (sm - 2 * x).ToString();

            this.FlagCount = dr["flag_trm"].ToString();
            this.SCount = dr["sick_trm"].ToString();
            this.MCount = dr["mourn_trm"].ToString();
            this.VCount = dr["priv_trm"].ToString();
            this.BCount = dr["pub_trm"].ToString();
            this.CCount = dr["cut_trm"].ToString();

            this.summaries = new List<string[]>();
            this.summaries.Add(new string[] {"集會", "曠課", this.FlagCount });
            this.summaries.Add(new string[] {"一般", "喪假", this.MCount });
            this.summaries.Add(new string[] {"一般", "事假", this.VCount });
            this.summaries.Add(new string[] {"一般", "病假", this.SCount });
            this.summaries.Add(new string[] {"一般", "公假", this.BCount });
            this.summaries.Add(new string[] {"一般", "曠課", this.CCount });
        }

        public List<string[]> GetSummary()
        {
            return this.summaries;
        }
    }
}
