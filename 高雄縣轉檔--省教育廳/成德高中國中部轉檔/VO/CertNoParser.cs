using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 成德高中國中部轉檔.VO
{
    class CertNoParser
    {
        public string year = "";
        public string schoolyear = "";
        public string approve_date = "";
        public string approve_doc_no = "";
        public string approve_by = "";

        private bool isGraduate = false;    //是否畢業文號

        public CertNoParser(string certNo, bool isGraduate)
        {
            if (string.IsNullOrEmpty(certNo))
                return;

            

            this.isGraduate = isGraduate;

            this.approve_by = "新竹市政府";
            if (certNo.IndexOf("新竹") == 0)
            {
                this.approve_by = "新竹市政府";
                certNo = certNo.Substring(5);
            }

            int index = certNo.IndexOf("府");
            if (index == -1) return;

            if (index == 0) //府教學字 ...
            {
                this.approve_doc_no = certNo;
                return;
            }
            else
            {
                this.approve_doc_no = certNo.Substring(index);
                string date = certNo.Substring(0, index ).Trim();
                string[] date_parts = date.Split(new char[] { '.' });
                this.schoolyear = date_parts[0];
                this.year = (1911 + int.Parse(schoolyear) + (isGraduate ? -1 : 0)).ToString();
                this.approve_date = string.Format("{0}/{1}/{2}", year, date_parts[1], date_parts[2]);
            }
        }

        public string GetNewStudUpdateDate()
        {
            return string.Format("{0}/8/1", this.year);
        }

        public string GetGraduateStudUpdateDate()
        {
            return string.Format("{0}/7/31", this.year);
        }
    }
}
