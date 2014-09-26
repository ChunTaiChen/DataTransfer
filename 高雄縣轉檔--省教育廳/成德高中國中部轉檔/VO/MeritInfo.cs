﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace 成德高中國中部轉檔.VO
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
        
        public MeritInfo(DataRow dr)
        {
            this.StudentNo = dr["學號"].ToString().Trim();
            this.MeritDate = DateTime.Parse(dr["日期"].ToString()).ToString("yyyy/MM/dd");
            this.SchoolYear = DAO.Util.ParseSchoolYear(dr["學年度"].ToString().Trim());
            this.Semester = DAO.Util.ParseSemester(dr["學年度"].ToString().Trim());  //1,2,3,4,5,6            
            this.Reason = dr["事由"].ToString().Trim();
            string type = dr["獎懲"].ToString().Trim();
            //if (type == "1")    //獎
            //{
            this.M1 = dr["大功"].ToString().Trim();
            this.M2 = dr["小功"].ToString().Trim();
            this.M3 = dr["嘉獎"].ToString().Trim();
            //}
            //else   //懲
            //{
            this.D1 = dr["大過"].ToString().Trim();
            this.D2 = dr["小過"].ToString().Trim();
            this.D3 = dr["警告"].ToString().Trim();
            //}

            this.IsCanceled = bool.Parse(dr["銷過"].ToString().Trim());
            if (this.IsCanceled)
                this.CancelDate = DateTime.Parse(dr["銷過日期"].ToString()).ToString("yyyy/MM/dd");          
        }

    }
}
