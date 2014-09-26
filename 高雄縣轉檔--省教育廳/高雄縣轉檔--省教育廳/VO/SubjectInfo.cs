using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace 高雄縣轉檔__省教育廳.VO
{
	class SubjectInfo
	{
        public string SubjNo="";
        public string SubjName = "";
        public string DefaultHour = "";
        public string SchoolYear = "";
        public string Semester = "";
        public string GradeYear = "";

        public SubjectInfo()
        {
        }

        public SubjectInfo(IDataReader dr)
        {
            this.SubjNo = dr["subj_no"].ToString().Trim();
            this.SubjName = dr["subj_name"].ToString().Trim();
            this.DefaultHour = dr["defa_hour"].ToString().Trim();
        }

        /// <summary>
        /// 科目代碼的第一碼加上00，就是領域代碼
        /// </summary>
        /// <returns></returns>
        public string GetDomainNo()
        {
            string result = "";
            result = this.SubjNo.Substring(0, 1) + "00";
            return result;
        }
	}
}
