using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace 高雄縣轉檔__省教育廳.VO
{
    /// <summary>
    /// 領域學期成績
    /// </summary>
    class DomainSemScoreInfo
    {
        public string StudentNo = "";
        public string DomainNo = "";
        public string Hour = "";
        public string Score = "";
        public int SchoolYear;
        public int Semester ;
        public int GradeYear;
        public string TempDomainName="";

        public DomainSemScoreInfo()
        {
        }

        public DomainSemScoreInfo(IDataReader dr)
        {
            this.StudentNo = dr["stud_no"].ToString().Trim();
            this.DomainNo = dr["subj_no"].ToString().Trim();
            this.Hour = dr["hour"].ToString().Trim();
            this.Score = dr["hdtmscore"].ToString().Trim();
        }

        public string GetDomainName()
        {
            if (this.DomainNo == "100")
                return this.TempDomainName;
            else
                return DAO.Domains.GetDomainNameByCode(this.DomainNo);
        }


    }
}
