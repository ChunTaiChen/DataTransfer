using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace 香山國中部_全訊_.DAO
{
    /// <summary>
    /// 日常生活表現
    /// </summary>
    class DailyLifeScore
    {
        private static List<VO.DailyLifeScoreInfo> lifeScores;
        public static void Load()
        {
            lifeScores = new List<VO.DailyLifeScoreInfo>();

            OleDbConnection cn = ConnectionHelper.GetConnection2();

            /* 上學期 */
            //string sql = "select * from dscorea";
            string sql = "select s_no, year, tena1, tena2, zno11,zno21,zno31,zno41,zno51,zno61,zno71 from dscorea";
            OleDbDataAdapter adp = new OleDbDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                VO.DailyLifeScoreInfo dscore = new VO.DailyLifeScoreInfo(row, ConnectionHelper.IsJH, "1");
                lifeScores.Add(dscore);
            }

            /* 下學期 */
            sql = "select s_no, year, tena1, tena2, zno11,zno21,zno31,zno41,zno51,zno61,zno71 from dscoreb";
            adp = new OleDbDataAdapter(sql, cn);
            ds = new DataSet();
            adp.Fill(ds);
            dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                VO.DailyLifeScoreInfo dscore = new VO.DailyLifeScoreInfo(row, ConnectionHelper.IsJH, "2");
                lifeScores.Add(dscore);                
            }
        }

        public static  List<VO.DailyLifeScoreInfo> GetAllDailyLiftScores()
        {
            return lifeScores;
        }
    }
}
