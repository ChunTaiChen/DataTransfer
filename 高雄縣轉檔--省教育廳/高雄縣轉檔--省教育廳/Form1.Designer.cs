namespace 高雄縣轉檔__省教育廳
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtStartYear = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtEndYear = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtDataPath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(13, 13);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "入學年度";
            this.labelX1.Click += new System.EventHandler(this.labelX1_Click);
            // 
            // txtStartYear
            // 
            // 
            // 
            // 
            this.txtStartYear.Border.Class = "TextBoxBorder";
            this.txtStartYear.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtStartYear.Location = new System.Drawing.Point(69, 12);
            this.txtStartYear.Name = "txtStartYear";
            this.txtStartYear.Size = new System.Drawing.Size(27, 22);
            this.txtStartYear.TabIndex = 1;
            this.txtStartYear.Text = "98";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(103, 12);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(25, 23);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "~";
            // 
            // txtEndYear
            // 
            // 
            // 
            // 
            this.txtEndYear.Border.Class = "TextBoxBorder";
            this.txtEndYear.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtEndYear.Location = new System.Drawing.Point(122, 12);
            this.txtEndYear.Name = "txtEndYear";
            this.txtEndYear.Size = new System.Drawing.Size(27, 22);
            this.txtEndYear.TabIndex = 3;
            this.txtEndYear.Text = "99";
            // 
            // txtDataPath
            // 
            // 
            // 
            // 
            this.txtDataPath.Border.Class = "TextBoxBorder";
            this.txtDataPath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDataPath.Location = new System.Drawing.Point(228, 17);
            this.txtDataPath.Name = "txtDataPath";
            this.txtDataPath.Size = new System.Drawing.Size(344, 22);
            this.txtDataPath.TabIndex = 4;
            this.txtDataPath.Text = "C:\\Projects\\高雄國中轉檔--省教育廳版\\RawData\\STUDENT";
            this.txtDataPath.Click += new System.EventHandler(this.textBoxX3_Click);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(171, 16);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(75, 23);
            this.labelX3.TabIndex = 5;
            this.labelX3.Text = "DBF 位置";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(578, 13);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(75, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 6;
            this.buttonX1.Text = "開始轉檔";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 448);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.txtDataPath);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.txtEndYear);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txtStartYear);
            this.Controls.Add(this.labelX1);
            this.Name = "Form1";
            this.Text = "高雄縣國中資料轉檔--省教育廳版";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtStartYear;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtEndYear;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDataPath;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.ButtonX buttonX1;
    }
}

