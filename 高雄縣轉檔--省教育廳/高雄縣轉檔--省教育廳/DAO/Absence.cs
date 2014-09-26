using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace 高雄縣轉檔__省教育廳.DAO
{
    class Absence
    {
        private static List<VO.AbsenceInfo> allAbsences;

        private static List<VO.AbsenceSummaryInfo> absenceSummary;    //獎懲統計資料

        public static void Load(int fromYear, int toYear)
        {
            loadDetail(fromYear, toYear);
            loadSummary(fromYear, toYear);
        }

        private static void loadDetail(int fromYear, int toYear)
        {
            allAbsences = new List<VO.AbsenceInfo>();

            for (int i = fromYear; i <= toYear; i++)
            {
                OleDbConnection cn = ConnectionHelper.GetAbsenceConnection(i);

                for (int semester = 1; semester < 3; semester++)
                {
                    for (int grade = 1; grade < 4; grade++)
                    {
                        string sql = string.Format("select * from yabs{0}{1}{2}.dbf", i.ToString(), semester.ToString(), grade.ToString());
                        OleDbDataAdapter adp = new OleDbDataAdapter(sql, cn);
                        DataSet ds = new DataSet();
                        adp.Fill(ds);
                        DataTable dt = ds.Tables[0];
                        foreach (DataRow row in dt.Rows)
                        {
                            VO.AbsenceInfo abs = new VO.AbsenceInfo(row);
                            abs.SchoolYear = i.ToString();
                            abs.Semester = semester.ToString();
                            abs.Grade = grade.ToString();
                            allAbsences.Add(abs);
                        }
                    }
                }
            }
        }

        private static void loadSummary(int fromYear, int toYear)
        {
            absenceSummary = new List<VO.AbsenceSummaryInfo>();

            for (int i = fromYear; i <= toYear; i++)
            {
                OleDbConnection cn = ConnectionHelper.GetPersonConnection(i);
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
