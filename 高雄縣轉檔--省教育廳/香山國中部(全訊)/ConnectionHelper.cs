using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;

namespace 香山國中部_全訊_
{
    class ConnectionHelper
    {
        public static bool IsJH = false;  //是否是國中，或是高中
        //public static string sourceFolder = @"C:\Projects\香山國中轉檔\CS20120131";
        //public static string sourceFolder = @"C:\Projects\竹科實中";
        //public static string sourceFolder = @"C:\projects\實驗中學_中學部";
      //  public static string sourceFolder = @"C:\projects\轉檔\實驗中學_國小部";
        //public static string sourceFolder = @"C:\projects\轉檔\惠文高中";
        public static string sourceFolder = @"D:\DB\lchs";
        

        /// <summary>
        /// 學籍和成績
        /// </summary>
        /// <returns></returns>
        public static OleDbConnection GetConnection1()
        {
            //string cn = (IsJH ? GetCnString("csv1J") : GetCnString("csv1") );     //香山中學國高中不同 DB
//            string cn =  GetCnString("csv1");   //新竹實驗中學國高中同一個 DB
          //  string cn = GetCnString("sv1");   //新竹實驗中學國小部 DB
            string cn = GetCnString("102");  // 路竹高中
            return new OleDbConnection(cn);
        }

        /// <summary>
        /// 缺曠獎懲及日常生活表現
        /// </summary>
        /// <returns></returns>
        public static OleDbConnection GetConnection2()
        {
            // string cn = (IsJH ? GetCnString("csv2J") : GetCnString("csv2"));     //香山中學國高中不同 DB
            //string cn = GetCnString("csv2");   //新竹實驗中學國高中同一個 DB
            //string cn = GetCnString("csv2");  // 路竹高中

            string str=@"csv2";
            string cn = GetCnString(str);  // 路竹高中


            return new OleDbConnection(cn);
        }


        public static string GetCnString(string path)
        {
            string cnstring = "Provider=VFPOLEDB.1; Data Source={0};";
            return string.Format(cnstring, sourceFolder + "\\" + path);
        }
    }
}
