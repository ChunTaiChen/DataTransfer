using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;

namespace 成德高中國中部轉檔.DAO
{
    class Subjects
    {
        private static Dictionary<string, VO.SubjectInfo> dicSubjects;

        public static void Load()
        {
            dicSubjects = new Dictionary<string, VO.SubjectInfo>();

            OdbcConnection cn = ConnectionHelper.GetConnection();
            string sql = "select * from sschcour2.dbf   ";
            OdbcDataAdapter adp = new OdbcDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                dicSubjects.Add(row["代號"].ToString(), new VO.SubjectInfo(row));
            }
        }

        public static List<string> SubjectList()
        {
            return new List<string>(dicSubjects.Keys);
        }

        public static List<VO.SubjectInfo> GetStudents()
        {
            return new List<VO.SubjectInfo>(dicSubjects.Values);
        }

        public static VO.SubjectInfo GetSubjectByNo(string subj_no)
        {
            VO.SubjectInfo result = null;
            if (dicSubjects.ContainsKey(subj_no))
                result = dicSubjects[subj_no];

            return result;
        }

    }
}
 