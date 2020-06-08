#region Using
using NLog;
using NLog.Config;
using NLog.Windows.Forms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechScan.Tool.U8.ServiceDeploy.Base.Exceptions;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils;
using TechScan.Tool.U8.ServiceDeploy.IIS.Impl;
using TechScan.Tool.U8.ServiceDeployWin.Models;
using TechScan.Tool.U8.ServiceDeployWin.Reports;
using TechScan.Tool.U8.ServiceDeployWin.Util;
using System.Collections.Generic;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;
using System.Linq;
using TechScan.Tool.U8.ServiceDeployWin.Properties;
using PrintNiceLabel.VICO.Controls;
using TechScan.Tool.U8.ServiceDeployWin.SQL;

#if TOAST_SHOW
using ToastHelper;
#endif

#endregion

namespace TechScan.Tool.U8.ServiceDeployWin.Deploy
{
    /// <summary>
    /// Web服务部署
    /// </summary>
    public partial class frmIISDeploy : Form
    {
        #region Fields & Property

        #region 当前回滚包对象
        /// <summary>
        /// 当前回滚包对象
        /// </summary>
        private IISDeployHistoryItem m_currentServiceRollBackItem;
        #endregion
        /// <summary>
        /// 是否正在部署中
        /// （正在部署时不允许关掉窗口）
        /// </summary>
        private bool m_IsRunning = false;
        /// <summary>
        /// 是否首次部署
        /// </summary>
        private bool m_IsFirstDeploy;

#if TOAST_SHOW
        NotificationService notificationService = new NotificationService();
#endif
        private string m_IconPath = "";

        /// <summary>
        /// Nlog对象
        /// </summary>
        private NLog.Logger nlog_iis;

        /// <summary>
        /// 包文件地址
        /// </summary>
        private string PackageFileName
        {
            get
            {
                return lblSelectedPackagePath.Text.Trim();
            }
        }
        /// <summary>
        /// 当前的部署包对象
        /// </summary>
        private IISPackageZipItem CurrentPkg;

        #endregion

        #region Ctor
        public frmIISDeploy(bool isFirstDeploy = false)
        {
#if IsMutipleLanguage
            LoadLanguage();
#endif
            m_IsFirstDeploy = isFirstDeploy;
            InitializeComponent();
#if TOAST_SHOW
            notificationService.Init("TechScan.U8.ServiceDeploy");
#endif
            //SaveIcon();
            NlogConfig();
            AddEvents();
        }

        #endregion

        #region Methods

        #region 添加界面事件处理
        /// <summary>
        /// 添加界面事件处理
        /// </summary>
        private void AddEvents()
        {
            //this.Paint += FrmIISDeploy_Paint;
            pnlMainPublish.DragEnter += PnlMainPublish_DragEnter;
            pnlMainPublish.DragDrop += PnlMainPublish_DragDrop;
            this.Load += FrmIISDeploy_Load;
            this.FormClosing += OnFormClosing;
        }
        #endregion

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (m_IsRunning)
                {
                    e.Cancel = true;
                }
                else
                {
                    if (!string.IsNullOrEmpty(rich_iis_log?.Text))
                    {
                        rich_iis_log?.SaveFile(System.IO.Path.Combine(DeployHelper.Deploy_IIS_LogPath, $"{System.DateTime.Now.ToString("yyyyMMddHHmmss")}.log"), RichTextBoxStreamType.PlainText);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void FrmIISDeploy_Load(object sender, EventArgs e)
        {
            try
            {
                Enum.GetValues(typeof(PackageType)).Cast<PackageType>().ForEach((s) => cmbPkgType.Items.Add(s));
                InitScreen();
                LoadServerSysInfo();
#region 首次自动化部署

                if (m_IsFirstDeploy)
                {
                    m_currentServiceRollBackItem = null;
                    lblSelectedPackagePath.Text = DeployHelper.FirstDeploy_IIS_Pkg_FileName;
                    ShowPackageInfo();
                    lblSelectedPackagePath.Visible = true;
                    btnPublish.Enabled = true;
                    OnPublishClickAsync(btnPublish, null);
                }

#endregion
            }
            catch (Exception ex)
            {
                MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        #region NLog配置
        private void NlogConfig()
        {
#region Nlog
            var config = new LoggingConfiguration();
            var richTarget = new RichTextBoxTarget
            {
                Name = "rich_iis_log",
                Layout =
                    "${date:format=yyyy\\:MM\\:dd HH\\:mm\\:ss}|${uppercase:${level}}|${message} ${exception:format=tostring} ${rtb-link:inner=${event-properties:item=ShowLink}}",
                FormName = nameof(frmIISDeploy),
                ControlName = "rich_iis_log",
                AutoScroll = true,
                MaxLines = 0,
                AllowAccessoryFormCreation = false,
                SupportLinks = true,
                UseDefaultRowColoringRules = true

            };
            config.AddTarget("rich_iis_log", richTarget);
            LoggingRule rule1 = new LoggingRule("*", LogLevel.Debug, richTarget);
            config.LoggingRules.Add(rule1);

            LogManager.Configuration = config;

            nlog_iis = NLog.LogManager.GetLogger("rich_iis_log");

            RichLogInit();

#endregion
        }
        private void RichLogInit()
        {
            RichTextBoxTarget.ReInitializeAllTextboxes(this);
            RichTextBoxTarget.GetTargetByControl(rich_iis_log).LinkClicked += LinkClicked;
        }
        private void LinkClicked(RichTextBoxTarget sender, string linktext, LogEventInfo logevent)
        {
            BeginInvokeLambda(() =>
            {
                try
                {
                    if (linktext.StartsWith("http") || linktext.StartsWith("file:"))
                    {
                        ProcessStartInfo sInfo = new ProcessStartInfo(linktext);
                        Process.Start(sInfo);
                    }
                    else
                    {
                        System.Windows.Forms.Clipboard.SetText(linktext);
                        MessageBoxEx.Show(this, "copy to Clipboard success", sender.Name);
                    }
                }
                catch (Exception)
                {
                    //ignore
                }
            }
            );
        }
        #endregion

        private void SaveIcon()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();// typeof(frmIISDeploy).Assembly;
            using (Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Resources.install.ico"))
            {
                if (stream != null)
                {
                    this.Icon = new Icon(stream);
                    m_IconPath = Path.Combine(DeployHelper.DeployDataPath, "ServiceInstall.ico");
                    try
                    {
                        using (var stream2 = new System.IO.FileStream(m_IconPath, System.IO.FileMode.Create))
                        {
                            this.Icon.Save(stream2);
                        }
                    }
                    catch (Exception)
                    {
                        m_IconPath = null;
                    }
                }
            }
        }
        private void BeginInvokeLambda(Action action)
        {
            BeginInvoke(action, null);
        }
        private void Notice(string title, string message)
        {
#if TOAST_SHOW
            BeginInvokeLambda(() =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(this.m_IconPath))
                    {
                        notificationService.Notify(this.m_IconPath, title, message);
                    }
                    else
                    {
                        notificationService.Notify(title, message);
                    }

                }
                catch (Exception)
                {
                }
            });
#endif
        }

#region 文件拖动
        private void PnlMainPublish_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                Array file = (System.Array)e.Data.GetData(DataFormats.FileDrop);
                if (file.Length > 1)
                {
                    MessagesHelper.ShowMessage("不可以选择多个文件！");
                    return;
                }
                string cFileName = file.GetValue(0).ToString();
                System.IO.FileInfo info = new System.IO.FileInfo(cFileName);
                //若为目录，则获取目录下所有子文件名
                if ((info.Attributes & System.IO.FileAttributes.Directory) != 0)
                {
                    //throw new Exception("暂时只支持文件夹打包！");
                    e.Effect = DragDropEffects.None;
                }
                else
                {
                    if (!info.Extension.ToUpper().Equals(".ZIP"))
                    {
                        e.Effect = DragDropEffects.None;
                    }
                    else
                    {
                        lblSelectedPackagePath.Visible = true;
                        btnPublish.Enabled = true;
                        lblSelectedPackagePath.Text = cFileName;
                        ShowPackageInfo();
                    }
                }
            }
            catch (Exception ex)
            {
                MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Error);
            }
        }

        private void PnlMainPublish_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
#endregion

        private void FrmIISDeploy_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;   //实例化Graphics 对象g
            Color FColor = Color.White; //颜色1
            Color TColor = this.BackColor;
            Brush b = new LinearGradientBrush(this.ClientRectangle, FColor, TColor, LinearGradientMode.Vertical);  //实例化刷子，第一个参数指示上色区域，第二个和第三个参数分别渐变颜色的开始和结束，第四个参数表示颜色的方向。
            g.FillRectangle(b, this.ClientRectangle);  //进行上色
        }

        /// <summary>
        /// 加载服务器系统信息
        /// </summary>
        private void LoadServerSysInfo()
        {
            this.lblMachineName.Text = Environment.MachineName;
            this.lblOsVersion.Text = $"{SystemInfoHelper.GetOsVersion(Environment.OSVersion.Version)} {Environment.OSVersion.ServicePack}";
            this.lblUserName.Text = Environment.UserName;
            this.lblSystemDir.Text = Environment.SystemDirectory;
            this.lblPlatform.Text = Environment.OSVersion.Platform.ToString();
            this.lblProcesserCounter.Text = Environment.ProcessorCount.ToString();
            this.lblSystemType.Text = Environment.Is64BitOperatingSystem ? "64bit" : "32bit";
            this.lblProcessType.Text = Environment.Is64BitProcess ? "64bit" : "32bit";
            this.lblCurDir.Text = Environment.CurrentDirectory;
            this.lblWorkSet.Text = $"{SystemInfoHelper.GetPhisicalMemory().ToString()}MB";
        }

        #region 部署包添加&显示

        /// <summary>
        /// 选择Web服务包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPackage_Click(object sender, EventArgs e)
        {
            AddPackageToPanel();
        }

        /// <summary>
        /// 添加部署包
        /// </summary>
        private void AddPackageToPanel()
        {
            try
            {
#region 检查

                //todo
                //是否正在发布等

#endregion

#region 选择包文件

                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "选择Android的Web服务包(zip)";
                    ofd.Multiselect = false;
                    ofd.Filter = "(*.zip)|*.zip";
                    if (ofd.ShowDialog(this) == DialogResult.OK)
                    {
                        lblSelectedPackagePath.Text = ofd.FileName;
                        ShowPackageInfo();
                        lblSelectedPackagePath.Visible = true;
                        btnPublish.Enabled = true;
                    }
                }
#endregion
            }
            catch (Exception ex)
            {
                MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 显示部署包信息
        /// </summary>
        private void ShowPackageInfo()
        {
            if (!string.IsNullOrEmpty(PackageFileName))
            {
                using (IISPackageRepository iisPackage = new IISPackageRepository())
                {
                    var vValue = iisPackage.GetPackagesDetailInfo(PackageFileName);
                    if (vValue.Item1)
                    {
                        var iList = vValue.Item3[0];
                        CurrentPkg = iList[PackageFileName];
                        if (CurrentPkg?.PackType != DeployTypes.IIS)
                        {
                            lblSelectedPackagePath.Text = string.Empty;
                            throw new SQLDeployBaseException("请选择正确的U8Android服务部署包！");
                        }
                        lblPKInfo_PublishVersion.Text = CurrentPkg?.Version;
                        lblPKInfo_PublishDateTime.Text = CurrentPkg?.PackageDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        lblPKInfo_PublishDescription.Text = CurrentPkg?.Description;
                        var pkType = CurrentPkg?.PkgType;
                        cmbPkgType.Text = pkType.ToString();//nameof(pkType);
                    }
                    else
                    {
                        throw new IISDeployBaseException(vValue.Item2);
                    }
                }
            }
        }

        #endregion

        #region 发布Web服务

        /// <summary>
        /// 窗口冻结
        /// </summary>
        /// <param name="bFrozen"></param>
        private void FrozenScreen(bool bFrozen)
        {
            tsTools.Enabled = !bFrozen;
            pnlMainPublish.AllowDrop = !bFrozen;
            btnAddPackage.Enabled = btnPublish.Enabled = !bFrozen;
        }

        /// <summary>
        /// 异步开始发布Web服务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnPublishClickAsync(object sender, EventArgs e)
        {
            try
            {
#region UI参数校验
                if (!System.IO.File.Exists(PackageFileName))
                {
                    throw new IISDeployBaseException("不存在的部署包文件！");
                }

#region 检查包是否有历史发布记录
                //如果有历史发布记录，则给出询问提示
                if (m_currentServiceRollBackItem == null)
                {
                    if (!m_IsFirstDeploy)
                    {
#region 包回滚时则不检查 - Myl 20190106
                        using (IISDeployHistoryRepository iisDeployHistory = new IISDeployHistoryRepository(HistoryType.DEPLOY_IIS, DeployHelper.DeployData_IISHistoryPath))
                        {
                            var hisDeployItem = iisDeployHistory.Details?.FirstOrDefault((A) =>
                             {
                                 var vHistoryFileNameTmp = Path.GetFileNameWithoutExtension(A.HistoryPackageFileFullName);
                                 return Path.GetFileNameWithoutExtension(PackageFileName).ToUpper().Equals(vHistoryFileNameTmp.ToUpper());
                             });

                            if (hisDeployItem != null)
                            {
                                if ((new MessageDialog()).ShowConfirm($"文件包{Path.GetFileNameWithoutExtension(PackageFileName)}已经部署过，是否重新部署？") == DialogResult.No)
                                {
                                    return;
                                }
                            }
                        }
#endregion
                    }
                }

#endregion

#region 判断当前部署的版本是否低于历史，给出相应提示询问
                //后期扩展todo
#endregion

#endregion

#region 弹框添加用户自定义发布描述

                var deployDescription = $"{lblUserName.Text.Trim()}{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}{System.DateTime.Now.ToString("yyyyMMddHHmmss")}";
                if (!m_IsFirstDeploy)
                {
                    var confirmResult = MessagesHelper.ShowInputMsgBox($"添加{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}描述", $"请添加一个文本描述!{System.Environment.NewLine}（标记当前服务{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}的描述信息）");
                    if (confirmResult.Item1)
                    {
                        deployDescription = confirmResult.Item2;
                    }
                    else
                    {
                        if (m_currentServiceRollBackItem != null)
                        {
                            btnPublish.Enabled = false;
                        }
                        return;
                    }
                }
                else
                {
                    deployDescription = $"{lblUserName.Text.Trim()}首次发布！";
                }

#endregion

#region 执行发布

                FrozenScreen(true);

                tslblPublishResult.Visible = false;
                tslblPublishingStatus.Visible = true;
                tspbProgress.Value = 0;
                tspbProgress.Visible = true;

#region 添加Circle等待动画

                //await Task.Run(() =>
                // {
                //     BeginInvokeLambda(() =>
                //     {
                //         Controls.SpinningCircles picLoading = new Tool.Controls.SpinningCircles()
                //         {
                //             BackColor = Color.Transparent,
                //             FullTransparent = true,
                //             Increment = 1F,
                //             Location = new System.Drawing.Point(161, 101),
                //             N = 8,
                //             Name = "picDeployLoading",
                //             Radius = 2.5F,
                //             Size = new System.Drawing.Size(90, 100),
                //             Text = "DeployLoading",
                //             Visible = true
                //         };
                //         this.Controls.Add(picLoading);
                //         picLoading.Dispose();
                //     });
                // });

                //BeginInvokeLambda(() => 
                //{
                //    #region ProgressBar显示
                //    while (true)
                //    {
                //        if (tspbProgress.Value == tspbProgress.Maximum)
                //        {
                //            tspbProgress.Value = 0;
                //        }

                //        tspbProgress.PerformStep();
                //        System.Threading.Thread.Sleep(300);
                //    }
                //    #endregion
                //});

                picDeployLoading.Visible = true;

#endregion

#endregion

                BeginInvokeLambda(() =>
                {
                    m_IsRunning = true;
                    tspbProgress.Value = 0;
                    tmProgress.Start();
                });

                this.nlog_iis.Info($"----开始Web服务{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}[{Path.GetFileNameWithoutExtension(PackageFileName)}]----");
                LogEventInfo publisEvent = new LogEventInfo(LogLevel.Info, "", "【IIS包地址】 ==> ");
                //var currentLogPath = @"C:\Users\Myl\AppData\Roaming\TechScanServiceDeploy\aa.log";
                //this.BeginInvokeLambda(() =>
                //{
                //    rich_iis_log.SaveFile(currentLogPath, RichTextBoxStreamType.PlainText);
                //});
                publisEvent.LoggerName = "rich_iis_log";
                publisEvent.Properties["ShowLink"] = "file://" + PackageFileName.Replace("\\", "\\\\");
                nlog_iis.Log(publisEvent);

                var deployRtn = await Task.Run<Tuple<bool, string>>(async () =>
                 {
                     using (IISPublishRepository publish = new IISPublishRepository())
                     {
                         publish.ConfigData = m_currentServiceRollBackItem == null ? DeployHelper.DeployConfigCacheInstance.IIS_Config : m_currentServiceRollBackItem.ConfigData;
                         publish.HistoryDataDir = DeployHelper.DeployData_IISHistoryPath;
                         publish.DeployParamData = new IISDeployParams(PackageFileName, m_currentServiceRollBackItem == null ? PublishTypes.General : PublishTypes.Rollback)
                         {
                             DeployDescription = deployDescription,
                             //DeployVersion = m_currentServiceRollBackItem?.DeployParam?.DeployVersion ?? null
                         };
                         publish.Log = FormOpUtils.GetCommLog(nlog_iis);

#region 异步执行服务发布

                         return await publish.ExecDeployAsync((S) =>
                         {
                             BeginInvokeLambda(() =>
                             {
                                 tslblPublishingStatus.Text = S;
                                 //if (tspbProgress.Value == tspbProgress.Maximum)
                                 //{
                                 //    tspbProgress.Value = 0;
                                 //}
                                 //tspbProgress.PerformStep();
                             });
                         });

#endregion
#region 同步执行服务发布 20200107Remark
                         //return publish.ExecDeploy((S) =>
                         //{
                         //    BeginInvokeLambda(() =>
                         //    {
                         //        tslblPublishingStatus.Text = S;
                         //        if (tspbProgress.Value == tspbProgress.Maximum)
                         //        {
                         //            tspbProgress.Value = 0;
                         //        }
                         //        tspbProgress.PerformStep();
                         //    });
                         //});
#endregion
                     }
                 });
                FrozenScreen(false);
                tslblPublishResult.Visible = true;
                tslblPublishingStatus.Visible = false;
                tspbProgress.Value = 0;
                tspbProgress.Visible = false;
                picDeployLoading.Visible = false;
                BeginInvokeLambda(() =>
                {
                    m_IsRunning = false;
                    tspbProgress.Value = 0;
                    tmProgress.Stop();
                });
                if (deployRtn.Item1)
                {
                    btnPublish.Enabled = false;
                    tslblPublishResult.Text = $"{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}成功";
                    tslblPublishResult.Image = Resources.success;
                    nlog_iis.Info($"AndroidWeb服务{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}成功!");
                    publisEvent = new LogEventInfo(LogLevel.Info, "", "【Web服务地址】 ==> ");
                    publisEvent.LoggerName = "rich_iis_log";

                    publisEvent.Properties["ShowLink"] = "http://" + deployRtn.Item2.Replace("\\", "\\\\");
                    nlog_iis.Log(publisEvent);


#region 首次发布时自动弹框到SQL发布

                    if (m_IsFirstDeploy)
                    {
                        using (frmSQLDeploy mFrmSql = new SQL.frmSQLDeploy(true))
                        {
                            mFrmSql.ShowDialog();
                        }
                        this.Close();
                    }
                    else
                    {
                        MessagesHelper.ShowMessage($"AndroidWeb服务{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}成功！{System.Environment.NewLine}http://{deployRtn.Item2}", MessageBoxIcon.Information);
                    }
#endregion
                }
                else
                {
                    if (m_currentServiceRollBackItem != null)
                    {
                        btnPublish.Enabled = false;
                    }
                    tslblPublishResult.Text = $"{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}失败";
                    nlog_iis.Error($"{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}失败：{deployRtn.Item2}");
                    tslblPublishResult.Image = Resources.fail;
                    MessagesHelper.ShowMessage($"{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}失败：{deployRtn.Item2}", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                FrozenScreen(false);
                if (m_currentServiceRollBackItem != null)
                {
                    btnPublish.Enabled = false;
                }
                tslblPublishResult.Visible = true;
                tslblPublishingStatus.Visible = false;
                tspbProgress.Value = 0;
                tspbProgress.Visible = false;
                picDeployLoading.Visible = false;

#region 记录NLog
                nlog_iis.Log(new LogEventInfo(LogLevel.Error, "rich_iis_log", $"【{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}失败】 ==> ")
                {
                    Message = ex.Message,
                    LoggerName = "rich_iis_log",
                    Exception = ex
                });

#endregion

                //nlog_iis.Error($"发布失败：{ex.Message}");

                tslblPublishResult.Text = $"{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}失败";
                tslblPublishResult.Image = Resources.fail;
                MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Error);
            }
            finally
            {
                BeginInvokeLambda(() =>
                {
                    m_IsRunning = false;
                    tspbProgress.Value = 0;
                    tmProgress.Stop();
                });
                m_currentServiceRollBackItem?.Dispose();
                m_currentServiceRollBackItem = null;
                Cursor.Current = Cursors.Default;
            }
        }
#endregion

        #region 多语言支持--后期有必要再说
        [Conditional("IsMutipleLanguage")]
        private void LoadLanguage(string lngname = "zh-CN")
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture =
                new System.Globalization.CultureInfo(lngname);

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(this.GetType());
            resources.ApplyResources(this, "$this");
            AppLang(this, resources);

        }
        /// <summary>
        /// 遍历菜单
        /// </summary>
        /// <param name="item"></param>
        /// <param name="resources"></param>
        private static void AppLang(ToolStripMenuItem item, System.ComponentModel.ComponentResourceManager resources)
        {
            if (item is ToolStripMenuItem)
            {
                resources.ApplyResources(item, item.Name);
                ToolStripMenuItem tsmi = (ToolStripMenuItem)item;
                if (tsmi.DropDownItems.Count > 0)
                {
                    foreach (ToolStripMenuItem c in tsmi.DropDownItems)
                    {
                        //if (tsmi != ToolStripSeparator)
                        //{ }
                        AppLang(c, resources);
                    }
                }
            }
        }
        private static void AppLang(System.Windows.Forms.Control control, System.ComponentModel.ComponentResourceManager resources)
        {
            if (control is MenuStrip)
            {
                //将资源应用与对应的属性
                resources.ApplyResources(control, control.Name);
                MenuStrip ms = (MenuStrip)control;
                if (ms.Items.Count > 0)
                {
                    foreach (ToolStripMenuItem c in ms.Items)
                    {
                        //调用 遍历菜单 设置语言
                        AppLang(c, resources);
                    }
                }
            }

            foreach (System.Windows.Forms.Control c in control.Controls)
            {
                resources.ApplyResources(c, c.Name);
                AppLang(c, resources);
            }
        }
        #endregion

        /// <summary>
        /// 版本回滚
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbRollBack_Click(object sender, EventArgs e)
        {
            //DeployHelper.DeployConfigCacheInstance.IIS_Config?.Ports?.Add(8989);
            //DeployHelper.DeployConfigCacheInstance.SaveConfig();

            using (frmHistory mFrm = new Reports.frmHistory(ServiceDeploy.Base.Enums.HistoryType.DEPLOY_IIS))
            {
                mFrm.ShowDialog();
                if (mFrm.SelectedRollbackHistoryItem != null)
                {
#region 确认回滚

                    var vServiceRollBackItem = mFrm.SelectedRollbackHistoryItem as IISDeployHistoryItem;
                    if (vServiceRollBackItem != null)
                    {
                        m_currentServiceRollBackItem = vServiceRollBackItem;
                        lblSelectedPackagePath.Text = m_currentServiceRollBackItem.HistoryPackageFileFullName;
                        ShowPackageInfo();
                        lblSelectedPackagePath.Visible = true;
                        btnPublish.Enabled = true;
                        OnPublishClickAsync(btnPublish, null);
                    }
#endregion
                }
            }
        }

        /// <summary>
        /// 帮助按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbHelp_Click(object sender, EventArgs e)
        {
            ////IISPublishRepository iss = new IISPublishRepository();
            ////iss.ExecDeploy((A) => { MessageBox.Show(A); });
            ////return;
            //this.nlog_iis.Info($"-----------------Start publish[Ver:1.0]-----------------");
            //nlog_iis.Debug("adfsadfasdfsdafasdf对对对");

            //this.nlog_iis.Error("publish error,please check build log");
            //this.nlog_iis.Info(@"C:\U8AndroidServiceTEST");

            //LogEventInfo publisEvent2 = new LogEventInfo(LogLevel.Info, "", "【Deploy log】 ==> ");
            //var currentLogPath = @"C:\Users\Myl\AppData\Roaming\TechScanServiceDeploy\aa.log";
            //this.BeginInvokeLambda(() =>
            //{
            //    rich_iis_log.SaveFile(currentLogPath, RichTextBoxStreamType.PlainText);
            //});
            //publisEvent2.LoggerName = "rich_iis_log";

            //publisEvent2.Properties["ShowLink"] = "file://" + currentLogPath.Replace("\\", "\\\\");
            //nlog_iis.Log(publisEvent2);

            ////Class112.ExecDeploy((A) => { nlog_iis..Error(A);
            ////    nlog_iis.Info(A);
            ////});

            //rich_iis_log.SaveFile(System.IO.Path.Combine(DeployHelper.DeployDataPath, "aa.log"), RichTextBoxStreamType.PlainText);

            //Class112.ExecDeploy((A, B) =>
            //{
            //    switch (A)
            //    {
            //        case ServiceDeploy.Base.Enums.DeployLogType.Trace:
            //            nlog_iis.Trace(B);
            //            break;
            //        case ServiceDeploy.Base.Enums.DeployLogType.Debug:
            //            nlog_iis.Debug(B);
            //            break;
            //        case ServiceDeploy.Base.Enums.DeployLogType.Info:
            //            nlog_iis.Info(B);
            //            break;
            //        case ServiceDeploy.Base.Enums.DeployLogType.Warn:
            //            nlog_iis.Warn(B);
            //            break;
            //        case ServiceDeploy.Base.Enums.DeployLogType.Error:
            //            nlog_iis.Error(B);
            //            break;
            //        case ServiceDeploy.Base.Enums.DeployLogType.Fatal:
            //            nlog_iis.Fatal(B);
            //            break;
            //        default:
            //            break;
            //    }
            //});
        }

        /// <summary>
        /// 清屏
        /// </summary>
        private void InitScreen()
        {
            //((IList<System.Windows.Forms.Control>)tpServerInfo.Controls).ForEach(A =>
            //{
            //    if (A.Name.StartsWith("lbl"))
            //    {
            //        A.Text = string.Empty;
            //    }
            //});
            //lblPKInfo_PublishVersion.Text = string.Empty;
            //lblPKInfo_PublishDateTime.Text = string.Empty;
            //lblPKInfo_PublishDescription.Text = string.Empty;
            CurrentPkg = null;
            tslblPublishResult.Visible = false;
            tslblPublishingStatus.Visible = false;
            tspbProgress.Value = 0;
            tspbProgress.Visible = false;
            picDeployLoading.Visible = false;

            FormOpUtils.InitCtls(new List<System.Windows.Forms.Control>() { tpServerInfo, tpPackageInfo });
        }

        /// <summary>
        /// 系统设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbSet_Click(object sender, EventArgs e)
        {
            using (frmSetting mFrm = new frmSetting(DeployTypes.IIS))
            {
                mFrm.ShowDialog();
            }
        }
       
        private void tmProgress_Tick(object sender, EventArgs e)
        {
            if (tspbProgress.Value == tspbProgress.Maximum)
            {
                tspbProgress.Value = 0;
            }
            tspbProgress.PerformStep();
        }

        private void cmsCopy_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(rich_iis_log.SelectedText))
                {
                    Clipboard.Clear();
                    Clipboard.SetData(DataFormats.Text, rich_iis_log.SelectedText);
                }
            }
            catch (Exception ex)
            {
                MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Error);
            }
        }

        private void cmsExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(rich_iis_log.Text))
                {
#region 选择包文件

                    SaveFileDialog sfd = new SaveFileDialog();
                    //设置文件类型
                    sfd.Filter = "日志文件（*.log）|*.log|文本文件（*.txt）|*.txt";
                    //设置默认文件类型显示顺序
                    sfd.FilterIndex = 1;
                    //保存对话框是否记忆上次打开的目录
                    sfd.RestoreDirectory = true;
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string localFilePath = sfd.FileName.ToString(); //获得文件路径
                        rich_iis_log.SaveFile(localFilePath, RichTextBoxStreamType.PlainText);
                        MessagesHelper.ShowMessage("日志导出成功！", MessageBoxIcon.Information);
                    }
#endregion
                }
            }
            catch (Exception ex)
            {
                MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Error);
            }
        }

#endregion
    }
}
