namespace TechScan.Tool.U8.ServiceDeployWin.Deploy
{
    partial class frmIISDeploy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIISDeploy));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.layeredTabControl1 = new LayeredSkin.Controls.LayeredTabControl();
            this.tpServerInfo = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.lblProcesserCounter = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblWorkSet = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCurDir = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblMachineName = new System.Windows.Forms.Label();
            this.lblProcessType = new System.Windows.Forms.Label();
            this.lblOsVersion = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSystemType = new System.Windows.Forms.Label();
            this.lblPlatform = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSystemDir = new System.Windows.Forms.Label();
            this.pnlMainPublish = new LayeredSkin.Controls.LayeredPanel();
            this.picDeployLoading = new TechScan.Tool.Controls.SpinningCircles();
            this.layeredLabel1 = new LayeredSkin.Controls.LayeredLabel();
            this.layeredLabel3 = new LayeredSkin.Controls.LayeredLabel();
            this.lblSelectedPackagePath = new LayeredSkin.Controls.LayeredLabel();
            this.btnPublish = new TechScan.Tool.Controls.ButtonEx();
            this.tsTools = new System.Windows.Forms.ToolStrip();
            this.tsbSet = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbRollBack = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHelp = new System.Windows.Forms.ToolStripButton();
            this.lblHintShow = new LayeredSkin.Controls.LayeredLabel();
            this.btnAddPackage = new TechScan.Tool.Controls.ButtonEx();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.layeredTabControl2 = new LayeredSkin.Controls.LayeredTabControl();
            this.tpPackageInfo = new System.Windows.Forms.TabPage();
            this.cmbPkgType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblPKInfo_PublishDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPKInfo_PublishVersion = new System.Windows.Forms.Label();
            this.lblPKInfo_PublishDateTime = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rich_iis_log = new TechScan.Tool.Controls.ExRichTextBox();
            this.CMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsExport = new System.Windows.Forms.ToolStripMenuItem();
            this.ssStatus = new System.Windows.Forms.StatusStrip();
            this.tspbProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tslblPublishingStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblPublishResult = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tmProgress = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.layeredTabControl1.SuspendLayout();
            this.tpServerInfo.SuspendLayout();
            this.pnlMainPublish.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPublish)).BeginInit();
            this.tsTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddPackage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.layeredTabControl2.SuspendLayout();
            this.tpPackageInfo.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.CMenu.SuspendLayout();
            this.ssStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(969, 633);
            this.splitContainer1.SplitterDistance = 406;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.layeredTabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pnlMainPublish);
            this.splitContainer2.Size = new System.Drawing.Size(406, 633);
            this.splitContainer2.SplitterDistance = 200;
            this.splitContainer2.TabIndex = 0;
            // 
            // layeredTabControl1
            // 
            this.layeredTabControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.layeredTabControl1.Borders.BottomColor = System.Drawing.Color.Empty;
            this.layeredTabControl1.Borders.BottomWidth = 1;
            this.layeredTabControl1.Borders.LeftColor = System.Drawing.Color.Empty;
            this.layeredTabControl1.Borders.LeftWidth = 1;
            this.layeredTabControl1.Borders.RightColor = System.Drawing.Color.Empty;
            this.layeredTabControl1.Borders.RightWidth = 1;
            this.layeredTabControl1.Borders.TopColor = System.Drawing.Color.Empty;
            this.layeredTabControl1.Borders.TopWidth = 1;
            this.layeredTabControl1.Canvas = ((System.Drawing.Bitmap)(resources.GetObject("layeredTabControl1.Canvas")));
            this.layeredTabControl1.Controls.Add(this.tpServerInfo);
            this.layeredTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layeredTabControl1.HoverBackColors = new System.Drawing.Color[] {
        System.Drawing.Color.Transparent,
        System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))))};
            this.layeredTabControl1.ItemBackgroundImage = null;
            this.layeredTabControl1.ItemBackgroundImageHover = null;
            this.layeredTabControl1.ItemBackgroundImageSelected = null;
            this.layeredTabControl1.Location = new System.Drawing.Point(0, 0);
            this.layeredTabControl1.Name = "layeredTabControl1";
            this.layeredTabControl1.NormalBackColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))))};
            this.layeredTabControl1.SelectedBackColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))))};
            this.layeredTabControl1.SelectedIndex = 0;
            this.layeredTabControl1.Size = new System.Drawing.Size(406, 200);
            this.layeredTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.layeredTabControl1.TabIndex = 1;
            this.layeredTabControl1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // tpServerInfo
            // 
            this.tpServerInfo.Controls.Add(this.label13);
            this.tpServerInfo.Controls.Add(this.lblProcesserCounter);
            this.tpServerInfo.Controls.Add(this.label17);
            this.tpServerInfo.Controls.Add(this.lblUserName);
            this.tpServerInfo.Controls.Add(this.lblWorkSet);
            this.tpServerInfo.Controls.Add(this.label3);
            this.tpServerInfo.Controls.Add(this.lblCurDir);
            this.tpServerInfo.Controls.Add(this.label2);
            this.tpServerInfo.Controls.Add(this.label14);
            this.tpServerInfo.Controls.Add(this.lblMachineName);
            this.tpServerInfo.Controls.Add(this.lblProcessType);
            this.tpServerInfo.Controls.Add(this.lblOsVersion);
            this.tpServerInfo.Controls.Add(this.label12);
            this.tpServerInfo.Controls.Add(this.label1);
            this.tpServerInfo.Controls.Add(this.lblSystemType);
            this.tpServerInfo.Controls.Add(this.lblPlatform);
            this.tpServerInfo.Controls.Add(this.label11);
            this.tpServerInfo.Controls.Add(this.label9);
            this.tpServerInfo.Controls.Add(this.label6);
            this.tpServerInfo.Controls.Add(this.lblSystemDir);
            this.tpServerInfo.Location = new System.Drawing.Point(0, 22);
            this.tpServerInfo.Name = "tpServerInfo";
            this.tpServerInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpServerInfo.Size = new System.Drawing.Size(406, 178);
            this.tpServerInfo.TabIndex = 0;
            this.tpServerInfo.Text = "服务器信息";
            this.tpServerInfo.ToolTipText = "服务器信息";
            this.tpServerInfo.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(196, 57);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 12);
            this.label13.TabIndex = 20;
            this.label13.Text = "CPU数：";
            // 
            // lblProcesserCounter
            // 
            this.lblProcesserCounter.AutoSize = true;
            this.lblProcesserCounter.Location = new System.Drawing.Point(260, 57);
            this.lblProcesserCounter.Name = "lblProcesserCounter";
            this.lblProcesserCounter.Size = new System.Drawing.Size(47, 12);
            this.lblProcesserCounter.TabIndex = 21;
            this.lblProcesserCounter.Text = "CPU数：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(196, 33);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(41, 12);
            this.label17.TabIndex = 18;
            this.label17.Text = "内存：";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(88, 33);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(53, 12);
            this.lblUserName.TabIndex = 5;
            this.lblUserName.Text = "用户名：";
            // 
            // lblWorkSet
            // 
            this.lblWorkSet.AutoSize = true;
            this.lblWorkSet.Location = new System.Drawing.Point(260, 33);
            this.lblWorkSet.Name = "lblWorkSet";
            this.lblWorkSet.Size = new System.Drawing.Size(41, 12);
            this.lblWorkSet.TabIndex = 19;
            this.lblWorkSet.Text = "内存：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "用户名：";
            // 
            // lblCurDir
            // 
            this.lblCurDir.AutoSize = true;
            this.lblCurDir.Location = new System.Drawing.Point(88, 129);
            this.lblCurDir.Name = "lblCurDir";
            this.lblCurDir.Size = new System.Drawing.Size(65, 12);
            this.lblCurDir.TabIndex = 16;
            this.lblCurDir.Text = "当前目录：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "操作系统：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 129);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 17;
            this.label14.Text = "当前目录：";
            // 
            // lblMachineName
            // 
            this.lblMachineName.AutoSize = true;
            this.lblMachineName.Location = new System.Drawing.Point(88, 9);
            this.lblMachineName.Name = "lblMachineName";
            this.lblMachineName.Size = new System.Drawing.Size(53, 12);
            this.lblMachineName.TabIndex = 3;
            this.lblMachineName.Text = "机器名：";
            // 
            // lblProcessType
            // 
            this.lblProcessType.AutoSize = true;
            this.lblProcessType.Location = new System.Drawing.Point(88, 105);
            this.lblProcessType.Name = "lblProcessType";
            this.lblProcessType.Size = new System.Drawing.Size(65, 12);
            this.lblProcessType.TabIndex = 12;
            this.lblProcessType.Text = "进程类型：";
            // 
            // lblOsVersion
            // 
            this.lblOsVersion.AutoSize = true;
            this.lblOsVersion.Location = new System.Drawing.Point(260, 9);
            this.lblOsVersion.Name = "lblOsVersion";
            this.lblOsVersion.Size = new System.Drawing.Size(65, 12);
            this.lblOsVersion.TabIndex = 6;
            this.lblOsVersion.Text = "操作系统：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 105);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 13;
            this.label12.Text = "进程类型：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "机器名：";
            // 
            // lblSystemType
            // 
            this.lblSystemType.AutoSize = true;
            this.lblSystemType.Location = new System.Drawing.Point(260, 81);
            this.lblSystemType.Name = "lblSystemType";
            this.lblSystemType.Size = new System.Drawing.Size(65, 12);
            this.lblSystemType.TabIndex = 14;
            this.lblSystemType.Text = "系统类型：";
            // 
            // lblPlatform
            // 
            this.lblPlatform.AutoSize = true;
            this.lblPlatform.Location = new System.Drawing.Point(88, 81);
            this.lblPlatform.Name = "lblPlatform";
            this.lblPlatform.Size = new System.Drawing.Size(65, 12);
            this.lblPlatform.TabIndex = 11;
            this.lblPlatform.Text = "平台标识：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(196, 81);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 15;
            this.label11.Text = "系统类型：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 10;
            this.label9.Text = "平台标识：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "系统目录：";
            // 
            // lblSystemDir
            // 
            this.lblSystemDir.AutoSize = true;
            this.lblSystemDir.Location = new System.Drawing.Point(88, 57);
            this.lblSystemDir.Name = "lblSystemDir";
            this.lblSystemDir.Size = new System.Drawing.Size(65, 12);
            this.lblSystemDir.TabIndex = 9;
            this.lblSystemDir.Text = "系统目录：";
            // 
            // pnlMainPublish
            // 
            this.pnlMainPublish.AllowDrop = true;
            this.pnlMainPublish.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnlMainPublish.Borders.BottomColor = System.Drawing.Color.Empty;
            this.pnlMainPublish.Borders.BottomWidth = 1;
            this.pnlMainPublish.Borders.LeftColor = System.Drawing.Color.Empty;
            this.pnlMainPublish.Borders.LeftWidth = 1;
            this.pnlMainPublish.Borders.RightColor = System.Drawing.Color.Empty;
            this.pnlMainPublish.Borders.RightWidth = 1;
            this.pnlMainPublish.Borders.TopColor = System.Drawing.Color.Empty;
            this.pnlMainPublish.Borders.TopWidth = 1;
            this.pnlMainPublish.Canvas = ((System.Drawing.Bitmap)(resources.GetObject("pnlMainPublish.Canvas")));
            this.pnlMainPublish.Controls.Add(this.picDeployLoading);
            this.pnlMainPublish.Controls.Add(this.layeredLabel1);
            this.pnlMainPublish.Controls.Add(this.layeredLabel3);
            this.pnlMainPublish.Controls.Add(this.lblSelectedPackagePath);
            this.pnlMainPublish.Controls.Add(this.btnPublish);
            this.pnlMainPublish.Controls.Add(this.tsTools);
            this.pnlMainPublish.Controls.Add(this.lblHintShow);
            this.pnlMainPublish.Controls.Add(this.btnAddPackage);
            this.pnlMainPublish.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainPublish.Location = new System.Drawing.Point(0, 0);
            this.pnlMainPublish.Name = "pnlMainPublish";
            this.pnlMainPublish.Size = new System.Drawing.Size(406, 429);
            this.pnlMainPublish.TabIndex = 0;
            // 
            // picDeployLoading
            // 
            this.picDeployLoading.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picDeployLoading.BackColor = System.Drawing.Color.Transparent;
            this.picDeployLoading.FullTransparent = true;
            this.picDeployLoading.Increment = 1F;
            this.picDeployLoading.Location = new System.Drawing.Point(139, 135);
            this.picDeployLoading.N = 8;
            this.picDeployLoading.Name = "picDeployLoading";
            this.picDeployLoading.Radius = 2.5F;
            this.picDeployLoading.Size = new System.Drawing.Size(137, 157);
            this.picDeployLoading.TabIndex = 6;
            this.picDeployLoading.Text = "DeployLoading";
            // 
            // layeredLabel1
            // 
            this.layeredLabel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.layeredLabel1.Borders.BottomColor = System.Drawing.Color.Empty;
            this.layeredLabel1.Borders.BottomWidth = 1;
            this.layeredLabel1.Borders.LeftColor = System.Drawing.Color.Empty;
            this.layeredLabel1.Borders.LeftWidth = 1;
            this.layeredLabel1.Borders.RightColor = System.Drawing.Color.Empty;
            this.layeredLabel1.Borders.RightWidth = 1;
            this.layeredLabel1.Borders.TopColor = System.Drawing.Color.Empty;
            this.layeredLabel1.Borders.TopWidth = 1;
            this.layeredLabel1.Canvas = ((System.Drawing.Bitmap)(resources.GetObject("layeredLabel1.Canvas")));
            this.layeredLabel1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.layeredLabel1.HaloSize = 5;
            this.layeredLabel1.Location = new System.Drawing.Point(237, 189);
            this.layeredLabel1.Name = "layeredLabel1";
            this.layeredLabel1.Size = new System.Drawing.Size(73, 21);
            this.layeredLabel1.TabIndex = 9;
            this.layeredLabel1.Text = "服务发布";
            // 
            // layeredLabel3
            // 
            this.layeredLabel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.layeredLabel3.Borders.BottomColor = System.Drawing.Color.Empty;
            this.layeredLabel3.Borders.BottomWidth = 1;
            this.layeredLabel3.Borders.LeftColor = System.Drawing.Color.Empty;
            this.layeredLabel3.Borders.LeftWidth = 1;
            this.layeredLabel3.Borders.RightColor = System.Drawing.Color.Empty;
            this.layeredLabel3.Borders.RightWidth = 1;
            this.layeredLabel3.Borders.TopColor = System.Drawing.Color.Empty;
            this.layeredLabel3.Borders.TopWidth = 1;
            this.layeredLabel3.Canvas = ((System.Drawing.Bitmap)(resources.GetObject("layeredLabel3.Canvas")));
            this.layeredLabel3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.layeredLabel3.HaloSize = 5;
            this.layeredLabel3.Location = new System.Drawing.Point(90, 189);
            this.layeredLabel3.Name = "layeredLabel3";
            this.layeredLabel3.Size = new System.Drawing.Size(90, 21);
            this.layeredLabel3.TabIndex = 8;
            this.layeredLabel3.Text = "选择安装包";
            // 
            // lblSelectedPackagePath
            // 
            this.lblSelectedPackagePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSelectedPackagePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.lblSelectedPackagePath.Borders.BottomColor = System.Drawing.Color.Empty;
            this.lblSelectedPackagePath.Borders.BottomWidth = 1;
            this.lblSelectedPackagePath.Borders.LeftColor = System.Drawing.Color.Empty;
            this.lblSelectedPackagePath.Borders.LeftWidth = 1;
            this.lblSelectedPackagePath.Borders.RightColor = System.Drawing.Color.Empty;
            this.lblSelectedPackagePath.Borders.RightWidth = 1;
            this.lblSelectedPackagePath.Borders.TopColor = System.Drawing.Color.Empty;
            this.lblSelectedPackagePath.Borders.TopWidth = 1;
            this.lblSelectedPackagePath.Canvas = ((System.Drawing.Bitmap)(resources.GetObject("lblSelectedPackagePath.Canvas")));
            this.lblSelectedPackagePath.HaloSize = 5;
            this.lblSelectedPackagePath.Location = new System.Drawing.Point(3, 290);
            this.lblSelectedPackagePath.Name = "lblSelectedPackagePath";
            this.lblSelectedPackagePath.Size = new System.Drawing.Size(400, 67);
            this.lblSelectedPackagePath.TabIndex = 5;
            this.lblSelectedPackagePath.Text = "D:\\项目\\太迅\\U8ServiceDeploy\\DemoEnvironment\\IIS_Publish_20191226192521.zip";
            this.lblSelectedPackagePath.Visible = false;
            // 
            // btnPublish
            // 
            this.btnPublish.BackColor = System.Drawing.Color.Transparent;
            this.btnPublish.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnPublish.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPublish.Enabled = false;
            this.btnPublish.Image = ((System.Drawing.Image)(resources.GetObject("btnPublish.Image")));
            this.btnPublish.ImageClick = ((System.Drawing.Image)(resources.GetObject("btnPublish.ImageClick")));
            this.btnPublish.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnPublish.ImageHover")));
            this.btnPublish.ImageNormal = ((System.Drawing.Image)(resources.GetObject("btnPublish.ImageNormal")));
            this.btnPublish.IsSwitch = false;
            this.btnPublish.Location = new System.Drawing.Point(235, 107);
            this.btnPublish.Name = "btnPublish";
            this.btnPublish.Selected = false;
            this.btnPublish.Size = new System.Drawing.Size(76, 76);
            this.btnPublish.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnPublish.TabIndex = 4;
            this.btnPublish.TabStop = false;
            this.toolTip1.SetToolTip(this.btnPublish, "单击发布U8Android Web服务");
            this.btnPublish.Click += new System.EventHandler(this.OnPublishClickAsync);
            // 
            // tsTools
            // 
            this.tsTools.GripMargin = new System.Windows.Forms.Padding(3);
            this.tsTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSet,
            this.tsbRollBack,
            this.toolStripSeparator1,
            this.tsbHelp});
            this.tsTools.Location = new System.Drawing.Point(0, 0);
            this.tsTools.Name = "tsTools";
            this.tsTools.Size = new System.Drawing.Size(406, 25);
            this.tsTools.TabIndex = 3;
            this.tsTools.Text = "toolStrip1";
            // 
            // tsbSet
            // 
            this.tsbSet.Image = ((System.Drawing.Image)(resources.GetObject("tsbSet.Image")));
            this.tsbSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSet.Name = "tsbSet";
            this.tsbSet.Size = new System.Drawing.Size(100, 22);
            this.tsbSet.Text = "参数配置(&E)";
            this.tsbSet.ToolTipText = "参数配置";
            this.tsbSet.Click += new System.EventHandler(this.tsbSet_Click);
            // 
            // tsbRollBack
            // 
            this.tsbRollBack.Image = global::TechScan.Tool.U8.ServiceDeployWin.Properties.Resources.History2;
            this.tsbRollBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRollBack.Name = "tsbRollBack";
            this.tsbRollBack.Size = new System.Drawing.Size(92, 22);
            this.tsbRollBack.Text = "版本回溯(&R)";
            this.tsbRollBack.ToolTipText = "版本回溯";
            this.tsbRollBack.Click += new System.EventHandler(this.tsbRollBack_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbHelp
            // 
            this.tsbHelp.Image = ((System.Drawing.Image)(resources.GetObject("tsbHelp.Image")));
            this.tsbHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHelp.Name = "tsbHelp";
            this.tsbHelp.Size = new System.Drawing.Size(69, 22);
            this.tsbHelp.Text = "帮助(&H)";
            this.tsbHelp.ToolTipText = "帮助";
            this.tsbHelp.Click += new System.EventHandler(this.tsbHelp_Click);
            // 
            // lblHintShow
            // 
            this.lblHintShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.lblHintShow.Borders.BottomColor = System.Drawing.Color.Empty;
            this.lblHintShow.Borders.BottomWidth = 1;
            this.lblHintShow.Borders.LeftColor = System.Drawing.Color.Empty;
            this.lblHintShow.Borders.LeftWidth = 1;
            this.lblHintShow.Borders.RightColor = System.Drawing.Color.Empty;
            this.lblHintShow.Borders.RightWidth = 1;
            this.lblHintShow.Borders.TopColor = System.Drawing.Color.Empty;
            this.lblHintShow.Borders.TopWidth = 1;
            this.lblHintShow.Canvas = ((System.Drawing.Bitmap)(resources.GetObject("lblHintShow.Canvas")));
            this.lblHintShow.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblHintShow.HaloSize = 5;
            this.lblHintShow.Location = new System.Drawing.Point(122, 241);
            this.lblHintShow.Name = "lblHintShow";
            this.lblHintShow.Size = new System.Drawing.Size(163, 17);
            this.lblHintShow.TabIndex = 2;
            this.lblHintShow.Text = "拖动安装包文件或者单击选择";
            // 
            // btnAddPackage
            // 
            this.btnAddPackage.BackColor = System.Drawing.Color.Transparent;
            this.btnAddPackage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnAddPackage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddPackage.Image = ((System.Drawing.Image)(resources.GetObject("btnAddPackage.Image")));
            this.btnAddPackage.ImageClick = ((System.Drawing.Image)(resources.GetObject("btnAddPackage.ImageClick")));
            this.btnAddPackage.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnAddPackage.ImageHover")));
            this.btnAddPackage.ImageNormal = ((System.Drawing.Image)(resources.GetObject("btnAddPackage.ImageNormal")));
            this.btnAddPackage.IsSwitch = false;
            this.btnAddPackage.Location = new System.Drawing.Point(95, 107);
            this.btnAddPackage.Name = "btnAddPackage";
            this.btnAddPackage.Selected = false;
            this.btnAddPackage.Size = new System.Drawing.Size(76, 76);
            this.btnAddPackage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnAddPackage.TabIndex = 1;
            this.btnAddPackage.TabStop = false;
            this.toolTip1.SetToolTip(this.btnAddPackage, "单击选择U8Android Web服务包");
            this.btnAddPackage.Click += new System.EventHandler(this.btnAddPackage_Click);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.layeredTabControl2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer3.Size = new System.Drawing.Size(559, 633);
            this.splitContainer3.SplitterDistance = 201;
            this.splitContainer3.TabIndex = 1;
            // 
            // layeredTabControl2
            // 
            this.layeredTabControl2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.layeredTabControl2.Borders.BottomColor = System.Drawing.Color.Empty;
            this.layeredTabControl2.Borders.BottomWidth = 1;
            this.layeredTabControl2.Borders.LeftColor = System.Drawing.Color.Empty;
            this.layeredTabControl2.Borders.LeftWidth = 1;
            this.layeredTabControl2.Borders.RightColor = System.Drawing.Color.Empty;
            this.layeredTabControl2.Borders.RightWidth = 1;
            this.layeredTabControl2.Borders.TopColor = System.Drawing.Color.Empty;
            this.layeredTabControl2.Borders.TopWidth = 1;
            this.layeredTabControl2.Canvas = ((System.Drawing.Bitmap)(resources.GetObject("layeredTabControl2.Canvas")));
            this.layeredTabControl2.Controls.Add(this.tpPackageInfo);
            this.layeredTabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layeredTabControl2.HoverBackColors = new System.Drawing.Color[] {
        System.Drawing.Color.Transparent,
        System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))))};
            this.layeredTabControl2.ItemBackgroundImage = null;
            this.layeredTabControl2.ItemBackgroundImageHover = null;
            this.layeredTabControl2.ItemBackgroundImageSelected = null;
            this.layeredTabControl2.Location = new System.Drawing.Point(0, 0);
            this.layeredTabControl2.Name = "layeredTabControl2";
            this.layeredTabControl2.NormalBackColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))))};
            this.layeredTabControl2.SelectedBackColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))))};
            this.layeredTabControl2.SelectedIndex = 0;
            this.layeredTabControl2.Size = new System.Drawing.Size(559, 201);
            this.layeredTabControl2.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.layeredTabControl2.TabIndex = 2;
            this.layeredTabControl2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // tpPackageInfo
            // 
            this.tpPackageInfo.Controls.Add(this.cmbPkgType);
            this.tpPackageInfo.Controls.Add(this.label5);
            this.tpPackageInfo.Controls.Add(this.label10);
            this.tpPackageInfo.Controls.Add(this.lblPKInfo_PublishDescription);
            this.tpPackageInfo.Controls.Add(this.label4);
            this.tpPackageInfo.Controls.Add(this.lblPKInfo_PublishVersion);
            this.tpPackageInfo.Controls.Add(this.lblPKInfo_PublishDateTime);
            this.tpPackageInfo.Controls.Add(this.label8);
            this.tpPackageInfo.Location = new System.Drawing.Point(0, 22);
            this.tpPackageInfo.Name = "tpPackageInfo";
            this.tpPackageInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpPackageInfo.Size = new System.Drawing.Size(559, 179);
            this.tpPackageInfo.TabIndex = 0;
            this.tpPackageInfo.Text = "部署包信息";
            this.tpPackageInfo.ToolTipText = "部署包信息";
            this.tpPackageInfo.UseVisualStyleBackColor = true;
            // 
            // cmbPkgType
            // 
            this.cmbPkgType.BackColor = System.Drawing.Color.White;
            this.cmbPkgType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPkgType.Enabled = false;
            this.cmbPkgType.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPkgType.FormattingEnabled = true;
            this.cmbPkgType.Location = new System.Drawing.Point(98, 27);
            this.cmbPkgType.Name = "cmbPkgType";
            this.cmbPkgType.Size = new System.Drawing.Size(145, 22);
            this.cmbPkgType.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "包类型：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 53);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 13;
            this.label10.Text = "版本描述：";
            // 
            // lblPKInfo_PublishDescription
            // 
            this.lblPKInfo_PublishDescription.BackColor = System.Drawing.Color.White;
            this.lblPKInfo_PublishDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPKInfo_PublishDescription.Location = new System.Drawing.Point(98, 55);
            this.lblPKInfo_PublishDescription.Multiline = true;
            this.lblPKInfo_PublishDescription.Name = "lblPKInfo_PublishDescription";
            this.lblPKInfo_PublishDescription.ReadOnly = true;
            this.lblPKInfo_PublishDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.lblPKInfo_PublishDescription.Size = new System.Drawing.Size(455, 118);
            this.lblPKInfo_PublishDescription.TabIndex = 12;
            this.lblPKInfo_PublishDescription.Text = "1）解决采购入库外币金额错误问题；\r\n2）解决盘点单异常退出问题；\r\n3）销售出库单序列号无法保存的问题；";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(275, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "时间：";
            // 
            // lblPKInfo_PublishVersion
            // 
            this.lblPKInfo_PublishVersion.AutoSize = true;
            this.lblPKInfo_PublishVersion.Location = new System.Drawing.Point(98, 9);
            this.lblPKInfo_PublishVersion.Name = "lblPKInfo_PublishVersion";
            this.lblPKInfo_PublishVersion.Size = new System.Drawing.Size(89, 12);
            this.lblPKInfo_PublishVersion.TabIndex = 9;
            this.lblPKInfo_PublishVersion.Text = "V1.0.191228.01";
            // 
            // lblPKInfo_PublishDateTime
            // 
            this.lblPKInfo_PublishDateTime.AutoSize = true;
            this.lblPKInfo_PublishDateTime.Location = new System.Drawing.Point(337, 9);
            this.lblPKInfo_PublishDateTime.Name = "lblPKInfo_PublishDateTime";
            this.lblPKInfo_PublishDateTime.Size = new System.Drawing.Size(119, 12);
            this.lblPKInfo_PublishDateTime.TabIndex = 10;
            this.lblPKInfo_PublishDateTime.Text = "2019-12-28 20:36:12";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "版本号：";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(559, 428);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rich_iis_log);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(551, 402);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "日志";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rich_iis_log
            // 
            this.rich_iis_log.ContextMenuStrip = this.CMenu;
            this.rich_iis_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rich_iis_log.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rich_iis_log.HiglightColor = TechScan.Tool.Controls.RtfColor.White;
            this.rich_iis_log.Location = new System.Drawing.Point(3, 3);
            this.rich_iis_log.Name = "rich_iis_log";
            this.rich_iis_log.ReadOnly = true;
            this.rich_iis_log.Size = new System.Drawing.Size(545, 396);
            this.rich_iis_log.TabIndex = 0;
            this.rich_iis_log.Text = "";
            this.rich_iis_log.TextColor = TechScan.Tool.Controls.RtfColor.Black;
            // 
            // CMenu
            // 
            this.CMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsCopy,
            this.cmsExport});
            this.CMenu.Name = "CMenu";
            this.CMenu.Size = new System.Drawing.Size(101, 48);
            // 
            // cmsCopy
            // 
            this.cmsCopy.Image = global::TechScan.Tool.U8.ServiceDeployWin.Properties.Resources.copy;
            this.cmsCopy.Name = "cmsCopy";
            this.cmsCopy.Size = new System.Drawing.Size(100, 22);
            this.cmsCopy.Text = "复制";
            this.cmsCopy.Click += new System.EventHandler(this.cmsCopy_Click);
            // 
            // cmsExport
            // 
            this.cmsExport.Image = global::TechScan.Tool.U8.ServiceDeployWin.Properties.Resources.export;
            this.cmsExport.Name = "cmsExport";
            this.cmsExport.Size = new System.Drawing.Size(100, 22);
            this.cmsExport.Text = "导出";
            this.cmsExport.Click += new System.EventHandler(this.cmsExport_Click);
            // 
            // ssStatus
            // 
            this.ssStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ssStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspbProgress,
            this.tslblPublishingStatus,
            this.tslblPublishResult});
            this.ssStatus.Location = new System.Drawing.Point(0, 0);
            this.ssStatus.Name = "ssStatus";
            this.ssStatus.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.ssStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ssStatus.Size = new System.Drawing.Size(969, 25);
            this.ssStatus.TabIndex = 1;
            this.ssStatus.Text = "statusStrip1";
            // 
            // tspbProgress
            // 
            this.tspbProgress.Name = "tspbProgress";
            this.tspbProgress.Size = new System.Drawing.Size(100, 19);
            this.tspbProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // tslblPublishingStatus
            // 
            this.tslblPublishingStatus.Image = ((System.Drawing.Image)(resources.GetObject("tslblPublishingStatus.Image")));
            this.tslblPublishingStatus.Name = "tslblPublishingStatus";
            this.tslblPublishingStatus.Size = new System.Drawing.Size(72, 20);
            this.tslblPublishingStatus.Text = "正在发布";
            this.tslblPublishingStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // tslblPublishResult
            // 
            this.tslblPublishResult.Image = global::TechScan.Tool.U8.ServiceDeployWin.Properties.Resources.success;
            this.tslblPublishResult.Name = "tslblPublishResult";
            this.tslblPublishResult.Size = new System.Drawing.Size(72, 20);
            this.tslblPublishResult.Text = "发布成功";
            this.tslblPublishResult.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer4.IsSplitterFixed = true;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.ssStatus);
            this.splitContainer4.Size = new System.Drawing.Size(969, 662);
            this.splitContainer4.SplitterDistance = 633;
            this.splitContainer4.TabIndex = 2;
            // 
            // tmProgress
            // 
            this.tmProgress.Interval = 300;
            this.tmProgress.Tick += new System.EventHandler(this.tmProgress_Tick);
            // 
            // frmIISDeploy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(152)))), ((int)(((byte)(177)))));
            this.ClientSize = new System.Drawing.Size(969, 662);
            this.Controls.Add(this.splitContainer4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmIISDeploy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WebService Publish  For U8_Android ";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.layeredTabControl1.ResumeLayout(false);
            this.tpServerInfo.ResumeLayout(false);
            this.tpServerInfo.PerformLayout();
            this.pnlMainPublish.ResumeLayout(false);
            this.pnlMainPublish.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPublish)).EndInit();
            this.tsTools.ResumeLayout(false);
            this.tsTools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddPackage)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.layeredTabControl2.ResumeLayout(false);
            this.tpPackageInfo.ResumeLayout(false);
            this.tpPackageInfo.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.CMenu.ResumeLayout(false);
            this.ssStatus.ResumeLayout(false);
            this.ssStatus.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private Controls.ExRichTextBox rich_iis_log;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.StatusStrip ssStatus;
        private System.Windows.Forms.Label lblProcessType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblSystemType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSystemDir;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblPlatform;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblOsVersion;
        private System.Windows.Forms.Label lblMachineName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblWorkSet;
        private System.Windows.Forms.Label lblCurDir;
        private System.Windows.Forms.Label label14;
        private LayeredSkin.Controls.LayeredTabControl layeredTabControl1;
        private System.Windows.Forms.TabPage tpServerInfo;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private LayeredSkin.Controls.LayeredTabControl layeredTabControl2;
        private System.Windows.Forms.TabPage tpPackageInfo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblPKInfo_PublishVersion;
        private System.Windows.Forms.Label lblPKInfo_PublishDateTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox lblPKInfo_PublishDescription;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private LayeredSkin.Controls.LayeredPanel pnlMainPublish;
        private Controls.ButtonEx btnAddPackage;
        private LayeredSkin.Controls.LayeredLabel lblHintShow;
        private System.Windows.Forms.ToolStrip tsTools;
        private System.Windows.Forms.ToolStripDropDownButton tsbSet;
        private System.Windows.Forms.ToolStripButton tsbHelp;
        private Controls.ButtonEx btnPublish;
        private System.Windows.Forms.ToolTip toolTip1;
        private LayeredSkin.Controls.LayeredLabel lblSelectedPackagePath;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblProcesserCounter;
        private System.Windows.Forms.ToolStripButton tsbRollBack;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbPkgType;
        private System.Windows.Forms.ToolStripProgressBar tspbProgress;
        private System.Windows.Forms.ToolStripStatusLabel tslblPublishingStatus;
        private System.Windows.Forms.ToolStripStatusLabel tslblPublishResult;
        private Controls.SpinningCircles picDeployLoading;
        private System.Windows.Forms.Timer tmProgress;
        private System.Windows.Forms.ContextMenuStrip CMenu;
        private System.Windows.Forms.ToolStripMenuItem cmsCopy;
        private System.Windows.Forms.ToolStripMenuItem cmsExport;
        private LayeredSkin.Controls.LayeredLabel layeredLabel1;
        private LayeredSkin.Controls.LayeredLabel layeredLabel3;
    }
}