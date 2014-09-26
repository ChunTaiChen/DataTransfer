using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;

namespace 香山國中部_全訊_.Exporter
{
    class SHScoreExporter
    {
        public void BeginExportSH()
        {
            ExportSemesterSubjScore();
            ExportSemesterCatScore();
            ExportSchoolYearSubjScore();
            ExportSchoolYearCatScore();
        }

        /// <summary>
        /// 匯出學年分項成績
        /// </summary>
        private void ExportSchoolYearCatScore()
        {
            string template = System.Environment.CurrentDirectory + "\\Template\\匯出學年分項成績.xls";
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(template);

            string wsName = "匯出學年分項成績";
            int wsCount = 0;    //需建立第二個worksheet  會用到

            int pageIndex = 1;

            Worksheet ws = wb.Worksheets[wsName];

            int rowIndex = 1;
            //TODO : 以下要重寫
            foreach (VO.SHLearnSemesterScore score in DAO.SHScores.GetLearnYearScores())
            {
                VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(score.StudentNo);
                if (!香山國中部_全訊_.Util.IsValidStudent(stud)) continue; //jump to next loop !

                ws.Cells[rowIndex, 0].PutValue(score.StudentNo);
                ws.Cells[rowIndex, 1].PutValue(stud.CurrentFullClassName);
                ws.Cells[rowIndex, 2].PutValue(stud.CurrentSeatNo);
                VO.ClassInfo cla = DAO.Classes.GetClassInfoByClassName(stud.CurrentFullClassName);
                if (cla != null) ws.Cells[rowIndex, 3].PutValue(cla.DeptName);
                ws.Cells[rowIndex, 4].PutValue(stud.StudentName);
                ws.Cells[rowIndex, 5].PutValue(score.SchoolYear);
                ws.Cells[rowIndex, 6].PutValue(score.GradeYear);
                ws.Cells[rowIndex, 7].PutValue(score.Score);

                rowIndex += 1;

            }// end foreach stud_no

            DateTime dt = DateTime.Now;
            wb.Save(System.Environment.CurrentDirectory + string.Format("\\Data\\匯出學年分項成績_{0}_{1}.xls", dt.ToString("yyyyMMdd"), dt.ToString("hhmmss")));
        }

        /// <summary>
        /// 匯出學期分項成績
        /// </summary>
        private void ExportSemesterCatScore()
        {
            string template = System.Environment.CurrentDirectory + "\\Template\\匯出學期分項成績.xls";
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(template);

            string wsName = "匯出學期分項成績";
            int wsCount = 0;    //需建立第二個worksheet  會用到

            int pageIndex = 1;

            Worksheet ws = wb.Worksheets[wsName];

            int rowIndex = 1;
            //TODO : 以下要重寫
            foreach (VO.SHLearnSemesterScore score in DAO.SHScores.GetLearnSemsScores())
            {
                VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(score.StudentNo);
                if (!香山國中部_全訊_.Util.IsValidStudent(stud)) continue; //jump to next loop !

                ws.Cells[rowIndex, 0].PutValue(score.StudentNo);
                ws.Cells[rowIndex, 1].PutValue(stud.CurrentFullClassName);
                ws.Cells[rowIndex, 2].PutValue(stud.CurrentSeatNo);
                VO.ClassInfo cla = DAO.Classes.GetClassInfoByClassName(stud.CurrentFullClassName);
                if (cla != null) ws.Cells[rowIndex, 3].PutValue(cla.DeptName);
                ws.Cells[rowIndex, 4].PutValue(stud.StudentName);
                ws.Cells[rowIndex, 5].PutValue(score.SchoolYear);
                ws.Cells[rowIndex, 6].PutValue(score.Semester);
                ws.Cells[rowIndex, 7].PutValue(score.GradeYear);
                ws.Cells[rowIndex, 8].PutValue(score.Score);

                rowIndex += 1;

            }// end foreach stud_no

            DateTime dt = DateTime.Now;
            wb.Save(System.Environment.CurrentDirectory + string.Format("\\Data\\匯出學期分項成績_{0}_{1}.xls", dt.ToString("yyyyMMdd"), dt.ToString("hhmmss")));
        }

        private void ExportSchoolYearSubjScore()
        {
            string template = System.Environment.CurrentDirectory + "\\Template\\匯出學年科目成績.xls";
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(template);

            string wsName = "匯出學年科目成績";
            int wsCount = 0;    //需建立第二個worksheet  會用到

            int pageIndex = 1;

            Worksheet ws = wb.Worksheets[wsName];

            int rowIndex = 1;
            //TODO : 以下要重寫
            foreach (VO.SHSubjectSemesterScore score in DAO.SHScores.GetSubjectYearScores())
            {
                VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(score.StudentNo);
                if (!香山國中部_全訊_.Util.IsValidStudent(stud)) continue; //jump to next loop !

                ws.Cells[rowIndex, 0].PutValue(score.StudentNo);
                ws.Cells[rowIndex, 1].PutValue(stud.CurrentFullClassName);
                ws.Cells[rowIndex, 2].PutValue(stud.CurrentSeatNo);
                VO.ClassInfo cla = DAO.Classes.GetClassInfoByClassName(stud.CurrentFullClassName);
                if (cla != null) ws.Cells[rowIndex, 3].PutValue(cla.DeptName);
                ws.Cells[rowIndex, 4].PutValue(stud.StudentName);
                ws.Cells[rowIndex, 5].PutValue(score.SchoolYear);
                ws.Cells[rowIndex, 6].PutValue(score.GradeYear);

                VO.CourseInfo cor = DAO.Courses.GetCourseByCourseCode(score.CourseNo);
                if (cor != null) ws.Cells[rowIndex, 7].PutValue(cor.CourseName);

                ws.Cells[rowIndex, 8].PutValue(score.Score);

                rowIndex += 1;

            }// end foreach stud_no

            DateTime dt = DateTime.Now;
            wb.Save(System.Environment.CurrentDirectory + string.Format("\\Data\\匯出學年科目成績_{0}_{1}.xls", dt.ToString("yyyyMMdd"), dt.ToString("hhmmss")));
        }

        private void ExportSemesterSubjScore()
        {
            string template = System.Environment.CurrentDirectory + "\\Template\\匯出學期科目成績.xls";
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(template);

            string wsName = "匯出學期科目成績";
            int wsCount = 0;    //需建立第二個worksheet  會用到

            int pageIndex = 1;

            Worksheet ws = wb.Worksheets[wsName];

            int rowIndex = 1;
            //TODO : 以下要重寫
            foreach (VO.SHSubjectSemesterScore score in DAO.SHScores.GetSubjectSemsScores())
            {
                VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(score.StudentNo);
                if (!香山國中部_全訊_.Util.IsValidStudent(stud)) continue; //jump to next loop !

                /* 處理超過 65535 筆 */
                if (rowIndex > 65535)
                {
                    pageIndex += 1;
                    //1.建立新的worksheet
                    ws = wb.Worksheets.Add(wsName + pageIndex.ToString());
                    //2. 重設 rowIndex 
                    rowIndex = 1;
                }

                ws.Cells[rowIndex, 0].PutValue(score.StudentNo);
                ws.Cells[rowIndex, 1].PutValue(stud.CurrentFullClassName);
                ws.Cells[rowIndex, 2].PutValue(stud.CurrentSeatNo);
                VO.ClassInfo cla = DAO.Classes.GetClassInfoByClassName(stud.CurrentFullClassName);
                if (cla != null) ws.Cells[rowIndex, 3].PutValue(cla.DeptName);
                ws.Cells[rowIndex, 4].PutValue(stud.StudentName);
                VO.CourseInfo cor = DAO.Courses.GetCourseByCourseCode(score.CourseNo);
                if (cor != null)
                {
                    ws.Cells[rowIndex, 5].PutValue(cor.CourseName);
                    ws.Cells[rowIndex, 6].PutValue(cor.GetSubjectLevel(score.GradeYear, score.Semester));
                }
                ws.Cells[rowIndex, 7].PutValue(score.SchoolYear);
                ws.Cells[rowIndex, 8].PutValue(score.Semester);
                ws.Cells[rowIndex, 9].PutValue(score.Credit);
                ws.Cells[rowIndex, 10].PutValue(score.ItemCat);
                ws.Cells[rowIndex, 11].PutValue(score.GradeYear);
                ws.Cells[rowIndex, 12].PutValue(score.IsRequired);
                ws.Cells[rowIndex, 13].PutValue("");
                ws.Cells[rowIndex, 14].PutValue(score.Score);
                ws.Cells[rowIndex, 15].PutValue(score.Score1);
                ws.Cells[rowIndex, 16].PutValue(score.Score2);
                ws.Cells[rowIndex, 17].PutValue(score.Score3);
                ws.Cells[rowIndex, 18].PutValue("");
                ws.Cells[rowIndex, 19].PutValue(score.ScoreYearAdjust);
                ws.Cells[rowIndex, 20].PutValue(score.IsGetScore);

                rowIndex += 1;

            }// end foreach stud_no

            DateTime dt = DateTime.Now;
            wb.Save(System.Environment.CurrentDirectory + string.Format("\\Data\\匯出學期科目成績_{0}_{1}.xls", dt.ToString("yyyyMMdd"), dt.ToString("hhmmss")));
        }

    }
}
