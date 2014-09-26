using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.OleDb;

namespace 香山國中部_全訊_.DAO
{
    class Courses
    {
        //<class_name, grade_year>
        private static Dictionary<string, VO.CourseInfo> dicCourses = new Dictionary<string, VO.CourseInfo>();

        private static Dictionary<string, string> dicCourseDomain = new Dictionary<string, string>();   //課程編號的領域代碼

        public static void Load()
        {
            dicCourses = new Dictionary<string, VO.CourseInfo>();

            OleDbConnection cn = ConnectionHelper.GetConnection1();
            string sql = "select * from cors ";
            OleDbDataAdapter adp = new OleDbDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                VO.CourseInfo ci = new VO.CourseInfo(row);
                if (!dicCourses.ContainsKey(ci.CourseNo)) { 
                    dicCourses.Add(ci.CourseNo, ci);
                }
            }

            //取得課程編號對應的領域代碼
            dicCourseDomain = new Dictionary<string, string>();
            sql = @"SELECT DISTINCT c_no, rra
                        FROM              `table`                         
                        ORDER BY   c_no, rra";
            adp = new OleDbDataAdapter(sql, cn);
            DataSet ds2 = new DataSet();
            adp.Fill(ds2);
            DataTable dt2 = ds2.Tables[0];
            foreach (DataRow row in dt2.Rows)
            {
                string c_no = row["c_no"].ToString().Trim();
                if (dicCourseDomain.ContainsKey(c_no))                
                    dicCourseDomain[c_no] = row["rra"].ToString().Trim();
                else
                    dicCourseDomain.Add(c_no, row["rra"].ToString().Trim());
            }
        }

        public static String GetDomainNameByCourseCode(string courseCode)
        {
            string result = "";
            if (dicCourseDomain.ContainsKey(courseCode))
                result = GetDomainName(dicCourseDomain[courseCode]);
            return result;
        }

        public static string GetDomainName(string domainCode)
        {
            string result = "";

            if (domainCode == "1") result = "語文";
            if (domainCode == "2") result = "語文";
            if (domainCode == "3") result = "數學";
            if (domainCode == "4") result = "社會";
            if (domainCode == "5") result = "藝術與人文";
            if (domainCode == "6") result = "自然與生活科技";
            if (domainCode == "7") result = "體育與健康教育";
            if (domainCode == "8") result = "綜合活動";
            if (domainCode == "9") result = "彈性課程";

            return result;
        }

        public static VO.CourseInfo GetCourseByCourseCode(string courseCode)
        {
            VO.CourseInfo result = null;
            if (dicCourses.ContainsKey(courseCode.Trim()))
                result = dicCourses[courseCode];

            return result;
        }
    }
}
