using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace 香山國中部_全訊_.VO
{
    class AbsenceInfo
    {
        public string StudentNo { get; set; }
        public string AbsentDate { get; set; }
        public string SchoolYear { get; set; }
        public string Semester { get; set; }
        public string Grade { get; set; }
        public string Code { get; set; }
        public string LsMark { get; set; }  //節次清單
        public string ClMark { get; set; }  //未銷假節次清單，只有Code=1 (曠課或遲到) 才會有

        public string AbsenceType { get; set; }

        public string PeriodList = "";

        private List<string> periods;

        private string beLatePeriods = "ABCDEFNGHIJKLM";  //遲到專用節次
        private string absencePeriods = "OP1234W5678QZX";  //一般節次
        private Dictionary<string, string> periodMappingLate;  //遲到節次對照表
        private Dictionary<string, string> periodMappingAbs;  //一般節次對照表

        public AbsenceInfo(DataRow dr)
        {
            this.periods = new List<string>();
            initPeriodMapping();

            this.StudentNo = dr["s_no"].ToString().Trim();
            this.AbsentDate = Util.FormatDate(dr["date2"].ToString());
            this.SchoolYear = DAO.Util.ParseSchoolYear(dr["year"].ToString().Trim());
            this.Semester = DAO.Util.ParseSemester(dr["year"].ToString().Trim());
            this.Code = dr["code"].ToString().Trim();
            this.LsMark = dr["lsmark"].ToString().Trim();
            this.ClMark = dr["clmark"].ToString().Trim();

            this.Grade = "";// DAO.Util.ParseGrade(dr["班號"].ToString().Trim());
            //判斷缺曠內容：
            //1. 如果 Code = 1且 ClMark 不為空值，這些節次才是
            if (Code == "1")
            {
                if (!string.IsNullOrWhiteSpace(this.ClMark))                
                    this.PeriodList = this.ClMark;                
            }
            else  //2. 如果 Code != 1，則看 LsMark
            {
                this.PeriodList = this.LsMark;                
            }

            for (int i = 0; i < this.PeriodList.Length; i++)
            {
                string period = this.PeriodList.Substring(i, 1).Trim();
                if (!string.IsNullOrWhiteSpace(period))
                    this.AddAbsenceRec( period.ToUpper());
            }

            //判斷缺曠類別
            if (this.PeriodList.Length > 0)
                this.AbsenceType = this.GetAbsType(this.Code, this.PeriodList.Substring(0, 1));
            else
                this.AbsenceType = "";
        }


        private void initPeriodMapping()
        {
            /*
             * OA 早讀
                PB 升旗
                1C  第1節
                2D  第2節
                3E  第3節
                4F  第4節
                WN 午休
                5G  第5節
                6H  第6節
                7I  第7節
                8J  第8節
                QK 降旗
                ZL  第9節
                XM 第10節
            */
            this.periodMappingLate = new Dictionary<string, string>();
            this.periodMappingAbs = new Dictionary<string, string>();
            //private string beLatePeriods = "ABCDEFNGHIJKLM";  //遲到專用節次
            //private string absencePeriods = "OP1234W5678QZX";  //一般節次
            this.periodMappingAbs.Add("O", "早讀");
            this.periodMappingAbs.Add("P", "升旗");
            this.periodMappingAbs.Add("1", "一");
            this.periodMappingAbs.Add("2", "二");
            this.periodMappingAbs.Add("3", "三");
            this.periodMappingAbs.Add("4", "四");
            this.periodMappingAbs.Add("W", "午休");
            this.periodMappingAbs.Add("5", "五");
            this.periodMappingAbs.Add("6", "六");
            this.periodMappingAbs.Add("7", "七");
            this.periodMappingAbs.Add("8", "八");
            this.periodMappingAbs.Add("Q", "降期");
            this.periodMappingAbs.Add("Z", "九");
            this.periodMappingAbs.Add("X", "十");

            this.periodMappingLate.Add("A", "早讀");
            this.periodMappingLate.Add("B", "升旗");
            this.periodMappingLate.Add("C", "一");
            this.periodMappingLate.Add("D", "二");
            this.periodMappingLate.Add("E", "三");
            this.periodMappingLate.Add("F", "四");
            this.periodMappingLate.Add("N", "午休");
            this.periodMappingLate.Add("G", "五");
            this.periodMappingLate.Add("H", "六");
            this.periodMappingLate.Add("I", "七");
            this.periodMappingLate.Add("J", "八");
            this.periodMappingLate.Add("K", "降期");
            this.periodMappingLate.Add("L", "九");
            this.periodMappingLate.Add("M", "十");
        }

        public List<string> GetPeriods()
        {
            return this.periods;
        }


        private void AddAbsenceRec( string periodCode)
        {
            /*
             * 1 曠課 或 遲到
                3 病假
                4 事假
                5 公假
                6 喪假
                7 婚假
                8 產假
                A 生理假
                B 產前假
                C 流產假
                D 育嬰假
             * */
            //1. 判斷節次
            string periodText = "";
            if (this.periodMappingLate.ContainsKey(periodCode))
                periodText = this.periodMappingLate[periodCode];
            else if (this.periodMappingAbs.ContainsKey(periodCode))
                periodText = this.periodMappingAbs[periodCode];
            else
                 periodText = "未知節次：" + periodCode;

            this.periods.Add(periodText);
        }

        private string GetAbsType(string absCode, string periodCode)
        {
            string result = "未知假別:" + absCode ;
            if (absCode == "1")
            {
                if (this.periodMappingLate.ContainsKey(periodCode.ToUpper()))
                    result = "遲到";

                if (this.periodMappingAbs.ContainsKey(periodCode.ToUpper()))
                    result = "曠課";
            }
            if (absCode.ToUpper() == "3") result = "病假";
            if (absCode.ToUpper() == "4") result = "事假";
            if (absCode.ToUpper() == "5") result = "公假";
            if (absCode.ToUpper() == "6") result = "喪假";
            if (absCode.ToUpper() == "7") result = "婚假";
            if (absCode.ToUpper() == "8") result = "產假";
            if (absCode.ToUpper() == "A") result = "生理假";
            if (absCode.ToUpper() == "B") result = "產前假";
            if (absCode.ToUpper() == "C") result = "流產假";
            if (absCode.ToUpper() == "D") result = "育嬰假";

            return result;
        }
    }
}
