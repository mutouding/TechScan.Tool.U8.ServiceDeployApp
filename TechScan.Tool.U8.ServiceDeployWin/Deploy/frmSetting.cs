using Microsoft.Web.Administration;
using PrintNiceLabel.VICO.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils;
using TechScan.Tool.U8.ServiceDeploy.IIS.Config;
using TechScan.Tool.U8.ServiceDeploy.IIS.Impl;
using TechScan.Tool.U8.ServiceDeployWin.Models;
using TechScan.Tool.U8.ServiceDeployWin.Util;
//452, 572
namespace TechScan.Tool.U8.ServiceDeployWin.Deploy
{
    //130, 152, 177
    //130, 152, 157
    public partial class frmSetting : Form
    {
        #region Fields & Property

        private bool IsReRegIisRunning = false;
        private bool IsReRegWcfRunning = false;

        Func<BaseCommandItem, string> FunGetCmdLines
        {
            get
            {
                return (A) =>
                {
                    var vCmds = A?.Commands;
                    string cCmdsTmp = string.Empty;
                    foreach (var item in vCmds)
                    {
                        cCmdsTmp += $"{item.ToString()}{System.Environment.NewLine}";
                    }
                    return cCmdsTmp.TrimEnd(new char[] { '\r', '\n' });
                };
            }
        }

        Func<TextBox, IList<string>> FunConvertStringToCmdLines
        {
            get
            {
                return (A) =>
                {
                    var cmdLines = new List<string>();
                    cmdLines.AddRange(A.Lines);
                    return cmdLines;
                };
            }
        }

        DeployTypes m_CurrentDeployType;

        #endregion

        #region Ctor
        public frmSetting(DeployTypes deployType = DeployTypes.IIS)
        {
            InitializeComponent();
            m_CurrentDeployType = deployType;
            this.Load += FrmSetting_Load;
            txtWebSiteName.TextChanged += TxtWebSiteName_TextChanged;
            this.FormClosing += FrmSetting_FormClosing;
            chkControlUserAuth.CheckedChanged += (A, B) => { txtUsersToControlAuth.Enabled = chkControlUserAuth.Checked; };
            chkAutoReRegNet.CheckedChanged += (A, B) => { txtRegNet.Enabled = chkAutoReRegNet.Checked; };
            chkAutoReRegWcf.CheckedChanged += (A, B) => { txtRegWcf.Enabled = chkAutoReRegWcf.Checked; };
        }

        #endregion

        #region Methods

        private void FrmSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                SaveConfig();
            }
            catch (Exception ex)
            {
                (new MessageDialog()).ShowError(ex.Message);
                e.Cancel = true;
            }
        }

        private void SaveConfig()
        {
            #region Global Config
            #endregion

            #region IIS Config
            DeployHelper.DeployConfigCacheInstance.IIS_Config.AppPoolConfig.ManagedPipelineMode =
                cmbPipeType.Text.Equals("经典") ? ManagedPipelineMode.Classic : ManagedPipelineMode.Integrated;
            DeployHelper.DeployConfigCacheInstance.IIS_Config.AppPoolConfig.Enable32BitAppOnWin64 = chkEnable32On64.Checked;
            DeployHelper.DeployConfigCacheInstance.IIS_Config.AppPoolConfig.ManagedRuntimeVersion = cmbNetFrameworks.Text;
            DeployHelper.DeployConfigCacheInstance.IIS_Config.AppPoolConfig.PoolName =
                txtAppPoolName.Text.Trim().Equals(string.Empty) ? ConstantValues.DEFAULT_IIS_APPLICATIONPOOL_NAME : txtAppPoolName.Text.Trim();

            string cPysicPath = txtSitePhysicPath.Text.Trim();
            if (System.IO.Directory.Exists(cPysicPath))
            {
                DeployHelper.DeployConfigCacheInstance.IIS_Config.PhysicalPath = cPysicPath;
            }
            DeployHelper.DeployConfigCacheInstance.IIS_Config.VirtualSiteName = txtWebSiteName.Text.Trim().Equals(string.Empty) ? ConstantValues.DEFAULT_IIS_VIRTUALSITE_NAME : txtWebSiteName.Text.Trim();
            if (!string.IsNullOrEmpty(txtPorts.Text.Trim()))
            {
                DeployHelper.DeployConfigCacheInstance.IIS_Config.Ports =
                     new Func<IList<int>>(
                        () =>
                        {
                            IList<int> lstPortsTmp = new List<int>();
                            string[] cPortsTmp = txtPorts.Text.Trim().Split(new char[] { ',' });
                            foreach (var item in cPortsTmp)
                            {
                                int iTemp = 0;
                                if (int.TryParse(item, out iTemp))
                                {
                                    if (!lstPortsTmp.Contains(iTemp))
                                    {
                                        lstPortsTmp.Add(iTemp);
                                    }
                                }
                                else
                                {
                                    throw new Exception("端口设置有误！");
                                }
                            }
                            return lstPortsTmp;
                        })();
            }

            DeployHelper.DeployConfigCacheInstance.IIS_Config.VirtualSiteUserAuth.IsAddUserAuth = chkControlUserAuth.Checked;
            if (chkControlUserAuth.Checked)
            {
                if (!string.IsNullOrEmpty(txtUsersToControlAuth.Text.Trim()))
                {
                    DeployHelper.DeployConfigCacheInstance.IIS_Config.VirtualSiteUserAuth.Users =
                         new Func<List<string>>(
                            () =>
                            {
                                List<string> lstUsersTmp = new List<string>();
                                string[] cUsersTmp = txtUsersToControlAuth.Text.Trim().Split(new char[] { ',' });

                                Func<string, bool> funCheckZhCharacter = (A) => { return A.Contains("，"); };

                                cUsersTmp.ForEach((A) =>
                                {
                                    if (funCheckZhCharacter(A))
                                    {
                                        throw new Exception("虚拟目录权限用户设置有误！");
                                    }
                                    else
                                    {
                                        if (!lstUsersTmp.Contains(A))
                                        {
                                            lstUsersTmp.Add(A);
                                        }
                                    }
                                });
                                return lstUsersTmp;
                            })();
                }
                else
                {
                    throw new Exception("虚拟目录权限用户设置有误！");
                }
            }
            #endregion

            #region SQL Config

            DeployHelper.DeployConfigCacheInstance.SQL_Config.ConnectionTimeout = new Func<int>(() =>
             {
                 var iTimeout = 0;
                 if (int.TryParse(txtDbConnectionTimeout.Text.Trim(), out iTimeout))
                 {
                     return iTimeout;
                 }
                 else
                 {
                     throw new Exception("数据库连接超时时间设置有误！");
                 }
             })();

            #endregion

            #region ReReg
            DeployHelper.DeployConfigCacheInstance.IIS_Config.RegNet.IsCmdRequired = chkAutoReRegNet.Checked;
            DeployHelper.DeployConfigCacheInstance.IIS_Config.RegWCF.IsCmdRequired = chkAutoReRegWcf.Checked;
            if (chkAutoReRegNet.Checked)
            {
                DeployHelper.DeployConfigCacheInstance.IIS_Config.RegNet.Commands = FunConvertStringToCmdLines(txtRegNet);
            }
            if (chkAutoReRegWcf.Checked)
            {
                DeployHelper.DeployConfigCacheInstance.IIS_Config.RegWCF.Commands = FunConvertStringToCmdLines(txtRegWcf);
            }
            #endregion

            var saveResult = DeployHelper.DeployConfigCacheInstance?.SaveConfig();
            if (!saveResult.Item1)
            {
                throw new Exception($"配置文件保存出错！{saveResult.Item2}");
            }
        }

        private void TxtWebSiteName_TextChanged(object sender, EventArgs e)
        {
            lblFullSiteName.Text = $"Default Web Site/{txtWebSiteName.Text.Trim()}";
        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            try
            {
                if (m_CurrentDeployType == DeployTypes.SQL)
                {
                    tcSetting.SelectedTab = tpSQL;
                }

                cmbNetFrameworks.DataSource = CommUtils.GetFrameworks();
                cmbPipeType.DataSource = CommUtils.GetManagedPipelineModeTypes();
                //  txtAppPoolName.Text = DeployHelper.DeployConfigCacheInstance?.IIS_Config?.AppPoolConfig?.PoolName;
                txtAppPoolName.Text = "U8AndroidAppPool";
                if (DeployHelper.DeployConfigCacheInstance?.IIS_Config?.AppPoolConfig?.ManagedPipelineMode == ManagedPipelineMode.Classic)
                {
                    cmbPipeType.Text = "经典";
                }
                else
                {
                    cmbPipeType.Text = "集成";
                }
                txtWebSiteName.Text = "U8Android";
                //txtWebSiteName.Text = DeployHelper.DeployConfigCacheInstance?.IIS_Config?.VirtualSiteName;
                //txtSitePhysicPath.Text = DeployHelper.DeployConfigCacheInstance?.IIS_Config?.PhysicalPath;
                txtSitePhysicPath.Text= @"C:\U8Android";
                txtBackUpPath.Text = "跟随系统";//这个目前不做配置！
                txtPorts.Text = new Func<string>(
                    () =>
                    {
                        string cPortsTmp = string.Empty;
                        foreach (var item in DeployHelper.DeployConfigCacheInstance?.IIS_Config?.Ports)
                        {
                            cPortsTmp += $"{item.ToString()},";
                        }
                        return cPortsTmp.TrimEnd(new char[] { ',' });
                    })();

                txtMIMEs.Text = "\".apk\",\"application / vnd.android.package - archive\"";

                //List<ApplicationPool> app =
                //IISHelper.GetServerCurrentAppPools((A) => { return A.Enable32BitAppOnWin64; });
                //comboBox3.DataSource = app;
                //comboBox3.ValueMember = "Name";
                //comboBox3.DisplayMember = "Name";

                #region 添加虚拟目录用户权限
                chkControlUserAuth.Checked = DeployHelper.DeployConfigCacheInstance?.IIS_Config?.VirtualSiteUserAuth?.IsAddUserAuth ?? true;
                txtUsersToControlAuth.Text = new Func<string>(
                   () =>
                   {
                       var vUsers = DeployHelper.DeployConfigCacheInstance?.IIS_Config?.VirtualSiteUserAuth?.Users ?? ConstantValues.DEFAULT_SITE_ADDAUTH_USERS;
                       string cUsersTmp = string.Empty;
                       foreach (var item in vUsers)
                       {
                           cUsersTmp += $"{item.ToString()},";
                       }
                       return cUsersTmp.TrimEnd(new char[] { ',' });
                   })();
                if (DeployHelper.DeployConfigCacheInstance?.IIS_Config?.VirtualSiteUserAuth?.IsAddUserAuth ?? true)
                {
                    txtUsersToControlAuth.Enabled = true;
                }
                else
                {
                    txtUsersToControlAuth.Enabled = false;
                }
                #endregion

                #region SQL

                txtDbConnectionTimeout.Text = DeployHelper.DeployConfigCacheInstance?.SQL_Config?.ConnectionTimeout.ToString();

                #endregion

                #region ReReg
                //Func<BaseCommandItem, string> vFunGetCmdLines = (A) =>
                //  {
                //      var vCmds = A?.Commands;
                //      string cCmdsTmp = string.Empty;
                //      foreach (var item in vCmds)
                //      {
                //          cCmdsTmp += $"{item.ToString()}{System.Environment.NewLine}";
                //      }
                //      return cCmdsTmp.TrimEnd(new char[] { '\r', '\n' });
                //  };

                chkAutoReRegNet.Checked = DeployHelper.DeployConfigCacheInstance?.IIS_Config?.RegNet.IsCmdRequired ?? false;
                chkAutoReRegWcf.Checked = DeployHelper.DeployConfigCacheInstance?.IIS_Config?.RegWCF.IsCmdRequired ?? false;
                txtRegNet.Text = FunGetCmdLines(DeployHelper.DeployConfigCacheInstance?.IIS_Config?.RegNet);
                txtRegWcf.Text = FunGetCmdLines(DeployHelper.DeployConfigCacheInstance?.IIS_Config?.RegWCF);
                #endregion
            }
            catch (Exception ex)
            {
                (new MessageDialog()).ShowError(ex.Message);
            }
        }        

        private void txtSitePhysicPath_ZoomClick(object sender, EventArgs e)
        {
            #region 选择文件夹
            var cSelectedDirectoryPath = "";
            using (var fsd = new FolderSelectDialog())
            {
                fsd.Title = "选择Web站点物理路径文件夹";
                if (fsd.ShowDialog(this.Handle))
                {
                    cSelectedDirectoryPath = fsd.FileName;
                }
            }
            if (string.IsNullOrWhiteSpace(cSelectedDirectoryPath) || !Directory.Exists(cSelectedDirectoryPath))
            {
                return;
            }
            txtSitePhysicPath.Text = cSelectedDirectoryPath;
            #endregion
        }

        private async void btnRegNet_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtRegNet.Text.Trim()))
                {
                    if ((new MessageDialog()).ShowConfirm($"现在执行重注册吗？") == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        BeginInvoke(new Action(() =>
                        {
                            IsReRegIisRunning = true;
                            /*picLoadingRegNet.Visible = true;*/
                            //this.Enabled = false;
                            gpReRegNet.Enabled = false;
                            tslblPublishingStatus.Visible = true;
                        }));

                        //new Task( () => {  Thread.Sleep(5000); }).Start();


                        IISRegNet vRegParam = new IISRegNet(true, FunConvertStringToCmdLines(txtRegNet));
                        //DeployHelper.DeployConfigCacheInstance.IIS_Config.RegNet.Commands = FunConvertStringToCmdLines(txtRegNet);
                        var vRegRtn = await IISHelper.IISReRegNet(vRegParam);
                        if (vRegRtn)
                        {
                            (new MessageDialog()).ShowInfo("注册成功！");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                (new MessageDialog()).ShowError(ex.Message);
            }
            finally
            {
                BeginInvoke(new Action(() => { IsReRegIisRunning = false; picLoadingRegNet.Visible = false; this.Enabled = true; gpReRegNet.Enabled = true; tslblPublishingStatus.Visible = false; }));
            }
        }

        private async void btnRegWcf_Click(object sender, EventArgs e)
        {
            try
            {
                if ((new MessageDialog()).ShowConfirm($"现在执行重注册吗？") == DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtRegNet.Text.Trim()))
                    {
                        BeginInvoke(new Action(() => { IsReRegWcfRunning = true; /*picLoadingRegWcf.Visible = true*/; this.Enabled = false; gpReRegWcf.Enabled = false; tslblPublishingStatus.Visible = true; }));
                        IISRegWCF vRegParam = new IISRegWCF(true, FunConvertStringToCmdLines(txtRegWcf));
                        //DeployHelper.DeployConfigCacheInstance.IIS_Config.RegWCF.Commands = FunConvertStringToCmdLines(txtRegWcf);
                        var vRegRtn = await IISHelper.IISReRegWcf(DeployHelper.DeployConfigCacheInstance.IIS_Config.RegWCF);
                        if (vRegRtn)
                        {
                            (new MessageDialog()).ShowInfo("注册成功！");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                (new MessageDialog()).ShowError(ex.Message);
            }
            finally
            {
                BeginInvoke(new Action(() => { IsReRegWcfRunning = false; picLoadingRegWcf.Visible = false; this.Enabled = true; gpReRegWcf.Enabled = true; tslblPublishingStatus.Visible = false; }));
            }
        }

        private void gpReRegNet_CollapseBoxClickedEvent(object sender)
        {
            if (!IsReRegIisRunning)
            {
                picLoadingRegNet.Visible = false;
            }
        }

        private void gpReRegWcf_CollapseBoxClickedEvent(object sender)
        {
            if (!IsReRegWcfRunning)
            {
                picLoadingRegWcf.Visible = false;
            }
        }

        #endregion
    }
}
