﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 成德高中國中部轉檔.DAO
{
    class Util
    {
        public static string ParseSchoolYear(string schoolyear_semester)
        {
            string result = "";
            if (!string.IsNullOrEmpty(schoolyear_semester))
            {
                if (schoolyear_semester.Length >= 2)
                    result = schoolyear_semester.Substring(0, 2);
                if (result == "00")
                    result = "1" + result;
            }
            return result;
        }

        public static string ParseSemester(string schoolyear_semester)
        {
            string result = "";
            if (!string.IsNullOrEmpty(schoolyear_semester))
            {
                if (schoolyear_semester.Length >= 3)
                    result = schoolyear_semester.Substring(2, 1);                
            }
            return result;
        }

        public static string ParseClassName(string class_seatno)
        {
            string result = "";
            if (!string.IsNullOrEmpty(class_seatno))
            {
                if (class_seatno.Length >= 3)
                    result = class_seatno.Substring(0, 3);
            }
            return result;
        }

        public static string ParseSeatNo(string class_seatno)
        {
            string result = "";
            if (!string.IsNullOrEmpty(class_seatno))
            {
                if (class_seatno.Length >= 5)
                    result = class_seatno.Substring(3, 2);
            }
            return result;
        }

        public static string ParseGrade(string class_seatno)
        {
            string result = "";
            if (!string.IsNullOrEmpty(class_seatno))
            {
                if (class_seatno.Length >= 3)
                    result = class_seatno.Substring(0, 1);
            }
            return result;
        }
    }
}
