using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using 高雄縣轉檔__省教育廳.DAO;

namespace 高雄縣轉檔__省教育廳
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void labelX1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxX3_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            loadData();

            Exporter.PermRecExporter pre = new Exporter.PermRecExporter();
            pre.BeginExport();

            Exporter.StudentAffairExporter studExp = new Exporter.StudentAffairExporter();
            studExp.BeginExport();

            //TODO : 準備匯出成績資料：
            Exporter.ScoreExporter scoreExp = new Exporter.ScoreExporter();
            scoreExp.BeginExport();

            MessageBox.Show("Finish !");
        }

        private void loadData()
        {
            ConnectionHelper.sourceFolder = this.txtDataPath.Text;

            int startYear = int.Parse(this.txtStartYear.Text);
            int endYear = int.Parse(this.txtEndYear.Text);
            
            Parents.Load(startYear, endYear);
            HomeTeachers.Load(startYear, endYear);
            Students.Load(startYear,  endYear);
            UpdateRecords.Load(startYear, endYear);
            
            //缺曠
            Merits.Load(startYear, endYear);
            Absence.Load(startYear, endYear);
            LifeScores.Load(startYear, endYear);  

            //成績
            Domains.Load();
            Subjects.Load(startYear, endYear);
            SubjectSemScores.Load(startYear, endYear);
            DomainSemScores.Load(startYear, endYear);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConnectionHelper.sourceFolder = this.txtDataPath.Text;
        }
    }
}
