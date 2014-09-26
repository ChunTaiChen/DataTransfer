using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace 香山國中部_全訊_.VO
{
    class UpdateRecordInfo
    {
        public string StudentNo = "";
        public string UpdateDate = "";
        public string UpdateType = "";   //（0新生，1轉入，2復學，3. 中輟 5輟學，6轉出，7修業，8畢業）
        public string UpdateReason = "";
        public string ApproveBy = "";
        public string ApproveDate = "";
        public string ApproveNo = "";
        public string WhereToGo = "";

        public string SchoolYear="";
        public string Semester="";
        public string GradeYear = "";

        public UpdateRecordInfo()
        {
        }

        public UpdateRecordInfo(IDataReader dr)
        {
            this.StudentNo = dr["s_no"].ToString().Trim();
            this.UpdateDate = dr["e_date"].ToString().Split(new char[] { ' ' })[0];
            this.UpdateType = dr["e_no"].ToString().Trim();
            VO.CertNoParser cert = new VO.CertNoParser(dr["stue_no"].ToString(), true);
            //this.UpdateReason = "";   // dr[""].ToString().Trim();
            //this.ApproveBy = dr["apprv_by"].ToString().Trim();
            //this.ApproveDate = dr["apprv_date"].ToString().Split(new char[] { ' ' })[0];
            this.ApproveDate = cert.approve_date;
            this.ApproveNo = cert.approve_doc_no;
            //this.ApproveNo = dr["apprv_no"].ToString().Trim();
            this.WhereToGo = dr["els"].ToString().Trim();

            this.SchoolYear = dr["e_year"].ToString().Substring(0, 3);
            this.Semester = dr["e_year"].ToString().Substring(3, 1);
            this.GradeYear = dr["grade"].ToString();
        }

        public string GetUpdateTypeString()
        {
            // （0新生，1轉入，2復學，3中輟，5輟學，6轉出，7修業，8畢業）
            string result = "";
            result = DAO.EDCode.GetName(this.UpdateType);
            /*
            if (this.UpdateType == "0") result = "新生";
            if (this.UpdateType == "1") result = "轉入";
            if (this.UpdateType == "2") result = "復學";
            if (this.UpdateType == "3") result = "中輟";
            if (this.UpdateType == "5") result = "輟學";
            if (this.UpdateType == "6") result = "轉出";
            if (this.UpdateType == "7") result = "修業";
            if (this.UpdateType == "8") result = "畢業";
            */
            return result;
        }

        public void CalculateSchoolYearSemester()
        {
            if (string.IsNullOrEmpty(this.UpdateDate)) return;

            DateTime dtUpdate = DateTime.Parse(this.UpdateDate);

            this.SchoolYear = (dtUpdate.Year - 1911 ).ToString();

            if (dtUpdate.Month > 1 && dtUpdate.Month < 8)  //下學期 , 2~7
            {
                this.Semester = "2";
                this.SchoolYear = (dtUpdate.Year - 1911 - 1).ToString();
            }
            else    //上學期， 1, 8~12
            {
                this.Semester = "1";
                if (dtUpdate.Month < 2)  //1月
                    this.SchoolYear = (dtUpdate.Year - 1911-1).ToString();
            }

            //計算年級
            string stud_no_first_key = this.StudentNo.Substring(0, 1);
            this.GradeYear = (int.Parse(this.SchoolYear) - (90 + int.Parse(stud_no_first_key)) + 1).ToString();
            
        }

    }
}
