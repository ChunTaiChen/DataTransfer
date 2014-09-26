using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

namespace 高雄縣轉檔__省教育廳.VO
{
    class ParentInfo
    {
        private DataRow dr;
        public ParentInfo(DataRow dr)
        {
            this.dr = dr;
        }

        public string StudentNo { get { return dr["stud_no"].ToString().Trim(); } }

        public string FatherName { get { return dr["f_name"].ToString().Trim(); } }
        public string FatherBirthday { get { return dr["f_birth"].ToString().Trim(); } }
        public string FatherLive { get { return (dr["f_live"].ToString() == "1" ? "存" : "歿"); } }
        public string FatherEduc { get { return dr["f_educ"].ToString().Trim(); } }
        public string FatherOccupation { get { return dr["f_occupt"].ToString().Trim(); } }
        public string FatherUnit { get { return dr["f_unit"].ToString().Trim(); } }
        public string FatherTitle { get { return dr["f_title"].ToString().Trim(); } }
        public string FatherTel { get { return dr["f_tel"].ToString().Trim(); } }

        public string MotherName { get { return dr["m_name"].ToString().Trim(); } }
        public string MotherBirthday { get { return dr["m_birth"].ToString().Trim(); } }
        public string MotherLive { get { return (dr["m_live"].ToString() == "1" ? "存" : "歿"); } }
        public string MotherEduc { get { return dr["m_educ"].ToString().Trim(); } }
        public string MotherOccupation { get { return dr["m_occupt"].ToString().Trim(); } }
        public string MotherUnit { get { return dr["m_unit"].ToString().Trim(); } }
        public string MotherTitle { get { return dr["m_title"].ToString().Trim(); } }
        public string MotherTel { get { return dr["m_tel"].ToString().Trim(); } }
    }
}
