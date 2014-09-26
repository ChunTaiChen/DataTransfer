using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 香山高中部轉檔_全訊_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            loadData();

            //Exporter.PermRecExporter pre = new Exporter.PermRecExporter();
            //pre.BeginExport();

            //Exporter.StudentAffairExporter studExp = new Exporter.StudentAffairExporter();
            //studExp.BeginExport();

            //TODO : 準備匯出成績資料：
            //Exporter.ScoreExporter scoreExp = new Exporter.ScoreExporter();
            //scoreExp.BeginExport();

            MessageBox.Show("Finish !");
        }

        private void loadData()
        {
            ConnectionHelper.sourceFolder = this.txtDataPath.Text;
            /*
            DAO.SemesterHistories.Load();
            DAO.Students.Load();
            DAO.UpdateRecords.Load();

            //缺曠
            DAO.Merits.Load();
            DAO.Absence.Load();
            //DAO.LifeScores.Load();

            //成績
            DAO.Subjects.Load();
            DAO.SubjectScores.Load();
            DAO.DomainScores.Load();
             * */
        }
    }
}
