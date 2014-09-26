using System;
using System.Collections.Generic;

using System.Text;
using Aspose.Cells;

namespace 香山國中部_全訊_.Exporter
{
    //匯出學生資料
    class PermRecExporter
    {
        public void BeginExport()
        {
            string template = System.Environment.CurrentDirectory + "\\Template\\學生資料.xls";
            Workbook wb = new Aspose.Cells.Workbook();
            wb.Open(template);

            FillStudentBasicInfo(wb);

            // FillSemesterHistory(wb);
            
            FillClasses(wb);

            FillNewStudentUpdateRecords(wb);

            FillGraduateStudentUpdateRecords(wb);

            FillUpdateRecords(wb);

            FillIdentityRecords(wb);
            
            //int rowIndexDomain = 1;
            //Worksheet wst2 = wb.Worksheets["學期領域成績"];
            DateTime dt = DateTime.Now;
            wb.Save(System.Environment.CurrentDirectory + string.Format("\\Data\\{2}_學生資料_{0}_{1}.xls", dt.ToString("yyyyMMdd"), dt.ToString("hhmmss"), (ConnectionHelper.IsJH ? "國中" : "高中")));
        }


        //填入新生異動紀錄
        private void FillNewStudentUpdateRecords(Workbook wb)
        {
            Worksheet ws = wb.Worksheets["新生異動"];
            int rowIndex = 1;
            foreach (VO.StudentInfo stud in DAO.Students.GetStudents())
            {
                if (string.IsNullOrEmpty(stud.EntranceCertNo)) continue; //jump to next loop !
                if (Util.IsValidStudent(stud))   //only 在校生
                {
                    VO.CertNoParser cert = new VO.CertNoParser(stud.EntranceCertNo, false);
                    ws.Cells[rowIndex, 0].PutValue(stud.StudentNumber);
                    ws.Cells[rowIndex, 1].PutValue(stud.CurrentFullClassName);
                    ws.Cells[rowIndex, 2].PutValue(stud.CurrentSeatNo);
                    ws.Cells[rowIndex, 3].PutValue(stud.StudentName);
                    ws.Cells[rowIndex, 4].PutValue(cert.schoolyear);
                    ws.Cells[rowIndex, 5].PutValue("1");
                    ws.Cells[rowIndex, 6].PutValue(stud.StudentNumber);
                    ws.Cells[rowIndex, 7].PutValue(stud.StudentName);
                    ws.Cells[rowIndex, 8].PutValue(stud.SSN);
                    ws.Cells[rowIndex, 9].PutValue(stud.Gender);
                    ws.Cells[rowIndex, 10].PutValue(Util.FormatDate(stud.Birthday));
                    ws.Cells[rowIndex, 11].PutValue(Util.FormatDate(stud.EntranceDate));
                    ws.Cells[rowIndex, 12].PutValue("");
                    ws.Cells[rowIndex, 13].PutValue(stud.KB_Sco);
                    ws.Cells[rowIndex, 14].PutValue(cert.approve_date);
                    ws.Cells[rowIndex, 15].PutValue(cert.approve_doc_no);
                    ws.Cells[rowIndex, 16].PutValue("");
                    ws.Cells[rowIndex, 17].PutValue("1");

                    rowIndex += 1;
                }
            }
        }

        //填入畢業生異動紀錄
        private void FillGraduateStudentUpdateRecords(Workbook wb)
        {
            Worksheet ws = wb.Worksheets["畢業異動"];
            int rowIndex = 1;
            foreach (VO.StudentInfo stud in DAO.Students.GetStudents())
            {
                if (string.IsNullOrEmpty(stud.CertNo)) continue; //jump to next loop !
                if (Util.IsValidStudent(stud))   //only 在校生
                {
                    VO.CertNoParser cert = new VO.CertNoParser(stud.CertNo, true);

                    ws.Cells[rowIndex, 0].PutValue(stud.StudentNumber);
                    ws.Cells[rowIndex, 1].PutValue(stud.CurrentFullClassName);
                    ws.Cells[rowIndex, 2].PutValue(stud.CurrentSeatNo);
                    ws.Cells[rowIndex, 3].PutValue(stud.StudentName);
                    ws.Cells[rowIndex, 4].PutValue("3");
                    ws.Cells[rowIndex, 5].PutValue((string.IsNullOrWhiteSpace(cert.schoolyear)) ? "" : ((int.Parse(cert.schoolyear) - 1).ToString()));
                    ws.Cells[rowIndex, 6].PutValue("2");
                    ws.Cells[rowIndex, 7].PutValue(stud.StudentNumber);
                    ws.Cells[rowIndex, 8].PutValue(stud.StudentName);
                    ws.Cells[rowIndex, 9].PutValue(stud.SSN);
                    ws.Cells[rowIndex, 10].PutValue(stud.Gender);
                    ws.Cells[rowIndex, 11].PutValue(Util.FormatDate(stud.Birthday));
                    ws.Cells[rowIndex, 12].PutValue(Util.FormatDate(stud.GraduateDate));
                    ws.Cells[rowIndex, 13].PutValue("畢業");
                    ws.Cells[rowIndex, 14].PutValue(stud.GraduateID);
                    ws.Cells[rowIndex, 15].PutValue(cert.approve_doc_no);
                    ws.Cells[rowIndex, 16].PutValue(cert.approve_date);


                    rowIndex += 1;
                }
            }
        }

        //填入異動紀錄
        private void FillUpdateRecords(Workbook wb)
        {
            Worksheet ws = wb.Worksheets["學籍異動"];
            int rowIndex = 1;
            foreach (string stud_no in DAO.UpdateRecords.GetStudentNosOfUpdateRecords())
            {
                
                    VO.StudentInfo stud = DAO.Students.GetStudentByStudNo(stud_no);
                    if (stud == null) continue; //jump to next loop !
                    if (Util.IsValidStudent(stud))   //only 在校生
                    {
                    foreach (VO.UpdateRecordInfo uptInfo in DAO.UpdateRecords.GetUpdateRecordsByStudentNo(stud_no))
                    {
                        ws.Cells[rowIndex, 0].PutValue(stud.StudentNumber);
                        ws.Cells[rowIndex, 1].PutValue(stud.CurrentFullClassName);
                        ws.Cells[rowIndex, 2].PutValue(stud.CurrentSeatNo);
                        ws.Cells[rowIndex, 3].PutValue(stud.StudentName);
                        ws.Cells[rowIndex, 4].PutValue(uptInfo.GradeYear);
                        ws.Cells[rowIndex, 5].PutValue(uptInfo.SchoolYear);
                        ws.Cells[rowIndex, 6].PutValue(uptInfo.Semester);

                        ws.Cells[rowIndex, 12].PutValue(uptInfo.GetUpdateTypeString());
                        ws.Cells[rowIndex, 13].PutValue(Util.FormatDate(uptInfo.UpdateDate));
                        ws.Cells[rowIndex, 14].PutValue(uptInfo.UpdateReason);
                        ws.Cells[rowIndex, 15].PutValue("");
                        ws.Cells[rowIndex, 16].PutValue(uptInfo.WhereToGo);

                        ws.Cells[rowIndex, 19].PutValue(uptInfo.ApproveDate);
                        ws.Cells[rowIndex, 20].PutValue(uptInfo.ApproveNo);

                        if (!ConnectionHelper.IsJH)
                            ws.Cells[rowIndex, 31].PutValue(uptInfo.UpdateType);
                        rowIndex += 1;
                    }  //end foreach
                } // end if
            } // end foreach
        }

        private void FillSemesterHistory(Workbook wb)
        {
            Worksheet ws = wb.Worksheets["學期歷程"];
            int rowIndex = 1;
            foreach (VO.StudentInfo stud in DAO.Students.GetStudents())
            {
                foreach (VO.SemesterHistoryInfo smh in stud.GetLearnHistory())
                {
                    ws.Cells[rowIndex, 0].PutValue(stud.StudentNumber);
                    ws.Cells[rowIndex, 1].PutValue(stud.CurrentGrade + stud.CurrentClass);
                    ws.Cells[rowIndex, 2].PutValue(stud.CurrentSeatNo);
                    ws.Cells[rowIndex, 3].PutValue(stud.StudentName);
                    ws.Cells[rowIndex, 4].PutValue(smh.SchoolYear);
                    ws.Cells[rowIndex, 5].PutValue(smh.Semester);
                    ws.Cells[rowIndex, 6].PutValue(smh.Grade);
                    ws.Cells[rowIndex, 7].PutValue(smh.Grade + smh.ClassName);
                    ws.Cells[rowIndex, 8].PutValue(smh.SeatNo);
                    //ws.Cells[rowIndex, 9].PutValue(smh.GetHomeTeacherName());    
                    rowIndex += 1;
                }
            }
        }

        //填入學生基本資料
        private void FillStudentBasicInfo(Workbook wb)
        {
            Worksheet ws = wb.Worksheets["學生基本資料"];
            int rowIndex = 1;
            foreach (VO.StudentInfo stud in DAO.Students.GetStudents())
            {
                if (stud.StudentName == "吳翊瑋")
                    Console.WriteLine("hello !");

                if (Util.IsValidStudent(stud))   //only 在校生
                {
                    ws.Cells[rowIndex, 0].PutValue(stud.StudentName);
                    ws.Cells[rowIndex, 1].PutValue(stud.StudentNumber);
                    ws.Cells[rowIndex, 2].PutValue(stud.SSN);
                    //ws.Cells[rowIndex, 3].PutValue(stud.StudentName);   //國籍？
                    ws.Cells[rowIndex, 4].PutValue(stud.BirthPlace);
                    ws.Cells[rowIndex, 5].PutValue(Util.FormatDate(stud.Birthday));
                    ws.Cells[rowIndex, 6].PutValue(stud.Gender);
                    ws.Cells[rowIndex, 7].PutValue(stud.EnglishName);   //英文姓名？
                    ws.Cells[rowIndex, 8].PutValue(stud.CurrentFullClassName);
                    ws.Cells[rowIndex, 9].PutValue(stud.CurrentGrade);
                    ws.Cells[rowIndex, 10].PutValue(stud.CurrentSeatNo);

                    ws.Cells[rowIndex, 19].PutValue(stud.OfficialAddress);
                    ws.Cells[rowIndex, 25].PutValue(stud.ContactAddress);
                    ws.Cells[rowIndex, 27].PutValue(stud.ContactTel);
                    ws.Cells[rowIndex, 28].PutValue(stud.MobilePhone);

                    ws.Cells[rowIndex, 32].PutValue(stud.GdName);
                    ws.Cells[rowIndex, 35].PutValue(stud.GdRelation);
                    ws.Cells[rowIndex, 37].PutValue(stud.GdJob);
                    ws.Cells[rowIndex, 38].PutValue(stud.GdTelH);

                    /*
                    VO.ParentInfo parent = stud.Parent;
                    if (parent != null)
                    {
                        ws.Cells[rowIndex, 39].PutValue(parent.FatherName);
                        ws.Cells[rowIndex, 42].PutValue(parent.FatherLive);
                        ws.Cells[rowIndex, 43].PutValue(parent.FatherEduc);
                        ws.Cells[rowIndex, 44].PutValue(parent.FatherOccupation);
                        ws.Cells[rowIndex, 45].PutValue(parent.FatherTel);

                        ws.Cells[rowIndex, 46].PutValue(parent.MotherName);
                        ws.Cells[rowIndex, 49].PutValue(parent.MotherLive);
                        ws.Cells[rowIndex, 50].PutValue(parent.MotherEduc);
                        ws.Cells[rowIndex, 51].PutValue(parent.MotherOccupation);
                        ws.Cells[rowIndex, 52].PutValue(parent.MotherTel);
                    }
                    */
                    ws.Cells[rowIndex, 53].PutValue(stud.StatusText);
                    ws.Cells[rowIndex, 54].PutValue(stud.EntQualify);

                    rowIndex += 1;
                }
            }

        }

        private void FillClasses(Workbook wb)
        {
            Worksheet ws = wb.Worksheets["班級"];
            int rowIndex = 1;
            
            foreach (VO.ClassInfo cls in DAO.Classes.GetAllClasses())
            {
                ws.Cells[rowIndex, 0].PutValue(cls.ClassFullName);
                ws.Cells[rowIndex, 1].PutValue("");
                ws.Cells[rowIndex, 2].PutValue(cls.GradeYear);
                ws.Cells[rowIndex, 6].PutValue(cls.DeptName);

                rowIndex += 1;
            }
        }

        private void FillIdentityRecords(Workbook wb)
        {
            Worksheet ws = wb.Worksheets["學生身分資料"];
            int rowIndex = 1;

            foreach (VO.StudentInfo stud in DAO.Students.GetStudents())
            {
                if (Util.IsValidStudent(stud))   //only 在校生
                {
                    if (!string.IsNullOrWhiteSpace(stud.Perna))
                    {
                        ws.Cells[rowIndex, 0].PutValue(stud.StudentNumber);
                        ws.Cells[rowIndex, 1].PutValue(stud.StudentName);
                        ws.Cells[rowIndex, 4].PutValue(stud.Perna2);
                        ws.Cells[rowIndex, 5].PutValue(stud.Perna);

                        rowIndex += 1;
                    }
                }
            }
        }
    }
}
