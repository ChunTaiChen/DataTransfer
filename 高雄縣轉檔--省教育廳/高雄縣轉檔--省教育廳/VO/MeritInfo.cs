using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace 高雄縣轉檔__省教育廳.VO
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
        
        public MeritInfo(DataRow dr, int entranceSchoolYear)
        {
            this.StudentNo = dr["stud_no"].ToString().Trim();
            this.MeritDate = DateTime.Parse(dr["dsrt_date"].ToString()).ToString("yyyy/MM/dd");
            this.Semester = dr["term"].ToString().Trim();  //1,2,3,4,5,6
            //計算確實的學年度學期
            int sm = int.Parse(this.Semester);
            int x = (sm -1) / 2 ;
            this.SchoolYear = (entranceSchoolYear + x).ToString();
            this.Semester = (sm - 2 * x).ToString();

            this.Reason = dr["reason"].ToString().Trim();
            string type = dr["is_merit"].ToString().Trim();
            if (type == "1")    //獎
            {
                this.M1 = dr["l_desert"].ToString().Trim();
                this.M2 = dr["m_desert"].ToString().Trim();
                this.M3 = dr["t_desert"].ToString().Trim();
            }
            else   //懲
            {
                this.D1 = dr["l_desert"].ToString().Trim();
                this.D2 = dr["m_desert"].ToString().Trim();
                this.D3 = dr["t_desert"].ToString().Trim();
            }

            this.IsCanceled = bool.Parse(dr["is_dem"].ToString().Trim());
            if (this.IsCanceled)
                this.CancelDate = DateTime.Parse(dr["dsrt_date"].ToString()).ToString("yyyy/MM/dd");          
        }

    }
}
