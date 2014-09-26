using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;

namespace 香山國中部_全訊_.DAO
{
    class Students
    {
        private static Dictionary<string, VO.StudentInfo> dicStudents;

        public static void Load()
        {
            dicStudents = new Dictionary<string, VO.StudentInfo>();

            OleDbConnection cn = ConnectionHelper.GetConnection1();
            //string sql = "select * from stu.dbf where ( (year = '100') OR (year = '101') OR (year = '102')) and ";
            string sql = "select * from stu.dbf where ( (year = '098') OR (year = '099') OR (year = '100') OR (year = '101') OR (year = '102') OR (year = '103') ) and ";
            /* 新竹實驗中學國高中都在同一個 DB 中, 所以部別代碼分開 */
            if (ConnectionHelper.IsJH) {
                //sql += "  dept='102'";    //國中部為102
                sql += "  dept='001'"; //實中國小部為 001
            }
            else
                sql += " dept='101'";

            OleDbDataAdapter adp = new OleDbDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                dicStudents.Add(row["s_no"].ToString().Trim(), new VO.StudentInfo(row));
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