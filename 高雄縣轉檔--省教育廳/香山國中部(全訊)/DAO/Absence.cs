using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.Odbc;

namespace 香山國中部_全訊_.DAO
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

            OleDbConnection cn = ConnectionHelper.GetConnection2();
            string sql = "select * from work";
            OleDbDataAdapter adp = new OleDbDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                    VO.AbsenceInfo abs = new VO.AbsenceInfo(row);
                    allAbsences.Add(abs);
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
