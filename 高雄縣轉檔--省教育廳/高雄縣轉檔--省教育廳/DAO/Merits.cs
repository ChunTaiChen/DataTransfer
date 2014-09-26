using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace 高雄縣轉檔__省教育廳.DAO
{
    class Merits
    {
        private static List<VO.MeritInfo> allMerits;

        private static Dictionary<string, int[]> statisticsData;    //獎懲統計資料

        public static void Load(int fromYear, int toYear)
        {
            allMerits = new List<VO.MeritInfo>();

            for (int i = fromYear; i <= toYear; i++)
            {
                OleDbConnection cn = ConnectionHelper.GetPersonConnection(i);
                string sql = string.Format("select * from XDESRT{0}.dbf", i.ToString());
                OleDbDataAdapter adp = new OleDbDataAdapter(sql, cn);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    VO.MeritInfo merit = new VO.MeritInfo(row, i);                    
                    allMerits.Add(merit);
                    AddStatisticsData(merit);
                }
            }
        }

        private static void AddStatisticsData(VO.MeritInfo mi)
        {
            if (statisticsData == null)
                statisticsData = new Dictionary<string, int[]>();

            string key = string.Format("{0}_{1}_{2}", mi.StudentNo, mi.SchoolYear, mi.Semester);
            if (!statisticsData.ContainsKey(key))
                statisticsData.Add(key, new int[6]);

            int[] sData = statisticsData[key];
            sData[0] += int.Parse((mi.M1 == null) ? "0" : mi.M1);
            sData[1] += int.Parse((mi.M2 == null) ? "0" : mi.M2);
            sData[2] += int.Parse((mi.M3 == null) ? "0" : mi.M3);
            sData[3] += int.Parse((mi.D1 == null) ? "0" : mi.D1);
            sData[4] += int.Parse((mi.D2 == null) ? "0" : mi.D2);
            sData[5] += int.Parse((mi.D3 == null) ? "0" : mi.D3);
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
