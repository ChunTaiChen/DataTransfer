using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 香山國中部_全訊_
{
    class Util
    {
        //將年份一律改為西元年
        public static string FormatDate(string d1)
        {
            string result = d1;

            if (string.IsNullOrWhiteSpace(result))
                return result;

            if (string.IsNullOrWhiteSpace(d1.Replace(".","/").Replace("/","").Trim()))
                return result ;

            string date = d1.Trim().Replace(".", "/");
            string[] date_parts = date.Split(new char[] { '/' });
            if (date_parts[0].Length < 4)
            {
                date_parts[0] = (1911 + int.Parse(date_parts[0])).ToString();
                result = string.Format("{0}/{1}/{2}", date_parts[0], date_parts[1], date_parts[2]);
            }
            return result;
        }

        //是否是要匯入的學生
        public static bool IsValidStudent(VO.StudentInfo stud)
        {
            bool result = true; 

            if (stud == null)
                result = false;

            //香山 只轉入 3, 2, 1年級的學生  , 但竹科實中不限制
            /*
            bool result = (stud.CurrentGrade == "1" || stud.CurrentGrade == "2" || stud.CurrentGrade == "3") && 
                                    DAO.Classes.IsValidClassName(stud.CurrentFullClassName) ;
            //Console.WriteLine(stud.CurrentGrade + ",  " + stud.CurrentFullClassName + ",  " + result.ToString());
            //if (stud.StudentNumber == "710214") result = true;  //要多匯出這個學生資料
            if (stud.StudentNumber == "710214" || stud.StudentNumber =="810234") 
                result = true;  //要多匯出這個學生資料
             * */
            return result ;
        }
    }
}
