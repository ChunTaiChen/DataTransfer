using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 香山國中部_全訊_.VO
{
    class ClassInfo
    {
        public string ClassName = "";
        public string GradeYear = "";
        public string ClassFullName = "";
        public string DeptName = "";

        public ClassInfo(string className, string gradeYear, string classFullName)
        {
            this.ClassName = className;
            this.GradeYear = gradeYear;
            this.ClassFullName = classFullName;
        }
    }
}
