﻿using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.OleDb;

namespace 高雄縣轉檔__省教育廳.DAO
{
    class Students
    {
        private static Dictionary<string, VO.StudentInfo> dicStudents;

        public static void Load(int fromYear , int toYear)
        {
            dicStudents = new Dictionary<string, VO.StudentInfo>();

            for (int i = fromYear; i <= toYear; i++)
            {
                OleDbConnection cn = ConnectionHelper.GetPersonConnection(i);
                string sql = string.Format("select * from XBASIC{0}.dbf", i.ToString());
                OleDbDataAdapter adp = new OleDbDataAdapter(sql, cn);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    dicStudents.Add(row["stud_no"].ToString(), new VO.StudentInfo(row));
                }               
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
