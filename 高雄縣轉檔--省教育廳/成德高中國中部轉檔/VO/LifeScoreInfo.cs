using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace 成德高中國中部轉檔.VO
{
    class LifeScoreInfo
    {
        public string StudentNo = "";
        public int SchoolYear ;
        public int Semester ;
        public int GradeYear ;


        public LifeScoreInfo()
        {
        }

        public LifeScoreInfo(IDataReader dr)
        {
            this.StudentNo = dr["stud_no"].ToString().Trim();
            this.Tidy = GetGrade(dr, 1);
            this.Polite = GetGrade(dr, 2);
            this.Behave = GetGrade(dr, 3);
            this.Responsible = GetGrade(dr, 4);
            this.CivicMinded = GetGrade(dr, 5);
            this.Caring = GetGrade(dr, 6);
            this.Coporative = GetGrade(dr, 7);

            this.TeamActivity = dr["teamact"].ToString().Trim();
            this.SchoolService = dr["shlsrvce"].ToString().Trim();
            this.CommunityService = dr["comsrvce"].ToString().Trim();
            this.GoodThingsInSchool = dr["shlin"].ToString().Trim();
            this.GoodThingsOutsideSchool = dr["shlout"].ToString().Trim();
            this.Recommend = dr["advise"].ToString().Trim();
            
        }

        private string GetGrade(IDataReader dr, int index)
        {
            string result = dr[index].ToString().Trim();
            if (!string.IsNullOrEmpty(result))
                result = (5 - int.Parse(result)).ToString();

            return result;
        }



        /// <summary>
        /// 愛整潔
        /// </summary>
        public string Tidy = "";
        /// <summary>
        /// 有禮貌
        /// </summary>
        public string Polite = "";
        /// <summary>
        /// 守秩序
        /// </summary>
        public string Behave = "";
        /// <summary>
        /// 責任心
        /// </summary>
        public string Responsible = "";
        /// <summary>
        /// 公德心
        /// </summary>
        public string CivicMinded = "";

        /// <summary>
        /// 友愛關懷
        /// </summary>
        public string Caring = "";

        /// <summary>
        /// 團隊合作
        /// </summary>
        public string Coporative = "";

        /// <summary>
        /// 學校活動
        /// </summary>
        public string TeamActivity = "";

        /// <summary>
        /// 校內服務
        /// </summary>
        public string SchoolService = "";

        /// <summary>
        /// 社區服務
        /// </summary>
        public string CommunityService = "";

        /// <summary>
        /// 校內特殊表現
        /// </summary>
        public string GoodThingsInSchool = "";

        /// <summary>
        /// 校外特殊表現
        /// </summary>
        public string GoodThingsOutsideSchool = "";

        /// <summary>
        /// 具體建議
        /// </summary>
        public string Recommend = "";



    }
}
