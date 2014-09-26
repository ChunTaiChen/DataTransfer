using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Odbc;

namespace 香山國中部_全訊_.DAO
{
    class Classes
    {
        //<class_name, grade_year>
        private static Dictionary<string, VO.ClassInfo> dicClasses = new Dictionary<string, VO.ClassInfo>();

        private static Dictionary<string, string> origClassDept = new Dictionary<string, string>();

        public static void Load()
        {
            origClassDept = new Dictionary<string, string>();

            //OleDbConnection cn = ConnectionHelper.GetConnection2();
            OleDbConnection cn = ConnectionHelper.GetConnection1(); //台中惠文的班級看來在 CSV1 目錄中的 cla 資料表較正確。
            //string sql = "select cla.cla, dept.d_na1 from cla inner join dept on cla.dept = dept.d_no -- where cla.year='100'";
            string sql = "select cla.cla,cla.year, dept.d_na1 from cla inner join dept on cla.dept = dept.d_no ";
            OleDbDataAdapter adp = new OleDbDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                if (row["year"].ToString() != "000")  //台中惠文資料表中有一堆 year="000" 的空白資料，所以多加判斷式去除
                {
                    //origClassDept.Add(row["cla"].ToString().Trim(), row["d_na1"].ToString().Trim());
                    origClassDept.Add(row["year"].ToString() + row["cla"].ToString().Trim(), row["d_na1"].ToString().Trim());    //實驗中學的班級名稱會重覆(已畢業班級名稱不會改變)
                    //AddStatisticsData(merit);
                }
            }
        }

        public static void Add(VO.ClassInfo classInfo)
        {
            if (origClassDept.ContainsKey(classInfo.ClassFullName))
                classInfo.DeptName = origClassDept[ classInfo.ClassFullName];

            if (!dicClasses.ContainsKey(classInfo.ClassFullName))
                dicClasses.Add(classInfo.ClassFullName, classInfo);
        }

        public static List<string> GetAllClassName()
        {
            return new List<string>(dicClasses.Keys);
        }

        public static List<VO.ClassInfo> GetAllClasses()
        {
            return new List<VO.ClassInfo>(dicClasses.Values);
        }

        public static string GetGradeYear(string className)
        {
            string result = "";

            if (dicClasses.ContainsKey(className))
                result = dicClasses[className].GradeYear;

            return result;
        }

        public static bool IsValidClassName(string classFullName)
        {
            return (origClassDept.ContainsKey(classFullName));
        }

        public static VO.ClassInfo GetClassInfoByClassName(string classFullName)
        {
            VO.ClassInfo result = null;
            if (dicClasses.ContainsKey(classFullName))
                result = dicClasses[classFullName];

            return result;
        }

    }
}
