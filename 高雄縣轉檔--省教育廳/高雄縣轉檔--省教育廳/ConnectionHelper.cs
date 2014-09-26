using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.OleDb;

namespace 高雄縣轉檔__省教育廳
{
    class ConnectionHelper
    {
        public static string sourceFolder = "";

        public static OleDbConnection GetCourseConnection(int schoolYear)
        {
            string cn = GetCnString(string.Format("Course\\R{0}", schoolYear.ToString()));
            return new OleDbConnection(cn);
        }

        public static OleDbConnection GetStageConnection(int schoolYear)
        {
            string cn = GetCnString(string.Format("Stage\\G{0}", schoolYear.ToString()));
            return new OleDbConnection(cn);
        }
        
        public static OleDbConnection GetLifeCheckConnection(int schoolYear)
        {
            string cn = GetCnString(string.Format("LIFECHK\\L{0}", schoolYear.ToString()));
            return new OleDbConnection(cn);
        }

        public static OleDbConnection GetAbsenceConnection(int schoolYear)
        {
            string cn = GetCnString(string.Format("Absent\\A{0}", schoolYear.ToString()));
            return new OleDbConnection(cn);
        }

        public static OleDbConnection GetPersonConnection(int schoolYear)
        {
            string cn = GetCnString(string.Format("Person\\P{0}", schoolYear.ToString()));
            return new OleDbConnection(cn);
        }

        public static OleDbConnection GetClassConnection(int schoolYear)
        {
            string cn = GetCnString(string.Format("Class\\C{0}", schoolYear.ToString()));
            return new OleDbConnection(cn);
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
