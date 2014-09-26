using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace 成德高中國中部轉檔.VO
{
    class SubjectInfo
    {
        public string No = "";
        public string Name = "";
        public string DomainID = "";
        public string Credit11 = "";
        public string Credit12 = "";
        public string Credit13 = "";
        public string Credit21 = "";
        public string Credit22 = "";
        public string Credit23 = "";
        public string Credit31 = "";
        public string Credit32 = "";
        public string Credit33 = "";

        public SubjectInfo(DataRow dr)
        {
            this.No = dr["代號"].ToString().Trim();
            this.Name = dr["名稱"].ToString().Trim();
            this.DomainID = dr["領域"].ToString().Trim();
            this.Credit11 = dr["節數11"].ToString().Trim();
            this.Credit12 = dr["節數12"].ToString().Trim();
            this.Credit13= dr["節數13"].ToString().Trim();
            this.Credit21 = dr["節數21"].ToString().Trim();
            this.Credit22 = dr["節數22"].ToString().Trim();
            this.Credit23 = dr["節數23"].ToString().Trim();
            this.Credit31 = dr["節數31"].ToString().Trim();
            this.Credit32 = dr["節數32"].ToString().Trim();
            this.Credit33 = dr["節數33"].ToString().Trim();
        }

        public String GetDomainName()
        {
            string result = "";
            if (!string.IsNullOrEmpty(this.DomainID))
            {
                string dnid = this.DomainID.Substring(0, 1);
                switch (dnid)
                {
                    case "1":
                        result = "語文";
                        break;
                    case "2":
                        result = "健康與體育";
                        break;
                    case "3":
                        result = "社會";
                        break;
                    case "4":
                        result = "藝術與人文";
                        break;
                    case "5":
                        result = "數學";
                        break;
                    case "6":
                        result = "自然與生活科技";
                        break;
                    case "7":
                        result = "綜合活動";
                        break;
                    case "8":
                        result = "學習";
                        break;
                }
            }
            return result;
        }
    }
}
