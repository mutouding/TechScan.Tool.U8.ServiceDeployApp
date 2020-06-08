namespace TechScan.Tool.U8.ServiceDeployWin.Deploy
{
    partial class PackageZipForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackageZipForm));
            this.tsTools = new System.Windows.Forms.ToolStrip();
            this.tbStartPack = new System.Windows.Forms.ToolStripSplitButton();
            this.tbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsddbSet = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbRecentObjects = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsRecentObjects = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.tsbtnNewSqlQuery = new System.Windows.Forms.ToolStripButton();
            this.cmbPackageType = new System.Windows.Forms.ComboBox();
            this.rboSQL = new System.Windows.Forms.RadioButton();
            this.rboIIS = new System.Windows.Forms.RadioButton();
            this.txtDestinationDirectory = new Control.MyTextBox();
            this.txtPackVersionCode = new Control.MyTextBox();
            this.txtPackager = new Control.MyTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gbPackType = new System.Windows.Forms.GroupBox();
            this.txtPackVersionDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSrcDir = new Control.MyTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.picLoading = new TechScan.Tool.Controls.SpinningCircles();
            this.web服务打包历史ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sQL脚本打包历史ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsTools.SuspendLayout();
            this.gbPackType.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsTools
            // 
            this.tsTools.GripMargin = new System.Windows.Forms.Padding(3);
            this.tsTools.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbStartPack,
            this.tbRefresh,
            this.tsddbSet,
            this.toolStripSeparator1,
            this.tbRecentObjects,
            this.tsbHelp,
            this.tsbtnNewSqlQuery});
            this.tsTools.Location = new System.Drawing.Point(0, 0);
            this.tsTools.Name = "tsTools";
            this.tsTools.Size = new System.Drawing.Size(570, 31);
            this.tsTools.TabIndex = 4;
            this.tsTools.Text = "toolStrip1";
            // 
            // tbStartPack
            // 
            this.tbStartPack.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbStartPack.Image = ((System.Drawing.Image)(resources.GetObject("tbStartPack.Image")));
            this.tbStartPack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbStartPack.Name = "tbStartPack";
            this.tbStartPack.Size = new System.Drawing.Size(114, 28);
            this.tbStartPack.Text = "执行打包";
            this.tbStartPack.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tbStartPack.ToolTipText = "执行打包";
            this.tbStartPack.ButtonClick += new System.EventHandler(this.tbStartPack_ButtonClick);
            // 
            // tbRefresh
            // 
            this.tbRefresh.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tbRefresh.Image")));
            this.tbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbRefresh.Margin = new System.Windows.Forms.Padding(4, 1, 0, 2);
            this.tbRefresh.Name = "tbRefresh";
            this.tbRefresh.Size = new System.Drawing.Size(70, 28);
            this.tbRefresh.Text = "刷新";
            // 
            // tsddbSet
            // 
            this.tsddbSet.Image = ((System.Drawing.Image)(resources.GetObject("tsddbSet.Image")));
            this.tsddbSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbSet.Name = "tsddbSet";
            this.tsddbSet.Size = new System.Drawing.Size(108, 28);
            this.tsddbSet.Text = "参数配置(&E)";
            this.tsddbSet.ToolTipText = "参数配置";
            this.tsddbSet.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tbRecentObjects
            // 
            this.tbRecentObjects.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRecentObjects,
            this.web服务打包历史ToolStripMenuItem,
            this.sQL脚本打包历史ToolStripMenuItem});
            this.tbRecentObjects.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbRecentObjects.Image = global::TechScan.Tool.U8.ServiceDeployWin.Properties.Resources.History2;
            this.tbRecentObjects.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbRecentObjects.Margin = new System.Windows.Forms.Padding(4, 1, 0, 2);
            this.tbRecentObjects.Name = "tbRecentObjects";
            this.tbRecentObjects.Size = new System.Drawing.Size(111, 28);
            this.tbRecentObjects.Text = "打包历史";
            this.tbRecentObjects.Click += new System.EventHandler(this.tbRecentObjects_Click);
            // 
            // tsRecentObjects
            // 
            this.tsRecentObjects.Name = "tsRecentObjects";
            this.tsRecentObjects.Size = new System.Drawing.Size(57, 6);
            // 
            // tsbHelp
            // 
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(77, 28);
            this.tsbHelp.Text = "帮助(&H)";
            this.tsbHelp.ToolTipText = "帮助";
            this.tsbHelp.Visible = false;
            // 
            // tsbtnNewSqlQuery
            // 
            this.tsbtnNewSqlQuery.Image = global::TechScan.Tool.U8.ServiceDeployWin.Properties.Resources.Edit2;
            this.tsbtnNewSqlQuery.Name = "tsbtnNewSqlQuery";
            this.tsbtnNewSqlQuery.Size = new System.Drawing.Size(107, 28);
            this.tsbtnNewSqlQuery.Text = "新建SQL查询";
            this.tsbtnNewSqlQuery.Visible = false;
            // 
            // cmbPackageType
            // 
            this.cmbPackageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPackageType.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPackageType.FormattingEnabled = true;
            this.cmbPackageType.Location = new System.Drawing.Point(105, 133);
            this.cmbPackageType.Name = "cmbPackageType";
            this.cmbPackageType.Size = new System.Drawing.Size(169, 27);
            this.cmbPackageType.TabIndex = 12;
            // 
            // rboSQL
            // 
            this.rboSQL.AutoSize = true;
            this.rboSQL.BackColor = System.Drawing.Color.Transparent;
            this.rboSQL.Font = new System.Drawing.Font("宋体", 12F);
            this.rboSQL.Location = new System.Drawing.Point(161, 25);
            this.rboSQL.Name = "rboSQL";
            this.rboSQL.Size = new System.Drawing.Size(82, 20);
            this.rboSQL.TabIndex = 11;
            this.rboSQL.TabStop = true;
            this.rboSQL.Text = "SQL脚本";
            this.rboSQL.UseVisualStyleBackColor = false;
            // 
            // rboIIS
            // 
            this.rboIIS.AutoSize = true;
            this.rboIIS.Checked = true;
            this.rboIIS.Font = new System.Drawing.Font("宋体", 12F);
            this.rboIIS.Location = new System.Drawing.Point(24, 25);
            this.rboIIS.Name = "rboIIS";
            this.rboIIS.Size = new System.Drawing.Size(130, 20);
            this.rboIIS.TabIndex = 10;
            this.rboIIS.TabStop = true;
            this.rboIIS.Text = "U8Android服务";
            this.rboIIS.UseVisualStyleBackColor = true;
            // 
            // txtDestinationDirectory
            // 
            this.txtDestinationDirectory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(152)))), ((int)(((byte)(177)))));
            this.txtDestinationDirectory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDestinationDirectory.Caption = "目标目录：";
            this.txtDestinationDirectory.CaptionColor = System.Drawing.SystemColors.ControlText;
            this.txtDestinationDirectory.Font = new System.Drawing.Font("宋体", 12F);
            this.txtDestinationDirectory.Location = new System.Drawing.Point(14, 390);
            this.txtDestinationDirectory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDestinationDirectory.Name = "txtDestinationDirectory";
            this.txtDestinationDirectory.PasswordChar = '\0';
            this.txtDestinationDirectory.SelectionStart = 0;
            this.txtDestinationDirectory.Size = new System.Drawing.Size(520, 33);
            this.txtDestinationDirectory.TabIndex = 18;
            this.txtDestinationDirectory.TextBoxType = Control.TextBoxType.txtZoom;
            this.txtDestinationDirectory.Value = null;
            this.txtDestinationDirectory.ZoomClick += new System.EventHandler(this.txtDestinationDirectory_ZoomClick);
            // 
            // txtPackVersionCode
            // 
            this.txtPackVersionCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPackVersionCode.Caption = "打包版本号：";
            this.txtPackVersionCode.CaptionColor = System.Drawing.SystemColors.ControlText;
            this.txtPackVersionCode.Font = new System.Drawing.Font("宋体", 12F);
            this.txtPackVersionCode.Location = new System.Drawing.Point(297, 131);
            this.txtPackVersionCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPackVersionCode.Name = "txtPackVersionCode";
            this.txtPackVersionCode.PasswordChar = '\0';
            this.txtPackVersionCode.SelectionStart = 0;
            this.txtPackVersionCode.Size = new System.Drawing.Size(232, 33);
            this.txtPackVersionCode.TabIndex = 13;
            this.txtPackVersionCode.TextBoxType = Control.TextBoxType.txt;
            this.txtPackVersionCode.Value = "";
            // 
            // txtPackager
            // 
            this.txtPackager.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(152)))), ((int)(((byte)(177)))));
            this.txtPackager.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPackager.Caption = "打 包 人：";
            this.txtPackager.CaptionColor = System.Drawing.SystemColors.ControlText;
            this.txtPackager.Font = new System.Drawing.Font("宋体", 12F);
            this.txtPackager.Location = new System.Drawing.Point(297, 66);
            this.txtPackager.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPackager.Name = "txtPackager";
            this.txtPackager.PasswordChar = '\0';
            this.txtPackager.SelectionStart = 0;
            this.txtPackager.Size = new System.Drawing.Size(239, 33);
            this.txtPackager.TabIndex = 15;
            this.txtPackager.TextBoxType = Control.TextBoxType.txt;
            this.txtPackager.Value = null;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(14, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 20;
            this.label2.Text = "版本类型：";
            // 
            // gbPackType
            // 
            this.gbPackType.Controls.Add(this.rboSQL);
            this.gbPackType.Controls.Add(this.rboIIS);
            this.gbPackType.Font = new System.Drawing.Font("宋体", 12F);
            this.gbPackType.Location = new System.Drawing.Point(12, 50);
            this.gbPackType.Name = "gbPackType";
            this.gbPackType.Size = new System.Drawing.Size(262, 61);
            this.gbPackType.TabIndex = 21;
            this.gbPackType.TabStop = false;
            this.gbPackType.Text = "打包类型";
            // 
            // txtPackVersionDescription
            // 
            this.txtPackVersionDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPackVersionDescription.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPackVersionDescription.Location = new System.Drawing.Point(105, 191);
            this.txtPackVersionDescription.Multiline = true;
            this.txtPackVersionDescription.Name = "txtPackVersionDescription";
            this.txtPackVersionDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPackVersionDescription.Size = new System.Drawing.Size(424, 126);
            this.txtPackVersionDescription.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(14, 191);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 23;
            this.label3.Text = "版本描述：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(532, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(532, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "*";
            // 
            // txtSrcDir
            // 
            this.txtSrcDir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(152)))), ((int)(((byte)(177)))));
            this.txtSrcDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSrcDir.Caption = "源 目 录：";
            this.txtSrcDir.CaptionColor = System.Drawing.SystemColors.ControlText;
            this.txtSrcDir.Font = new System.Drawing.Font("宋体", 12F);
            this.txtSrcDir.Location = new System.Drawing.Point(14, 334);
            this.txtSrcDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSrcDir.Name = "txtSrcDir";
            this.txtSrcDir.PasswordChar = '\0';
            this.txtSrcDir.SelectionStart = 0;
            this.txtSrcDir.Size = new System.Drawing.Size(520, 33);
            this.txtSrcDir.TabIndex = 26;
            this.txtSrcDir.TextBoxType = Control.TextBoxType.txtZoom;
            this.txtSrcDir.Value = null;
            this.txtSrcDir.ZoomClick += new System.EventHandler(this.txtSrcDir_ZoomClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(532, 353);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(532, 410);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(103, 371);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(235, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "待打包的源Android服务或者脚本所在文件夹";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(103, 427);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(115, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "打包生成的目标目录";
            // 
            // picLoading
            // 
            this.picLoading.BackColor = System.Drawing.Color.Transparent;
            this.picLoading.FullTransparent = true;
            this.picLoading.Increment = 1F;
            this.picLoading.Location = new System.Drawing.Point(225, 183);
            this.picLoading.N = 8;
            this.picLoading.Name = "picLoading";
            this.picLoading.Radius = 2.5F;
            this.picLoading.Size = new System.Drawing.Size(132, 139);
            this.picLoading.TabIndex = 19;
            this.picLoading.Text = "spinningCircles1";
            this.picLoading.Visible = false;
            // 
            // web服务打包历史ToolStripMenuItem
            // 
            this.web服务打包历史ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("web服务打包历史ToolStripMenuItem.Image")));
            this.web服务打包历史ToolStripMenuItem.Name = "web服务打包历史ToolStripMenuItem";
            this.web服务打包历史ToolStripMenuItem.Size = new System.Drawing.Size(219, 30);
            this.web服务打包历史ToolStripMenuItem.Text = "Web服务打包历史";
            this.web服务打包历史ToolStripMenuItem.Click += new System.EventHandler(this.web服务打包历史ToolStripMenuItem_Click);
            // 
            // sQL脚本打包历史ToolStripMenuItem
            // 
            this.sQL脚本打包历史ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("sQL脚本打包历史ToolStripMenuItem.Image")));
            this.sQL脚本打包历史ToolStripMenuItem.Name = "sQL脚本打包历史ToolStripMenuItem";
            this.sQL脚本打包历史ToolStripMenuItem.Size = new System.Drawing.Size(219, 30);
            this.sQL脚本打包历史ToolStripMenuItem.Text = "SQL脚本打包历史";
            this.sQL脚本打包历史ToolStripMenuItem.Click += new System.EventHandler(this.sQL脚本打包历史ToolStripMenuItem_Click);
            // 
            // PackageZipForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(152)))), ((int)(((byte)(177)))));
            this.ClientSize = new System.Drawing.Size(570, 445);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.picLoading);
            this.Controls.Add(this.txtSrcDir);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPackVersionDescription);
            this.Controls.Add(this.gbPackType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDestinationDirectory);
            this.Controls.Add(this.txtPackVersionCode);
            this.Controls.Add(this.txtPackager);
            this.Controls.Add(this.cmbPackageType);
            this.Controls.Add(this.tsTools);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PackageZipForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件打包";
            this.tsTools.ResumeLayout(false);
            this.tsTools.PerformLayout();
            this.gbPackType.ResumeLayout(false);
            this.gbPackType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsTools;
        private System.Windows.Forms.ToolStripSplitButton tbStartPack;
        private System.Windows.Forms.ToolStripButton tbRefresh;
        private System.Windows.Forms.ToolStripDropDownButton tsddbSet;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton tbRecentObjects;
        private System.Windows.Forms.ToolStripSeparator tsRecentObjects;
        private System.Windows.Forms.ToolStripButton tsbHelp;
        private System.Windows.Forms.ToolStripButton tsbtnNewSqlQuery;
        private System.Windows.Forms.ComboBox cmbPackageType;
        private System.Windows.Forms.RadioButton rboSQL;
        private System.Windows.Forms.RadioButton rboIIS;
        private Controls.SpinningCircles picLoading;
        private Control.MyTextBox txtDestinationDirectory;
        private Control.MyTextBox txtPackVersionCode;
        private Control.MyTextBox txtPackager;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbPackType;
        private System.Windows.Forms.TextBox txtPackVersionDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private Control.MyTextBox txtSrcDir;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripMenuItem web服务打包历史ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sQL脚本打包历史ToolStripMenuItem;
    }
}