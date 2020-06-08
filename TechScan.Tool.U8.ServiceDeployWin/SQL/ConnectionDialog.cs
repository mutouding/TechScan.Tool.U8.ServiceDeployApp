using System;
using System.Linq;
using System.Windows.Forms;
using TechScan.Tool.U8.ServiceDeploy.SQL.Enums;
using System.Collections.Generic;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils;
using TechScan.Tool.U8.ServiceDeploy.SQL.Config;
using TechScan.Tool.U8.ServiceDeployWin.Util;
using TechScan.Tool.U8.ServiceDeploy.SQL.Impl;
using TechScan.Tool.U8.ServiceDeploy.Base.Exceptions;

namespace TechScan.Tool.U8.ServiceDeployWin.SQL
{
    public partial class ConnectionDialog : BaseDialog
    {
        public ConnectionDialog()
        {
            InitializeComponent();

            Enum.GetValues(typeof(AuthTypes)).Cast<AuthTypes>().ForEach((s) => cboAuthTypes.Items.Add(s));
            cboAuthTypes.SelectedIndex = 0;
        }

        public ConnectionDialog(DbServerInfo info)
            : this()
        {
            if (info != null)
            {
                AuthType = info.AuthType;
                Server = info.Server;
                UserName = info.User;
                Password = info.Password;
                AuthType = info.AuthType;
            }
        }

        public AuthTypes AuthType
        {
            get { return (AuthTypes)cboAuthTypes.SelectedItem; }
            set { cboAuthTypes.SelectedItem = value; }
        }

        public string Server
        {
            get { return cboServers.Text; }
            set { cboServers.Text = value; }
        }

        public string UserName
        {
            get { return txtUserName.Text; }
            set { txtUserName.Text = value; }
        }

        public string Password
        {
            get { return txtPassword.Text; }
            set { txtPassword.Text = value; }
        }

        private DbServerInfo GetServerInfo
        {
            get
            {
                return new DbServerInfo
                {
                    AuthType = this.AuthType,
                    Server = this.Server,
                    User = this.UserName,
                    Password = this.Password,
                    Database = "master"
                };
            }
        }

        private Func<bool> CheckParamsFunc()
        {
            return new Func<bool>(() =>
            {
                if (string.IsNullOrEmpty(cboServers.Text.Trim()))
                {
                    throw new SQLDeployBaseException("请输入服务器数据库地址！");
                }
                var authType = (AuthTypes)Enum.Parse(typeof(AuthTypes), cboAuthTypes.Text);
                if (authType == AuthTypes.SqlServer)
                {
                    if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
                    {
                        throw new SQLDeployBaseException("请输入数据库用户名！");
                    }
                }
                return true;
            });
        }

        private void OnTestConnectionClick(object sender, EventArgs e)
        {
            try
            {
                CheckParamsFunc();
                if (IsSqlServer2005OrAbove())
                {
                    MessagesHelper.ShowMessage("测试连接成功!");
                }
            }
            catch (Exception ex)
            {
                MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Warning);
            }
        }

        private bool IsSqlServer2005OrAbove()
        {
            try
            {
                var version = SQLMgmtEngine.GetServerVersion(GetServerInfo);
                var is2005OrAbove = version >= 9;
                if (!is2005OrAbove)
                    MessagesHelper.ShowMessage(string.Format("您当前版本 {0}, 仅支持 SQL Server 2005或更新的版本.", version));
                return is2005OrAbove;
            }
            catch (Exception ex)
            {
                MessagesHelper.ShowMessage(ex.Message);
                return false;
            }
        }
        private void OnAuthTypesSelectedIndexChanged(object sender, EventArgs e)
        {
            var enable = (AuthTypes)cboAuthTypes.SelectedItem == AuthTypes.SqlServer;
            txtUserName.Enabled = enable;
            txtPassword.Enabled = enable;
        }
        private void OnSaveClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Server))
            {
                if (!string.IsNullOrEmpty(UserName) || AuthType == AuthTypes.Windows)
                {
                    if (IsSqlServer2005OrAbove())
                        this.DialogResult = DialogResult.OK;
                }
                else
                {
                    epHint.SetError(txtUserName, "请输入用户名.");
                }
            }
            else
            {
                epHint.SetError(cboServers, "请输入数据库IP地址.");
            }
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnServersDropDown(object sender, EventArgs e)
        {
            if (cboServers.Items.Count == 0)
            {
                cboServers.Items.Clear();
                cboServers.Items.AddRange(DeployHelper.DeployConfigCacheInstance.SQL_Config.DbServers.Select((p) => p.Server).ToArray());
            }
        }
    }
}
