using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 香山國中部_全訊_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            showMsg("====  1. load data ...  ====");
            loadData();

            showMsg("====  2. export permrec data ...  ====");
            Exporter.PermRecExporter pre = new Exporter.PermRecExporter();
            pre.BeginExport();

            //showMsg("====  3. export student affair  data ...  ====");
            //Exporter.StudentAffairExporter studExp = new Exporter.StudentAffairExporter();
            //studExp.BeginExport();
            //studExp.ExportLifeScores();
            

            //TODO : 準備匯出成績資料：
            if (ConnectionHelper.IsJH)
            {
                Exporter.JHScoreExporter scoreExp = new Exporter.JHScoreExporter();
                scoreExp.BeginExport();
            }
            else
            {
                showMsg("====  4. export High school student score  data ...  ====");
                Exporter.SHScoreExporter scoreExp = new Exporter.SHScoreExporter();
                scoreExp.BeginExportSH();
            }

            MessageBox.Show("Finish !");
        }

        private void loadData()
        {
            ConnectionHelper.sourceFolder = this.txtDataPath.Text;

            showMsg("=======. load EDCode data ...  ======");
            DAO.EDCode.Load();
            showMsg("=======. load Class data ...  ======");
            DAO.Classes.Load();
            showMsg("=======. load Student data ...  ======");
            DAO.Students.Load();
            showMsg("=======. load Update Records data ...  ======");
            DAO.UpdateRecords.Load();
            
            //缺曠  //實中國小沒有缺況獎懲
            //showMsg("=======. load Merit data ...  ======");
            //DAO.Merits.Load();
            //showMsg("=======. load Absence data ...  ======");
            //DAO.Absence.Load();
            //showMsg("=======. load DailyLifeScore data ...  ======");
            //DAO.DailyLifeScore.Load();
            
            
            //成績            
            showMsg("=======. load Courses data ...  ======");
            DAO.Courses.Load();
            if (ConnectionHelper.IsJH)
                DAO.JHScores.Load();
            else
            {
                showMsg("=======. load SH Score data ...  ======");
                DAO.SHScores.Load();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ConnectionHelper.IsJH = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ConnectionHelper.IsJH = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConnectionHelper.IsJH = (this.radioButton1.Checked);
        }

        private void showMsg(String msg)
        {
            Console.WriteLine(msg);

        }
    }
}
