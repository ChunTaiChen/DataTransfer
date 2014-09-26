using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;

namespace 成德高中國中部轉檔.Exporter
{
    class ScoreExporter
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
            foreach (string stud_no in DAO.SubjectScores.GetAllStudentNos())
            {
                VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(stud_no);
                if (stud == null) continue; //jump to next loop !
                Dictionary<string, Dictionary<string, List<VO.SubjectScoreInfo>>> subjScores =  DAO.SubjectScores.GetSubjectSemScoresByStudentNo(stud_no);
                foreach (string schoolYear in subjScores.Keys)
                {
                    foreach (string semester in subjScores[schoolYear].Keys)
                    {
                        foreach (VO.SubjectScoreInfo subjScore in subjScores[schoolYear][semester])
                        {
                            if (rowIndex > 65535)
                            {
                                pageIndex += 1;
                                //1.建立新的worksheet
                                ws = wb.Worksheets[wsName +pageIndex.ToString()];                                
                                //2. 重設 rowIndex 
                                rowIndex = 1;
                            }
                            ws.Cells[rowIndex, 0].PutValue(subjScore.StudentNumber);

                            ws.Cells[rowIndex, 1].PutValue(stud.CurrentGrade + stud.CurrentClass);
                            ws.Cells[rowIndex, 2].PutValue(stud.CurrentSeatNo);
                            ws.Cells[rowIndex, 3].PutValue(stud.StudentName);

                            ws.Cells[rowIndex, 4].PutValue(subjScore.SchoolYear.ToString());
                            ws.Cells[rowIndex, 5].PutValue(subjScore.Semester.ToString());

                            ws.Cells[rowIndex, 6].PutValue(subjScore.GetDomainName());
                            ws.Cells[rowIndex, 7].PutValue(subjScore.GetSubjectName());
                            ws.Cells[rowIndex, 8].PutValue(subjScore.GetCredit());
                            ws.Cells[rowIndex, 9].PutValue(subjScore.GetCredit());
                            ws.Cells[rowIndex, 10].PutValue(subjScore.GetAverageScore());
                            ws.Cells[rowIndex, 11].PutValue("");
                            ws.Cells[rowIndex, 12].PutValue("");
                            ws.Cells[rowIndex, 13].PutValue("");

                            rowIndex += 1;
                        }
                    } // end foreach semester
                } // end foreach schoolyear

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
            foreach (string stud_no in DAO.DomainScores.GetAllStudentNos())
            {
                VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(stud_no);
                if (stud == null) continue; //jump to next loop !
                Dictionary<string, Dictionary<string, List<VO.DomainScoreInfo>>> domainScores = DAO.DomainScores.GetDomainSemScoresByStudentNo(stud_no);
                foreach (string schoolYear in domainScores.Keys)
                {
                    foreach (string semester in domainScores[schoolYear].Keys)
                    {
                        foreach (VO.DomainScoreInfo domainScore in domainScores[schoolYear][semester])
                        {
                            FillOneDomainScore(domainScore, ws, rowIndex, stud);
                            rowIndex += 1;
                            
                            if (rowIndex > 65535)
                            {
                                pageIndex += 1;
                                ws = wb.Worksheets["學期領域成績" + pageIndex];
                                rowIndex = 1;
                            }
                            
                            /*
                            if (domainScore.DomainNo == "100") //如果是語文領域，則要分別顯示維 國語文 和 英語
                            {
                                List<VO.SubjectSemScoreInfo> subjScores = DAO.SubjectSemScores.GetSubjectSemScoresByStudentNo(stud_no)[schoolYear][semester];
                                foreach (VO.SubjectSemScoreInfo subjScore in subjScores)
                                {
                                    if (subjScore.GetDomainNo() == "100")
                                    {
                                        if (subjScore.GetSubjectName() == "國文" || subjScore.GetSubjectName() == "英語")
                                        {
                                            VO.DomainSemScoreInfo dmScore = new VO.DomainSemScoreInfo();
                                            dmScore.StudentNo = subjScore.StudentNo;
                                            dmScore.SchoolYear = subjScore.SchoolYear;
                                            dmScore.Semester = subjScore.Semester;
                                            dmScore.GradeYear = subjScore.GradeYear;
                                            dmScore.Hour = subjScore.Hour;
                                            dmScore.Score = subjScore.Score;
                                            dmScore.DomainNo = subjScore.GetDomainNo();
                                            dmScore.TempDomainName = subjScore.GetDomainName();
                                            FillOneDomainScore(dmScore, ws, rowIndex, stud);
                                            rowIndex += 1;
                                        }
                                    }
                                }
                            }
                            else if (domainScore.DomainNo == "800")     //彈性課程領域不列出
                                continue;
                            else
                            {
                                FillOneDomainScore(domainScore, ws, rowIndex, stud);
                                rowIndex += 1;
                            }
                             * */
                        }
                    } // end foreach semester
                } // end foreach schoolyear
            }// end foreach stud_no

            Console.WriteLine(rowIndex.ToString());
        }

        private void FillOneDomainScore(VO.DomainScoreInfo domainScore, Worksheet ws, int rowIndex, VO.StudentInfo stud)
        {
            ws.Cells[rowIndex, 0].PutValue(domainScore.StudentNo);

            ws.Cells[rowIndex, 1].PutValue(stud.CurrentGrade + stud.CurrentClass);
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
        }
    }
}
