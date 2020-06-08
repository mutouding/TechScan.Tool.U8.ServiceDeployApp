namespace TechScan.Tool.Controls
{
    partial class uclDirectory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uclDirectory));
            this.picDic = new System.Windows.Forms.PictureBox();
            this.btnDelete = new TechScan.Tool.Controls.ButtonEx();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picDic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // picDic
            // 
            this.picDic.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picDic.BackgroundImage")));
            this.picDic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picDic.Location = new System.Drawing.Point(30, 22);
            this.picDic.Name = "picDic";
            this.picDic.Size = new System.Drawing.Size(57, 52);
            this.picDic.TabIndex = 0;
            this.picDic.TabStop = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.ImageClick = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageClick")));
            this.btnDelete.ImageHover = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageHover")));
            this.btnDelete.ImageNormal = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageNormal")));
            this.btnDelete.IsSwitch = false;
            this.btnDelete.Location = new System.Drawing.Point(5, 92);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Selected = false;
            this.btnDelete.Size = new System.Drawing.Size(20, 20);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.TabStop = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // uclDirectory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.picDic);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "uclDirectory";
            this.Size = new System.Drawing.Size(120, 120);
            ((System.ComponentModel.ISupportInitialize)(this.picDic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picDic;
        private ButtonEx btnDelete;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
