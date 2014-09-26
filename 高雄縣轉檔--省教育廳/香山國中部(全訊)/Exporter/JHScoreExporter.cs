using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;

namespace 香山國中部_全訊_.Exporter
{
    class JHScoreExporter
    {
        public void BeginExport()
        {
            string template = System.Environment.CurrentDirectory + "\\Template\\成績資料.xls";
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(template);

            FillSubjectSemScores(wb);
            FillDomainSemScores(wb);

            //int rowIndexDomain = 1;
            //Worksheet wst2 = wb.Worksheets["學期領域成績"];
            DateTime dt = DateTime.Now;
            wb.Save(System.Environment.CurrentDirectory + string.Format("\\Data\\成績資料_{0}_{1}.xls", dt.ToString("yyyyMMdd"), dt.ToString("hhmmss")));
        }

        private void FillSubjectSemScores(Workbook wb)
        {
            string wsName = "學期科目成績" ;
            int wsCount = 0;    //需建立第二個worksheet  會用到

            int pageIndex = 1;

            Worksheet ws = wb.Worksheets[wsName];

            int rowIndex = 1;
            //TODO : 以下要重寫
            foreach (string stud_no in DAO.JHScores.GetAllStudentNos())
            {
                VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(stud_no);
                if (stud == null) continue; //jump to next loop !

                foreach (VO.JHSubjectSemesterScore subjScore in DAO.JHScores.GetSubjectSemsScores(stud_no))
                {
                    if (rowIndex > 65535)
                    {
                        pageIndex += 1;
                        //1.建立新的worksheet
                        ws = wb.Worksheets[wsName + pageIndex.ToString()];
                        //2. 重設 rowIndex 
                        rowIndex = 1;
                    }


                    ws.Cells[rowIndex, 0].PutValue(subjScore.StudentNo);

                    ws.Cells[rowIndex, 1].PutValue(stud.CurrentGrade + stud.CurrentClass);
                    ws.Cells[rowIndex, 2].PutValue(stud.CurrentSeatNo);
                    ws.Cells[rowIndex, 3].PutValue(stud.StudentName);
                    ws.Cells[rowIndex, 4].PutValue(subjScore.SchoolYear.ToString());
                    ws.Cells[rowIndex, 5].PutValue(subjScore.Semester.ToString());

                    VO.CourseInfo cor = DAO.Courses.GetCourseByCourseCode(subjScore.CourseNo);
                    if (cor != null)
                    {
                        ws.Cells[rowIndex, 6].PutValue(cor.Domain);
                        ws.Cells[rowIndex, 7].PutValue(cor.CourseName);
                    }
                    
                    ws.Cells[rowIndex, 8].PutValue(subjScore.Credit);
                    ws.Cells[rowIndex, 9].PutValue(subjScore.Credit);
                    ws.Cells[rowIndex, 10].PutValue(subjScore.Score);
                    ws.Cells[rowIndex, 11].PutValue("");
                    ws.Cells[rowIndex, 12].PutValue("");
                    ws.Cells[rowIndex, 13].PutValue("");

                    rowIndex += 1;
                }

            }// end foreach stud_no
           
        }

        private Worksheet createSubjSemScoreWorksheet(int wsCount, string wsName, Workbook wb)
        {
            Worksheet ws = wb.Worksheets[wb.Worksheets.Add()];
            ws.Name = wsName + (wsCount.ToString());
            ws.Cells[0, 0].PutValue("學號");
            ws.Cells[0, 1].PutValue("班級");
            ws.Cells[0, 2].PutValue("座號");
            ws.Cells[0, 3].PutValue("姓名");
            ws.Cells[0, 4].PutValue("學年度");
            ws.Cells[0, 5].PutValue("學期");
            ws.Cells[0, 6].PutValue("領域");
            ws.Cells[0, 7].PutValue("科目");
            ws.Cells[0, 8].PutValue("權數");
            ws.Cells[0, 9].PutValue("節數");
            ws.Cells[0, 10].PutValue("分數評量");
            ws.Cells[0, 11].PutValue("努力程度");
            ws.Cells[0, 12].PutValue("文字描述");
            ws.Cells[0, 13].PutValue("註記");
            return ws;
        }

        private void FillDomainSemScores(Workbook wb)
        {
            Worksheet ws = wb.Worksheets["學期領域成績"];
            int rowIndex = 1;
            int pageIndex = 1;
            //TODO : 以下要重寫
            foreach (string stud_no in DAO.JHScores.GetAllStudentNos())
            {
                VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(stud_no);
                if (stud == null) continue; //jump to next loop !
                List<VO.DomainScoreInfo> domainScores = DAO.JHScores.GetDomainSemsScores(stud_no);
                foreach (VO.DomainScoreInfo domainScore in domainScores)
                {
                    ws.Cells[rowIndex, 0].PutValue(stud_no);

                    ws.Cells[rowIndex, 1].PutValue(stud.CurrentFullClassName);
                    ws.Cells[rowIndex, 2].PutValue(stud.CurrentSeatNo);
                    ws.Cells[rowIndex, 3].PutValue(stud.StudentName);

                    ws.Cells[rowIndex, 4].PutValue(domainScore.DomainName);

                    ws.Cells[rowIndex, 5].PutValue(domainScore.SchoolYear.ToString());
                    ws.Cells[rowIndex, 6].PutValue(domainScore.Semester.ToString());

                    ws.Cells[rowIndex, 7].PutValue(domainScore.Hour);
                    ws.Cells[rowIndex, 8].PutValue(domainScore.Hour);
                    ws.Cells[rowIndex, 9].PutValue(domainScore.Score);

                    ws.Cells[rowIndex, 10].PutValue("");
                    ws.Cells[rowIndex, 11].PutValue("");
                    ws.Cells[rowIndex, 12].PutValue("");

                    rowIndex += 1;
                }
                
            }// end foreach stud_no

            Console.WriteLine(rowIndex.ToString());
        }

    }
}
