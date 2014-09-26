using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace 高雄縣轉檔__省教育廳.DAO
{
    class UpdateRecords
    {
        private static Dictionary<string, List<VO.UpdateRecordInfo>> updateRecords;

        //在省教育廳版的資料庫中，新生異動是以一筆學生學號為 X0000  ，且 chg_kind=0 代表該學年度的新生異動紀錄，其上有核准文號等資訊。
        private static Dictionary<string, VO.UpdateRecordInfo> newStudentUptRecords;

        //在省教育廳版的資料庫中，畢業異動是以一筆學生學號為 X0000 ，且 chg_kind=8 代表該學年度的新生異動紀錄，其上有核准文號等資訊。
        private static Dictionary<string, VO.UpdateRecordInfo> graduateStudentUptRecords;   

        public static void Load(int fromYear, int toYear)
        {
            updateRecords = new Dictionary<string, List<VO.UpdateRecordInfo>>();
            newStudentUptRecords = new Dictionary<string, VO.UpdateRecordInfo>();
            graduateStudentUptRecords = new Dictionary<string, VO.UpdateRecordInfo>();

            for (int i = fromYear; i <= toYear; i++)
            {
                OleDbConnection cn = ConnectionHelper.GetPersonConnection(i);
                string sql = string.Format("select * from xregch{0}.dbf", i.ToString());
                OleDbCommand cmd = new OleDbCommand(sql, cn);
                cn.Open();
                IDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    string stud_no = dr["stud_no"].ToString().Trim();
                    if (!updateRecords.ContainsKey(stud_no))
                        updateRecords.Add(stud_no, new List<VO.UpdateRecordInfo>());

                    VO.UpdateRecordInfo uptInfo = new VO.UpdateRecordInfo(dr);
                    uptInfo.CalculateSchoolYearSemester();

                    updateRecords[stud_no].Add(uptInfo);

                    //檢查是否是新生異動紀錄？
                    if (stud_no.Substring(1) == "0000" && uptInfo.UpdateType=="0")
                    {
                        newStudentUptRecords.Add(stud_no, uptInfo);
                    }

                    //檢查是否是新生異動紀錄？
                    if (stud_no.Substring(1) == "0000" && uptInfo.UpdateType == "8")
                    {
                        graduateStudentUptRecords.Add(stud_no, uptInfo);
                    }
                }
                dr.Close();
                cn.Close();
            }            
        }

        /// <summary>
        /// 取得所有的新生異動紀錄
        /// </summary>
        /// <returns></returns>
        public  static List<VO.UpdateRecordInfo> GetNewStudentUpdateRecords()
        {
            List<VO.UpdateRecordInfo> newStudentUpdateRecords = new List<VO.UpdateRecordInfo>();
            foreach (VO.StudentInfo stud in DAO.Students.GetStudents())
            {
                if (stud.EntStatus == "0") //入學狀態： 0 代表新生入學。
                {
                    string stud_no_firstkey = stud.StudentNumber.Substring(0, 1) ;
                    VO.UpdateRecordInfo uptInfo = new VO.UpdateRecordInfo();
                    uptInfo.StudentNo = stud.StudentNumber;
                    uptInfo.UpdateType = "";
                    uptInfo.UpdateReason = "";
                    uptInfo.UpdateDate = string.Format("{0}/8/1", (1911 + 90 + int.Parse(stud_no_firstkey)).ToString() ) ;
                    
                    //取得新生異動
                    string newUptKey = stud_no_firstkey + "0000";
                    if (newStudentUptRecords.ContainsKey(newUptKey))
                    {
                        VO.UpdateRecordInfo newUptInfo = newStudentUptRecords[newUptKey];
                        uptInfo.ApproveBy = newUptInfo.ApproveBy;
                        uptInfo.ApproveDate = newUptInfo.ApproveDate;
                        uptInfo.ApproveNo = newUptInfo.ApproveNo;
                    }

                    uptInfo.CalculateSchoolYearSemester();

                    newStudentUpdateRecords.Add(uptInfo);
                }
            }

            return newStudentUpdateRecords;
        }

        /// <summary>
        /// 取得所有的畢業生異動紀錄
        /// </summary>
        /// <returns></returns>
        public static List<VO.UpdateRecordInfo> GetGraduateStudentUpdateRecords()
        {
            List<VO.UpdateRecordInfo> graduateStudentUpdateRecords = new List<VO.UpdateRecordInfo>();
            foreach (VO.StudentInfo stud in DAO.Students.GetStudents())
            {
                if (stud.EntStatus == "0") //入學狀態： 0 代表新生入學。
                {
                    string stud_no_firstkey = stud.StudentNumber.Substring(0, 1);
                    VO.UpdateRecordInfo uptInfo = new VO.UpdateRecordInfo();
                    uptInfo.StudentNo = stud.StudentNumber;
                    uptInfo.UpdateType = "";
                    uptInfo.UpdateReason = "";
                    //uptInfo.UpdateDate = string.Format("{0}/8/1", (1911 + 90 + int.Parse(stud_no_firstkey)).ToString());
                    

                    //取得新生異動
                    string graduateUptKey = stud_no_firstkey + "0000";
                    if (graduateStudentUptRecords.ContainsKey(graduateUptKey))
                    {
                        VO.UpdateRecordInfo newUptInfo = graduateStudentUptRecords[graduateUptKey];
                        uptInfo.ApproveBy = newUptInfo.ApproveBy;
                        uptInfo.ApproveDate = newUptInfo.ApproveDate;
                        uptInfo.ApproveNo = newUptInfo.ApproveNo;
                        uptInfo.SchoolYear = (90 + int.Parse(stud.StudentNumber.Substring(0,1)) + 2).ToString();
                        //uptInfo.UpdateDate = newUptInfo.ApproveDate;
                    }

                    //uptInfo.CalculateSchoolYearSemester();

                    graduateStudentUpdateRecords.Add(uptInfo);
                }
            }

            return graduateStudentUpdateRecords;
        }

        /// <summary>
        /// 取得具有異動紀錄的學生學號清單
        /// </summary>
        /// <returns></returns>
        public static List<string> GetStudentNosOfUpdateRecords()
        {
            return new List<string>( updateRecords.Keys) ;
        }

        /// <summary>
        /// 取得指定學號的學生的異動紀錄
        /// </summary>
        /// <param name="stud_no"></param>
        /// <returns></returns>
        public static List<VO.UpdateRecordInfo> GetUpdateRecordsByStudentNo(string stud_no)
        {
            List<VO.UpdateRecordInfo> result = new List<VO.UpdateRecordInfo>();

            if (updateRecords.ContainsKey(stud_no))
                result = updateRecords[stud_no];

            return result;
        }


    }
}
