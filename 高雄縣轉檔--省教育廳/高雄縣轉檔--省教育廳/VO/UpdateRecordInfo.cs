using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace 高雄縣轉檔__省教育廳.VO
{
    class UpdateRecordInfo
    {
        public string StudentNo = "";
        public string UpdateDate = "";
        public string UpdateType = "";   //（0新生，1轉入，2復學，5輟學，6轉出，7修業，8畢業）
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
            this.StudentNo = dr["stud_no"].ToString().Trim();
            this.UpdateDate = dr["chg_date"].ToString().Split(new char[] { ' ' })[0];
            this.UpdateType = dr["chg_kind"].ToString().Trim();
            this.UpdateReason = dr["chg_reason"].ToString().Trim();
            this.ApproveBy = dr["apprv_by"].ToString().Trim();
            this.ApproveDate = dr["apprv_date"].ToString().Split(new char[] { ' ' })[0];
            if (this.ApproveDate == "1899/12/30") this.ApproveDate = "";
            this.ApproveNo = dr["apprv_no"].ToString().Trim();
            this.WhereToGo = dr["where_go"].ToString().Trim();
        }

        public string GetUpdateTypeString()
        {            
           // （0新生，1轉入，2復學，5輟學，6轉出，7修業，8畢業）
            string result = "";
            if (this.UpdateType == "0") result = "新生";
            if (this.UpdateType == "1") result = "轉入";
            if (this.UpdateType == "2") result = "復學";
            if (this.UpdateType == "5") result = "輟學";
            if (this.UpdateType == "6") result = "轉出";
            if (this.UpdateType == "7") result = "修業";
            if (this.UpdateType == "8") result = "畢業";
            
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
