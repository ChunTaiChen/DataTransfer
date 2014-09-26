using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.Odbc;

namespace 成德高中國中部轉檔.DAO
{
    //學期歷程
    class SemesterHistories
    {
        private static Dictionary<string, List<VO.SemesterHistoryInfo>> dtHistories;
        private static Dictionary<string, string> dtClasses;

        public static void Load()
        {
            dtHistories = new Dictionary<string, List<VO.SemesterHistoryInfo>>();
            dtClasses = new Dictionary<string, string>();

            OdbcConnection cn = ConnectionHelper.GetConnection();
            string sql = "select * from ycls.dbf";
            OdbcDataAdapter adp = new OdbcDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                string stud_no = row["學號"].ToString();
                if (!dtHistories.ContainsKey(stud_no))
                    dtHistories.Add(stud_no, new List<VO.SemesterHistoryInfo>());

                dtHistories[stud_no].Add(new VO.SemesterHistoryInfo(row));

                parseClass(row);
            }
        }

        private static void parseClass(DataRow dr)
        {
            if (dr["學年度"].ToString().Substring(0, 2) == "00")   //只處理 100 學年度的班級
            {
                string class_name = dr["班號"].ToString().Substring(1, 2);
                string grade = dr["班號"].ToString().Substring(0, 1);
                if (!dtClasses.ContainsKey(grade + class_name))
                    dtClasses.Add(grade + class_name, grade);
            }
        }

        public static List<VO.SemesterHistoryInfo> GetSemesterHistoriesByStudentNo(string studNo)
        {
            if (dtHistories == null)
                SemesterHistories.Load();

            List<VO.SemesterHistoryInfo> result = new List<VO.SemesterHistoryInfo>();
            if (dtHistories.ContainsKey(studNo))
            {
                result = dtHistories[studNo];
            }
            return result;
        }
        public static Dictionary<string, string> GetCurrentClasses()
        {
            return dtClasses;
        }

    }
}
