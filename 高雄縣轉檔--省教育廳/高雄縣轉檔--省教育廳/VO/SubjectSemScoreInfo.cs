using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace 高雄縣轉檔__省教育廳.VO
{
    /// <summary>
    /// 科目學期成績
    /// </summary>
    class SubjectSemScoreInfo
    {
        public string StudentNo = "";
        public string SubjectNo = "";
        public string Hour = "";
        public string Score = "";
        public int SchoolYear;
        public int Semester ;
        public int GradeYear;

        public SubjectSemScoreInfo()
        {
        }

        public SubjectSemScoreInfo(IDataReader dr)
        {
            this.StudentNo = dr["stud_no"].ToString().Trim();
            this.SubjectNo = dr["subj_no"].ToString().Trim();
            this.Hour = dr["hour"].ToString().Trim();
            this.Score = dr["hdtmscore"].ToString().Trim();
        }

        public string GetSubjectName()
        {
            string result = "";
            VO.SubjectInfo subj = DAO.Subjects.GetSubject(this.SchoolYear, this.Semester, this.GradeYear, this.SubjectNo);
            if (subj != null)
            {
                result = subj.SubjName;
            }
            
            return result;
        }

        /// <summary>
        /// 科目代碼的第一碼加上00，就是領域代碼
        /// </summary>
        /// <returns></returns>
        public string GetDomainNo()
        {
            string result = "";
            result = this.SubjectNo.Substring(0, 1) + "00";
            return result;
        }

        public string GetDomainName()
        {
            string domainName =  DAO.Domains.GetDomainNameByCode(this.GetDomainNo());
            if (this.GetDomainNo() == "100")
            {
                if (this.GetSubjectName().Trim() == "國文")
                    domainName = "國語文";
                else if (this.GetSubjectName().Trim() == "英語")
                    domainName = "英語";
            }

            if (this.GetDomainNo() =="800")
                domainName = "";

            return domainName;

        }
    }
}
