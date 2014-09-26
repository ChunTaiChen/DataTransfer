using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;

namespace 成德高中國中部轉檔.DAO
{
    class Students
    {
        private static Dictionary<string, VO.StudentInfo> dicStudents;

        public static void Load()
        {
            dicStudents = new Dictionary<string, VO.StudentInfo>();

            OdbcConnection cn = ConnectionHelper.GetConnection();
            string sql = "select * from XBASIC.dbf   ";
            OdbcDataAdapter adp = new OdbcDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                dicStudents.Add(row["學號"].ToString(), new VO.StudentInfo(row));
            }               
        }

        public static List<string> GetStudentNoList()
        {
            return new List<string>(dicStudents.Keys);            
        }

        public static List<VO.StudentInfo> GetStudents()
        {
            return new List<VO.StudentInfo>(dicStudents.Values);
        }

        public static VO.StudentInfo GetStudentByStudNo(string stud_no)
        {
            VO.StudentInfo result = null;
            if (dicStudents.ContainsKey(stud_no))
                result = dicStudents[stud_no];

            return result;
        }

    }
}
