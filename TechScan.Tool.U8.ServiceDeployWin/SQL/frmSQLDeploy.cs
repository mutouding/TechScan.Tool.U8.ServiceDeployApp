#region Using

//#define LOAD_UTHER_DB_ROLES//是否加载其他数据库选项（表，视图等）

using NLog;
using NLog.Config;
using NLog.Windows.Forms;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechScan.Tool.U8.ServiceDeploy.Base.Exceptions;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils;
using TechScan.Tool.U8.ServiceDeploy.SQL.Config;
using TechScan.Tool.U8.ServiceDeployWin.Models;
using TechScan.Tool.U8.ServiceDeployWin.Reports;
using TechScan.Tool.U8.ServiceDeployWin.Util;
using System.Linq;
using TechScan.Tool.U8.ServiceDeploy.SQL.Impl;
using TechScan.Tool.U8.ServiceDeploy.SQL.Enums;
using System.Data.SqlClient;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;
using PrintNiceLabel.VICO.Controls;
using TechScan.Tool.U8.ServiceDeployWin.Properties;
using TechScan.Tool.U8.ServiceDeployWin.Deploy;

#if TOAST_SHOW
using ToastHelper;
#endif

#endregion

namespace TechScan.Tool.U8.ServiceDeployWin.SQL
{
    /// <summary>
    /// SQL脚本部署
    /// </summary>
    public partial class frmSQLDeploy : Form
    {
        #region Fields & Property

        #region Filelds：数据库相关
        private const string KeyName = "Name";
        private const string KeySchemaName = "SchemaName";
        private const string KeyServer = "|Server";
        private const string KeyLoading = "|Loading";
        private const string KeyDatabase = "|Database";
        private const string KeyTables = "|Tables";
        private const string KeyViews = "|Views";
        private const string KeyFunctions = "|Functions";
        private const string KeyTable = "|Table";
        private const string KeySp = "|SP";
        private const string KeyView = "|View";
        private const string KeyFunction = "|Function";
        private const string KeyTrigger = "|Trigger";
        private const string KeyAssembly = "|Assembly";
        private const string KeySPs = "|SPs";
        private const string KeyAssemblies = "|Assemblies";
        private const string KeyTriggers = "|Triggers";
        private const string KeyState = "State";
        private const string KeySpaceUsed = "SpaceUsed";
        private const string KeySpaceUsed2 = "SpaceUsed2";
        private const string KeyCount = "Count";
        private const string KeyCreateDate = "CreateDate";
        private const string KeyModifyDate = "ModifyDate";
        private const string KeyPath = "Path";
        private const string KeyValue = "Value";
        private const string KeyType = "Type";
        private const string KeyText = "Text";
        private string _previousObjectType = string.Empty;
        private string _previousDatabase = string.Empty;
        private ObjectModes _currentObjectMode = ObjectModes.None;
        private ObjectModes _previousObjectMode = ObjectModes.None;
        private string _currentObjectType = string.Empty;
        private DbServerInfo _currentServerInfo = new DbServerInfo();
        private DbServerInfo _previousServerInfo = new DbServerInfo();
        private string _currentDatabase = string.Empty;
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
        /// <summary>
        /// 当前回滚包对象
        /// </summary>
        private SQLDeployHistoryItem m_currentServiceRollBackItem;
        /// <summary>
        /// 当前的部署包对象
        /// </summary>
        private SQLPackageZipItem CurrentPkg;
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
#if TOAST_SHOW
        NotificationService notificationService = new NotificationService();
#endif
        private string m_IconPath = "";

        /// <summary>
        /// 日志对象
        /// </summary>
        private NLog.Logger nlog_iis;

        private SqlConnection NewConnection
        {
            get { return SqlHelper.CreateNewConnection(DefaultServerInfo); }
        }
        private static frmSQLDeploy _instance;
        //////internal Font SetFont()
        //////{
        //////    return new Font(Settings.Instance.EditorFont.Name, Settings.Instance.EditorFont.Size, Settings.Instance.EditorFont.Bold ? FontStyle.Bold : FontStyle.Regular);
        //////}
        internal static frmSQLDeploy Instance
        {

            get { return _instance; }
        }
        private bool _isInSearch;
        private bool _isUpdating;
        internal DbServerInfo CurrentServerInfo
        {
            get { return _currentServerInfo; }
        }
        internal ToolStripItem[] Commands
        {
            get
            {
                //return tsCommands.Items.Cast<ToolStripItem>().Where(t => t != tbProjectHomepage).ToArray();
                return null;
            }
        }
        private WorkModes _currentWorkMode = WorkModes.Summary;
        private const string CommandRefresh = "tbRefresh";
        private const int ImageIndexOnline = 5;
        private DataTable NewObjects
        {
            get
            {
                var objects = new DataTable();
                objects.Columns.Add(new DataColumn { ColumnName = KeyState });
                objects.Columns.Add(new DataColumn { ColumnName = KeyName, Caption = "Name" });
                objects.Columns.Add(new DataColumn { ColumnName = KeySpaceUsed, Caption = "Space Used", DataType = typeof(float) });
                objects.Columns.Add(new DataColumn { ColumnName = KeySpaceUsed2, Caption = "Space Used2", DataType = typeof(float) });
                objects.Columns.Add(new DataColumn { ColumnName = KeyCount, Caption = "Count", DataType = typeof(long) });
                objects.Columns.Add(new DataColumn { ColumnName = KeyCreateDate, Caption = "Create Date", DataType = typeof(DateTime) });
                objects.Columns.Add(new DataColumn { ColumnName = KeyModifyDate, Caption = "Modify Date", DataType = typeof(DateTime) });
                objects.Columns.Add(new DataColumn { ColumnName = KeyPath, Caption = "Path" });
                objects.Columns.Add(new DataColumn { ColumnName = KeyValue });
                objects.Columns.Add(new DataColumn { ColumnName = KeyType, DataType = typeof(ObjectModes) });
                return objects;
            }
        }
        #endregion

        #region Ctor
        public frmSQLDeploy(bool isFirstDeploy = false)
        {
#if IsMutipleLanguage
            LoadLanguage();
#endif
            InitializeComponent();
#if TOAST_SHOW
            notificationService.Init("TechScan.U8.ServiceDeploy");
#endif
            //SaveIcon();
            m_IsFirstDeploy = isFirstDeploy;
            NlogConfig();
            AddEvents();
            _instance = this;
        }
        #endregion

        #region Methods

        /// <summary>
        /// 添加界面事件处理
        /// </summary>
        private void AddEvents()
        {
            //this.Paint += FrmIISDeploy_Paint;
            pnlMainPublish.DragEnter += PnlMainPublish_DragEnter;
            pnlMainPublish.DragDrop += PnlMainPublish_DragDrop;
            this.Load += FrmIISDeploy_Load;
            this.FormClosing += FrmSQLDeploy_FormClosing;
        }
        private void FrmSQLDeploy_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_IsRunning)
            {
                e.Cancel = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(rich_iis_log?.Text))
                {
                    rich_iis_log?.SaveFile(System.IO.Path.Combine(DeployHelper.Deploy_SQL_LogPath, $"{System.DateTime.Now.ToString("yyyyMMddHHmmss")}.log"), RichTextBoxStreamType.PlainText);
                }
            }
        }
        private void FrmIISDeploy_Load(object sender, EventArgs e)
        {
            try
            {
                Enum.GetValues(typeof(PackageType)).Cast<PackageType>().ForEach((s) => cmbPkgType.Items.Add(s));
                LoadServerSysInfo();
                InitScreen();
                LoadServers();
            }
            catch (Exception ex)
            {
                MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Error);
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
                    "${date:format=HH\\:mm\\:ss}|${uppercase:${level}}|${message} ${exception:format=tostring} ${rtb-link:inner=${event-properties:item=ShowLink}}",
                FormName = nameof(frmSQLDeploy),
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
        public void RichLogInit()
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
                    else if (linktext.StartsWith("SQL:"))
                    {
                        #region SQL脚本定位 Myl20200114

                        using (SqlScriptShowForm frm = new SQL.SqlScriptShowForm(linktext.Substring(6)))
                        {
                            frm.ShowDialog();
                        }
                        #endregion
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

        #region 文件拖动-如果App以管理员方式运行，则不支持拖动
        private void PnlMainPublish_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                Array file = (System.Array)e.Data.GetData(DataFormats.FileDrop);
                if (file.Length > 1)
                {
                    MessageBox.Show("不可以选择多个文件！");
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
                MessageBox.Show(ex.Message);
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

        #region 部署包处理
        /// <summary>
        /// 选择SQL脚本包
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
                    ofd.Title = "选择SQL脚本包(zip)";
                    ofd.Multiselect = false;
                    ofd.Filter = "(*.zip)|*.zip";
                    if (ofd.ShowDialog(this) == DialogResult.OK)
                    {
                        lblSelectedPackagePath.Visible = true;
                        btnPublish.Enabled = true;
                        lblSelectedPackagePath.Text = ofd.FileName;
                        ShowPackageInfo();
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 显示部署包信息
        /// </summary>
        private void ShowPackageInfo()
        {
            if (!string.IsNullOrEmpty(PackageFileName))
            {
                using (SQLPackageRepository sqlPkg = new SQLPackageRepository())
                {
                    var vValue = sqlPkg.GetPackagesDetailInfo(PackageFileName);
                    if (vValue.Item1)
                    {

                        var iList = vValue.Item3[0];

                        CurrentPkg = iList[PackageFileName];
                        if (CurrentPkg?.PackType != DeployTypes.SQL)
                        {
                            lblSelectedPackagePath.Text = string.Empty;
                            throw new SQLDeployBaseException("请选择正确的SQL脚本部署包！");
                        }
                        lblPKInfo_PublishVersion.Text = CurrentPkg.Version;
                        lblPKInfo_PublishDateTime.Text = CurrentPkg.PackageDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        lblPKInfo_PublishDescription.Text = CurrentPkg.Description;
                        var pkType = CurrentPkg?.PkgType;
                        cmbPkgType.Text = pkType.ToString();
                    }
                    else
                    {
                        throw new SQLDeployBaseException(vValue.Item2);
                    }
                }
            }
        }
        #endregion

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
        /// 异步部署SQL脚本
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
                    throw new SQLDeployBaseException("不存在的部署包文件！");
                }

                #region 检查包是否有历史发布记录
                //如果有历史发布记录，则给出询问提示
                if (m_currentServiceRollBackItem == null)
                {
                    if (!m_IsFirstDeploy)
                    {
                        #region 包回滚时则不检查 - Myl 20190106
                        using (SQLDeployHistoryRepository sqlDeployHistory = new SQLDeployHistoryRepository(HistoryType.DEPLOY_SQL, DeployHelper.DeployData_SQLHistoryPath))
                        {
                            var hisDeployItem = sqlDeployHistory.Details?.FirstOrDefault((A) =>
                            {
                                var vHistoryFileNameTmp = Path.GetFileNameWithoutExtension(A.HistoryPackageFileFullName);
                                return Path.GetFileNameWithoutExtension(PackageFileName).ToUpper().Equals(vHistoryFileNameTmp.ToUpper());
                            });

                            if (hisDeployItem != null)
                            {
                                if ((new MessageDialog()).ShowConfirm($"脚本包{Path.GetFileNameWithoutExtension(PackageFileName)}已经部署过，是否重新部署？") == DialogResult.No)
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

                List<DbServerInfo> serversList = await GetSecectedDbServersAsync();

                if (serversList == null || serversList.Count == 0)
                {
                    throw new SQLDeployBaseException("请确定待部署的服务器和数据库！");
                }

                Func<string> GetSqlExcuteDbServerInfoMsgFunc =
                    () =>
                    {
                        var strMsg = "";
                        foreach (var item in serversList)
                        {
                            strMsg += $"服务器:[{item.Server}]-数据库[{item.Database}]{System.Environment.NewLine}";
                        }
                        return strMsg.TrimEnd(new char[] { '\r', '\n' });
                    };

                if (m_currentServiceRollBackItem == null)
                {
                    if (!MessagesHelper.ShowQuestion($"是否在以下账套内执行脚本？{System.Environment.NewLine}{GetSqlExcuteDbServerInfoMsgFunc()}"))
                    {
                        return;
                    }
                }

                #region 弹框添加用户自定义发布描述

                var deployDescription = $"{lblUserName.Text.Trim()}{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}{System.DateTime.Now.ToString("yyyyMMddHHmmss")}";
                if (!m_IsFirstDeploy)
                {
                    var confirmResult = MessagesHelper.ShowInputMsgBox($"添加{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}描述", $"请添加一个文本描述!{System.Environment.NewLine}（标记当前脚本{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}的描述信息）");
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

                this.nlog_iis.Info($"----开始SQL脚本{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}[{Path.GetFileNameWithoutExtension(PackageFileName)}]----");
                LogEventInfo publisEvent = new LogEventInfo(LogLevel.Info, "", "【SQL包地址】 ==> ");
                publisEvent.LoggerName = "rich_iis_log";
                publisEvent.Properties["ShowLink"] = "file://" + PackageFileName.Replace("\\", "\\\\");
                nlog_iis.Log(publisEvent);
                //List<DbServerInfo> serversList = await GetSecectedDbServersAsync();
                if (serversList?.Count == 0)
                {
                    throw new SQLDeployBaseException("请勾选有效的数据库连接对象！");
                }
                var deployRtn = await Task.Run<Tuple<bool, string>>(async () =>
                {
                    using (SQLPublishRepository publish = new SQLPublishRepository())
                    {
                        publish.ConfigData = m_currentServiceRollBackItem == null ? DeployHelper.DeployConfigCacheInstance.SQL_Config : m_currentServiceRollBackItem.ConfigData;
                        publish.HistoryDataDir = DeployHelper.DeployData_SQLHistoryPath;
                        publish.DeployParamData = new SQLDeployParams(PackageFileName, m_currentServiceRollBackItem == null ? PublishTypes.General : PublishTypes.Rollback)
                        {
                            DbServers = serversList,
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
                    tslblPublishResult.Text = $"SQL脚本{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}成功";
                    tslblPublishResult.Image = TechScan.Tool.U8.ServiceDeployWin.Properties.Resources.success;
                    nlog_iis.Info($"SQL脚本{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}成功!");
                    //publisEvent = new LogEventInfo(LogLevel.Info, "", "【SQL脚本地址】 ==> ");
                    //publisEvent.LoggerName = "rich_iis_log";
                    //publisEvent.Properties["ShowLink"] = "http://" + deployRtn.Item2.Replace("\\", "\\\\");
                    //nlog_iis.Log(publisEvent);
                    MessagesHelper.ShowMessage($"SQL脚本{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}成功！{System.Environment.NewLine}http://{deployRtn.Item2}", MessageBoxIcon.Information);
                    if (m_IsFirstDeploy)
                    {
                        this.Close();
                    }
                }
                else
                {
                    if (m_currentServiceRollBackItem != null)
                    {
                        btnPublish.Enabled = false;
                    }
                    tslblPublishResult.Text = $"SQL脚本{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}失败";
                    nlog_iis.Error($"SQL脚本{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}失败：{deployRtn.Item2}");
                    tslblPublishResult.Image = TechScan.Tool.U8.ServiceDeployWin.Properties.Resources.fail;
                    MessagesHelper.ShowMessage($"SQL脚本{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}失败：{deployRtn.Item2}", MessageBoxIcon.Error);
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

                tslblPublishResult.Text = $"SQL脚本{(m_currentServiceRollBackItem == null ? "发布" : "回滚")}失败";
                tslblPublishResult.Image = TechScan.Tool.U8.ServiceDeployWin.Properties.Resources.fail;
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

        # region 获取已经选择的数据库集合

        //private delegate List<DbServerInfo> GetSecectedDbServersHandler();
        /// <summary>
        /// 获取已经选择的数据库集合
        /// </summary>
        /// <returns></returns>
        private List<DbServerInfo> GetSecectedDbServers()
        {
            //var vDele = new GetSecectedDbServersHandler(() =>
            //{
            //    var dbServerList = new List<DbServerInfo>();

            //    var selectedServer = tvObjects.SelectedNode?.Tag as DbServerInfo;
            //    if (selectedServer == null)
            //    {
            //        return null;
            //    }

            //    if (string.IsNullOrEmpty(selectedServer.Database))
            //    {
            //        throw new SQLDeployBaseException("请勾选有效的数据库连接对象！");
            //    }
            //    dbServerList.Add(selectedServer);
            //    return dbServerList;
            //});

            var vRtn = tvObjects.BeginInvoke(new Func<List<DbServerInfo>>(() =>
              {
                  var dbServerList = new List<DbServerInfo>();

                  Action<TreeNode> actionTmp = new Action<TreeNode>((A) =>
                  {
                      //foreach (TreeNode eachNode in A.Nodes)
                      {
                          var vStateTmp = A.Tag as ServerState;
                          if (vStateTmp.Key.Equals(KeyDatabase) && A.Checked)
                          {
                              dbServerList.Add(vStateTmp);
                          }
                      }
                  });

                  foreach (TreeNode eachNode in tvObjects.Nodes)
                  {
                      var vStateTmp = eachNode.Tag as ServerState;
                      if (vStateTmp.Key.Equals(KeyDatabase) && eachNode.Checked)
                      {
                          dbServerList.Add(vStateTmp);
                      }
                      if (eachNode.Nodes?.Count > 0)
                      {
                          foreach (TreeNode item in eachNode.Nodes)
                          {
                              actionTmp(item);
                          }
                      }
                  }

                  //var selectedServer = tvObjects.SelectedNode?.Tag as DbServerInfo;
                  //if (selectedServer == null)
                  //{
                  //    return null;
                  //}
                  //if (string.IsNullOrEmpty(selectedServer.Database))
                  //{
                  //    throw new SQLDeployBaseException("请勾选有效的数据库连接对象！");
                  //}
                  //dbServerList.Add(selectedServer);
                  return dbServerList;
              }));
            var vServers = tvObjects.EndInvoke(vRtn);
            return vServers as List<DbServerInfo>;
        }
        /// <summary>
        /// 异步获取已经选择的数据库集合
        /// </summary>
        /// <returns></returns>
        private async Task<List<DbServerInfo>> GetSecectedDbServersAsync()
        {
            return await Task.Run<List<DbServerInfo>>(() => { return GetSecectedDbServers(); });
        }

        #endregion

        /// <summary>
        /// SQL脚本部署回滚
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
            }
        }

        private void tsbHelp_Click(object sender, EventArgs e)
        {


        }
        /// <summary>
        /// 清屏
        /// </summary>
        private void InitScreen()
        {
            CurrentPkg = null;
            tslblPublishResult.Visible = false;
            tslblPublishingStatus.Visible = false;
            tspbProgress.Value = 0;
            tspbProgress.Visible = false;
            picDeployLoading.Visible = false;
            lblPKInfo_PublishVersion.Text = string.Empty;
            lblPKInfo_PublishDateTime.Text = string.Empty;
            lblPKInfo_PublishDescription.Text = string.Empty;
        }

        private void tbNewConnection_ButtonClick(object sender, EventArgs e)
        {
            EditConnection();
        }

        private void LoadServer(DbServerInfo info)
        {
            var state = new ServerState { IsCloud = info.IsCloud, AuthType = info.AuthType, Server = info.Server, Database = info.Database, User = info.User, Password = info.Password, IsReady = false, Key = KeyServer };
            var node = new TreeNode { Text = info.Server, Name = info.Server, ImageIndex = 0, SelectedImageIndex = 0, Tag = state };
            tvObjects.Nodes.Add(node);
            node.Nodes.Add(new TreeNode { Text = "Loading...", Tag = new ServerState { Key = KeyLoading } });
        }
        private void EditConnection(DbServerInfo info = null)
        {
            #region 加载数据库连接
            using (var dlg = new ConnectionDialog(info))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    var server = dlg.Server;
                    DbServerInfo item;
                    var isNew = false;
                    if (info == null)
                    {
                        item = DeployHelper.DeployConfigCacheInstance.FindServer(server, dlg.UserName);
                    }
                    else
                    {
                        item = DeployHelper.DeployConfigCacheInstance.FindServer(info.Server, dlg.UserName);
                    }

                    if (item == null)
                    {
                        item = new DbServerInfo() { };
                        if (DeployHelper.DeployConfigCacheInstance?.SQL_Config?.DbServers == null)
                        {
                            DeployHelper.DeployConfigCacheInstance.SQL_Config.DbServers = new List<DbServerInfo>();
                        }
                        DeployHelper.DeployConfigCacheInstance?.SQL_Config?.DbServers?.Add(item);
                        isNew = true;
                    }

                    item.AuthType = dlg.AuthType;
                    item.Server = server;
                    item.Password = dlg.Password;
                    item.User = dlg.UserName;
                    var saveResult = DeployHelper.DeployConfigCacheInstance?.SaveConfig();
                    if (!saveResult.Item1)
                    {
                        throw new SQLDeployBaseException($"配置文件保存出错！{saveResult.Item2}");
                    }
                    if (info != null)
                    {
                        info.AuthType = item.AuthType;
                        info.Password = item.Password;
                        info.Server = item.Server;
                        info.User = item.User;
                    }
                    if (isNew)
                    {
                        LoadServer(item);
                    }
                    else
                    {
                        if (m_IsFirstDeploy)
                        {
                            LoadServer(item);
                        }
                    }
                }
            }
            #endregion
        }

        private void ShowObjects(object e)
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                var arg = e as TreeViewCancelEventArgs;
                var state = arg.Node.Tag as ServerState;
                if (arg.Node.Tag == null || (state != null && !state.IsReady))
                    arg.Cancel = !ShowObjects(arg.Node);
            }));
        }
        private DisposableState NewWait()
        {
            return new DisposableState(this, Commands);
        }
        
        private DataTable GetDatabasesInfo()
        {
            return SQLMgmtEngine.GetDatabasesInfo(DefaultServerInfo);
        }
        
        private DbServerInfo DefaultServerInfo
        {
            get { return GetServerInfo(string.Empty); }
        }
        private DbServerInfo GetServerInfo(string catalog)
        {
            return SQLMgmtEngine.GetServerInfo(CurrentServerInfo, catalog);
        }
        private TreeNode GetRootNode(TreeNode node)
        {
            var root = node;
            while (root != null && root.Parent != null)
            {
                root = root.Parent;
            }
            return root;
        }
        private DataTable GetObjects(string objectType)
        {
            string types;
            switch (objectType)
            {
                case KeyTriggers:
                    return Query("SELECT '' AS SchemaName, name, create_date AS CreateDate, modify_date AS ModifyDate, type FROM sys.triggers WITH (NOLOCK) WHERE parent_class = 0", CurrentServerInfo);
                default:
                    switch (objectType)
                    {
                        case KeyTables:
                            types = "'U'";
                            break;
                        case KeyViews:
                            types = "'V'";
                            break;
                        case KeyFunctions:
                            types = "'FN', 'IF', 'TF'";
                            break;
                        case KeySPs:
                            types = "'P'";
                            break;
                        default:
                            types = string.Empty;
                            break;
                    }
                    var filter = !string.IsNullOrEmpty(types) ? " WHERE so.type IN (" + types + ")" : string.Empty;
                    return GetObjectsFilter(filter);
            }
        }
        internal DataTable Query(string sql)
        {
            return Query(sql, DefaultServerInfo);
        }
        private DataSet QuerySet(string sql, DbServerInfo info)
        {
            //using (NewWait())
            {
                try
                {
                    return SqlHelper.QuerySet(sql, info);
                }
                catch (Exception ex)
                {
                    MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Error);
                    return null;
                }
            }
        }
        internal DataTable Query(string sql, DbServerInfo info)
        {
            var data = QuerySet(sql, info);
            if (data != null && data.Tables.Count > 0)
                return data.Tables[0];
            else
                return null;
        }
        private DataTable GetObjectsFilter(string filter)
        {
            return Query(string.Format("SELECT su.name AS SchemaName, so.name, so.create_date AS CreateDate, so.modify_date AS ModifyDate, so.type FROM sys.objects so WITH (NOLOCK) LEFT JOIN sys.schemas su WITH (NOLOCK) ON so.schema_id = su.schema_id {0} ORDER BY su.name, so.name", filter), CurrentServerInfo);
        }
        private void RefreshData()
        {
            RefreshData(true);
        }
        private void SaveSettings()
        {
            //var tcbActivityStatuses = FindCommand<ToolStripComboBox>(CommandActivityStatuses);
            //if (tcbActivityStatuses != null)
            //    Settings.Instance.ActivityState = (ActivityStatuses)tcbActivityStatuses.SelectedItem;
            //Settings.Instance.ActivityType = ActivitiesObjectType;
            //if (_currentServerInfo != null)
            //    Settings.Instance.LastServer = _currentServerInfo.Server;
            //Settings.Instance.AutoWordWrap = chkAutoWordWrap.Checked;
            //Settings.Instance.LogHistory = chkLogHistory.Checked;
            //Settings.Instance.AlertMailServer = cboAlertMailServers.Text;
            //Settings.Instance.AlertMailUser = txtAlertMailUser.Text;
            //Settings.Instance.AlertMailPassword = txtAlertMailPassword.Text;
            //Settings.Instance.AlertMailReceiver = txtAlertMailReceiver.Text;
            //Settings.Instance.AlertTemplate = rtbAlertTemplate.Text;
            //Settings.Instance.Save();
        }
        private void RefreshData(bool reload)
        {
            SaveSettings();
            LoadData(reload);
        }
        private void LoadData(bool reload)
        {
            _isUpdating = true;
            var sql = string.Empty;

            using (NewWait())
            {
                switch (_currentWorkMode)
                {
                    case WorkModes.TableData:
                        var tableData = tcMain.SelectedTab.Controls[0] as UserTableData;
                        tableData.Execute();
                        break;
                    case WorkModes.Query:
                        var queryData = tcMain.SelectedTab.Controls[0] as UserQuery;
                        queryData.Execute();
                        break;
                    default:
                        break;
                }
                _isUpdating = false;
            }
        }
        private void LoadServers()
        {
            if (m_IsFirstDeploy)
            {
                EditConnection();

                #region 首次部署

                m_currentServiceRollBackItem = null;
                lblSelectedPackagePath.Text = DeployHelper.FirstDeploy_SQL_Pkg_FileName;
                ShowPackageInfo();
                lblSelectedPackagePath.Visible = true;
                btnPublish.Enabled = true;
                #endregion
            }
            else
            {
                if ((DeployHelper.DeployConfigCacheInstance.SQL_Config?.DbServers?.Count ?? 0) == 0)
                {
                    #region 如果没有数据库连接则默认弹框让用户创建连接
                    EditConnection();
                    #endregion
                }
                else
                {
                    tvObjects.Nodes.Clear();
                    DeployHelper.DeployConfigCacheInstance.SQL_Config?.DbServers?.ForEach(s =>
                    {
                        LoadServer(s);
                    });
                }
            }
        }
        private void SetOnlineOffline(string database, bool isOnline)
        {
            using (NewWait())
            {
                SqlHelper.ExecuteNonQuery(string.Format("ALTER DATABASE [{0}] SET {1} WITH ROLLBACK IMMEDIATE", database, isOnline ? "ONLINE" : "OFFLINE"), isOnline ? DefaultServerInfo : GetServerInfo(database));
                if (tvObjects.SelectedNode != null)
                    tvObjects.SelectedNode.ImageIndex = ImageIndexOnline;
                RefreshData();
            }
        }
        private bool CheckCurrentServer()
        {
            return CurrentServerInfo != null && !string.IsNullOrEmpty(CurrentServerInfo.Server);
        }
        private DataTable GetDatabaseInfo(string database)
        {
            return SQLMgmtEngine.GetDatabaseInfo(DefaultServerInfo, database);
        }
        private bool LoadServer(TreeNode node)
        {
            if (CheckCurrentServer())
            {
                _currentDatabase = string.Empty;
                try
                {
                    var result = SqlHelper.ExecuteScalar("SELECT @@version", DefaultServerInfo);
                    var version = result != null ? result.ToString() : "(N/A)";
                    var lines = version.Split('\t').ToList();
                    if (lines.Count > 1)
                    {
                        var line = lines[1];
                        DateTime date;
                        if (DateTime.TryParse(line, out date))
                        {
                            lines.RemoveAt(1);
                            txtServerInstallationTime.Text = date.ToString();
                        }
                    }
                    var serverState = node.Tag as ServerState;
                    serverState.IsCloud = lines[0].IndexOf("Azure", StringComparison.InvariantCultureIgnoreCase) != -1;
                    txtVersion.Text = string.Join("\r\n", lines.ToArray());

                    try
                    {
                        DateTime serverStartTime;
                        SQLMgmtEngine.GetOsInfo(DefaultServerInfo, out serverStartTime);
                        txtServerStartTime.Text = serverStartTime.ToString();
                    }
                    catch (Exception)
                    {
                    }

                    result = SqlHelper.ExecuteScalar(string.Format("SELECT {0}", serverState.IsCloud ? "@@SERVERNAME" : "@@SERVICENAME"), DefaultServerInfo);
                    var serviceName = result != null ? result.ToString() : "(N/A)";
                    txtServerInstanceName.Text = serviceName;

                    result = SqlHelper.ExecuteScalar("SELECT ServerProperty('ProcessID')", DefaultServerInfo);
                    var processId = result != null ? result.ToString() : "(N/A)";
                    txtServerProcessID.Text = processId;

                    using (var connection = NewConnection)
                    {
                        connection.Open();
                        var data = connection.GetSchema("Databases");
                        node.Nodes.Clear();
                        var databases = GetDatabasesInfo();
                        data.AsEnumerable().OrderBy(r => r.Field<string>("database_name")).ForEach((d) =>
                        {
                            var name = d["database_name"].ToString();
                            var info = GetDatabaseInfo(name);
                            if (info != null && info.Rows.Count > 0)
                            {
                                var row = info.Rows[0];
                                var state = databases.AsEnumerable().FirstOrDefault(r => r["name"].ToString() == name);
                                if (state != null)
                                {
                                    //Myl 2020-01-09
                                    var isReady = state != null && Convert.ToInt32(state["state"]) == 0;
                                    var image = isReady ? ImageIndexOnline : 0;
                                    var tag = new ServerState
                                    {
                                        //Myl 2020-01-13 Add
                                        Key = KeyDatabase,
                                        IsReady = false,
                                        State = isReady,
                                        AuthType = serverState.AuthType,
                                        ConnectionTimeout = serverState.ConnectionTimeout,
                                        Database = name,
                                        IsCloud = serverState.IsCloud,
                                        IsEncrypted = serverState.IsEncrypted,
                                        Password = serverState.Password,
                                        Server = serverState.Server,
                                        User = serverState.User
                                    };
                                    var databaseNode = new TreeNode { Name = name, Text = name, ImageIndex = image, SelectedImageIndex = image, Tag = tag };
                                    node.Nodes.Add(databaseNode);
                                }
                            }
                        });

#if LOAD_UTHER_DB_ROLES

                        node.Nodes.Cast<TreeNode>().ForEach((n) =>
                        {
                            n.Nodes.AddRange(new[] { new TreeNode { Name = KeyTables, Text = "Tables", Tag = new ServerState { Key = KeyTables, IsReady = false } }
                                , new TreeNode { Name = KeyViews, Text = "Views", Tag = new ServerState { Key = KeyViews, IsReady = false } }
                                , new TreeNode { Name = KeyFunctions, Text = "Functions", Tag = new ServerState { Key = KeyFunctions, IsReady = false } }
                                , new TreeNode { Name = KeySPs, Text = "Stored Procedures", Tag = new ServerState { Key = KeySPs, IsReady = false } }
                                , new TreeNode { Name = KeyAssemblies, Text = "Assemblies", Tag = new ServerState { Key = KeyAssemblies, IsReady = false } }
                                , new TreeNode { Name = KeyTriggers, Text = "Triggers", Tag = new ServerState { Key = KeyTriggers, IsReady = false } }   });
                            n.Nodes.Cast<TreeNode>().ForEach((m) => m.SelectedImageIndex = 1);
                        });
#endif
                        connection.Close();
                    }
                    //LoadDatabase(Node);
                    var counts = node.Nodes.Count;
                    lblObjectCount.Text = counts.ToString();

                    _currentServerInfo.IsCloud = serverState.IsCloud;
                    //////////////////////////////ResetPerformance();

                    if (counts == 1 && (node.Nodes[0].Tag as ServerState).Key == KeyLoading)
                    {
                        node.Collapse();
                        return false;
                    }
                    else
                        return true;
                }
                catch (Exception ex)
                {
                    node.Collapse();
                    MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                node.Collapse();
                return false;
            }
        }
        private void OnObjectsAfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Label))
            {
                var tag = e.Node.Tag as ServerState;
                switch (tag.Key)
                {
                    case KeySp:
                    case KeyTable:
                    case KeyFunction:
                    case KeyDatabase:
                        if (MessagesHelper.ShowQuestion(string.Format("你确认更改数据库名称 {0} 为 {1}?", e.Node.Text, e.Label)))
                        {
                            string schemaName;
                            var newObjectName = SQLMgmtEngine.ParseObjectName(e.Label, out schemaName);
                            if (tag.Key == KeyDatabase)
                                SqlHelper.ExecuteNonQuery(string.Format("EXEC sp_renamedb N'{0}', N'{1}'", e.Node.Text, newObjectName), CurrentServerInfo);
                            else
                                SqlHelper.ExecuteNonQuery(string.Format("EXEC sp_rename '{0}', '{1}'", e.Node.Text, newObjectName), CurrentServerInfo);
                        }
                        break;
                    default:
                        e.CancelEdit = true;
                        break;
                }
            }
        }
        private void OnObjectsBeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            var tag = e.Node.Tag as ServerState;
            switch (tag.Key)
            {
                case KeySp:
                case KeyTable:
                case KeyFunction:
                case KeyDatabase:
                    break;
                default:
                    e.CancelEdit = true;
                    break;
            }
        }
        private void OnObjectsBeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            new Thread(ShowObjects).Start(e);
        }
        private void OnObjectsAfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowObject(e.Node);
        }
        
        private void SetSearchMode(bool isSearch)
        {
            _isInSearch = isSearch;
            //pnlSearchCommands.Visible = _isInSearch;
            //rtbObjectScript.Height = pnlObjectScript.Height - (pnlSearchCommands.Visible ? pnlSearchCommands.Height : 0) - rtbObjectScript.Top - 2;
        }

        private void LoadDatabase(TreeNode parent)
        {
            SetSearchMode(false);
            var objects = NewObjects;
            parent.Nodes.Cast<TreeNode>().ForEach((n) =>
            {
                var row = objects.NewRow();
                row[KeyName] = n.Text;
                var tableInfo = GetDatabaseInfo(n.Text);
                if (tableInfo != null && tableInfo.Rows.Count > 0)
                {
                    var item = tableInfo.Rows[0];
                    var sizeDBfile = (Convert.ToDecimal(item["size"]) / 1024);
                    decimal sizeINDEXfile = 0;
                    if (tableInfo.Rows.Count > 1)
                        sizeINDEXfile = (Convert.ToDecimal(tableInfo.Rows[1]["size"]) / 1024);
                    row[KeySpaceUsed] = Math.Round(sizeDBfile, 2);
                    row[KeySpaceUsed2] = Math.Round(sizeINDEXfile, 2);
                    if (n.Nodes.ContainsKey(KeyTables))
                        row[KeyCount] = n.Nodes[KeyTables].Nodes.Count;
                    row[KeyPath] = item["Physical_Name"];
                    row[KeyValue] = item["state"];
                }
                else
                    row[KeyValue] = 1;
                objects.Rows.Add(row);

                //if (tableInfo != null)
                //{
                //    var count = 0;
                //    tableInfo.AsEnumerable().ForEach((r) => 
                //    {
                //        var row = objects.NewRow();                        
                //        row[KeyName] = r["Logical_Name"];
                //        row[KeyPath] = r["Physical_Name"];
                //        if (n.Nodes.ContainsKey(KeyTables))
                //            row[KeyCount] = n.Nodes[KeyTables].Nodes.Count;
                //        row[KeySpaceUsed] = r["Size"];
                //        objects.Rows.Add(row);
                //        count++;
                //    });
                //}
            });
            //dgvObjects.Columns[1].HeaderText = "库名";
            //dgvObjects.Columns[2].HeaderText = "数据文件(MB)";
            //dgvObjects.Columns[3].HeaderText = "日志文件(MB)";
            //dgvObjects.Columns[4].HeaderText = "表总数";

            LoadObjectsDB(objects);
            _currentObjectMode = ObjectModes.Databases;

            if (!string.IsNullOrEmpty(CurrentServerInfo.Database))
                GetVersionControlState();
        }

        private void GetVersionControlState()
        {
            ////////this.Invoke(new Action( () =>
            ////////{
            ////////    bool exists;
            ////////    var node = tvObjects.SelectedNode;
            ////////    var isOnline = true;
            ////////    if (node != null)
            ////////    {
            ////////        var tag = node.Tag as ServerState;
            ////////        isOnline = node.Tag == null || tag.State;
            ////////    }
            ////////    if (isOnline)
            ////////    {
            ////////        var state = CheckVersionControl(out exists) && exists;
            ////////        tmiSetVersionControl.Text = state ? "禁用对象版本控制" : "启用对象版本控制";
            ////////        tmiSetVersionControl.Tag = state;
            ////////    }
            ////////}));
        }
        private bool CheckVersionControl(out bool exists)
        {
            var data = SqlHelper.ExecuteScalar(string.Format("SELECT is_disabled FROM sys.triggers WITH (NOLOCK) WHERE NAME = '{0}'", "trg_SQLMonSystemObjectVersionControls"), CurrentServerInfo);
            if (data != null)
            {
                exists = true;
                return !Convert.ToBoolean(data);
            }
            else
            {
                exists = false;
                return false;
            }
        }
        private void LoadObjectsDB(DataTable objects)
        {
            ////////// this.Invoke(new Action(() =>
            //////////{
            //////////    cboObjectScriptVersions.DataSource = null;
            //////////    SetCompareVersion();
            //////////    rtbObjectScript.Text = string.Empty;
            //////////    dgvObjects.DataSource = null;
            //////////    SpaceUsed.Visible = true;
            //////////    SpaceUsed2.Visible = true;
            //////////    Count.Visible = true;
            //////////    Path.Visible = true;
            //////////    HName.HeaderText = "库名";
            //////////    SpaceUsed.HeaderText = "数据文件(MB)";
            //////////    SpaceUsed2.HeaderText = "日志文件(MB)";
            //////////    Count.HeaderText = "表总数";
            //////////    dgvObjects.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //////////    dgvObjects.DataSource = objects;
            //////////}));
        }
        private void ShowObject(TreeNode node)
        {
            #region 判断添加 20200116
            if (node == null)
            {
                return;
            }
            var serverStateTmp = node.Tag as ServerState;
            if (serverStateTmp == null)
            {
                return;
            }
            if (serverStateTmp.Key.Equals(KeyDatabase))
            {
                return;
            }
            #endregion

            Task.Factory.StartNew(() =>
            {
                using (NewWait())
                {
                    SetSearchMode(false);
                    _previousDatabase = _currentDatabase;
                    _currentDatabase = string.Empty;
                    _previousObjectType = _currentObjectType;
                    _previousObjectMode = _currentObjectMode;
                    _currentObjectType = string.Empty;
                    var enableContextMenu = false;
                    if (node != null)
                    {
                        var root = GetRootNode(node);
                        DataTable data;
                        var objects = NewObjects;
                        var serverState = node.Tag as ServerState;
                        _currentServerInfo = root.Tag as ServerState;

                        //server is changed
                        if (_currentServerInfo != null && _previousServerInfo != null
                            && _currentServerInfo.Server != _previousServerInfo.Server)
                        {
                            ////// this.Invoke(new Action(() =>
                            //////{
                            //////    dgvServerHealth.Rows.Clear();
                            //////}));
                        }

                        switch (serverState.Key)
                        {
                            case KeyDatabase:
                                _currentDatabase = node.Text;
                                _currentServerInfo.Database = _currentDatabase;
                                _currentObjectType = KeyDatabase;
                                LoadDatabase(node.Parent);
                                break;
                            case KeyTables:
                            case KeyViews:
                            case KeyFunctions:
                            case KeyAssemblies:
                            case KeySPs:
                            case KeyTriggers:
                                var key = node.Name;
                                if (_previousObjectType == key)
                                    return;
                                enableContextMenu = true;
                                _currentDatabase = node.Parent.Text;
                                _currentObjectType = key;
                                switch (key)
                                {
                                    case KeyAssemblies:
                                        //todo:
                                        data = Query("SELECT a.name, NULL AS SpaceUsed, NULL AS Count, create_date AS CreateDate, modify_date AS ModifyDate, f.name AS Path FROM sys.assemblies a WITH (NOLOCK) LEFT JOIN sys.assembly_files f WITH (NOLOCK) ON a.assembly_id = f.assembly_id", CurrentServerInfo);
                                        break;
                                    case KeyTables:
                                        data = GetObjects(KeyTables);

                                        var count = Query(string.Format(SQLMgmtEngine.SqlTableInfo, string.Empty), CurrentServerInfo);
                                        data.AsEnumerable().ForEach((d) =>
                                        {
                                            var row = objects.NewRow();
                                            var tableName = d[KeyName] as string;
                                            var schemaName = d[KeySchemaName] as string;
                                            row[KeyName] = SQLMgmtEngine.GetObjectName(schemaName, tableName);
                                            var space = Query(string.Format("EXEC sp_spaceused '{0}'", row[KeyName]), CurrentServerInfo);
                                            if (space.Rows.Count > 0)
                                                //计算表的大小 表名
                                                row[KeySpaceUsed] = ToMb(space.Rows[0]["data"]);
                                            row[KeySpaceUsed2] = ToMb(space.Rows[0]["index_size"]);
                                            var rows = count.Select("TableName = '" + tableName + "'");
                                            if (rows.Length > 0)
                                                row[KeyCount] = rows[0]["RowCount"];
                                            row[KeyCreateDate] = d[KeyCreateDate];
                                            row[KeyModifyDate] = d[KeyModifyDate];
                                            row[KeyType] = ObjectModes.Table;
                                            row[KeyState] = il16.Images[8];
                                            objects.Rows.Add(row);
                                        });

                                        LoadObjectsTable(objects);
                                        break;
                                    case KeySPs:
                                    case KeyViews:
                                    case KeyFunctions:
                                    case KeyTriggers:
                                        data = GetObjects(key);
                                        data.AsEnumerable().ForEach((d) =>
                                        {
                                            var row = objects.NewRow();
                                            row[KeyName] = SQLMgmtEngine.GetObjectName(d[KeySchemaName], d[KeyName].ToString());
                                            row[KeyCreateDate] = d[KeyCreateDate];
                                            row[KeyModifyDate] = d[KeyModifyDate];
                                            int image;
                                            switch (key)
                                            {
                                                case KeySPs:
                                                    image = 3;
                                                    row[KeyType] = ObjectModes.Sp;
                                                    break;
                                                case KeyViews:
                                                    row[KeyType] = ObjectModes.View;
                                                    image = 4;
                                                    break;
                                                case KeyFunctions:
                                                    row[KeyType] = ObjectModes.Function;
                                                    image = 3;
                                                    break;
                                                case KeyTriggers:
                                                    row[KeyType] = ObjectModes.Trigger;
                                                    image = 3;
                                                    break;
                                                default:
                                                    image = 0;
                                                    break;
                                            }
                                            row[KeyState] = il16.Images[image];
                                            objects.Rows.Add(row);
                                        });
                                        LoadObjects(objects);
                                        break;
                                    default:
                                        break;
                                }
                                _currentObjectMode = ObjectModes.Objects;
                                this.Invoke(new Action(() =>
                                {
                                    node.ExpandAll();
                                }));
                                break;
                            case KeyTable:
                            case KeySp:
                            case KeyView:
                            case KeyFunction:
                            case KeyTrigger:
                            case KeyAssembly:
                                enableContextMenu = true;
                                _currentDatabase = node.Parent.Parent.Text;
                                _currentObjectType = node.Parent.Name;
                                if (_currentServerInfo != GetRootNode(node).Tag as ServerState
                                    || _previousDatabase != _currentDatabase
                                    || _previousObjectType != _currentObjectType)
                                {
                                    _currentServerInfo = GetRootNode(node).Tag as ServerState;
                                    _currentServerInfo.Database = _currentDatabase;
                                    ShowObject(node.Parent);
                                }
                                GetObjectScript(node.Text, null, ObjectModes.Objects);
                                break;
                            default:
                                _currentObjectMode = ObjectModes.Server;
                                break;
                        }
                        this.Invoke(new Action(() =>
                        {
                            lblObjectCount.Text = node.Nodes.Count > 0 ? node.Nodes.Count.ToString() : "1";
                        }));
                        //tcMain.SelectedTab = tpObjects;
                    }
                    //////////this.Invoke(() =>
                    //////////{
                    //////////    SetupPerformance();
                    //////////    dgvObjects.ContextMenuStrip = enableContextMenu ? cmsObjectList : null;
                    //////////});
                    _previousDatabase = _currentDatabase;
                    _previousObjectType = _currentObjectType;
                    _previousObjectMode = _currentObjectMode;
                }
            }).LogExceptions();
        }
        private void LoadObjectsTable(DataTable objects)
        {
            ////////this.Invoke(new Action(() =>
            ////////{
            ////////    cboObjectScriptVersions.DataSource = null;
            ////////    SetCompareVersion();
            ////////    rtbObjectScript.Text = string.Empty;
            ////////    //dgvObjects  = new DataGridView();
            ////////    dgvObjects.DataSource = null;
            ////////    SpaceUsed.Visible = true;
            ////////    SpaceUsed2.Visible = true;
            ////////    Count.Visible = true;
            ////////    Path.Visible = false;
            ////////    HName.HeaderText = "表名";
            ////////    SpaceUsed.HeaderText = "表大小(MB)";
            ////////    SpaceUsed2.HeaderText = "索引大小(MB)";
            ////////    Count.HeaderText = "表行数";
            ////////    dgvObjects.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ////////    dgvObjects.DataSource = objects;
            ////////}));
        }
        private void GetObjectScript(string objectName, string script, ObjectModes objectMode)
        {
            //////this.Invoke(new Action(() =>
            //////{
            //////    _currentObjectName = objectName;
            //////    _currentObjectScript = script;
            //////    _currentObjectMode = objectMode;
            //////    cboObjectScriptVersions.DataSource = GetObjectVersions();
            //////    cboObjectScriptVersions.SelectedIndex = 0;
            //////    SetCompareVersion();
            //////}));
        }
        private void LoadObjects(DataTable objects)
        {
            ////////this.Invoke(new Action(() =>
            ////////{
            ////////    cboObjectScriptVersions.DataSource = null;
            ////////    SetCompareVersion();
            ////////    rtbObjectScript.Text = string.Empty;
            ////////    dgvObjects.DataSource = null;
            ////////    SpaceUsed.Visible = false;
            ////////    SpaceUsed2.Visible = false;
            ////////    Count.Visible = false;
            ////////    Path.Visible = false;
            ////////    HName.Visible = true;
            ////////    HName.HeaderText = "对象名";
            ////////    dgvObjects.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ////////    dgvObjects.DataSource = objects;
            ////////}));
        }
        private string ToMb(object kb)
        {
            var value = ToKb(kb);
            return (value / 1024).ToString("0.###");
        }
        private decimal ToKb(object kb)
        {
            return Convert.ToDecimal(Regex.Replace(kb.ToString(), "KB", string.Empty, RegexOptions.IgnoreCase));
        }
        
        private bool ShowObjects(TreeNode node)
        {
            var ready = false;
            using (NewWait())
            {
                DataTable data;
                var key = node.Name;
                var root = GetRootNode(node);
                var serverState = node.Tag as ServerState;
                _previousServerInfo = _currentServerInfo;
                _currentServerInfo = root.Tag as ServerState;
                switch (serverState.Key)
                {
                    case KeyServer:
                        ready = LoadServer(node);
                        break;
                    case KeyDatabase:
                        _currentDatabase = node.Text;
                        _currentServerInfo.Database = _currentDatabase;
                        var databases = GetDatabasesInfo();
                        var state = databases.AsEnumerable().First(r => r["name"].ToString() == _currentDatabase);
                        if (state != null && Convert.ToInt32(state["state"]) == 0)
                        {
                            var objects = new string[] { KeyTables, KeyViews, KeyFunctions, KeySPs, KeyTriggers };
                            objects.ForEach(o =>
                            {
                                data = GetObjects(o);
                                data.AsEnumerable().ForEach((d) =>
                                {
                                    var image = 4;
                                    var type = string.Empty;
                                    switch (o)
                                    {
                                        case KeyTables:
                                            type = KeyTable;
                                            image = 8;
                                            break;
                                        case KeyViews:
                                            type = KeyView;
                                            image = 4;
                                            break;
                                        case KeySPs:
                                            type = KeySp;
                                            image = 3;
                                            break;
                                        case KeyFunctions:
                                            type = KeyFunction;
                                            image = 3;
                                            break;
                                        case KeyAssemblies:
                                            type = KeyAssembly;
                                            image = 3;
                                            break;
                                        case KeyTriggers:
                                            type = KeyTrigger;
                                            image = 3;
                                            break;
                                        default:
                                            break;
                                    }
                                    var tag = new ServerState { Key = type, IsReady = false };
                                    var child = new TreeNode { Text = SQLMgmtEngine.GetObjectName(d[KeySchemaName].ToString(), d[KeyName].ToString()), ImageIndex = image, SelectedImageIndex = image, Tag = tag };
                                    node.Nodes[o].Nodes.Add(child);
                                });
                            });
                            ready = true;
                        }
                        else if (MessagesHelper.ShowQuestion(string.Format("当前数据库 [{0}] 已脱机，你是否要进行联机操作?", _currentDatabase)))
                        {
                            SetOnlineOffline(_currentDatabase, true);
                            ShowObjects(node);
                        }
                        break;
                    default:
                        ready = true;
                        break;
                }
                serverState.IsReady = ready;
            }
            return ready;
        }

        public void SqlQuery()
        {
            try
            {
                var node = tvObjects.SelectedNode;
                var sql = string.Empty;//1
                var serverInfo = CurrentServerInfo;
                string objectName;
                string objectType;
                if (node != null)
                {
                    var root = GetRootNode(node);
                    serverInfo = root.Tag as ServerState;
                    objectName = node.Text;
                    objectType = (node.Tag as ServerState).Key;
                }
                else
                {
                    objectName = string.Empty;
                    objectType = string.Empty;
                }
                #region 添加判断 Myl 2020-01-13
                if (serverInfo == null || string.IsNullOrEmpty(_currentDatabase))
                {
                    throw new SQLDeployBaseException("请确定服务器和数据库！");
                }
                List<DbServerInfo> serversList = GetSecectedDbServers();
                if (serversList == null || serversList.Count == 0)
                {
                    throw new SQLDeployBaseException("请确定服务器和数据库！");
                }
                #endregion
                NewQuery(serverInfo, _currentDatabase, objectType, objectName);
            }
            catch (Exception ex)
            {
                MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Error);
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        private int _userQueryCount;
        private void NewQuery(DbServerInfo server, string database, string objectType, string objectName)
        {
            _userQueryCount++;
            var tabPage = new TabPage("查询 " + _userQueryCount + " (双击关闭)");
            tabPage.Tag = WorkModes.Query;
            var sql = string.Empty;
            if (!string.IsNullOrEmpty(objectName))
            {
                tabPage.Text = server.Server + "." + _currentDatabase + " " + tabPage.Text;
                switch (objectType)
                {
                    case KeyTable:
                    case KeyView:
                        sql = "SELECT * FROM " + objectName;
                        break;
                    case KeyFunction:
                        sql = "SELECT " + objectName + "()";
                        break;
                    case KeySp:
                        sql = "EXEC " + objectName;
                        break;
                    default:
                        if (File.Exists(objectName))
                            sql = File.ReadAllText(objectName);
                        break;
                }
                AddRecentObject(server, database, objectName, RecentObjectTypes.Other, objectType);
            }
            var serverInfo = server.Clone();
            serverInfo.Database = database;
            //var userQuery = new UserQuery(_userQueryCount == 1 && string.IsNullOrEmpty(sql) ? Settings.Instance.LastQuery : sql, serverInfo);

            var userQuery = new UserQuery(sql, serverInfo);

            userQuery.Dock = DockStyle.Fill;
            tabPage.Controls.Add(userQuery);
            tcMain.TabPages.Add(tabPage);
            tcMain.SelectedTab = tabPage;
        }

        internal void SetExecute(bool cancel)
        {
            this.Invoke(new Action(() =>
            {
                tbRefresh.Image = cancel ? TechScan.Tool.U8.ServiceDeployWin.Properties.Resources.Cross2 : TechScan.Tool.U8.ServiceDeployWin.Properties.Resources.Refresh2;
            }));
        }
        //////////internal List<KeyValuePair<string, int>> GetObjectVersions()
        //////////{
        //////////    var versions = new List<KeyValuePair<string, int>>();
        //////////    versions.Add(new KeyValuePair<string, int>("Current", 0));
        //////////    try
        //////////    {
        //////////        bool exists;
        //////////        if (CheckVersionControl(out exists) || exists)
        //////////        {
        //////////            string schemaName;
        //////////            _currentObjectName = SQLMgmtEngine.ParseObjectName(_currentObjectName, out schemaName);
        //////////            var data = Query(string.Format("SELECT HostName, LoginName, PostTime, Version FROM {2} WITH (NOLOCK) WHERE databasename = '{0}' AND objectname = '{1}' ORDER BY Version DESC", CurrentServerInfo.Database, _currentObjectName, Settings.Instance.VersionControlTableName), CurrentServerInfo);
        //////////            data.AsEnumerable().ForEach(r => versions.Add(new KeyValuePair<string, int>(string.Format("{0}  at ({1}) from ({2}) as ({3})", r["Version"].ToString(), r["PostTime"].ToString(), r["HostName"].ToString(), r["LoginName"].ToString()), Convert.ToInt32(r["Version"]))));
        //////////        }
        //////////    }
        //////////    catch (Exception ex)
        //////////    {
        //////////        ShowMessage(string.Format("无法获取版本，可能是版本控制表 {0} 不存在. \r\n{1}", Settings.Instance.VersionControlTableName, ex.Message));
        //////////    }
        //////////    return versions;
        //////////}
        internal void AddRecentObject(DbServerInfo server, string database, string objectName, RecentObjectTypes recentObjectType, string objectType)
        {
            //var recentObject = new RecentObject { Server = server.Server, User = server.User, Database = database, ObjectName = objectName, RecentObjectType = recentObjectType, ObjectType = objectType };
            //var match = Settings.Instance.RecentObjects.FirstOrDefault(o => o.Server.ToLower() == recentObject.Server.ToLower()
            //    && o.User.ToLower() == recentObject.User.ToLower()
            //    && o.Database.ToLower() == recentObject.Database.ToLower()
            //    && o.ObjectName.ToLower() == recentObject.ObjectName.ToLower()
            //    && o.RecentObjectType == recentObject.RecentObjectType);
            //if (match != null)
            //    Settings.Instance.RecentObjects.Remove(match);
            //Settings.Instance.RecentObjects.Insert(0, recentObject);
            //ClearRecentObjects();
            //LoadRecentObjects();
        }
        private void OnNewSqlQueryClick(object sender, EventArgs e)
        {
            try
            {
                SqlQuery();
            }
            catch (Exception ex)
            {
                MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }
        internal string SaveScript(string fileName, string script)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.OverwritePrompt = true;
                dlg.Filter = "SQL Script File(*.sql)|*.sql|All Files(*.*)|*.*";
                dlg.FileName = fileName;
                if (dlg.ShowDialog(this.ParentForm) == DialogResult.OK)
                {
                    File.WriteAllText(dlg.FileName, script);
                    return dlg.FileName;
                }
                else
                    return string.Empty;
            }
        }

        #region 关闭TabPage

        private void CloseCurrentTabPage(Point location)
        {
            if (tcMain.SelectedIndex > (int)WorkModes.Summary)
            {
                var rect = tcMain.GetTabRect(tcMain.SelectedIndex);
                if (rect.Contains(location))
                {
                    CloseCurrentTab();
                }
            }
        }

        private void CloseCurrentTab()
        {
            if (tcMain.SelectedTab != null && tcMain.SelectedTab.Tag != null)
            {
                var objectType = (WorkModes)tcMain.SelectedTab.Tag;
                switch (objectType)
                {
                    case WorkModes.Query:
                    case WorkModes.TableData:
                        //case WorkModes.UserPerformance:
                        //    if (objectType == WorkModes.UserPerformance)
                        //    {
                        //        var performance = tcMain.SelectedTab.Controls[0] as Performance;
                        //        performance.RemovePerformanceItem();
                        //    }
                        tcMain.TabPages.Remove(tcMain.SelectedTab);
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        private void tcMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CloseCurrentTabPage(e.Location);
        }
        private void AddCommand(string key, string text, Image icon)
        {
            var item = FindCommand<ToolStripButton>(key);
            if (item != null)
            {
                item.Text = text;
                if (icon != null)
                    item.Image = icon;
            }
        }
        private T FindCommand<T>(string key) where T : ToolStripItem
        {
            return (T)tsTools.Items.Cast<ToolStripItem>().FirstOrDefault(t => t.Name == key);
        }
        private Image GetExecuteIcon()
        {
            Image icon;
            switch (_currentWorkMode)
            {
                case WorkModes.Query:
                    var queryData = tcMain.SelectedTab.Controls[0] as UserQuery;
                    icon = queryData.IsRunning ? Resources.Cross2 : Resources.Excute2;
                    break;
                case WorkModes.TableData:
                    var tableData = tcMain.SelectedTab.Controls[0] as UserTableData;
                    icon = tableData.IsRunning ? Resources.Cross2 : Resources.Excute2;
                    break;
                default:
                    icon = null;
                    break;
            }
            return icon;
        }
        private void tcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            //tsCommands.Items.Cast<ToolStripItem>().ForEach(t => { if (t.Tag != null) t.Visible = false; });
            //SetSeparator(false);
            if (tcMain.SelectedIndex < (int)WorkModes.Objects)
                _currentWorkMode = (WorkModes)tcMain.SelectedIndex;
            else
                _currentWorkMode = (WorkModes)tcMain.SelectedTab.Tag;
            switch (_currentWorkMode)
            {
                case WorkModes.Query:
                    AddCommand(CommandRefresh, "执行", GetExecuteIcon());
                    break;
                case WorkModes.TableData:
                    AddCommand(CommandRefresh, "执行", GetExecuteIcon());
                    break;
                default:
                    AddCommand(CommandRefresh, "刷新", Resources.Refresh2);
                    break;
            }
            SaveSettings();
        }
        private void tbRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        #region 更改TreeNode节点Checked属性 Myl20200113Add
        private void ChangeTreeNodeCheckedState(TreeNode tn)
        {
            ChildrenChecked(tn, tn.Checked);
            ParentChecked(tn);
        }

        #region 联动

        /// <summary>
        /// 把每一个父级当作子级
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private void ParentChecked(TreeNode node)
        {
            if (node.Parent == null) return;
            ///单根
            //if (node.PrevNode == null && node.NextNode == null)
            //{
            //    node.Parent.Checked = node.Checked;
            //    return;
            //}
            ///多根
            //当前节点两种状态
            if (!node.Checked)//节点没被选中,父节点不被选中
            {
                node.Parent.Checked = node.Checked;
            }
            else//节点被选中，同级节点决定父节点状态
            {
                node.Parent.Checked = PeerChecked(node);
            }
            ParentChecked(node.Parent);//继续上级忽略同级
        }
        private bool PeerChecked(TreeNode node)
        {
            if (node == null) return true;
            return PeerPrevNodeChecked(node.PrevNode) && PeerNextNodeChecked(node.NextNode);
        }
        private bool PeerNextNodeChecked(TreeNode node)
        {
            if (node == null) return true;
            return PeerNextNodeChecked(node.NextNode) & node.Checked;
        }
        private bool PeerPrevNodeChecked(TreeNode node)
        {
            if (node == null) return true;
            if (node.Checked)
                return PeerPrevNodeChecked(node.PrevNode);
            return false;
        }
        /// <summary>
        /// 把每一个子级当作父级
        /// </summary>
        /// <param name="node"></param>
        /// <param name="check"></param>
        private void ChildrenChecked(TreeNode node, bool check)
        {
            foreach (TreeNode item in node.Nodes)
            {
                ChildrenChecked(item, check);
                item.Checked = check;
            }
        }

        #endregion

        private void tvObjects_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.ByMouse) return;
            ChangeTreeNodeCheckedState(e.Node);

            if (e.Node?.Checked ?? false)
            {
                var vStateTmp = e.Node.Tag as ServerState;

                if (vStateTmp.Key.Equals(KeyDatabase))
                {
                    _currentDatabase = vStateTmp.Database;
                    _currentServerInfo = vStateTmp;
                }
                if (e.Node.Parent != null)
                {
                    e.Node.Parent.Checked = true;
                }
            }
            else
            {

            }
        }
        #endregion

        private void tsddbSet_Click(object sender, EventArgs e)
        {
            using (frmSetting frm = new frmSetting(DeployTypes.SQL))
            {
                frm.ShowDialog();
            }
        }

        private void tmiRegisterServer_Click(object sender, EventArgs e)
        {
            EditConnection(null);
        }

        private void tmiEditServer_Click(object sender, EventArgs e)
        {
            var server = tvObjects.SelectedNode?.Tag as ServerState;
            if (server == null)
            {
                MessagesHelper.ShowMessage("请先选择要修改的服务器连接对象！");
            }
            else
            {
                if (!server.Key.Equals(KeyServer))
                {
                    MessagesHelper.ShowMessage("请先选择要修改的服务器连接对象！");
                }
                else
                {
                    EditConnection(server);
                }
            }
        }

        private void tmiRemoveServer_Click(object sender, EventArgs e)
        {
            try
            {
                #region 删除数据库连接对象
                var server = tvObjects.SelectedNode?.Tag as ServerState;
                if (server == null)
                {
                    MessagesHelper.ShowMessage("请先选择要删除的服务器连接对象！");
                }
                else
                {
                    if (!server.Key.Equals(KeyServer))
                    {
                        MessagesHelper.ShowMessage("请先选择要删除的服务器连接对象！");
                    }
                    else
                    {
                        if (MessagesHelper.ShowQuestion($"你确认要删除数据库连接{server.Server}?"))
                        {
                            var item = DeployHelper.DeployConfigCacheInstance.FindServer(server.Server);
                            if (item != null)
                            {
                                DeployHelper.DeployConfigCacheInstance?.SQL_Config?.DbServers.Remove(item);
                                var saveResult = DeployHelper.DeployConfigCacheInstance?.SaveConfig();
                                if (!saveResult.Item1)
                                {
                                    throw new SQLDeployBaseException($"配置文件保存出错！{saveResult.Item2}");
                                }

                                tvObjects.Nodes.Remove(tvObjects.SelectedNode);
                                _currentServerInfo = null;
                                _currentDatabase = string.Empty;
                                _currentObjectMode = ObjectModes.None;
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Error);
            }
        }

        private void tmiNewQuery_Click(object sender, EventArgs e)
        {
            SqlQuery();
        }

        #region 清空TreeNode节点的Check状态
        private void ClearTreeNodeCheckState(TreeNode tn, bool bChecked)
        {
            BeginInvokeLambda(new Action(() =>
            {
                tn.Nodes.OfType<TreeNode>().ForEach((A) => { A.Checked = bChecked; });
            }));
            //这里只有一层，暂时不考虑递归了
        }
        private void ClearTreeNodeCheckStates(TreeView tv, bool bChecked)
        {
            foreach (TreeNode tn in tv.Nodes)
            {
                ClearTreeNodeCheckState(tn, bChecked);
            }
        }
        #endregion

        /// <summary>
        /// SQL脚本部署回滚
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void tbRecentObjects_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmHistory mFrm = new Reports.frmHistory(HistoryType.DEPLOY_SQL))
                {
                    mFrm.ShowDialog();
                    if (mFrm.SelectedRollbackHistoryItem != null)
                    {
                        #region 确认回滚

                        var vServiceRollBackItem = mFrm.SelectedRollbackHistoryItem as SQLDeployHistoryItem;
                        if (vServiceRollBackItem != null)
                        {
                            m_currentServiceRollBackItem = vServiceRollBackItem;
                            lblSelectedPackagePath.Text = m_currentServiceRollBackItem.HistoryPackageFileFullName;
                            ShowPackageInfo();
                            lblSelectedPackagePath.Visible = true;
                            btnPublish.Enabled = true;

                            #region 添加数据库连接勾选 Myl20200114
                            ClearTreeNodeCheckStates(this.tvObjects, false);
                            foreach (TreeNode eachNode in tvObjects.Nodes)
                            {
                                var vStateTmp = eachNode.Tag as ServerState;
                                if (vStateTmp.Key.Equals(KeyDatabase))
                                {
                                    var vToRollBackDataBase = m_currentServiceRollBackItem.DeployParam?.DbServers?.FirstOrDefault((A) => { return A.Database.Equals(vStateTmp.Database); });
                                    if (vToRollBackDataBase != null)
                                    {
                                        eachNode.Checked = true;
                                    }
                                }
                                else if (vStateTmp.Key.Equals(KeyServer))
                                {
                                    //ShowObject(eachNode);
                                    ShowObjects(eachNode);
                                    eachNode.ExpandAll();
                                    foreach (TreeNode eachDbNode in eachNode.Nodes)
                                    {
                                        var vDBStateTmp = eachDbNode.Tag as ServerState;
                                        if (vDBStateTmp.Key.Equals(KeyDatabase))
                                        {
                                            var vToRollBackDataBase = m_currentServiceRollBackItem.DeployParam?.DbServers?.FirstOrDefault((A) => { return A.Database.Equals(vDBStateTmp.Database); });
                                            if (vToRollBackDataBase != null)
                                            {
                                                _currentDatabase = vToRollBackDataBase?.Database;
                                                eachDbNode.Checked = true;
                                                tvObjects.SelectedNode = eachDbNode;
                                            }
                                        }
                                    }
                                }
                            }

                            List<DbServerInfo> serversList = await GetSecectedDbServersAsync();

                            if (serversList == null || serversList.Count == 0)
                            {
                                throw new SQLDeployBaseException("待回滚的包部署时的数据库连接对象丢失，请重新创建数据库连接！");
                            }
                            #endregion

                            OnPublishClickAsync(btnPublish, null);
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Error);
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
