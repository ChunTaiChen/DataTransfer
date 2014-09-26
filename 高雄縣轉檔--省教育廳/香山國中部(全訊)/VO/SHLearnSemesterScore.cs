using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace 香山國中部_全訊_.VO
{
    /// <summary>
    /// 高中分項學期成績
    /// </summary>
    class SHLearnSemesterScore
    {
        public string StudentNo { get; set; }
        public string SchoolYear { get; set; }
        public string Semester { get; set; }
        public string ItemCat { get; set; } //分項類別
        public string GradeYear { get; set; } //成績年級
        public string Score { get; set; }  //科目成績
        public string Score1 { get; set; }  //原始成績
        public string Score2 { get; set; }  //補考成績
        public string Score3 { get; set; }  //重修成績      

        public SHLearnSemesterScore(DataRow dr, string semester)
        {
            this.StudentNo = dr["s_no"].ToString().Trim();
            VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(StudentNo);
            this.Semester = semester;
            this.GradeYear = dr["grade"].ToString().Trim();
            this.SchoolYear = (GradeYear == "1" ? stud.SchoolYear1 : (GradeYear == "2" ? stud.SchoolYear2 : (GradeYear == "3" ? stud.SchoolYear3 : "")));

            this.ItemCat = "學業";

            if (Semester == "1")
            {
                this.Score1 = dr["sa"].ToString().Trim();
                this.Score2 = dr["sap"].ToString().Trim();
                this.Score3 = dr["sah"].ToString().Trim() ;
            }
            else if (Semester == "2")
            {
                this.Score1 = dr["sb"].ToString().Trim();
                this.Score2 = dr["sbp"].ToString().Trim();
                this.Score3 = dr["sbh"].ToString().Trim();
            }
            else if (Semester == "0")
            {
                this.Score1 = dr["sc"].ToString().Trim();
                this.Score2 = dr["scp"].ToString().Trim();
                this.Score3 = dr["sch"].ToString().Trim();
            }

            //計算最後成績
            this.Score = this.Score1;
            if (decimal.Parse(this.Score2) > decimal.Parse(this.Score))
                this.Score = this.Score2;

            if (decimal.Parse(this.Score3) > decimal.Parse(this.Score))
                this.Score = this.Score3;
        }

    }
}
