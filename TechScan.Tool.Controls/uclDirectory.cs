using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechScan.Tool.Controls
{
    public partial class uclDirectory : UserControl
    {
        /// <summary>
        /// 已经选择的文件夹目录地址
        /// </summary>
        public string SelectedDirectoryPath
        {
            get;
            set;
        }

        public uclDirectory()
        {
            InitializeComponent();
            this.MouseHover += UclDirectory_MouseHover;
        }

        private void UclDirectory_MouseHover(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(this.picDic, SelectedDirectoryPath);
            this.toolTip1.SetToolTip(this, SelectedDirectoryPath);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
