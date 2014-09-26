using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace 成德高中國中部轉檔.VO
{
    /// <summary>
    /// 科目學期成績
    /// </summary>
    class SubjectScoreInfo
    {   
        public string SchoolYear = "";
        public string Semester = "";
        public string ClassName = "";
        public string GradeYear = "";
        public string SeatNo = "";
        public string StudentNumber = "";
        public string SubjectNo = "";
        public string PScore1 = "";
        public string CScore1 = "";
        public string PScore2 = "";
        public string CScore2 = "";
        public string PScore3 = "";
        public string CScore3 = "";

        public SubjectScoreInfo(DataRow dr)
        {
            this.SchoolYear =  DAO.Util.ParseSchoolYear(dr["學年度"].ToString().Trim());
            this.Semester = DAO.Util.ParseSemester(dr["學年度"].ToString().Trim());
            this.GradeYear = DAO.Util.ParseGrade(dr["班號"].ToString().Trim());
            this.ClassName = DAO.Util.ParseClassName(dr["班號"].ToString().Trim());
            this.SeatNo = DAO.Util.ParseSeatNo(dr["班號"].ToString().Trim());
            this.StudentNumber = dr["學號"].ToString().Trim();
            this.SubjectNo = dr["科目"].ToString().Trim();
            this.PScore1 = dr["段考一"].ToString().Trim();
            this.CScore1 = dr["日常一"].ToString().Trim();
            this.PScore2 = dr["段考二"].ToString().Trim();
            this.CScore2 = dr["日常二"].ToString().Trim();
            this.PScore3 = dr["段考三"].ToString().Trim();
            this.CScore3 = dr["日常三"].ToString().Trim();
        }

        public VO.SubjectInfo GetSubjectInfo()
        {
            return DAO.Subjects.GetSubjectByNo(this.SubjectNo); 
        }

        public string GetSubjectName()
        {
            string result = "";
            VO.SubjectInfo subj = this.GetSubjectInfo();
            if (subj != null)
                result = subj.Name;

            return result;
        }

        public string GetDomainName()
        {
            string result = "";
            VO.SubjectInfo subj = this.GetSubjectInfo();
            if (subj != null)
                result = subj.GetDomainName();

            return result;
        }

        public string GetCredit()
        {
            string result = "";
            VO.SubjectInfo subj = this.GetSubjectInfo();
            if (subj != null)
            {
                if (this.GradeYear == "1")
                    result = subj.Credit11;
                else if (this.GradeYear == "2")
                    result = subj.Credit21;
                else if (this.GradeYear == "3")
                    result = subj.Credit31;
            }
            return result;
        }

        public string GetAverageScore()
        {
            VO.SubjectInfo subj =  GetSubjectInfo();
            string result = "";
            decimal totalscore = 0;
            decimal totalcredit = 0;
            bool hasScore = false;
            if (subj != null)
            {
                //1. 該學期的每一次評量總成績，對該次學分數的加權平均
                decimal p1 = (decimal.Parse(this.PScore1) + decimal.Parse(this.CScore1)) / 2;
                decimal credit1 = (this.GradeYear == "1" ? decimal.Parse(subj.Credit11) : (this.GradeYear == "2" ? decimal.Parse(subj.Credit21) : (this.GradeYear == "3" ? decimal.Parse(subj.Credit31) : 0)));
                if (p1 >= 0 || credit1 >= 0) //沒有缺考或該次考試比重不為0
                {
                    totalscore += p1 * credit1 ;
                    totalcredit += credit1;
                    hasScore = true;
                }
                decimal p2 = (decimal.Parse(this.PScore2) + decimal.Parse(this.CScore2)) / 2;
                decimal credit2 = (this.GradeYear == "1" ? decimal.Parse(subj.Credit12) : (this.GradeYear == "2" ? decimal.Parse(subj.Credit22) : (this.GradeYear == "3" ? decimal.Parse(subj.Credit32) : 0)));
                if (p2 >= 0 || credit2 >= 0)
                {
                    totalscore += p2 * credit2;
                    totalcredit += credit2;
                    hasScore = true;
                }
                decimal p3 = (decimal.Parse(this.PScore3) + decimal.Parse(this.CScore3)) / 2;
                decimal credit3 = (this.GradeYear == "1" ? decimal.Parse(subj.Credit13) : (this.GradeYear == "2" ? decimal.Parse(subj.Credit23) : (this.GradeYear == "3" ? decimal.Parse(subj.Credit33) : 0)));
                if (p3 >= 0 || credit3 >= 0)
                {
                    totalscore += p3 * credit3;
                    totalcredit += credit3;
                    hasScore = true;
                }
                if (hasScore)
                    result =(totalcredit==0? 0 : Math.Round(totalscore / totalcredit, 2)).ToString();
            }
            return result;
        }
    }
}
