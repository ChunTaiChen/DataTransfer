using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace 香山國中部_全訊_.DAO
{
    class Merits
    {
        private static List<VO.MeritInfo> allMerits;

        private static Dictionary<string, int[]> statisticsData;    //獎懲統計資料

        private static Dictionary<string, string> codes;  //獎懲類別代碼對照表

        public static void Load()
        {
            allMerits = new List<VO.MeritInfo>();

            OleDbConnection cn = ConnectionHelper.GetConnection2();
            string sql = "select * from ds";
            OleDbDataAdapter adp = new OleDbDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                VO.MeritInfo merit = new VO.MeritInfo(row);                    
                allMerits.Add(merit);
                //AddStatisticsData(merit);
            }

            codes = new Dictionary<string, string>();
            codes.Add("1", "大功");
            codes.Add("2", "小功");
            codes.Add("3", "嘉獎");
            codes.Add("4", "留察");
            codes.Add("5", "大過");
            codes.Add("6", "小過");
            codes.Add("7", "警告");
            codes.Add("8", "勒退");
            codes.Add("9", "輔導");
            codes.Add("A", "優點");
            codes.Add("B", "缺點");
            codes.Add("C", "假日輔導");
            
        }
        
        public static List<VO.MeritInfo> GetAllMerits()
        {
            return allMerits;
        }

        public static Dictionary<string, int[]> GetMeritStatisticsData()
        {
            return statisticsData ;
        }
    }
}
