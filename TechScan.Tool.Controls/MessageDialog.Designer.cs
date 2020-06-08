namespace PrintNiceLabel.VICO.Controls
{
	// Token: 0x02000006 RID: 6
	public partial class MessageDialog //: global::System.Windows.Forms.Form
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00004606 File Offset: 0x00002806
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00004628 File Offset: 0x00002828
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.imgIcon = new System.Windows.Forms.PictureBox();
            this.btnNo = new PrintNiceLabel.VICO.Controls.GraphicButton(this.components);
            this.btnYes = new PrintNiceLabel.VICO.Controls.GraphicButton(this.components);
            this.btnClose = new PrintNiceLabel.VICO.Controls.GraphicButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(235)))));
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessage.Font = new System.Drawing.Font("宋体", 14F);
            this.txtMessage.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtMessage.Location = new System.Drawing.Point(44, 6);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ReadOnly = true;
            this.txtMessage.Size = new System.Drawing.Size(350, 51);
            this.txtMessage.TabIndex = 23;
            this.txtMessage.TabStop = false;
            this.txtMessage.Text = "该数据在页面中不存在,请刷新数据！";
            this.txtMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // imgIcon
            // 
            this.imgIcon.Location = new System.Drawing.Point(6, 15);
            this.imgIcon.Name = "imgIcon";
            this.imgIcon.Size = new System.Drawing.Size(32, 32);
            this.imgIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.imgIcon.TabIndex = 24;
            this.imgIcon.TabStop = false;
            // 
            // btnNo
            // 
            this.btnNo.DisableColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnNo.DisableColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnNo.Font = new System.Drawing.Font("宋体", 14F);
            this.btnNo.ForeColor = System.Drawing.Color.White;
            this.btnNo.Location = new System.Drawing.Point(220, 63);
            this.btnNo.MaskColor = System.Drawing.Color.Red;
            this.btnNo.Name = "btnNo";
            this.btnNo.RightOutsideColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNo.Size = new System.Drawing.Size(100, 40);
            this.btnNo.TabIndex = 21;
            this.btnNo.Text = "取消";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnYes
            // 
            this.btnYes.DisableColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnYes.DisableColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnYes.Font = new System.Drawing.Font("宋体", 14F);
            this.btnYes.ForeColor = System.Drawing.Color.White;
            this.btnYes.Location = new System.Drawing.Point(80, 63);
            this.btnYes.MaskColor = System.Drawing.Color.Red;
            this.btnYes.Name = "btnYes";
            this.btnYes.RightOutsideColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnYes.Size = new System.Drawing.Size(100, 40);
            this.btnYes.TabIndex = 20;
            this.btnYes.Text = "确定";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnClose
            // 
            this.btnClose.DisableColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnClose.DisableColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnClose.Font = new System.Drawing.Font("宋体", 14F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(150, 63);
            this.btnClose.MaskColor = System.Drawing.Color.Red;
            this.btnClose.Name = "btnClose";
            this.btnClose.RightOutsideColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnClose.Size = new System.Drawing.Size(100, 40);
            this.btnClose.TabIndex = 22;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // MessageDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(235)))));
            this.ClientSize = new System.Drawing.Size(398, 109);
            this.ControlBox = false;
            this.Controls.Add(this.imgIcon);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtMessage);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.imgIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		// Token: 0x0400003E RID: 62
		private global::System.ComponentModel.IContainer components;

		// Token: 0x0400003F RID: 63
        private global::PrintNiceLabel.VICO.Controls.GraphicButton btnYes;

		// Token: 0x04000040 RID: 64
        private global::PrintNiceLabel.VICO.Controls.GraphicButton btnNo;

		// Token: 0x04000041 RID: 65
        private global::PrintNiceLabel.VICO.Controls.GraphicButton btnClose;

		// Token: 0x04000042 RID: 66
		protected global::System.Windows.Forms.TextBox txtMessage;

		// Token: 0x04000043 RID: 67
		protected global::System.Windows.Forms.PictureBox imgIcon;
	}
}
