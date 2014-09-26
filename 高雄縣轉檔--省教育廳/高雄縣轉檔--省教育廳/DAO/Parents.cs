using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Data.OleDb;

namespace 高雄縣轉檔__省教育廳.DAO
{
    class Parents
    {
        private static Dictionary<string, VO.ParentInfo> allParents;
        public static void Load(int fromYear, int toYear)
        {
            allParents = new Dictionary<string, VO.ParentInfo>();

            for (int i = fromYear; i <= toYear; i++)
            {
                OleDbConnection cn = ConnectionHelper.GetPersonConnection(i);
                string sql = string.Format("select * from XPARNT{0}.dbf", i.ToString());
                OleDbDataAdapter adp = new OleDbDataAdapter(sql, cn);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    VO.ParentInfo parent = new VO.ParentInfo(row);
                    if (!allParents.ContainsKey(parent.StudentNo))
                        allParents.Add(parent.StudentNo, parent);
                    else
                    {
                        Console.WriteLine(string.Format("student_no :{0} 的parent 資料重複！", parent.StudentNo));
                    }
                }
            }
        }

        public static VO.ParentInfo GetParentByStudNo(string stud_no)
        {
            VO.ParentInfo result = null;
            if (allParents.ContainsKey(stud_no))
                result = allParents[stud_no];

            return result;
        }
    }
}
