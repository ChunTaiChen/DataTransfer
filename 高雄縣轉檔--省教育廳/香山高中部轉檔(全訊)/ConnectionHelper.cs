using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;

namespace 香山高中部轉檔_全訊_
{
    class ConnectionHelper
    {
        public static string sourceFolder = @"C:\Projects\香山國中轉檔\CS";

        /// <summary>
        /// 學籍和成績
        /// </summary>
        /// <returns></returns>
        public static OdbcConnection GetConnection1()
        {
//            string cn = GetCnString("csv1");
            string cn = GetCnString("101");
            return new OdbcConnection(cn);
        }

        /// <summary>
        /// 缺曠獎懲及日常生活表現
        /// </summary>
        /// <returns></returns>
        public static OdbcConnection GetConnection2()
        {
            string cn = GetCnString("csv2");
            return new OdbcConnection(cn);
        }


        public static string GetCnString(string path)
        {
            string cnstring = "Provider=VFPOLEDB.1; Data Source={0};";
            return string.Format(cnstring, sourceFolder + "\\" + path);
        }
    }
}
