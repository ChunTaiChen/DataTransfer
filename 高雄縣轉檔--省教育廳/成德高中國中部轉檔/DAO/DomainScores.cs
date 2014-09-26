using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;

namespace 成德高中國中部轉檔.DAO
{
    /// <summary>
    /// 學期領域成績, 
    /// </summary>
    class DomainScores
    {
        //                                        stud_no,           schoolyear,                semester  ,
        private static Dictionary<string, Dictionary<string, Dictionary<string, List<VO.DomainScoreInfo>>>> allDomainSemScores;
        private static List<VO.DomainScoreInfo> domainScores;

        public static void Load()
        {
            domainScores = new List<VO.DomainScoreInfo>();
            OdbcConnection cn = ConnectionHelper.GetConnectionA();
            string sql = "select 學年度, 班號, 學號, 姓名, 語文節數, 健康節數, 社會節數, 藝術節數, 數學節數, 自然節數, 綜合節數, 語文平均, 健康平均, 社會平均, 藝術平均, 數學平均, 自然平均, 綜合平均 from a81.dbf   ";
            //string sql = "select 學年度, 班號, 學號, 姓名, 語文節數, 健康節數, 社會節數, 藝術節數, 數學節數, 自然節數, 綜合節數, 語文平均, 健康平均, 社會平均, 藝術平均, 數學平均, 自然平均, 綜合平均 from aa81.dbf   ";  //只轉100上學期資料
            OdbcDataAdapter adp = new OdbcDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                    addDomainScore("語文",  row);
                    addDomainScore("健康",  row);
                    addDomainScore("社會", row);
                    addDomainScore("藝術", row);
                    addDomainScore("數學", row);
                    addDomainScore("自然", row);
                    addDomainScore("綜合", row);
            }

            dispatchRecords();
        }
        
        private static void addDomainScore(string domainName, DataRow dr)
        {
            VO.DomainScoreInfo score = new VO.DomainScoreInfo();

            score.SchoolYear = DAO.Util.ParseSchoolYear(dr["學年度"].ToString().Trim());
            score.Semester = DAO.Util.ParseSemester(dr["學年度"].ToString().Trim());
            score.GradeYear = DAO.Util.ParseGrade(dr["班號"].ToString().Trim());                        
            score.StudentNo = dr["學號"].ToString().Trim();
            score.Hour = dr[domainName + "節數"].ToString().Trim();
            score.Score = dr[domainName + "平均"].ToString().Trim();

            if (domainName == "自然")
                domainName = "自然與生活科技";
            else if (domainName == "健康")
                domainName = "健康與體育";
            else if (domainName == "藝術")
                domainName = "藝術與人文";
            else if (domainName == "綜合")
                domainName = "綜合活動";

            score.DomainName = domainName;

            domainScores.Add(score);
        }

        private static void dispatchRecords()
        {
            allDomainSemScores = new Dictionary<string, Dictionary<string, Dictionary<string, List<VO.DomainScoreInfo>>>>();
            foreach (VO.DomainScoreInfo score in domainScores)
            {
                string stud_no = score.StudentNo;
                if (!allDomainSemScores.ContainsKey(stud_no))
                    allDomainSemScores.Add(stud_no, new Dictionary<string, Dictionary<string, List<VO.DomainScoreInfo>>>());
                if (!allDomainSemScores[stud_no].ContainsKey(score.SchoolYear.ToString()))
                    allDomainSemScores[stud_no].Add(score.SchoolYear.ToString(), new Dictionary<string, List<VO.DomainScoreInfo>>());
                if (!allDomainSemScores[stud_no][score.SchoolYear.ToString()].ContainsKey(score.Semester.ToString()))
                    allDomainSemScores[stud_no][score.SchoolYear.ToString()].Add(score.Semester.ToString(), new List<VO.DomainScoreInfo>());

                allDomainSemScores[stud_no][score.SchoolYear.ToString()][score.Semester.ToString()].Add(score);
            }
        }

        public static List<VO.DomainScoreInfo> GetDomainScoreInfos()
        {
            return domainScores;
        }


        public static Dictionary<string, Dictionary<string, List<VO.DomainScoreInfo>>> GetDomainSemScoresByStudentNo(string stud_no)
        {
            Dictionary<string, Dictionary<string, List<VO.DomainScoreInfo>>> result = null;
            if (allDomainSemScores.ContainsKey(stud_no))
                result = allDomainSemScores[stud_no];

            return result;
        }

        public static List<string> GetAllStudentNos()
        {
            return new List<string>(allDomainSemScores.Keys);
        }

    }
}
