using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechScan.Tool.U8.ServiceDeployWin.Deploy;
using TechScan.Tool.U8.ServiceDeployWin.SQL;

namespace TechScan.Tool.U8.ServiceDeployWin
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void 发布ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmIISDeploy frm = new frmIISDeploy())
            {
                frm.ShowDialog();
            }
        }

        private void 打包ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //using (frmZipDeployPackage frm = new Deploy.frmZipDeployPackage())
            //{
            //    frm.ShowDialog();
            //}
        }

        private void 测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (TestForm frm = new ServiceDeployWin.TestForm())
            {
                frm.ShowDialog();
            }
        }

        private void 设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmSetting frm = new frmSetting( ))
            {
                frm.ShowDialog();
            }
        }

        private void sQL脚本发布ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (frmSQLDeploy frm = new frmSQLDeploy())
            {
                frm.ShowDialog();
            }
        }
    }
}
