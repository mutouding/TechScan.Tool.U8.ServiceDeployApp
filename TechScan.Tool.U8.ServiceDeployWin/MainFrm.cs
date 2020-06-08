using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechScan.Tool.U8.ServiceDeploy.Base.Encrypt;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils;
using TechScan.Tool.U8.ServiceDeployWin.Deploy;
using TechScan.Tool.U8.ServiceDeployWin.SQL;
using TechScan.Tool.U8.ServiceDeployWin.Util;

namespace TechScan.Tool.U8.ServiceDeployWin
{
    public partial class MainFrm : Form
    {
        #region Ctor
        public MainFrm()
        {
            InitializeComponent();
            LoadRoles();
        }
        #endregion

        #region Methods

        private void btnFirstPublish_Click(object sender, EventArgs e)
        {
            try
            {
                #region 首次自动化部署
                if (MessagesHelper.ShowQuestion("是否开始首次默认部署？"))
                {

                    DeployHelper.CheckFirstDeployEnv();
                    using (frmIISDeploy mFrmSql = new frmIISDeploy(true))
                    {
                        mFrmSql.ShowDialog();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Error);
            }
        }

        private void btnWebPublish_Click(object sender, EventArgs e)
        {
            using (frmIISDeploy frm = new frmIISDeploy())
            {
                frm.ShowDialog();
            }
        }

        private void btnSqlPublish_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmSQLDeploy frm = new frmSQLDeploy())
                {
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Warning);
            }
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            using (frmSetting frm = new frmSetting())
            {
                frm.ShowDialog();
            }
        }

        private void LoadRoles()
        {
            try
            {
                /*
                 * TechScan:6D9B4A2746A7680ED7E0F4B66D838D47
                   Customer:D651E9A147650EE07451A6EBCE037C46
                 */
                if (!DESEncrypt.Decrypt(ConfigurationManager.AppSettings["AppRole"].Trim()).Equals(ConstantValues.DEFAULT_CORP_NAME))
                {
                    btnZipPackage.Enabled = false;
                }
                else
                {
                    btnZipPackage.Enabled = true;
                }
            }
            catch (Exception)
            {
            }
        }
        private void btnZipPackage_Click(object sender, EventArgs e)
        {
            using (PackageZipForm frm = new Deploy.PackageZipForm())
            {
                frm.ShowDialog();
            }
        }

        #endregion

        private void MainFrm_Load(object sender, EventArgs e)
        {
            try
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                string cVersion = version.ToString();
                string cVersionTime = System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location).ToString();
                //Text = $"U8Android实施工具[Ver:{cVersion}-{cVersionTime}]";
                txtVersion.Text = $"Ver:{cVersion}";
                txtVersionTime.Text = $"发布时间:{cVersionTime}";
            }
            catch (Exception)
            { }
        }
    }
}
