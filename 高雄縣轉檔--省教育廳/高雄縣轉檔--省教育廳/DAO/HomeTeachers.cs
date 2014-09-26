using System;
using System.Collections.Generic;

using System.Text;
using System.Data.OleDb;
using System.Data;

namespace 高雄縣轉檔__省教育廳.DAO
{
    class HomeTeachers
    {
        // Dictionary<schoolyear_semester, Dictionary<class_name, HomeTeacherInfo>>
        private static Dictionary<string, Dictionary<string,VO.HomeTeacherInfo>> homeTeachers;

        public static  void Load(int startYear, int endYear)
        {
            homeTeachers = new Dictionary<string,Dictionary<string,VO.HomeTeacherInfo>>();

            for (int i = startYear; i <= endYear; i++)
            {
                for (int semester = 1; semester < 3; semester++)
                {
                    OleDbConnection cn = ConnectionHelper.GetClassConnection(i);
                    cn.Open();
                    string sql = string.Format("select * from YHomt{0}{1}.dbf", i.ToString(), semester.ToString());
                    OleDbCommand cmd = new OleDbCommand(sql, cn);
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        string key1 = string.Format("{0}_{1}", i.ToString(), semester.ToString());
                        if (!homeTeachers.ContainsKey(key1))
                            homeTeachers.Add(key1, new Dictionary<string, VO.HomeTeacherInfo>());

                        string class_name = dr["gclass"].ToString();
                        VO.HomeTeacherInfo tea = new VO.HomeTeacherInfo();
                        tea.TeacherNo = dr["emp_no"].ToString().Trim();
                        tea.TeacherName = dr["homtname"].ToString().Trim();

                        if (!homeTeachers[key1].ContainsKey(class_name))
                            homeTeachers[key1].Add(class_name, tea);
                    }

                    dr.Close();
                    cn.Close();
                }
            }
        }

        public static Dictionary<string, VO.HomeTeacherInfo> GetClassesInfo(string schoolyear, string semester)
        {
            Dictionary<string, VO.HomeTeacherInfo> result = new Dictionary<string,VO.HomeTeacherInfo>(); 
            string key1 = schoolyear.ToString() + "_" + semester.ToString();
            if (homeTeachers.ContainsKey(key1))
                result = homeTeachers[key1];

            return result;
        }

        public  static VO.HomeTeacherInfo GetHomeTeacher(string schoolyear, string semester, string className)
        {
            VO.HomeTeacherInfo result = null;
            string key1 = schoolyear.ToString() + "_" + semester.ToString();
            if (homeTeachers.ContainsKey(key1))
            {
                if (homeTeachers[key1] != null)
                {
                    if (homeTeachers[key1].ContainsKey(className))
                    {
                        result = homeTeachers[key1][className];
                    }
                }
            }
            return result;
        }
    }
}
