using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

namespace 香山國中部_全訊_.VO
{
    class StudentInfo
    {
        private DataRow dr;
        private string currentGrade = "";
        private string currentClass = "";
        private string currentFullClassName = "";
        private string currentSeatNo = "";
        private List<SemesterHistoryInfo> smHistories;

        public StudentInfo(DataRow dr)
        {
            this.dr = dr;
            this.currentGrade = dr["grade"].ToString().Trim();
            this.currentClass = dr["class"].ToString().Trim();
            this.currentFullClassName = dr["cla"].ToString().Trim();
            this.currentSeatNo = dr["s_no1"].ToString().Trim();
            DAO.Classes.Add(new VO.ClassInfo(this.currentClass, this.currentGrade, this.currentFullClassName));
        }

        public string StudentNumber { get { return dr["s_no"].ToString().Trim(); } }
        public string StudentName { get { return dr["s_na"].ToString().Trim(); } }
        public string EnglishName { get { return dr["s_na2"].ToString().Trim(); } }
        public string SSN { get { return dr["perno"].ToString().Trim(); } }
        public string Gender { get { return (dr["sex"].ToString() == "1" ? "男" : "女"); } }
        public string Birthday { 
            get { 
                String dt = dr["birth"].ToString().Replace(" ", "");    //因為台中惠文有學生的生日中有空白，所以要先去空白。2014/07/17, kevin
                return dt.Split(new char[] { ' ' })[0]; 
            } 
        }
        public string BirthPlace { get { return ""; } }    //出生地

        public string GraduateDate { get { return dr["gra_date"].ToString().Trim(); } } //畢業日期
        public string GraduateID { get { return dr["gra_id"].ToString().Trim(); } } //畢修業證書字號
        public string CertNo { get { return dr["gra_no"].ToString().Trim(); } } //畢修業證書文號

        public string KB_Year { get { return dr["kb_year"].ToString().Trim(); } }   //國小畢業年度
        public string KB_Area { get { return dr["kb_area"].ToString().Trim(); } }   //國小區域
        public string KB_Sco { get { return dr["kb_sco"].ToString().Trim(); } }   //國小名稱
        public string KB_Cla { get { return dr["kb_cla"].ToString().Trim(); } }   //國小畢業班級

        public string EntQualify { get { return dr["code_no"].ToString().Trim(); } }   //入學資格
        public string EntranceDate { get { return dr["in_date"].ToString().Trim(); } } //入學日期        
        public string EntranceCertNo { get { return dr["in_no"].ToString().Trim(); } } //入學核准文號        

        public string OfficialAddress { get { return dr["ch_addr"].ToString().Trim(); } }
        public string ContactAddress { get { return dr["address"].ToString().Trim(); } }
        public string ContactTel { get { return dr["tel"].ToString().Trim(); } }
        public string MobilePhone { get { return dr["mobile"].ToString().Trim(); } }
        
        public string GdName { get { return dr["name"].ToString().Trim(); } }
        public string GdRelation { get { return dr["rel"].ToString().Trim(); } }
        public string GdAddr { get { return ""; } }
        public string GdTelO { get { return ""; } }
        public string GdTelH { get { return ""; } }
        public string GdJob { get { return ""; } }

        public string SchoolYear1 { get { return dr["yr1"].ToString().Trim(); } }   //一年級的學年度
        public string SchoolYear2 { get { return dr["yr2"].ToString().Trim(); } }   //二年級的學年度
        public string SchoolYear3 { get { return dr["yr3"].ToString().Trim(); } }   //三年級的學年度
        public string SchoolYear4 { get { return dr["yr4"].ToString().Trim(); } }   //四年級的學年度
        public string SchoolYear5 { get { return dr["yr5"].ToString().Trim(); } }   //五年級的學年度
        public string SchoolYear6 { get { return dr["yr6"].ToString().Trim(); } }   //六年級的學年度

        public string EntType { get { return dr["war"].ToString().Trim(); } }   //入學身分
        public string EntStatus { get { return dr["war"].ToString().Trim(); } }

        public string LastUpdateCode { get { return dr["e_no"].ToString().Trim(); } }

        public string Perna { get { return dr["perna"].ToString().Trim(); } }  //學生身分

        public string Perna2 { get { return dr["perna2"].ToString().Trim(); } }  //原住民族別

        //public string EntStatus { get { return dr["war"].ToString().Trim(); } } //入學狀態，0 : 新生，1: 轉入

        //目前狀態。 0: 沒有異動，1:轉入，2:復學，5:輟學，6:轉出，7: 修業，8: 畢業。
        public string Status
        {
            get
            {
                return dr["status"].ToString();
            }
        }

        public bool IsInSchool
        {
            get
            {
                return (this.Status == "0" || this.Status == "1" || this.Status == "2");
            }
        }

        public string StatusText
        {
            get
            {
                string result = "";

                if (!string.IsNullOrWhiteSpace(this.CertNo))  //判斷是否畢業
                    result = "畢業或離校";
                else {
                    if (!string.IsNullOrWhiteSpace(this.LastUpdateCode))  //判斷異動代碼                
                        result = DAO.EDCode.GetName(this.LastUpdateCode);
                    else
                        result = "一般生";
                }
                return result;
            }
        }

        /*
        public string StatusText
        {
            get
            {
                string result = "一般";
                switch (this.Status)
                {
                    case "0":
                        result = "一般";
                        break;
                    case "1":
                        result = "轉入";
                        break;
                    case "2":
                        result = "復學";
                        break;
                    case "5":
                        result = "輟學";
                        break;
                    case "6":
                        result = "轉出";
                        break;
                    case "7":
                        result = "休業";
                        break;
                    case "8":
                        result = "畢業";
                        break;
                }

                return result;
            }
        }
         * */

        public string CurrentClass { get { return this.currentClass; } }
        public string CurrentFullClassName { get { return this.currentFullClassName; } }
        public string CurrentSeatNo { get { return this.currentSeatNo; } }
        public string CurrentGrade { get { return this.currentGrade; } }

        /*
        public VO.ParentInfo Parent
        {
            get {
                VO.ParentInfo p = DAO.Parents.GetParentByStudNo(this.StudentNumber);
                return p;
            }
        }
        */
        /// <summary>
        /// 取得這位學生的學習歷程
        /// </summary>
        /// <returns></returns>
        public List<VO.SemesterHistoryInfo> GetLearnHistory()
        {
            return this.smHistories;
        }

    }
}

