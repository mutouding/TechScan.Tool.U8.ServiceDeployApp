using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechScan.Tool.U8.ServiceDeployWin.Deploy
{
    [Obsolete("已经停用，用PackageZipForm窗口代替", true)]
    public partial class frmZipDeployPackage : Form
    {
        public frmZipDeployPackage()
        {
            InitializeComponent();
            this.MouseWheel += FrmZipDeployPackage_MouseWheel;
        }

        private void FrmZipDeployPackage_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                //获取光标位置
                Point mousePoint = new Point(e.X, e.Y);
                //换算成相对本窗体的位置
                mousePoint.Offset(this.Location.X, this.Location.Y);
                //判断是否在panel内
                if (pnlZipCtls.RectangleToScreen(pnlZipCtls.DisplayRectangle).Contains(mousePoint))
                {
                    //滚动
                    pnlZipCtls.AutoScrollPosition = new Point(0, pnlZipCtls.VerticalScroll.Value - e.Delta);
                }
            }
            catch (Exception ex)
            {
                //log.Error(ex.Message, ex);
                //(new MessageDialog()).ShowError(ex.Message);
            }
        }

        private void btnAddZipCtl_Click(object sender, EventArgs e)
        {
            try
            {
                pnlZipCtls.Controls.Remove(btnAddZipCtl);
                uclZipPackage ucZipItem = new uclZipPackage();
                pnlZipCtls.Controls.Add(ucZipItem);
                pnlZipCtls.Controls.Add(btnAddZipCtl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
