using System.Drawing;
using System.Windows.Forms;
using TechScan.Tool.U8.ServiceDeployWin.Properties;

namespace TechScan.Tool.U8.ServiceDeployWin.SQL
{
    /// <summary>
    /// 基类窗口
    /// </summary>
    public partial class BaseDialog : Form
    {
        #region Property

        /// <summary>
        /// 窗口标题
        /// </summary>
        public string Description
        {
            set { lblDescription.Text = value; }
        }
        #endregion

        #region Ctor
        public BaseDialog()
        {
            InitializeComponent();
            this.Icon = Icon.FromHandle(Resources.Server2.GetHicon());
        }
        #endregion        
    }
}
