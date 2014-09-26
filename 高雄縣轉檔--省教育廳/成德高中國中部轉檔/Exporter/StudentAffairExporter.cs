using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;

namespace 成德高中國中部轉檔.Exporter
{
    class StudentAffairExporter
    {
        public void BeginExport()
        {
            string template = System.Environment.CurrentDirectory + "\\Template\\學務資料.xls";
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(template);

            FillMeritInfo(wb);
            //FillMeritStatistics(wb);

            FillAbsenceInfo(wb);
            //FillAbsenceSummary(wb);

            //FillLifeScores(wb);  //日常生活表現成績

            //int rowIndexDomain = 1;
            //Worksheet wst2 = wb.Worksheets["學期領域成績"];
            DateTime dt = DateTime.Now;
            wb.Save(System.Environment.CurrentDirectory + string.Format("\\Data\\學務資料_{0}_{1}.xls", dt.ToString("yyyyMMdd"), dt.ToString("hhmmss")));
        }
        /*
        private void FillLifeScores(Workbook wb)
        {
            string wsName = "日常生活表現資料";
            Worksheet ws = wb.Worksheets[wsName];

            int rowIndex = 1;
            //TODO : 以下要重寫
            foreach (string stud_no in DAO.LifeScores.GetAllStudentNos())
            {
                VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(stud_no);
                if (stud == null) continue; //jump to next loop !
                Dictionary<string, Dictionary<string, List<VO.LifeScoreInfo>>> lifeScores = DAO.LifeScores.GetLifeScoresByStudentNo(stud_no);
                foreach (string schoolYear in lifeScores.Keys)
                {
                    foreach (string semester in lifeScores[schoolYear].Keys)
                    {
                        foreach (VO.LifeScoreInfo lifeScore in lifeScores[schoolYear][semester])
                        {                            
                            ws.Cells[rowIndex, 0].PutValue(lifeScore.StudentNo);

                            ws.Cells[rowIndex, 1].PutValue(stud.CurrentGrade + stud.CurrentClass);
                            ws.Cells[rowIndex, 2].PutValue(stud.CurrentSeatNo);
                            ws.Cells[rowIndex, 3].PutValue(stud.StudentName);

                            ws.Cells[rowIndex, 4].PutValue(lifeScore.SchoolYear.ToString());
                            ws.Cells[rowIndex, 5].PutValue(lifeScore.Semester.ToString());

                            ws.Cells[rowIndex, 6].PutValue(toText(lifeScore.Tidy));
                            ws.Cells[rowIndex, 7].PutValue(toText(lifeScore.Polite));
                            ws.Cells[rowIndex, 8].PutValue(toText(lifeScore.Behave));
                            ws.Cells[rowIndex, 9].PutValue(toText(lifeScore.Responsible));
                            ws.Cells[rowIndex, 10].PutValue(toText(lifeScore.CivicMinded));
                            ws.Cells[rowIndex, 11].PutValue(toText(lifeScore.Caring));
                            ws.Cells[rowIndex, 12].PutValue(toText(lifeScore.Coporative)); //團隊合作
                            ws.Cells[rowIndex, 13].PutValue("");
                            ws.Cells[rowIndex, 14].PutValue(lifeScore.TeamActivity);    //校內活動
                            ws.Cells[rowIndex, 15].PutValue("");
                            ws.Cells[rowIndex, 16].PutValue("");    //班級活動描述
                            ws.Cells[rowIndex, 17].PutValue("");
                            ws.Cells[rowIndex, 18].PutValue("");    //自治活動描述
                            ws.Cells[rowIndex, 19].PutValue(lifeScore.SchoolService);    //校內服務
                            ws.Cells[rowIndex, 20].PutValue(lifeScore.CommunityService);    //社區服務
                            ws.Cells[rowIndex, 21].PutValue(lifeScore.GoodThingsInSchool);    //校內特殊表現
                            ws.Cells[rowIndex, 22].PutValue(lifeScore.GoodThingsOutsideSchool);    //校外特殊表現
                            ws.Cells[rowIndex, 23].PutValue(lifeScore.Recommend);    //具體建議

                            rowIndex += 1;
                        }
                    } // end foreach semester
                } // end foreach schoolyear

            }// end foreach stud_no
           
        }
         * */

        private string toText(string level)
        {
            string result = "";
            if (level.Trim() == "1") result = "需再努力";
            if (level.Trim() == "2") result = "部份符合";
            if (level.Trim() == "3") result = "大部份符合";
            if (level.Trim() == "4") result = "完全符合";
            return result;
        }

        private void FillAbsenceSummary(Workbook wb)
        {
            Worksheet ws = wb.Worksheets["缺曠統計"];
            int rowIndex = 1;
            List<VO.AbsenceSummaryInfo> records = DAO.Absence.GetAllSummary();

            foreach (VO.AbsenceSummaryInfo sum in records)
            {
                foreach (string[] summary in sum.GetSummary())
                {
                    VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(sum.StudentNo);
                    if (stud != null)
                    {
                        ws.Cells[rowIndex, 0].PutValue(sum.StudentNo);
                    
                        ws.Cells[rowIndex, 1].PutValue(stud.CurrentGrade + stud.CurrentClass);
                        ws.Cells[rowIndex, 2].PutValue(stud.CurrentSeatNo);
                        ws.Cells[rowIndex, 3].PutValue(stud.StudentName);

                        ws.Cells[rowIndex, 4].PutValue(sum.SchoolYear);
                        ws.Cells[rowIndex, 5].PutValue(sum.Semester);

                        ws.Cells[rowIndex, 6].PutValue(summary[0]);
                        ws.Cells[rowIndex, 7].PutValue(summary[1]);    //地點
                        ws.Cells[rowIndex, 8].PutValue(int.Parse(summary[2]));

                        rowIndex += 1;
                    }  //end if                    
                }   // end for
            }   // end for
        }

        private void FillAbsenceInfo(Workbook wb)
        {
            Worksheet ws = wb.Worksheets["缺曠紀錄"];
            int rowIndex = 1;
            List<VO.AbsenceInfo> records = DAO.Absence.GetAllAbsence();

            foreach (VO.AbsenceInfo abs in records)
            {
                foreach (string period in abs.GetAbsenceDetail().Keys)  //每一節課都是一筆記錄
                {
                    if (rowIndex > 65535)
                    {
                        ws = wb.Worksheets["缺曠紀錄2"];
                        rowIndex = 1;
                    }
                    VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(abs.StudentNo);
                    
                    if (stud != null && stud.IsInSchool)  //找到學生才寫入
                    {
                        ws.Cells[rowIndex, 0].PutValue(abs.StudentNo);
                        ws.Cells[rowIndex, 1].PutValue(stud.CurrentGrade + stud.CurrentClass);
                        ws.Cells[rowIndex, 2].PutValue(stud.CurrentSeatNo);
                        ws.Cells[rowIndex, 3].PutValue(stud.StudentName);

                        ws.Cells[rowIndex, 4].PutValue(abs.SchoolYear);
                        ws.Cells[rowIndex, 5].PutValue(abs.Semester);
                        ws.Cells[rowIndex, 6].PutValue(abs.AbsentDate);

                        ws.Cells[rowIndex, 7].PutValue(abs.GetAbsenceDetail()[period]);
                        ws.Cells[rowIndex, 8].PutValue(period);
                        
                        rowIndex += 1;
                    }  //enf if
                }  //end for
            }  //end for
        }

        private void FillMeritStatistics(Workbook wb)
        {
            Worksheet ws = wb.Worksheets["獎懲統計"];
            int rowIndex = 1;
            Dictionary<string, int[]> statisticsData = DAO.Merits.GetMeritStatisticsData();

            foreach (string key in statisticsData.Keys)
            {
                string[] ks = key.Split(new char[] { '_' });
                string stud_no = ks[0];
                string schoolyear = ks[1];
                string semester = ks[2];
                int[] sdata = statisticsData[key];

                VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(stud_no);

                ws.Cells[rowIndex, 0].PutValue(stud_no);
                if (stud != null)
                {
                    ws.Cells[rowIndex, 1].PutValue(stud.CurrentGrade + stud.CurrentClass);
                    ws.Cells[rowIndex, 2].PutValue(stud.CurrentSeatNo);
                    ws.Cells[rowIndex, 3].PutValue(stud.StudentName);
                }
                ws.Cells[rowIndex, 4].PutValue(schoolyear);
                ws.Cells[rowIndex, 5].PutValue(semester);
                ws.Cells[rowIndex, 6].PutValue(sdata[0]);
                ws.Cells[rowIndex, 7].PutValue(sdata[1]);
                ws.Cells[rowIndex, 8].PutValue(sdata[2]);
                ws.Cells[rowIndex, 9].PutValue(sdata[3]);
                ws.Cells[rowIndex, 10].PutValue(sdata[4]);
                ws.Cells[rowIndex, 11].PutValue(sdata[5]);

                rowIndex += 1;
            }
        }

        private void FillMeritInfo(Workbook wb)
        {
            Worksheet ws = wb.Worksheets["獎懲紀錄"];
            int rowIndex = 1;
            List<VO.MeritInfo> records = DAO.Merits.GetAllMerits();

            foreach (VO.MeritInfo mi in records)
            {
                VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(mi.StudentNo);

                ws.Cells[rowIndex, 0].PutValue(mi.StudentNo);
                if (stud != null)
                {
                    ws.Cells[rowIndex, 1].PutValue(stud.CurrentGrade + stud.CurrentClass);
                    ws.Cells[rowIndex, 2].PutValue(stud.CurrentSeatNo);
                    ws.Cells[rowIndex, 3].PutValue(stud.StudentName);
                }
                ws.Cells[rowIndex, 4].PutValue(mi.SchoolYear);
                ws.Cells[rowIndex, 5].PutValue(mi.Semester);
                ws.Cells[rowIndex, 6].PutValue(mi.MeritDate);
                //ws.Cells[rowIndex, 7].PutValue(mi.M1);    //地點
                ws.Cells[rowIndex, 8].PutValue(toMerit(mi.M1));
                ws.Cells[rowIndex, 9].PutValue(toMerit(mi.M2));
                ws.Cells[rowIndex, 10].PutValue(toMerit(mi.M3));
                ws.Cells[rowIndex, 11].PutValue(toMerit(mi.D1));
                ws.Cells[rowIndex, 12].PutValue(toMerit(mi.D2));
                ws.Cells[rowIndex, 13].PutValue(toMerit(mi.D3));
                ws.Cells[rowIndex, 14].PutValue(mi.Reason);
                ws.Cells[rowIndex, 15].PutValue(mi.IsCanceled ? "是" : "");
                if (mi.IsCanceled)
                    ws.Cells[rowIndex, 16].PutValue(mi.CancelDate);

                rowIndex += 1;
            }
        }

        private string toMerit(string merit)
        {
            string result = string.IsNullOrEmpty(merit) ? "0" : merit.Trim();
            
            return result;
        }
    }
}
