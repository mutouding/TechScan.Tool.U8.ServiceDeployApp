namespace TechScan.Tool.U8.ServiceDeployWin.Deploy
{
    partial class frmSetting
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetting));
            this.txtBackUpPath = new Control.MyTextBox();
            this.txtPorts = new Control.MyTextBox();
            this.chkEnable32On64 = new System.Windows.Forms.CheckBox();
            this.cmbPipeType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbNetFrameworks = new System.Windows.Forms.ComboBox();
            this.txtAppPoolName = new Control.MyTextBox();
            this.chkCreateAppPool = new System.Windows.Forms.CheckBox();
            this.tcSetting = new LayeredSkin.Controls.LayeredTabControl();
            this.tpIISPublish = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtUsersToControlAuth = new Control.MyTextBox();
            this.chkControlUserAuth = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblFullSiteName = new System.Windows.Forms.Label();
            this.txtMIMEs = new Control.MyTextBox();
            this.txtSitePhysicPath = new Control.MyTextBox();
            this.txtWebSiteName = new Control.MyTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.tpIISReg = new System.Windows.Forms.TabPage();
            this.ssStatus = new System.Windows.Forms.StatusStrip();
            this.tslblPublishingStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblPublishResult = new System.Windows.Forms.ToolStripStatusLabel();
            this.gpReRegWcf = new TechScan.Tool.Controls.CollapsibleGroupBox();
            this.picLoadingRegWcf = new System.Windows.Forms.PictureBox();
            this.chkAutoReRegWcf = new System.Windows.Forms.CheckBox();
            this.btnRegWcf = new System.Windows.Forms.Button();
            this.txtRegWcf = new System.Windows.Forms.TextBox();
            this.gpReRegNet = new TechScan.Tool.Controls.CollapsibleGroupBox();
            this.picLoadingRegNet = new System.Windows.Forms.PictureBox();
            this.chkAutoReRegNet = new System.Windows.Forms.CheckBox();
            this.btnRegNet = new System.Windows.Forms.Button();
            this.txtRegNet = new System.Windows.Forms.TextBox();
            this.tpSQL = new System.Windows.Forms.TabPage();
            this.txtDbConnectionTimeout = new Control.MyTextBox();
            this.tpSys = new System.Windows.Forms.TabPage();
            this.groupBox2 = new TechScan.Tool.Controls.GroupBox();
            this.groupBox1 = new TechScan.Tool.Controls.GroupBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tcSetting.SuspendLayout();
            this.tpIISPublish.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tpIISReg.SuspendLayout();
            this.ssStatus.SuspendLayout();
            this.gpReRegWcf.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoadingRegWcf)).BeginInit();
            this.gpReRegNet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoadingRegNet)).BeginInit();
            this.tpSQL.SuspendLayout();
            this.tpSys.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtBackUpPath
            // 
            this.txtBackUpPath.BackColor = System.Drawing.Color.White;
            this.txtBackUpPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBackUpPath.Caption = "服务备份目录：";
            this.txtBackUpPath.CaptionColor = System.Drawing.SystemColors.ControlText;
            this.txtBackUpPath.Enabled = false;
            this.txtBackUpPath.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBackUpPath.Location = new System.Drawing.Point(28, 155);
            this.txtBackUpPath.Margin = new System.Windows.Forms.Padding(5);
            this.txtBackUpPath.Name = "txtBackUpPath";
            this.txtBackUpPath.PasswordChar = '\0';
            this.txtBackUpPath.SelectionStart = 0;
            this.txtBackUpPath.Size = new System.Drawing.Size(493, 42);
            this.txtBackUpPath.TabIndex = 8;
            this.txtBackUpPath.TextBoxType = Control.TextBoxType.txtZoom;
            this.txtBackUpPath.Value = "80";
            // 
            // txtPorts
            // 
            this.txtPorts.BackColor = System.Drawing.Color.White;
            this.txtPorts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPorts.Caption = "Web 端口：";
            this.txtPorts.CaptionColor = System.Drawing.SystemColors.ControlText;
            this.txtPorts.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPorts.Location = new System.Drawing.Point(28, 205);
            this.txtPorts.Margin = new System.Windows.Forms.Padding(5);
            this.txtPorts.Name = "txtPorts";
            this.txtPorts.PasswordChar = '\0';
            this.txtPorts.SelectionStart = 0;
            this.txtPorts.Size = new System.Drawing.Size(493, 42);
            this.txtPorts.TabIndex = 7;
            this.txtPorts.TextBoxType = Control.TextBoxType.txt;
            this.txtPorts.Value = "80";
            // 
            // chkEnable32On64
            // 
            this.chkEnable32On64.AutoSize = true;
            this.chkEnable32On64.Checked = true;
            this.chkEnable32On64.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnable32On64.Enabled = false;
            this.chkEnable32On64.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkEnable32On64.Location = new System.Drawing.Point(337, 26);
            this.chkEnable32On64.Margin = new System.Windows.Forms.Padding(4);
            this.chkEnable32On64.Name = "chkEnable32On64";
            this.chkEnable32On64.Size = new System.Drawing.Size(174, 22);
            this.chkEnable32On64.TabIndex = 6;
            this.chkEnable32On64.Text = "启用32位应用程序";
            this.chkEnable32On64.UseVisualStyleBackColor = true;
            // 
            // cmbPipeType
            // 
            this.cmbPipeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPipeType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPipeType.FormattingEnabled = true;
            this.cmbPipeType.Location = new System.Drawing.Point(268, 148);
            this.cmbPipeType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPipeType.Name = "cmbPipeType";
            this.cmbPipeType.Size = new System.Drawing.Size(252, 25);
            this.cmbPipeType.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(89, 151);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "托管管道模式";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(28, 110);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = ".Net Framework版本";
            // 
            // cmbNetFrameworks
            // 
            this.cmbNetFrameworks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNetFrameworks.Enabled = false;
            this.cmbNetFrameworks.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbNetFrameworks.FormattingEnabled = true;
            this.cmbNetFrameworks.Location = new System.Drawing.Point(268, 106);
            this.cmbNetFrameworks.Margin = new System.Windows.Forms.Padding(4);
            this.cmbNetFrameworks.Name = "cmbNetFrameworks";
            this.cmbNetFrameworks.Size = new System.Drawing.Size(252, 25);
            this.cmbNetFrameworks.TabIndex = 2;
            // 
            // txtAppPoolName
            // 
            this.txtAppPoolName.BackColor = System.Drawing.Color.White;
            this.txtAppPoolName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAppPoolName.Caption = "程序池名称：";
            this.txtAppPoolName.CaptionColor = System.Drawing.SystemColors.ControlText;
            this.txtAppPoolName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtAppPoolName.Location = new System.Drawing.Point(28, 58);
            this.txtAppPoolName.Margin = new System.Windows.Forms.Padding(5);
            this.txtAppPoolName.Name = "txtAppPoolName";
            this.txtAppPoolName.PasswordChar = '\0';
            this.txtAppPoolName.SelectionStart = 0;
            this.txtAppPoolName.Size = new System.Drawing.Size(493, 42);
            this.txtAppPoolName.TabIndex = 1;
            this.txtAppPoolName.TextBoxType = Control.TextBoxType.txt;
            this.txtAppPoolName.Value = null;
            // 
            // chkCreateAppPool
            // 
            this.chkCreateAppPool.AutoSize = true;
            this.chkCreateAppPool.Checked = true;
            this.chkCreateAppPool.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCreateAppPool.Enabled = false;
            this.chkCreateAppPool.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkCreateAppPool.Location = new System.Drawing.Point(28, 26);
            this.chkCreateAppPool.Margin = new System.Windows.Forms.Padding(4);
            this.chkCreateAppPool.Name = "chkCreateAppPool";
            this.chkCreateAppPool.Size = new System.Drawing.Size(120, 22);
            this.chkCreateAppPool.TabIndex = 0;
            this.chkCreateAppPool.Text = "创建程序池";
            this.chkCreateAppPool.UseVisualStyleBackColor = true;
            // 
            // tcSetting
            // 
            this.tcSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tcSetting.Borders.BottomColor = System.Drawing.Color.Empty;
            this.tcSetting.Borders.BottomWidth = 1;
            this.tcSetting.Borders.LeftColor = System.Drawing.Color.Empty;
            this.tcSetting.Borders.LeftWidth = 1;
            this.tcSetting.Borders.RightColor = System.Drawing.Color.Empty;
            this.tcSetting.Borders.RightWidth = 1;
            this.tcSetting.Borders.TopColor = System.Drawing.Color.Empty;
            this.tcSetting.Borders.TopWidth = 1;
            this.tcSetting.Canvas = ((System.Drawing.Bitmap)(resources.GetObject("tcSetting.Canvas")));
            this.tcSetting.Controls.Add(this.tpIISPublish);
            this.tcSetting.Controls.Add(this.tpIISReg);
            this.tcSetting.Controls.Add(this.tpSQL);
            this.tcSetting.Controls.Add(this.tpSys);
            this.tcSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSetting.HoverBackColors = new System.Drawing.Color[] {
        System.Drawing.Color.Transparent,
        System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))))};
            this.tcSetting.ItemBackgroundImage = null;
            this.tcSetting.ItemBackgroundImageHover = null;
            this.tcSetting.ItemBackgroundImageSelected = null;
            this.tcSetting.Location = new System.Drawing.Point(0, 0);
            this.tcSetting.Margin = new System.Windows.Forms.Padding(4);
            this.tcSetting.Name = "tcSetting";
            this.tcSetting.NormalBackColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))))};
            this.tcSetting.SelectedBackColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))))};
            this.tcSetting.SelectedIndex = 0;
            this.tcSetting.Size = new System.Drawing.Size(581, 670);
            this.tcSetting.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tcSetting.TabIndex = 3;
            this.tcSetting.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // tpIISPublish
            // 
            this.tpIISPublish.Controls.Add(this.groupBox4);
            this.tpIISPublish.Controls.Add(this.groupBox3);
            this.tpIISPublish.ImageIndex = 2;
            this.tpIISPublish.Location = new System.Drawing.Point(0, 26);
            this.tpIISPublish.Margin = new System.Windows.Forms.Padding(4);
            this.tpIISPublish.Name = "tpIISPublish";
            this.tpIISPublish.Padding = new System.Windows.Forms.Padding(4);
            this.tpIISPublish.Size = new System.Drawing.Size(581, 644);
            this.tpIISPublish.TabIndex = 0;
            this.tpIISPublish.Text = "IIS-Web服务";
            this.tpIISPublish.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtUsersToControlAuth);
            this.groupBox4.Controls.Add(this.chkControlUserAuth);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.lblFullSiteName);
            this.groupBox4.Controls.Add(this.txtMIMEs);
            this.groupBox4.Controls.Add(this.txtSitePhysicPath);
            this.groupBox4.Controls.Add(this.txtWebSiteName);
            this.groupBox4.Controls.Add(this.txtPorts);
            this.groupBox4.Controls.Add(this.txtBackUpPath);
            this.groupBox4.Location = new System.Drawing.Point(8, 211);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(561, 416);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Web站点";
            // 
            // txtUsersToControlAuth
            // 
            this.txtUsersToControlAuth.BackColor = System.Drawing.Color.White;
            this.txtUsersToControlAuth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsersToControlAuth.Caption = "虚拟站点用户：";
            this.txtUsersToControlAuth.CaptionColor = System.Drawing.SystemColors.ControlText;
            this.txtUsersToControlAuth.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUsersToControlAuth.Location = new System.Drawing.Point(28, 366);
            this.txtUsersToControlAuth.Margin = new System.Windows.Forms.Padding(5);
            this.txtUsersToControlAuth.Name = "txtUsersToControlAuth";
            this.txtUsersToControlAuth.PasswordChar = '\0';
            this.txtUsersToControlAuth.SelectionStart = 0;
            this.txtUsersToControlAuth.Size = new System.Drawing.Size(493, 42);
            this.txtUsersToControlAuth.TabIndex = 15;
            this.txtUsersToControlAuth.TextBoxType = Control.TextBoxType.txt;
            this.txtUsersToControlAuth.Value = "80";
            // 
            // chkControlUserAuth
            // 
            this.chkControlUserAuth.AutoSize = true;
            this.chkControlUserAuth.Checked = true;
            this.chkControlUserAuth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkControlUserAuth.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkControlUserAuth.Location = new System.Drawing.Point(28, 338);
            this.chkControlUserAuth.Margin = new System.Windows.Forms.Padding(4);
            this.chkControlUserAuth.Name = "chkControlUserAuth";
            this.chkControlUserAuth.Size = new System.Drawing.Size(210, 22);
            this.chkControlUserAuth.TabIndex = 14;
            this.chkControlUserAuth.Text = "添加虚拟站点用户权限";
            this.chkControlUserAuth.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(28, 258);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(206, 18);
            this.label4.TabIndex = 13;
            this.label4.Text = "多个端口用英文逗号隔开";
            // 
            // lblFullSiteName
            // 
            this.lblFullSiteName.AutoSize = true;
            this.lblFullSiteName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFullSiteName.ForeColor = System.Drawing.Color.Red;
            this.lblFullSiteName.Location = new System.Drawing.Point(28, 72);
            this.lblFullSiteName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFullSiteName.Name = "lblFullSiteName";
            this.lblFullSiteName.Size = new System.Drawing.Size(161, 18);
            this.lblFullSiteName.TabIndex = 12;
            this.lblFullSiteName.Text = "Default Web Site/";
            // 
            // txtMIMEs
            // 
            this.txtMIMEs.BackColor = System.Drawing.Color.White;
            this.txtMIMEs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMIMEs.Caption = "MIME：";
            this.txtMIMEs.CaptionColor = System.Drawing.SystemColors.ControlText;
            this.txtMIMEs.Enabled = false;
            this.txtMIMEs.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtMIMEs.Location = new System.Drawing.Point(28, 286);
            this.txtMIMEs.Margin = new System.Windows.Forms.Padding(5);
            this.txtMIMEs.Name = "txtMIMEs";
            this.txtMIMEs.PasswordChar = '\0';
            this.txtMIMEs.SelectionStart = 0;
            this.txtMIMEs.Size = new System.Drawing.Size(493, 42);
            this.txtMIMEs.TabIndex = 11;
            this.txtMIMEs.TextBoxType = Control.TextBoxType.txt;
            this.txtMIMEs.Value = "80";
            // 
            // txtSitePhysicPath
            // 
            this.txtSitePhysicPath.BackColor = System.Drawing.Color.White;
            this.txtSitePhysicPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSitePhysicPath.Caption = "站点物理路径：";
            this.txtSitePhysicPath.CaptionColor = System.Drawing.SystemColors.ControlText;
            this.txtSitePhysicPath.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSitePhysicPath.Location = new System.Drawing.Point(28, 105);
            this.txtSitePhysicPath.Margin = new System.Windows.Forms.Padding(5);
            this.txtSitePhysicPath.Name = "txtSitePhysicPath";
            this.txtSitePhysicPath.PasswordChar = '\0';
            this.txtSitePhysicPath.SelectionStart = 0;
            this.txtSitePhysicPath.Size = new System.Drawing.Size(493, 42);
            this.txtSitePhysicPath.TabIndex = 10;
            this.txtSitePhysicPath.TextBoxType = Control.TextBoxType.txtZoom;
            this.txtSitePhysicPath.Value = "80";
            this.txtSitePhysicPath.ZoomClick += new System.EventHandler(this.txtSitePhysicPath_ZoomClick);
            // 
            // txtWebSiteName
            // 
            this.txtWebSiteName.BackColor = System.Drawing.Color.White;
            this.txtWebSiteName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWebSiteName.Caption = "Web服务名称：";
            this.txtWebSiteName.CaptionColor = System.Drawing.SystemColors.ControlText;
            this.txtWebSiteName.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtWebSiteName.Location = new System.Drawing.Point(28, 25);
            this.txtWebSiteName.Margin = new System.Windows.Forms.Padding(5);
            this.txtWebSiteName.Name = "txtWebSiteName";
            this.txtWebSiteName.PasswordChar = '\0';
            this.txtWebSiteName.SelectionStart = 0;
            this.txtWebSiteName.Size = new System.Drawing.Size(493, 42);
            this.txtWebSiteName.TabIndex = 9;
            this.txtWebSiteName.TextBoxType = Control.TextBoxType.txt;
            this.txtWebSiteName.Value = null;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.chkCreateAppPool);
            this.groupBox3.Controls.Add(this.comboBox3);
            this.groupBox3.Controls.Add(this.txtAppPoolName);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.cmbPipeType);
            this.groupBox3.Controls.Add(this.cmbNetFrameworks);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.chkEnable32On64);
            this.groupBox3.Location = new System.Drawing.Point(8, 10);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(561, 192);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "应用程序池";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Enabled = false;
            this.checkBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(164, 26);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(156, 22);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "自动启动程序池";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(536, 19);
            this.comboBox3.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(20, 28);
            this.comboBox3.TabIndex = 9;
            this.comboBox3.Visible = false;
            // 
            // tpIISReg
            // 
            this.tpIISReg.Controls.Add(this.ssStatus);
            this.tpIISReg.Controls.Add(this.gpReRegWcf);
            this.tpIISReg.Controls.Add(this.gpReRegNet);
            this.tpIISReg.Location = new System.Drawing.Point(0, 26);
            this.tpIISReg.Margin = new System.Windows.Forms.Padding(4);
            this.tpIISReg.Name = "tpIISReg";
            this.tpIISReg.Padding = new System.Windows.Forms.Padding(4);
            this.tpIISReg.Size = new System.Drawing.Size(581, 644);
            this.tpIISReg.TabIndex = 3;
            this.tpIISReg.Text = "IIS注册参数";
            this.tpIISReg.UseVisualStyleBackColor = true;
            // 
            // ssStatus
            // 
            this.ssStatus.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ssStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblPublishingStatus,
            this.tslblPublishResult});
            this.ssStatus.Location = new System.Drawing.Point(4, 618);
            this.ssStatus.Name = "ssStatus";
            this.ssStatus.Padding = new System.Windows.Forms.Padding(19, 0, 1, 0);
            this.ssStatus.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ssStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ssStatus.Size = new System.Drawing.Size(573, 22);
            this.ssStatus.TabIndex = 5;
            this.ssStatus.Text = "statusStrip1";
            // 
            // tslblPublishingStatus
            // 
            this.tslblPublishingStatus.Image = ((System.Drawing.Image)(resources.GetObject("tslblPublishingStatus.Image")));
            this.tslblPublishingStatus.Name = "tslblPublishingStatus";
            this.tslblPublishingStatus.Size = new System.Drawing.Size(104, 20);
            this.tslblPublishingStatus.Text = "正在重注册";
            this.tslblPublishingStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tslblPublishingStatus.Visible = false;
            // 
            // tslblPublishResult
            // 
            this.tslblPublishResult.Image = global::TechScan.Tool.U8.ServiceDeployWin.Properties.Resources.success;
            this.tslblPublishResult.Name = "tslblPublishResult";
            this.tslblPublishResult.Size = new System.Drawing.Size(89, 20);
            this.tslblPublishResult.Text = "注册成功";
            this.tslblPublishResult.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tslblPublishResult.Visible = false;
            // 
            // gpReRegWcf
            // 
            this.gpReRegWcf.Controls.Add(this.picLoadingRegWcf);
            this.gpReRegWcf.Controls.Add(this.chkAutoReRegWcf);
            this.gpReRegWcf.Controls.Add(this.btnRegWcf);
            this.gpReRegWcf.Controls.Add(this.txtRegWcf);
            this.gpReRegWcf.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpReRegWcf.Location = new System.Drawing.Point(4, 268);
            this.gpReRegWcf.Margin = new System.Windows.Forms.Padding(4);
            this.gpReRegWcf.Name = "gpReRegWcf";
            this.gpReRegWcf.Padding = new System.Windows.Forms.Padding(4);
            this.gpReRegWcf.Size = new System.Drawing.Size(573, 264);
            this.gpReRegWcf.TabIndex = 4;
            this.gpReRegWcf.TabStop = false;
            this.gpReRegWcf.Text = "重注册WCF";
            this.gpReRegWcf.CollapseBoxClickedEvent += new TechScan.Tool.Controls.CollapsibleGroupBox.CollapseBoxClickedEventHandler(this.gpReRegWcf_CollapseBoxClickedEvent);
            // 
            // picLoadingRegWcf
            // 
            this.picLoadingRegWcf.Image = ((System.Drawing.Image)(resources.GetObject("picLoadingRegWcf.Image")));
            this.picLoadingRegWcf.Location = new System.Drawing.Point(440, 230);
            this.picLoadingRegWcf.Margin = new System.Windows.Forms.Padding(4);
            this.picLoadingRegWcf.Name = "picLoadingRegWcf";
            this.picLoadingRegWcf.Size = new System.Drawing.Size(23, 21);
            this.picLoadingRegWcf.TabIndex = 16;
            this.picLoadingRegWcf.TabStop = false;
            this.picLoadingRegWcf.Visible = false;
            // 
            // chkAutoReRegWcf
            // 
            this.chkAutoReRegWcf.AutoSize = true;
            this.chkAutoReRegWcf.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkAutoReRegWcf.Location = new System.Drawing.Point(11, 229);
            this.chkAutoReRegWcf.Margin = new System.Windows.Forms.Padding(4);
            this.chkAutoReRegWcf.Name = "chkAutoReRegWcf";
            this.chkAutoReRegWcf.Size = new System.Drawing.Size(156, 22);
            this.chkAutoReRegWcf.TabIndex = 15;
            this.chkAutoReRegWcf.Text = "发布时自动注册";
            this.chkAutoReRegWcf.UseVisualStyleBackColor = true;
            // 
            // btnRegWcf
            // 
            this.btnRegWcf.Location = new System.Drawing.Point(465, 226);
            this.btnRegWcf.Margin = new System.Windows.Forms.Padding(4);
            this.btnRegWcf.Name = "btnRegWcf";
            this.btnRegWcf.Size = new System.Drawing.Size(100, 29);
            this.btnRegWcf.TabIndex = 5;
            this.btnRegWcf.Text = "重注册";
            this.btnRegWcf.UseVisualStyleBackColor = true;
            this.btnRegWcf.Click += new System.EventHandler(this.btnRegWcf_Click);
            // 
            // txtRegWcf
            // 
            this.txtRegWcf.BackColor = System.Drawing.Color.White;
            this.txtRegWcf.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegWcf.Enabled = false;
            this.txtRegWcf.Location = new System.Drawing.Point(8, 22);
            this.txtRegWcf.Margin = new System.Windows.Forms.Padding(4);
            this.txtRegWcf.Multiline = true;
            this.txtRegWcf.Name = "txtRegWcf";
            this.txtRegWcf.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRegWcf.Size = new System.Drawing.Size(557, 196);
            this.txtRegWcf.TabIndex = 13;
            this.txtRegWcf.Text = "1）解决采购入库外币金额错误问题；\r\n2）解决盘点单异常退出问题；\r\n3）销售出库单序列号无法保存的问题；";
            // 
            // gpReRegNet
            // 
            this.gpReRegNet.Controls.Add(this.picLoadingRegNet);
            this.gpReRegNet.Controls.Add(this.chkAutoReRegNet);
            this.gpReRegNet.Controls.Add(this.btnRegNet);
            this.gpReRegNet.Controls.Add(this.txtRegNet);
            this.gpReRegNet.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpReRegNet.Location = new System.Drawing.Point(4, 4);
            this.gpReRegNet.Margin = new System.Windows.Forms.Padding(4);
            this.gpReRegNet.Name = "gpReRegNet";
            this.gpReRegNet.Padding = new System.Windows.Forms.Padding(4);
            this.gpReRegNet.Size = new System.Drawing.Size(573, 264);
            this.gpReRegNet.TabIndex = 3;
            this.gpReRegNet.TabStop = false;
            this.gpReRegNet.Text = "重注册.NET";
            this.gpReRegNet.CollapseBoxClickedEvent += new TechScan.Tool.Controls.CollapsibleGroupBox.CollapseBoxClickedEventHandler(this.gpReRegNet_CollapseBoxClickedEvent);
            // 
            // picLoadingRegNet
            // 
            this.picLoadingRegNet.Image = ((System.Drawing.Image)(resources.GetObject("picLoadingRegNet.Image")));
            this.picLoadingRegNet.Location = new System.Drawing.Point(440, 231);
            this.picLoadingRegNet.Margin = new System.Windows.Forms.Padding(4);
            this.picLoadingRegNet.Name = "picLoadingRegNet";
            this.picLoadingRegNet.Size = new System.Drawing.Size(23, 21);
            this.picLoadingRegNet.TabIndex = 15;
            this.picLoadingRegNet.TabStop = false;
            this.picLoadingRegNet.Visible = false;
            // 
            // chkAutoReRegNet
            // 
            this.chkAutoReRegNet.AutoSize = true;
            this.chkAutoReRegNet.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkAutoReRegNet.Location = new System.Drawing.Point(11, 229);
            this.chkAutoReRegNet.Margin = new System.Windows.Forms.Padding(4);
            this.chkAutoReRegNet.Name = "chkAutoReRegNet";
            this.chkAutoReRegNet.Size = new System.Drawing.Size(156, 22);
            this.chkAutoReRegNet.TabIndex = 14;
            this.chkAutoReRegNet.Text = "发布时自动注册";
            this.chkAutoReRegNet.UseVisualStyleBackColor = true;
            // 
            // btnRegNet
            // 
            this.btnRegNet.Location = new System.Drawing.Point(465, 228);
            this.btnRegNet.Margin = new System.Windows.Forms.Padding(4);
            this.btnRegNet.Name = "btnRegNet";
            this.btnRegNet.Size = new System.Drawing.Size(100, 29);
            this.btnRegNet.TabIndex = 5;
            this.btnRegNet.Text = "重注册";
            this.btnRegNet.UseVisualStyleBackColor = true;
            this.btnRegNet.Click += new System.EventHandler(this.btnRegNet_Click);
            // 
            // txtRegNet
            // 
            this.txtRegNet.BackColor = System.Drawing.Color.White;
            this.txtRegNet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegNet.Enabled = false;
            this.txtRegNet.Location = new System.Drawing.Point(8, 22);
            this.txtRegNet.Margin = new System.Windows.Forms.Padding(4);
            this.txtRegNet.Multiline = true;
            this.txtRegNet.Name = "txtRegNet";
            this.txtRegNet.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRegNet.Size = new System.Drawing.Size(557, 196);
            this.txtRegNet.TabIndex = 13;
            this.txtRegNet.Text = "1）解决采购入库外币金额错误问题；\r\n2）解决盘点单异常退出问题；\r\n3）销售出库单序列号无法保存的问题；";
            // 
            // tpSQL
            // 
            this.tpSQL.Controls.Add(this.txtDbConnectionTimeout);
            this.tpSQL.ImageIndex = 0;
            this.tpSQL.Location = new System.Drawing.Point(0, 26);
            this.tpSQL.Margin = new System.Windows.Forms.Padding(4);
            this.tpSQL.Name = "tpSQL";
            this.tpSQL.Padding = new System.Windows.Forms.Padding(4);
            this.tpSQL.Size = new System.Drawing.Size(581, 644);
            this.tpSQL.TabIndex = 1;
            this.tpSQL.Text = "SQL";
            this.tpSQL.UseVisualStyleBackColor = true;
            // 
            // txtDbConnectionTimeout
            // 
            this.txtDbConnectionTimeout.BackColor = System.Drawing.Color.White;
            this.txtDbConnectionTimeout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDbConnectionTimeout.Caption = "数据库连接超时（秒）：";
            this.txtDbConnectionTimeout.CaptionColor = System.Drawing.SystemColors.ControlText;
            this.txtDbConnectionTimeout.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDbConnectionTimeout.Location = new System.Drawing.Point(17, 20);
            this.txtDbConnectionTimeout.Margin = new System.Windows.Forms.Padding(5);
            this.txtDbConnectionTimeout.Name = "txtDbConnectionTimeout";
            this.txtDbConnectionTimeout.PasswordChar = '\0';
            this.txtDbConnectionTimeout.SelectionStart = 0;
            this.txtDbConnectionTimeout.Size = new System.Drawing.Size(493, 42);
            this.txtDbConnectionTimeout.TabIndex = 11;
            this.txtDbConnectionTimeout.TextBoxType = Control.TextBoxType.txt;
            this.txtDbConnectionTimeout.Value = "80";
            // 
            // tpSys
            // 
            this.tpSys.Controls.Add(this.groupBox2);
            this.tpSys.Controls.Add(this.groupBox1);
            this.tpSys.ImageIndex = 1;
            this.tpSys.Location = new System.Drawing.Point(0, 26);
            this.tpSys.Margin = new System.Windows.Forms.Padding(4);
            this.tpSys.Name = "tpSys";
            this.tpSys.Padding = new System.Windows.Forms.Padding(4);
            this.tpSys.Size = new System.Drawing.Size(581, 644);
            this.tpSys.TabIndex = 2;
            this.tpSys.Text = "系统";
            this.tpSys.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Image = ((System.Drawing.Image)(resources.GetObject("groupBox2.Image")));
            this.groupBox2.LineColorBottom = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(57, 49);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(137, 109);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SQL";
            this.groupBox2.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Image = ((System.Drawing.Image)(resources.GetObject("groupBox1.Image")));
            this.groupBox1.LineColorBottom = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(239, 70);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(107, 69);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "系统";
            this.groupBox1.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "SQL32.png");
            this.imageList1.Images.SetKeyName(1, "系统.png");
            this.imageList1.Images.SetKeyName(2, "IIS.png");
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(152)))), ((int)(((byte)(177)))));
            this.ClientSize = new System.Drawing.Size(581, 670);
            this.Controls.Add(this.tcSetting);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统设置";
            this.tcSetting.ResumeLayout(false);
            this.tpIISPublish.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tpIISReg.ResumeLayout(false);
            this.tpIISReg.PerformLayout();
            this.ssStatus.ResumeLayout(false);
            this.ssStatus.PerformLayout();
            this.gpReRegWcf.ResumeLayout(false);
            this.gpReRegWcf.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoadingRegWcf)).EndInit();
            this.gpReRegNet.ResumeLayout(false);
            this.gpReRegNet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoadingRegNet)).EndInit();
            this.tpSQL.ResumeLayout(false);
            this.tpSys.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.GroupBox groupBox1;
        private Controls.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkCreateAppPool;
        private Control.MyTextBox txtAppPoolName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbNetFrameworks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkEnable32On64;
        private System.Windows.Forms.ComboBox cmbPipeType;
        private Control.MyTextBox txtPorts;
        private Control.MyTextBox txtBackUpPath;
        private LayeredSkin.Controls.LayeredTabControl tcSetting;
        private System.Windows.Forms.TabPage tpIISPublish;
        private System.Windows.Forms.TabPage tpSQL;
        private System.Windows.Forms.TabPage tpSys;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox checkBox1;
        private Control.MyTextBox txtWebSiteName;
        private Control.MyTextBox txtSitePhysicPath;
        private Control.MyTextBox txtMIMEs;
        private System.Windows.Forms.Label lblFullSiteName;
        private System.Windows.Forms.Label label4;
        private Control.MyTextBox txtDbConnectionTimeout;
        private Control.MyTextBox txtUsersToControlAuth;
        private System.Windows.Forms.CheckBox chkControlUserAuth;
        private System.Windows.Forms.TabPage tpIISReg;
        private Controls.CollapsibleGroupBox gpReRegNet;
        private System.Windows.Forms.TextBox txtRegNet;
        private System.Windows.Forms.Button btnRegNet;
        private Controls.CollapsibleGroupBox gpReRegWcf;
        private System.Windows.Forms.Button btnRegWcf;
        private System.Windows.Forms.TextBox txtRegWcf;
        private System.Windows.Forms.CheckBox chkAutoReRegWcf;
        private System.Windows.Forms.CheckBox chkAutoReRegNet;
        private System.Windows.Forms.PictureBox picLoadingRegNet;
        private System.Windows.Forms.PictureBox picLoadingRegWcf;
        private System.Windows.Forms.StatusStrip ssStatus;
        private System.Windows.Forms.ToolStripStatusLabel tslblPublishingStatus;
        private System.Windows.Forms.ToolStripStatusLabel tslblPublishResult;
    }
}