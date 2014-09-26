using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;

namespace 成德高中國中部轉檔
{
    class ConnectionHelper
    {
        public static string sourceFolder = "";

        public static OdbcConnection GetConnection()
        {
            string cn = "Dsn=成德高中轉檔;";
            return new OdbcConnection(cn);
        }

        public static OdbcConnection GetConnectionA()
        {
            string cn = "Dsn=成德高中轉檔A;";
            return new OdbcConnection(cn);
        }
        

        public static string GetCnString(string path) {
            string cnstring = "Provider=vfpoledb;" + 
                                           "Data Source={0};" +
                                           "Mode=ReadWrite|Share Deny None;" +
                                           "Collating Sequence=MACHINE;" +
                                           "Password=''" ;
            return  string.Format(cnstring ,    sourceFolder + "\\" + path );            
        }
    }
}
