namespace TechScan.Tool.U8.ServiceDeployWin.Deploy
{
    partial class uclZipPackage
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uclZipPackage));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cmbPackageType = new System.Windows.Forms.ComboBox();
            this.picLoading = new TechScan.Tool.Controls.SpinningCircles();
            this.txtDestinationDirectory = new Control.MyTextBox();
            this.dtpPackDateTime = new System.Windows.Forms.DateTimePicker();
            this.layeredButton1 = new LayeredSkin.Controls.LayeredButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPackVersionCode = new Control.MyTextBox();
            this.txtPackVersionDescription = new Control.MyTextBox();
            this.rboSQL = new System.Windows.Forms.RadioButton();
            this.rboIIS = new System.Windows.Forms.RadioButton();
            this.txtPackager = new Control.MyTextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pnlDirectory = new TechScan.Tool.Controls.FlowPanelEx();
            this.btnAddDirectory = new TechScan.Tool.Controls.ButtonEx();
            this.pnlPublish = new TechScan.Tool.Controls.FlowPanelEx();
            this.btnStartPack = new TechScan.Tool.Controls.ButtonEx();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.pnlDirectory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddDirectory)).BeginInit();
            this.pnlPublish.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnStartPack)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(152)))), ((int)(((byte)(177)))));
            this.splitContainer1.Panel1.Controls.Add(this.cmbPackageType);
            this.splitContainer1.Panel1.Controls.Add(this.picLoading);
            this.splitContainer1.Panel1.Controls.Add(this.txtDestinationDirectory);
            this.splitContainer1.Panel1.Controls.Add(this.dtpPackDateTime);
            this.splitContainer1.Panel1.Controls.Add(this.layeredButton1);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.txtPackVersionCode);
            this.splitContainer1.Panel1.Controls.Add(this.txtPackVersionDescription);
            this.splitContainer1.Panel1.Controls.Add(this.rboSQL);
            this.splitContainer1.Panel1.Controls.Add(this.rboIIS);
            this.splitContainer1.Panel1.Controls.Add(this.txtPackager);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(412, 379);
            this.splitContainer1.SplitterDistance = 240;
            this.splitContainer1.TabIndex = 8;
            // 
            // cmbPackageType
            // 
            this.cmbPackageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPackageType.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbPackageType.FormattingEnabled = true;
            this.cmbPackageType.Location = new System.Drawing.Point(311, 9);
            this.cmbPackageType.Name = "cmbPackageType";
            this.cmbPackageType.Size = new System.Drawing.Size(93, 27);
            this.cmbPackageType.TabIndex = 9;
            // 
            // picLoading
            // 
            this.picLoading.BackColor = System.Drawing.Color.Transparent;
            this.picLoading.FullTransparent = true;
            this.picLoading.Increment = 1F;
            this.picLoading.Location = new System.Drawing.Point(161, 101);
            this.picLoading.N = 8;
            this.picLoading.Name = "picLoading";
            this.picLoading.Radius = 2.5F;
            this.picLoading.Size = new System.Drawing.Size(90, 100);
            this.picLoading.TabIndex = 8;
            this.picLoading.Text = "spinningCircles1";
            this.picLoading.Visible = false;
            // 
            // txtDestinationDirectory
            // 
            this.txtDestinationDirectory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(152)))), ((int)(((byte)(177)))));
            this.txtDestinationDirectory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDestinationDirectory.Caption = "目标目录：";
            this.txtDestinationDirectory.CaptionColor = System.Drawing.SystemColors.ControlText;
            this.txtDestinationDirectory.Font = new System.Drawing.Font("宋体", 12F);
            this.txtDestinationDirectory.Location = new System.Drawing.Point(12, 190);
            this.txtDestinationDirectory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDestinationDirectory.Name = "txtDestinationDirectory";
            this.txtDestinationDirectory.PasswordChar = '\0';
            this.txtDestinationDirectory.SelectionStart = 0;
            this.txtDestinationDirectory.Size = new System.Drawing.Size(315, 30);
            this.txtDestinationDirectory.TabIndex = 7;
            this.txtDestinationDirectory.TextBoxType = Control.TextBoxType.txtZoom;
            this.txtDestinationDirectory.Value = null;
            this.txtDestinationDirectory.ZoomClick += new System.EventHandler(this.txtDestinationDirectory_ZoomClick);
            // 
            // dtpPackDateTime
            // 
            this.dtpPackDateTime.Enabled = false;
            this.dtpPackDateTime.Font = new System.Drawing.Font("宋体", 12F);
            this.dtpPackDateTime.Location = new System.Drawing.Point(103, 155);
            this.dtpPackDateTime.Name = "dtpPackDateTime";
            this.dtpPackDateTime.Size = new System.Drawing.Size(221, 26);
            this.dtpPackDateTime.TabIndex = 5;
            // 
            // layeredButton1
            // 
            this.layeredButton1.AdaptImage = true;
            this.layeredButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("layeredButton1.BackgroundImage")));
            this.layeredButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.layeredButton1.BaseColor = System.Drawing.Color.Wheat;
            this.layeredButton1.Borders.BottomColor = System.Drawing.Color.Empty;
            this.layeredButton1.Borders.BottomWidth = 1;
            this.layeredButton1.Borders.LeftColor = System.Drawing.Color.Empty;
            this.layeredButton1.Borders.LeftWidth = 1;
            this.layeredButton1.Borders.RightColor = System.Drawing.Color.Empty;
            this.layeredButton1.Borders.RightWidth = 1;
            this.layeredButton1.Borders.TopColor = System.Drawing.Color.Empty;
            this.layeredButton1.Borders.TopWidth = 1;
            this.layeredButton1.Canvas = ((System.Drawing.Bitmap)(resources.GetObject("layeredButton1.Canvas")));
            this.layeredButton1.ControlState = LayeredSkin.Controls.ControlStates.Normal;
            this.layeredButton1.HaloColor = System.Drawing.Color.White;
            this.layeredButton1.HaloSize = 5;
            this.layeredButton1.HoverImage = null;
            this.layeredButton1.IsPureColor = false;
            this.layeredButton1.Location = new System.Drawing.Point(332, 197);
            this.layeredButton1.Name = "layeredButton1";
            this.layeredButton1.NormalImage = null;
            this.layeredButton1.PressedImage = null;
            this.layeredButton1.Radius = 10;
            this.layeredButton1.ShowBorder = true;
            this.layeredButton1.Size = new System.Drawing.Size(77, 23);
            this.layeredButton1.TabIndex = 6;
            this.layeredButton1.Text = "layeredButton1";
            this.layeredButton1.TextLocationOffset = new System.Drawing.Point(0, 0);
            this.layeredButton1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.layeredButton1.TextShowMode = LayeredSkin.TextShowModes.Halo;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(9, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "打包时间：";
            // 
            // txtPackVersionCode
            // 
            this.txtPackVersionCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPackVersionCode.Caption = "打包版本号：";
            this.txtPackVersionCode.CaptionColor = System.Drawing.SystemColors.ControlText;
            this.txtPackVersionCode.Font = new System.Drawing.Font("宋体", 12F);
            this.txtPackVersionCode.Location = new System.Drawing.Point(9, 39);
            this.txtPackVersionCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPackVersionCode.Name = "txtPackVersionCode";
            this.txtPackVersionCode.PasswordChar = '\0';
            this.txtPackVersionCode.SelectionStart = 0;
            this.txtPackVersionCode.Size = new System.Drawing.Size(315, 30);
            this.txtPackVersionCode.TabIndex = 2;
            this.txtPackVersionCode.TextBoxType = Control.TextBoxType.txt;
            this.txtPackVersionCode.Value = "";
            // 
            // txtPackVersionDescription
            // 
            this.txtPackVersionDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPackVersionDescription.Caption = "版本描述：";
            this.txtPackVersionDescription.CaptionColor = System.Drawing.SystemColors.ControlText;
            this.txtPackVersionDescription.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPackVersionDescription.Location = new System.Drawing.Point(9, 79);
            this.txtPackVersionDescription.Margin = new System.Windows.Forms.Padding(5);
            this.txtPackVersionDescription.Name = "txtPackVersionDescription";
            this.txtPackVersionDescription.PasswordChar = '\0';
            this.txtPackVersionDescription.SelectionStart = 0;
            this.txtPackVersionDescription.Size = new System.Drawing.Size(315, 30);
            this.txtPackVersionDescription.TabIndex = 3;
            this.txtPackVersionDescription.TextBoxType = Control.TextBoxType.txt;
            this.txtPackVersionDescription.Value = null;
            // 
            // rboSQL
            // 
            this.rboSQL.AutoSize = true;
            this.rboSQL.BackColor = System.Drawing.Color.Transparent;
            this.rboSQL.Font = new System.Drawing.Font("宋体", 12F);
            this.rboSQL.Location = new System.Drawing.Point(148, 10);
            this.rboSQL.Name = "rboSQL";
            this.rboSQL.Size = new System.Drawing.Size(82, 20);
            this.rboSQL.TabIndex = 1;
            this.rboSQL.TabStop = true;
            this.rboSQL.Text = "SQL脚本";
            this.rboSQL.UseVisualStyleBackColor = false;
            // 
            // rboIIS
            // 
            this.rboIIS.AutoSize = true;
            this.rboIIS.Checked = true;
            this.rboIIS.Font = new System.Drawing.Font("宋体", 12F);
            this.rboIIS.Location = new System.Drawing.Point(12, 10);
            this.rboIIS.Name = "rboIIS";
            this.rboIIS.Size = new System.Drawing.Size(130, 20);
            this.rboIIS.TabIndex = 0;
            this.rboIIS.TabStop = true;
            this.rboIIS.Text = "U8Android服务";
            this.rboIIS.UseVisualStyleBackColor = true;
            // 
            // txtPackager
            // 
            this.txtPackager.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(152)))), ((int)(((byte)(177)))));
            this.txtPackager.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPackager.Caption = "打 包 人：";
            this.txtPackager.CaptionColor = System.Drawing.SystemColors.ControlText;
            this.txtPackager.Font = new System.Drawing.Font("宋体", 12F);
            this.txtPackager.Location = new System.Drawing.Point(9, 119);
            this.txtPackager.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPackager.Name = "txtPackager";
            this.txtPackager.PasswordChar = '\0';
            this.txtPackager.SelectionStart = 0;
            this.txtPackager.Size = new System.Drawing.Size(155, 30);
            this.txtPackager.TabIndex = 4;
            this.txtPackager.TextBoxType = Control.TextBoxType.txt;
            this.txtPackager.Value = null;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.pnlDirectory);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.pnlPublish);
            this.splitContainer2.Size = new System.Drawing.Size(412, 135);
            this.splitContainer2.SplitterDistance = 264;
            this.splitContainer2.TabIndex = 1;
            // 
            // pnlDirectory
            // 
            this.pnlDirectory.AllowDrop = true;
            this.pnlDirectory.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlDirectory.BackgroundImage")));
            this.pnlDirectory.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(200)))), ((int)(((byte)(210)))));
            this.pnlDirectory.BorderWidth = 1;
            this.pnlDirectory.Controls.Add(this.btnAddDirectory);
            this.pnlDirectory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDirectory.Location = new System.Drawing.Point(0, 0);
            this.pnlDirectory.Name = "pnlDirectory";
            this.pnlDirectory.Size = new System.Drawing.Size(264, 135);
            this.pnlDirectory.TabIndex = 0;
            // 
            // btnAddDirectory
            // 
            this.btnAddDirectory.BackColor = System.Drawing.Color.Transparent;
            this.btnAddDirectory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddDirectory.Image = ((System.Drawing.Image)(resources.GetObject("btnAddDirectory.Image")));
            this.btnAddDirectory.ImageClick = ((System.Drawing.Image)(resources.GetObject("btnAddDirectory.ImageClick")));
            this.btnAddDirectory.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnAddDirectory.ImageHover")));
            this.btnAddDirectory.ImageNormal = ((System.Drawing.Image)(resources.GetObject("btnAddDirectory.ImageNormal")));
            this.btnAddDirectory.IsSwitch = false;
            this.btnAddDirectory.Location = new System.Drawing.Point(5, 5);
            this.btnAddDirectory.Margin = new System.Windows.Forms.Padding(5);
            this.btnAddDirectory.Name = "btnAddDirectory";
            this.btnAddDirectory.Selected = false;
            this.btnAddDirectory.Size = new System.Drawing.Size(120, 120);
            this.btnAddDirectory.TabIndex = 0;
            this.btnAddDirectory.TabStop = false;
            this.toolTip1.SetToolTip(this.btnAddDirectory, "选择待打包目录");
            this.btnAddDirectory.Click += new System.EventHandler(this.btnAddDirectory_Click);
            // 
            // pnlPublish
            // 
            this.pnlPublish.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlPublish.BackgroundImage")));
            this.pnlPublish.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(200)))), ((int)(((byte)(210)))));
            this.pnlPublish.BorderWidth = 1;
            this.pnlPublish.Controls.Add(this.btnStartPack);
            this.pnlPublish.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPublish.Location = new System.Drawing.Point(0, 0);
            this.pnlPublish.Name = "pnlPublish";
            this.pnlPublish.Size = new System.Drawing.Size(144, 135);
            this.pnlPublish.TabIndex = 1;
            // 
            // btnStartPack
            // 
            this.btnStartPack.BackColor = System.Drawing.Color.Transparent;
            this.btnStartPack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnStartPack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartPack.Image = ((System.Drawing.Image)(resources.GetObject("btnStartPack.Image")));
            this.btnStartPack.ImageClick = ((System.Drawing.Image)(resources.GetObject("btnStartPack.ImageClick")));
            this.btnStartPack.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnStartPack.ImageHover")));
            this.btnStartPack.ImageNormal = ((System.Drawing.Image)(resources.GetObject("btnStartPack.ImageNormal")));
            this.btnStartPack.IsSwitch = false;
            this.btnStartPack.Location = new System.Drawing.Point(5, 5);
            this.btnStartPack.Margin = new System.Windows.Forms.Padding(5);
            this.btnStartPack.Name = "btnStartPack";
            this.btnStartPack.Selected = false;
            this.btnStartPack.Size = new System.Drawing.Size(131, 120);
            this.btnStartPack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnStartPack.TabIndex = 0;
            this.btnStartPack.TabStop = false;
            this.toolTip1.SetToolTip(this.btnStartPack, "执行打包");
            this.btnStartPack.Click += new System.EventHandler(this.btnStartPack_Click);
            // 
            // uclZipPackage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.splitContainer1);
            this.Name = "uclZipPackage";
            this.Size = new System.Drawing.Size(412, 379);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.pnlDirectory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnAddDirectory)).EndInit();
            this.pnlPublish.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnStartPack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Control.MyTextBox txtPackVersionCode;
        private Control.MyTextBox txtPackVersionDescription;
        private System.Windows.Forms.RadioButton rboSQL;
        private System.Windows.Forms.DateTimePicker dtpPackDateTime;
        private System.Windows.Forms.RadioButton rboIIS;
        private Control.MyTextBox txtPackager;
        private Controls.FlowPanelEx pnlDirectory;
        private Controls.ButtonEx btnAddDirectory;
        private LayeredSkin.Controls.LayeredButton layeredButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Controls.FlowPanelEx pnlPublish;
        private Control.MyTextBox txtDestinationDirectory;
        private System.Windows.Forms.ToolTip toolTip1;
        private Controls.SpinningCircles picLoading;
        private Controls.ButtonEx btnStartPack;
        private System.Windows.Forms.ComboBox cmbPackageType;
    }
}
