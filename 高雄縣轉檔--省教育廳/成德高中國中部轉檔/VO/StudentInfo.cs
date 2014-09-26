using System;
using System.Collections.Generic;

using System.Text;
using System.Data;

namespace 成德高中國中部轉檔.VO
{
    class StudentInfo
    {
        private DataRow dr;
        private string currentGrade = "";
        private string currentClass = "";
        private string currentSeatNo = "";
        private List<SemesterHistoryInfo> smHistories;

        public StudentInfo(DataRow dr)
        {
            this.dr = dr;
            this.getSemesterHistories();
            //GetCurrentClassSeatNo();
            
        }

        public string StudentNumber { get { return dr["學號"].ToString().Trim(); } }
        public string StudentName { get { return dr["name"].ToString().Trim(); } }
        public string SSN { get { return dr["id_no"].ToString().Trim(); } }
        public string Gender { get {    return (dr["sex"].ToString() =="1" ? "男" : "女") ;  } }
        public string Birthday { get {    return dr["birthday"].ToString().Split(new char[]{' '})[0] ;  } }
        public string BirthPlace { get { return dr["native"].ToString().Trim(); } }    //出生地
        public string EntQualify { get { return dr["ent_qua"].ToString().Trim(); } }   //入學資格
        public string CertNo { get { return dr["畢業文號"].ToString().Trim(); } } //必修業證書字號
        public string EntranceCertNo { get { return dr["入學文號"].ToString().Trim(); } } //入學文號        
        public string OfficialAddress { get { return dr["reg_addr"].ToString().Trim(); } }
        public string ContactAddress { get { return dr["psnt_addr"].ToString().Trim(); } }
        public string ContactTel { get { return dr["tel"].ToString().Trim(); } }
        public string GdName { get { return dr["gd_name"].ToString().Trim(); } }
        public string GdRelation { get { return dr["gd_relate"].ToString().Trim(); } }
        public string GdAddr { get { return dr["gd_addr"].ToString().Trim(); } }
        public string GdTelO { get { return dr["gd_tel_o"].ToString().Trim(); } }
        public string GdTelH { get { return dr["gd_tel_h"].ToString().Trim(); } }
        public string GdJob { get { return dr["gd_occupt"].ToString().Trim(); } }
        public string EntStatus { get { return dr["ent_status"].ToString().Trim(); } } //入學狀態，0 : 新生，1: 轉入
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
            get {
                return (this.Status == "0" || this.Status == "1" || this.Status == "2");
            }
        }
        public string StatusText
        {
            get
            {
                string result = "一般";
                switch (this.Status)
                {
                    case "0" :
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
        /*
        public string SM1Year { get { return dr["schyear1"].ToString().Trim(); } }
        public string SM2Year { get { return dr["schyear2"].ToString().Trim(); } }
        public string SM3Year { get { return dr["schyear3"].ToString().Trim(); } }
        public string SM4Year { get { return dr["schyear4"].ToString().Trim(); } }
        public string SM5Year { get { return dr["schyear5"].ToString().Trim(); } }
        public string SM6Year { get { return dr["schyear6"].ToString().Trim(); } }
        public string Class1 { get { return dr["class1"].ToString().Trim(); } }
        public string Seat1 { get { return dr["seat1"].ToString().Trim(); } }
        public string Class2 { get { return dr["class2"].ToString().Trim(); } }
        public string Seat2 { get { return dr["seat2"].ToString().Trim(); } }
        public string Class3 { get { return dr["class3"].ToString().Trim(); } }
        public string Seat3 { get { return dr["seat3"].ToString().Trim(); } }
        public string Class4 { get { return dr["class4"].ToString().Trim(); } }
        public string Seat4 { get { return dr["seat4"].ToString().Trim(); } }
        public string Class5 { get { return dr["class5"].ToString().Trim(); } }
        public string Seat5 { get { return dr["seat5"].ToString().Trim(); } }
        public string Class6 { get { return dr["class6"].ToString().Trim(); } }
        public string Seat6 { get { return dr["seat6"].ToString().Trim(); } }
        */
        public string CurrentClass { get { return this.currentClass; } }
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

        private void getSemesterHistories()
        {
            this.smHistories = DAO.SemesterHistories.GetSemesterHistoriesByStudentNo(this.StudentNumber);

            //計算在校生的班級
            if (string.IsNullOrEmpty(this.StudentNumber) || this.StudentNumber.Length < 6)
                return;

            if (this.StudentNumber.Substring(0, 2) == "00" || this.StudentNumber.Substring(0, 2) == "99" || this.StudentNumber.Substring(0, 2) == "98")
            {
                if (this.Status == "0" || this.Status == "1" || this.Status == "2")
                {
                    string schoolsemester = "";
                    if (this.smHistories.Count > 0)
                    {
                        schoolsemester = this.smHistories[0].SchoolYear + this.smHistories[0].Semester;
                        if (schoolsemester.Substring(0, 1) == "0")
                            schoolsemester = ("1" + schoolsemester); //for 100 學年度
                        this.currentClass = this.smHistories[0].ClassName;
                        this.currentSeatNo = this.smHistories[0].SeatNo;
                        this.currentGrade = this.smHistories[0].Grade;

                        //find current class no and semester
                        foreach (VO.SemesterHistoryInfo history in this.smHistories)
                        {
                            string newschoolyearsemester = history.SchoolYear + history.Semester;
                            if (newschoolyearsemester.Substring(0, 1) == "0")
                                newschoolyearsemester = ("1" + newschoolyearsemester); //for 100 學年度

                            if (int.Parse(newschoolyearsemester) > int.Parse(schoolsemester))
                            {
                                schoolsemester = history.SchoolYear + history.Semester;
                                if (schoolsemester.Substring(0, 1) == "0")
                                    schoolsemester = ("1" + schoolsemester); //for 100 學年度
                                this.currentClass = history.ClassName;
                                this.currentSeatNo = history.SeatNo;
                                this.currentGrade = history.Grade;
                            }
                        }
                    }
                } // end if
            } // end if
        }

        /*
        private void parseSemesterHistories()
        {
            this.smHistories = new List<SemesterHistoryInfo>();
            if (!string.IsNullOrEmpty(this.SM1Year.Trim()))
                this.smHistories.Add(makeSemesterHistory(1));
            if (!string.IsNullOrEmpty(this.SM2Year.Trim()))
                this.smHistories.Add(makeSemesterHistory(2));
            if (!string.IsNullOrEmpty(this.SM3Year.Trim()))
                this.smHistories.Add(makeSemesterHistory(3));
            if (!string.IsNullOrEmpty(this.SM4Year.Trim()))
                this.smHistories.Add(makeSemesterHistory(4));
            if (!string.IsNullOrEmpty(this.SM5Year.Trim()))
                this.smHistories.Add(makeSemesterHistory(5));
            if (!string.IsNullOrEmpty(this.SM6Year.Trim()))
                this.smHistories.Add(makeSemesterHistory(6));
        }
        private SemesterHistoryInfo makeSemesterHistory(int semesterOrder)
        {
            SemesterHistoryInfo result = null;
            if (semesterOrder == 1)
                result = new SemesterHistoryInfo(this.SM1Year, "1", "7", this.Class1, this.Seat1);
            if (semesterOrder == 2)
                result = new SemesterHistoryInfo(this.SM2Year, "2", "7", this.Class2, this.Seat2);
            if (semesterOrder == 3)
                result = new SemesterHistoryInfo(this.SM3Year, "1", "8", this.Class3, this.Seat3);
            if (semesterOrder == 4)
                result = new SemesterHistoryInfo(this.SM4Year, "2", "8", this.Class4, this.Seat4);
            if (semesterOrder == 5)
                result = new SemesterHistoryInfo(this.SM5Year, "1", "9", this.Class5, this.Seat5);
            if (semesterOrder == 6)
                result = new SemesterHistoryInfo(this.SM6Year, "2", "9", this.Class6, this.Seat6);

            return result;
        }
        
        private void GetCurrentClassSeatNo()
        {
            if (!string.IsNullOrEmpty(this.Class6))
            {
                this.currentGrade = "9";
                this.currentClass = this.Class6.Trim();
                this.currentSeatNo = this.Seat6;
            }
            else
            {
                if (!string.IsNullOrEmpty(this.Class5))
                {
                    this.currentGrade = "9";
                    this.currentClass = this.Class5;
                    this.currentSeatNo = this.Seat5;
                }
                else
                {
                    if (!string.IsNullOrEmpty(this.Class4))
                    {
                        this.currentGrade = "8";
                        this.currentClass =  this.Class4;
                        this.currentSeatNo = this.Seat4;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.Class3))
                        {
                            this.currentGrade = "8";
                            this.currentClass = this.Class3;
                            this.currentSeatNo = this.Seat3;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(this.Class2))
                            {
                                this.currentGrade = "7";
                                this.currentClass = this.Class2;
                                this.currentSeatNo = this.Seat2;
                            }
                            else
                            {
                                this.currentGrade = "7";
                                this.currentClass = this.Class1;
                                this.currentSeatNo = this.Seat1;
                            }
                        }
                    }
                }
            }
         
        }
         * * */
    }
}
