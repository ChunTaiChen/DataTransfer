using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.Odbc;

namespace 成德高中國中部轉檔.DAO
{
    class Absence
    {
        private static List<VO.AbsenceInfo> allAbsences;

        private static List<VO.AbsenceSummaryInfo> absenceSummary;    //獎懲統計資料

        public static void Load()
        {
            loadDetail();
            //loadSummary(fromYear, toYear);
        }

        private static void loadDetail()
        {
            allAbsences = new List<VO.AbsenceInfo>();

            OdbcConnection cn = ConnectionHelper.GetConnectionA();
            string sql = "select * from abs.dbf";
            //string sql = "select * from aabs.dbf"; //只轉100上學期資料
            OdbcDataAdapter adp = new OdbcDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                if (row["學號"].ToString().Substring(0, 2) == "98" || row["學號"].ToString().Substring(0, 2) == "99" || row["學號"].ToString().Substring(0, 2) == "00")
                {
                    VO.AbsenceInfo abs = new VO.AbsenceInfo(row);
                    allAbsences.Add(abs);
                }
            }
        }
        /*
        private static void loadSummary()
        {
            absenceSummary = new List<VO.AbsenceSummaryInfo>();

            for (int i = fromYear; i <= toYear; i++)
            {
                OleDbConnection cn = ConnectionHelper.GetConnectionA();
                string sql = string.Format("select * from XABSTM{0}.dbf", i.ToString());
                OleDbDataAdapter adp = new OleDbDataAdapter(sql, cn);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    VO.AbsenceSummaryInfo summary = new VO.AbsenceSummaryInfo(row, i);
                    absenceSummary.Add(summary);
                }
            }
        }
         * */

        public static List<VO.AbsenceSummaryInfo> GetAllSummary()
        {
            return absenceSummary;
        }

        public static List<VO.AbsenceInfo> GetAllAbsence()
        {
            return allAbsences;
        }

    }
}
