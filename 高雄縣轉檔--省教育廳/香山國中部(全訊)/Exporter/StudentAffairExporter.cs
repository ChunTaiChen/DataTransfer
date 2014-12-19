using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Cells;

namespace 香山國中部_全訊_.Exporter
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
            //int rowIndexDomain = 1;
            //Worksheet wst2 = wb.Worksheets["學期領域成績"];
            DateTime dt = DateTime.Now;
            wb.Save(System.Environment.CurrentDirectory + string.Format("\\Data\\{2}_學務資料_{0}_{1}.xls", dt.ToString("yyyyMMdd"), dt.ToString("hhmmss"), (ConnectionHelper.IsJH ? "國中" : "高中")));

            //FillLifeScores();  

       }

        //匯出日常生活表現成績
        public  void ExportLifeScores()
        {
            string template = System.Environment.CurrentDirectory + "\\Template\\" + (ConnectionHelper.IsJH ? "新竹國中日常生活表現.xls" : "高中導師評語.xls");
            //string template = System.Environment.CurrentDirectory + "\\Template\\新竹國中日常生活表現.xls";   //實中國高中部都是相同欄位
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(template);

            string wsName = (ConnectionHelper.IsJH ? "匯出日常生活表現" : "匯出導師評語");
            //string wsName =  "匯出日常生活表現" ;

            Worksheet ws = wb.Worksheets[wsName];

            int rowIndex = 1;
            //TODO : 以下要重寫
            foreach (VO.DailyLifeScoreInfo lifeScore in DAO.DailyLifeScore.GetAllDailyLiftScores())
            {
                VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(lifeScore.StudentNo);
                if (stud == null) continue; //jump to next loop !
                if (Util.IsValidStudent(stud))
                {
                    ws.Cells[rowIndex, 0].PutValue(lifeScore.StudentNo);

                    ws.Cells[rowIndex, 1].PutValue(stud.CurrentFullClassName);
                    ws.Cells[rowIndex, 2].PutValue(stud.CurrentSeatNo);
                    ws.Cells[rowIndex, 3].PutValue(stud.StudentName);

                    ws.Cells[rowIndex, 4].PutValue(lifeScore.SchoolYear.ToString());
                    ws.Cells[rowIndex, 5].PutValue(lifeScore.Semester.ToString());
                    ws.Cells[rowIndex, 6].PutValue(lifeScore.TeacherComment);

                    if (ConnectionHelper.IsJH)    //實中的國高中部欄位都相同
                    {
                        ws.Cells[rowIndex, 7].PutValue(lifeScore.OtherBehavior);
                        ws.Cells[rowIndex, 8].PutValue(lifeScore.Z1);
                        ws.Cells[rowIndex, 9].PutValue(lifeScore.Z2);
                        ws.Cells[rowIndex, 10].PutValue(lifeScore.Z3);
                        ws.Cells[rowIndex, 11].PutValue(lifeScore.Z4);
                        ws.Cells[rowIndex, 12].PutValue(lifeScore.Z5); //團隊合作
                        ws.Cells[rowIndex, 13].PutValue(lifeScore.Z6);
                        ws.Cells[rowIndex, 14].PutValue(lifeScore.Z7);
                    }
                    rowIndex += 1;
                }
            }

            DateTime dt = DateTime.Now;
            wb.Save(System.Environment.CurrentDirectory + string.Format("\\Data\\{2}_{0}_{1}.xls", dt.ToString("yyyyMMdd"), dt.ToString("hhmmss"), (ConnectionHelper.IsJH ? "國中_日常生活表現" : "高中_導師評語")));
        }
        

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

            if(records !=null)
            foreach (VO.AbsenceInfo abs in records)
            {
                foreach (string period in abs.GetPeriods())  //每一節課都是一筆記錄
                {
                    if (rowIndex > 65535)
                    {
                        ws = wb.Worksheets["缺曠紀錄2"];
                        rowIndex = 1;
                    }
                    VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(abs.StudentNo);
                    if (Util.IsValidStudent(stud))                    
                    {
                        ws.Cells[rowIndex, 0].PutValue(abs.StudentNo);
                        //ws.Cells[rowIndex, 1].PutValue(stud.CurrentGrade + stud.CurrentClass);
                        ws.Cells[rowIndex, 1].PutValue(stud.CurrentFullClassName);
                        ws.Cells[rowIndex, 2].PutValue(stud.CurrentSeatNo);
                        ws.Cells[rowIndex, 3].PutValue(stud.StudentName);

                        ws.Cells[rowIndex, 4].PutValue(abs.SchoolYear);
                        ws.Cells[rowIndex, 5].PutValue(abs.Semester);
                        ws.Cells[rowIndex, 6].PutValue(abs.AbsentDate);

                        ws.Cells[rowIndex, 7].PutValue(abs.AbsenceType);
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

            if(records !=null)
            foreach (VO.MeritInfo mi in records)
            {
                VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(mi.StudentNo);
                if (Util.IsValidStudent(stud))
                {
                    ws.Cells[rowIndex, 0].PutValue(mi.StudentNo);
                    if (stud != null)
                    {
                        //ws.Cells[rowIndex, 1].PutValue(stud.CurrentGrade + stud.CurrentClass);
                        ws.Cells[rowIndex, 1].PutValue(stud.CurrentFullClassName);
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
        }

        private string toMerit(string merit)
        {
            string result = string.IsNullOrEmpty(merit) ? "0" : merit.Trim();
            
            return result;
        }
    }
}
