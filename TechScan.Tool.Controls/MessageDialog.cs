using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PrintNiceLabel.VICO.Controls
{
    // Token: 0x02000006 RID: 6
    public partial class MessageDialog : Form
    {
        // Token: 0x0600003B RID: 59 RVA: 0x00004127 File Offset: 0x00002327
        public MessageDialog()
        {
            this.InitializeComponent();
            _g = this.CreateGraphics();
        }

        // Token: 0x0600003C RID: 60 RVA: 0x00004138 File Offset: 0x00002338
        public virtual void Show(MessageDialog.Info no)
        {
            this.btnClose.Visible = true;
            this.btnYes.Visible = false;
            this.btnNo.Visible = false;
            base.ActiveControl = this.btnClose;
            switch (no)
            {
                case MessageDialog.Info.Insert:
                    this.txtMessage.Text = "登录。";
                    break;
                case MessageDialog.Info.Update:
                    this.txtMessage.Text = "更新。";
                    break;
                case MessageDialog.Info.Delete:
                    this.txtMessage.Text = "删除。";
                    break;
                case MessageDialog.Info.Cancel:
                    this.txtMessage.Text = "中断。";
                    break;
                case MessageDialog.Info.Print:
                    this.txtMessage.Text = "打印。";
                    break;
                case MessageDialog.Info.Go:
                    this.txtMessage.Text = "处理完了。";
                    break;
                default:
                    return;
            }
            this.imgIcon.Image = SystemIcons.Information.ToBitmap();
            base.ShowDialog();
        }

        // Token: 0x0600003D RID: 61 RVA: 0x0000421C File Offset: 0x0000241C
        public virtual DialogResult Show(MessageDialog.Confirm no)
        {
            this.btnClose.Visible = false;
            this.btnYes.Visible = true;
            this.btnNo.Visible = true;
            this.btnYes.TabIndex = 1;
            this.btnNo.TabIndex = 2;
            switch (no)
            {
                case MessageDialog.Confirm.Insert:
                    this.txtMessage.Text = "确认要登录吗？";
                    break;
                case MessageDialog.Confirm.Update:
                    this.txtMessage.Text = "确认要更新吗？";
                    break;
                case MessageDialog.Confirm.Delete:
                    this.txtMessage.Text = "确认要删除吗？";
                    break;
                case MessageDialog.Confirm.Cancel:
                    this.txtMessage.Text = "确认要中断吗？";
                    break;
                case MessageDialog.Confirm.Print:
                    this.txtMessage.Text = "确认要打印吗？";
                    break;
                case MessageDialog.Confirm.Go:
                    this.txtMessage.Text = "确认要进行处理吗？";
                    this.btnNo.TabIndex = 1;
                    this.btnYes.TabIndex = 2;
                    break;
                default:
                    return DialogResult.No;
            }
            this.imgIcon.Image = SystemIcons.Question.ToBitmap();
            return base.ShowDialog();
        }

        // Token: 0x0600003E RID: 62 RVA: 0x0000432B File Offset: 0x0000252B
        public virtual void Show(MessageDialog.Error no)
        {
            this.Show(no, string.Empty);
        }

        // Token: 0x0600003F RID: 63 RVA: 0x0000433C File Offset: 0x0000253C
        public virtual void Show(MessageDialog.Error no, string errorCode)
        {
            this.btnClose.Visible = true;
            this.btnYes.Visible = false;
            this.btnNo.Visible = false;
            base.ActiveControl = this.btnClose;
            switch (no)
            {
                case MessageDialog.Error.Insert:
                    this.txtMessage.Text = "不能登录。";
                    break;
                case MessageDialog.Error.Update:
                    this.txtMessage.Text = "不能更新。";
                    break;
                case MessageDialog.Error.Delete:
                    this.txtMessage.Text = "不能删除。";
                    break;
                case MessageDialog.Error.Cancel:
                    this.txtMessage.Text = "不能中断。";
                    break;
                case MessageDialog.Error.Print:
                    this.txtMessage.Text = "不能打印。";
                    break;
                case MessageDialog.Error.Go:
                    this.txtMessage.Text = "处理中错误发生。";
                    break;
                default:
                    return;
            }
            if (errorCode != null && errorCode.Length != 0)
            {
                TextBox textBox = this.txtMessage;
                textBox.Text = textBox.Text + "\r\n错误代码(" + errorCode + ")";
            }
            this.imgIcon.Image = SystemIcons.Error.ToBitmap();
            base.ShowDialog();
        }

        // Token: 0x06000040 RID: 64 RVA: 0x0000444C File Offset: 0x0000264C
        public DialogResult ShowConfirm(string message)
        {
            this.btnClose.Visible = false;
            this.btnYes.Visible = true;
            this.btnNo.Visible = true;
            this.txtMessage.Text = message;
            this.imgIcon.Image = SystemIcons.Question.ToBitmap();
            return base.ShowDialog();
        }

        // Token: 0x06000041 RID: 65 RVA: 0x000044A4 File Offset: 0x000026A4
        public void ShowError(string message)
        {
            this.btnClose.Visible = true;
            this.btnYes.Visible = false;
            this.btnNo.Visible = false;
            base.ActiveControl = this.btnClose;
            this.txtMessage.Text = message;
            this.imgIcon.Image = SystemIcons.Error.ToBitmap();
            MeasureSize();
            base.ShowDialog();
        }

        // Token: 0x06000042 RID: 66 RVA: 0x0000450C File Offset: 0x0000270C
        public void ShowInfo(string message)
        {
            this.btnClose.Visible = true;
            this.btnYes.Visible = false;
            this.btnNo.Visible = false;
            base.ActiveControl = this.btnClose;
            this.txtMessage.Text = message;
            this.imgIcon.Image = SystemIcons.Information.ToBitmap();
            base.ShowDialog();
        }

        // Token: 0x06000043 RID: 67 RVA: 0x00004574 File Offset: 0x00002774
        public void ShowWarn(string message)
        {
            this.btnClose.Visible = true;
            this.btnYes.Visible = false;
            this.btnNo.Visible = false;
            base.ActiveControl = this.btnClose;
            this.txtMessage.Text = message;
            this.imgIcon.Image = SystemIcons.Warning.ToBitmap();
            base.ShowDialog();
        }

        // Token: 0x06000044 RID: 68 RVA: 0x000045D9 File Offset: 0x000027D9
        private void btnYes_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.Yes;
            base.Close();
        }

        // Token: 0x06000045 RID: 69 RVA: 0x000045E8 File Offset: 0x000027E8
        private void btnNo_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.No;
            base.Close();
        }

        // Token: 0x06000046 RID: 70 RVA: 0x000045F7 File Offset: 0x000027F7
        private void btnClose_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        private const int buttonLocation = 40;
        private Rectangle textRect;
        int iYPos = 6;
        int iXPos = 44;
        int iTxtRecWidth = 350;
        int iHightB = 46;
        Graphics _g;
        private string MESSAGE
        {
            get
            {
                return txtMessage.Text.Trim();
            }
        }
        public void MeasureSize()
        {
            try
            {
                textRect = new Rectangle(iXPos, iYPos, iTxtRecWidth, 50);//130,50
                SizeF textSize = _g.MeasureString(MESSAGE, txtMessage.Font);
                float f;
                int i = 0, formHeight = 0;
                if (textSize.Width > textRect.Width)
                {
                    int linesCount = 0;
                    string[] content = MESSAGE.Split('\n');
                    for (i = 0; i < content.Length; i++)
                    {
                        SizeF subTextSize = _g.MeasureString(content[i], this.Font);
                        if (subTextSize.Width < textRect.Width)
                        {
                            linesCount++;
                        }
                        else
                        {
                            // 确定需要几行显示
                            f = subTextSize.Width / textRect.Width;
                            linesCount += Convert.ToInt32(Math.Ceiling(f));
                        }
                    }
                    formHeight = Convert.ToInt32(Math.Ceiling(textSize.Height / content.Length)) * linesCount + iYPos + buttonLocation + iHightB;
                }
                else
                {
                    formHeight = Convert.ToInt32(Math.Ceiling(textSize.Height) + iYPos + buttonLocation + iHightB);
                }

                // 窗体高度
                if (formHeight > 500)
                {
                    formHeight = 500;
                }
                textRect = new Rectangle(iXPos, iYPos, iTxtRecWidth, formHeight);
                this.Size = new Size(400, formHeight);

                // center
                //f = (320 - this.Height - 20) / 2;
                //i = f < 0 ? 0 : Convert.ToInt32(f);
                //this.Location = new Point(15, i);
                txtMessage.Size = new System.Drawing.Size(iTxtRecWidth, this.Height - buttonLocation - 10);
                btnClose.Top = this.Height - buttonLocation - 5;
                btnNo.Top = this.Height - buttonLocation - 5;
                btnYes.Top = this.Height - buttonLocation - 5;
            }
            catch (Exception ex)
            {
#if RECORD_LOG
                logger.Error(ex.Message, ex);
#endif
                //MessageBox.Show("MesureError:" + ex.Message);
            }
        }


        // Token: 0x02000007 RID: 7
        public enum Info
        {
            // Token: 0x04000045 RID: 69
            Insert,
            // Token: 0x04000046 RID: 70
            Update,
            // Token: 0x04000047 RID: 71
            Delete,
            // Token: 0x04000048 RID: 72
            Cancel,
            // Token: 0x04000049 RID: 73
            Print,
            // Token: 0x0400004A RID: 74
            Go
        }

        // Token: 0x02000008 RID: 8
        public enum Confirm
        {
            // Token: 0x0400004C RID: 76
            Insert,
            // Token: 0x0400004D RID: 77
            Update,
            // Token: 0x0400004E RID: 78
            Delete,
            // Token: 0x0400004F RID: 79
            Cancel,
            // Token: 0x04000050 RID: 80
            Print,
            // Token: 0x04000051 RID: 81
            Go
        }

        // Token: 0x02000009 RID: 9
        public enum Error
        {
            // Token: 0x04000053 RID: 83
            Insert,
            // Token: 0x04000054 RID: 84
            Update,
            // Token: 0x04000055 RID: 85
            Delete,
            // Token: 0x04000056 RID: 86
            Cancel,
            // Token: 0x04000057 RID: 87
            Print,
            // Token: 0x04000058 RID: 88
            Go
        }
    }
}
