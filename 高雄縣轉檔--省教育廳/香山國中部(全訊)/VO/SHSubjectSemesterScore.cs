using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace 香山國中部_全訊_.VO
{
    /// <summary>
    /// 高中科目學期成績
    /// </summary>
    class SHSubjectSemesterScore
    {

        public string StudentNo { get; set; }
        public string SchoolYear { get; set; }
        public string Semester { get; set; }
        public string Credit { get; set; }
        public string ItemCat { get; set; } //分項類別
        public string GradeYear { get; set; } //成績年級
        public bool IsRequired { get; set; }
        public string Score { get; set; }  //科目成績
        public string Score1 { get; set; }  //原始成績
        public string Score2 { get; set; }  //補考成績
        public string Score3 { get; set; }  //重修成績      
        public bool IsGetScore { get; set; }  //是否取得學分
        public string CourseNo { get; set; }  //科目代碼
        public string ScoreYearAdjust { get; set ;}  //學年調整成績

        private string yearScore { get; set; } //學年成績

        private bool isYearAdjustScore { get; set; }    //是否年度調整成績

        public SHSubjectSemesterScore(DataRow dr, string semester)
        {
            this.StudentNo = dr["s_no"].ToString().Trim();
            VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(StudentNo);
            this.Semester = semester;
            this.GradeYear = dr["grade"].ToString().Trim();
            this.SchoolYear = (GradeYear == "1" ? stud.SchoolYear1 : (GradeYear == "2" ? stud.SchoolYear2 : (GradeYear == "3" ? stud.SchoolYear3 : "")));
            this.CourseNo = dr["c_no"].ToString().Trim();
            this.yearScore = dr["sc"].ToString().Trim();
            this.ItemCat = "學業";            

            if (Semester == "1")
            {
                this.IsRequired = (dr["u_mka"].ToString().Trim() == "A");
                this.Score1 = dr["sa"].ToString().Trim();
                this.Score2 = (dr["sap"].ToString().Trim() == "0.0") ? "" : dr["sap"].ToString().Trim();
                this.Score3 = (dr["sah"].ToString().Trim() == "0.0") ? "" : dr["sah"].ToString().Trim();
                this.isYearAdjustScore = (dr["cpa"].ToString().Trim() == "X");  //是否有年度調整成績                
            }
            else if (Semester == "2")
            {
                this.IsRequired = (dr["u_mkb"].ToString().Trim() == "A");
                this.Score1 = dr["sb"].ToString().Trim();
                this.Score2 = (dr["sbp"].ToString().Trim() == "0.0") ? "" : dr["sbp"].ToString().Trim() ;
                this.Score3 = (dr["sbh"].ToString().Trim() == "0.0") ? "" : dr["sbh"].ToString().Trim();
                this.isYearAdjustScore = (dr["cpb"].ToString().Trim() == "X");
                if (this.isYearAdjustScore) this.Score = this.Score3;
            }
            else if (Semester == "0")
            {
                this.Score1 = dr["sc"].ToString().Trim();
                this.Score2 = (dr["scp"].ToString().Trim() == "0.0") ? "" : dr["scp"].ToString().Trim();
                this.Score3 = (dr["sch"].ToString().Trim() == "0.0") ? "" : dr["sch"].ToString().Trim();
            }


                //計算科目成績

            //計算是否取得學分
            string g_mark = dr["g_mk"].ToString().Trim(); //空白代表取得學分，1: 上學期未取得，2: 下學期未取得，B: 上下學期都未取得
            if (string.IsNullOrWhiteSpace(g_mark))
                this.IsGetScore = true;
            else
            {
                if (this.Semester == "1")
                    this.IsGetScore = (g_mark != "B" && g_mark != "1");

                if (this.Semester == "2")
                    this.IsGetScore = (g_mark != "B" && g_mark != "2");
            }


                //原始分數
                this.Score = this.Score1;

                if (decimal.Parse(this.Score1) >= 60m)
                {
                    this.Score = this.Score1;
                }
                else
                {
                    //重修分數
                    if (!string.IsNullOrWhiteSpace(this.Score3) && (decimal.Parse(this.Score3) >= decimal.Parse(this.Score)))
                    {
                        this.Score = this.Score3;
                    }
                    else
                    {
                        //補考分數
                        if (!string.IsNullOrWhiteSpace(this.Score2) && (decimal.Parse(this.Score2) >= decimal.Parse(this.Score)))
                        {
                            this.Score = this.Score2;
                        }
                    }                  
                }

                if (this.Semester != "0")
                {
                    if (this.isYearAdjustScore)
                    {
                        this.Score = this.Score3;
                        this.ScoreYearAdjust = this.Score3;
                        this.Score3 = "";   //如果是學年調整成績，則sah, sbh 表是學年調整成績，而非重修成績。
                    }

                    //this.IsGetScore = (decimal.Parse(this.Score) >= 60m);
                }
        }

    }
}
