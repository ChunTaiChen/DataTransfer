using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace 香山國中部_全訊_.VO
{
    class DailyLifeScoreInfo
    {
        public string StudentNo { get; set; }
        public string SchoolYear { get; set; }
        public string Semester { get; set; }
        public string TeacherComment { get; set; }   //具體建議或導師評語
        public string OtherBehavior { get; set; }   //其它行為
        public string Z1 { get; set; }  //愛整潔
        public string Z2 { get; set; }  //有禮貌
        public string Z3 { get; set; }
        public string Z4 { get; set; }
        public string Z5 { get; set; }
        public string Z6 { get; set; }
        public string Z7 { get; set; }  //團隊合作

        public DailyLifeScoreInfo(DataRow dr, bool isJH,  string semester)
        {
            this.Semester = semester;
            this.StudentNo = dr["s_no"].ToString().Trim();
            this.SchoolYear = dr["year"].ToString().Trim();
            /* 香山國高中不同欄位，但實中國高中同一個欄位
            if (isJH)
            {
                this.TeacherComment = (semester == "1" ? dr["tena1"].ToString().Trim() : dr["tena2"].ToString().Trim());

                this.OtherBehavior = string.Format("團體活動紀錄：{0} ; 公共服務紀錄：{1} ; 校內外特殊表現紀錄：{2}",
                    dr["w91"].ToString().Trim(),
                    dr["w92a"].ToString().Trim() + "," + dr["w92b"].ToString().Trim(),
                    dr["w94a"].ToString().Trim() + "," + dr["w94b"].ToString()).Trim();

                this.Z1 = dr["zno1"].ToString();
                this.Z2 = dr["zno2"].ToString();
                this.Z3 = dr["zno3"].ToString();
                this.Z4 = dr["zno4"].ToString();
                this.Z5 = dr["zno5"].ToString();
                this.Z6 = dr["zno6"].ToString();
                this.Z7 = dr["zno7"].ToString();
            }
            else
            {
                this.TeacherComment = dr["w91"].ToString().Trim();
                if (!string.IsNullOrWhiteSpace(this.TeacherComment)) TeacherComment += ",";
                this.TeacherComment += dr["w92"].ToString().Trim();
                if (!string.IsNullOrWhiteSpace(this.TeacherComment)) TeacherComment += ",";
                this.TeacherComment += dr["w93"].ToString().Trim();
            }
             * */

            /*  實中的欄位 */
            this.TeacherComment = (semester == "1" ? dr["tena1"].ToString().Trim() : dr["tena2"].ToString().Trim());
            
            // 路竹轉檔註解
            /*
            this.Z1 = dr["zno11"].ToString();   //整潔習慣
            this.Z2 = dr["zno21"].ToString();   //禮節
            this.Z3 = dr["zno31"].ToString();   //生活秩序
            this.Z4 = dr["zno41"].ToString();   //責任心
            this.Z5 = dr["zno51"].ToString();   //公德心
            this.Z6 = dr["zno61"].ToString();   //友愛關懷
            this.Z7 = dr["zno71"].ToString();   //團隊合作
             * */
        }
    }
}
