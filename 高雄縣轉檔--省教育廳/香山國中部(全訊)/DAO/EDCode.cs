using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;

namespace 香山國中部_全訊_.DAO
{
    /// <summary>
    /// 異動代碼表
    /// </summary>
    class EDCode
    {
        private static Dictionary<string, string> dicCodes;

        public static void Load()
        {
            dicCodes = new Dictionary<string, string>();

            OleDbConnection cn = ConnectionHelper.GetConnection1();
            string sql = "select * from edcode  ";

            OleDbDataAdapter adp = new OleDbDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                if (!string.IsNullOrWhiteSpace(row["code"].ToString()))
                    dicCodes.Add(row["code"].ToString(),row["name"].ToString());
            }
        }

        public static List<string> GetCodeList()
        {
            return new List<string>(dicCodes.Keys);
        }

        public static string GetName(string code)
        {
            string result = "";
            if (dicCodes.ContainsKey(code))
                result = dicCodes[code];

            return result;
        }

    }
}
