using PrintNiceLabel.VICO.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TechScan.Tool.Controls;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;
using TechScan.Tool.U8.ServiceDeploy.Base.Impl;
using TechScan.Tool.U8.ServiceDeploy.IIS.Impl;
using TechScan.Tool.U8.ServiceDeploy.SQL.Impl;
using TechScan.Tool.U8.ServiceDeployWin.Properties;
using TechScan.Tool.U8.ServiceDeployWin.Util;

namespace TechScan.Tool.U8.ServiceDeployWin.Reports
{
    /// <summary>
    /// 操作历史窗口
    /// </summary>
    public partial class frmHistory : Form
    {
        #region Fields & Property
        HistoryType m_HistoryReportType;
        MasterGrid @MastDeployGrid;
        object m_HistoryObj;

        /// <summary>
        /// 当前选择的回滚包文件名称
        /// </summary>
        public string SelectedRollbackPackageFileName;

        /// <summary>
        /// 当前选择的回滚包对象
        /// </summary>
        public BaseHistoryItem SelectedRollbackHistoryItem
        {
            get;
            private set;
        }
        #endregion

        #region Ctor

        public frmHistory(HistoryType type)
        {
            InitializeComponent();
            m_HistoryReportType = type;
        }

        #endregion

        #region Methods
        private void LoadHistory()
        {
            try
            {
                if (m_HistoryReportType == HistoryType.DEPLOY_IIS)
                {
                    #region IIS部署历史
                    using (IISDeployHistoryRepository iisHis =
                        new IISDeployHistoryRepository(m_HistoryReportType, DeployHelper.DeployData_IISHistoryPath))
                    {
                        if ((iisHis.Details?.Count ?? 0) == 0)
                        {
                            tsbRollBack.Enabled = false;
                        }
                        else
                        {
                            DataSet hisData = iisHis.DetailsData;
                            MasterGrid masterControl = new MasterGrid(ref hisData);
                            this.@MastDeployGrid = masterControl;
                            this.spcPanel.Panel2.Controls.Clear();
                            this.spcPanel.Panel2.Controls.Add(this.@MastDeployGrid);
                            this.@MastDeployGrid.SetParentSource("DeployMain", "GUID");
                            this.@MastDeployGrid.childView.Add("DeploySite", "网站配置");
                            this.@MastDeployGrid.childView.Add("DeployAppPool", "AppPool配置");
                            ShowTitleInfo(iisHis.Details?.Count.ToString());
                            m_HistoryObj = iisHis.Details;
                        }
                    }
                    #endregion
                }
                else if (m_HistoryReportType == HistoryType.DEPLOY_SQL)
                {
                    #region SQL脚本部署历史

                    using (SQLDeployHistoryRepository iisHis =
                       new SQLDeployHistoryRepository(m_HistoryReportType, DeployHelper.DeployData_SQLHistoryPath))
                    {
                        if ((iisHis.Details?.Count ?? 0) == 0)
                        {
                            tsbRollBack.Enabled = false;
                        }
                        else
                        {
                            DataSet hisData = iisHis.DetailsData;
                            MasterGrid masterControl = new MasterGrid(ref hisData);
                            this.@MastDeployGrid = masterControl;
                            this.spcPanel.Panel2.Controls.Clear();
                            this.spcPanel.Panel2.Controls.Add(this.@MastDeployGrid);
                            this.@MastDeployGrid.SetParentSource("DeployMain", "GUID");
                            this.@MastDeployGrid.childView.Add("DataBase", "数据库");

                            ShowTitleInfo(iisHis.Details?.Count.ToString());
                            m_HistoryObj = iisHis.Details;
                        }
                    }
                    #endregion
                }
                else if (m_HistoryReportType == HistoryType.PACK_IIS)
                {
                    #region Web服务打包历史
                    ShowPackHistory(DeployTypes.IIS);
                    #endregion
                }
                else
                {
                    #region SQL脚本打包历史
                    ShowPackHistory(DeployTypes.SQL);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                (new MessageDialog()).ShowError(ex.Message);
            }
        }

        private void ShowPackHistory(DeployTypes dpType)
        {
            if (System.IO.Directory.Exists(DeployHelper.PackData_HistoryPath))
            {
                FilePackHistoryRepository<FilePackHistoryItem<BasePackageZipItem>, BasePackageZipItem>
                hisFactory = new FilePackHistoryRepository<FilePackHistoryItem<BasePackageZipItem>, BasePackageZipItem>(dpType == DeployTypes.IIS ? HistoryType.PACK_IIS : HistoryType.PACK_SQL
                , DeployHelper.PackData_HistoryPath)
                {
                    HistoryDir = DeployHelper.PackData_HistoryPath
                };
                var vDatasFilter = hisFactory.ToDataSet(dpType);
                m_HistoryObj = vDatasFilter;
                if ((vDatasFilter?.Tables?[0].Rows.Count ?? 0) == 0)
                {
                    tsbRollBack.Enabled = false;
                }
                else
                {
                    tsbRollBack.Image = Resources.findfile;
                    tsbRollBack.Text = "文件定位";
                    DataSet hisData = vDatasFilter;
                    MasterGrid masterControl = new MasterGrid(ref hisData);
                    this.@MastDeployGrid = masterControl;
                    this.spcPanel.Panel2.Controls.Clear();
                    this.spcPanel.Panel2.Controls.Add(this.@MastDeployGrid);
                    this.@MastDeployGrid.SetParentSource("PackMain", "GUID");
                    this.@MastDeployGrid.childView.Add("PackVersion", "版本描述");
                    ShowTitleInfo(hisData?.Tables?[0].Rows.Count.ToString());
                    //m_HistoryObj = iisHis.Details;
                    tsbRollBack.Enabled = true;
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadHistory();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (IISDeployHistoryRepository iisHis =
                    new IISDeployHistoryRepository(m_HistoryReportType, DeployHelper.DeployData_IISHistoryPath))
                {
                    DataSet hisData = iisHis.DetailsData;
                    //MasterGrid masterControl = new MasterGrid(ref hisData);
                    masterGrid1.InitGrid(ref hisData);

                    //this.@MastDeployGrid = masterControl;
                    //this.spcPanel.Panel2.Controls.Add(this.@MastDeployGrid);
                    this.masterGrid1.SetParentSource("DeployMain", "GUID");
                    this.masterGrid1.childView.Add("DeploySite", "网站配置");
                    this.masterGrid1.childView.Add("DeployAppPool", "AppPool配置");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmHistory_Load(object sender, EventArgs e)
        {
            InitScreen();
            LoadHistory();
        }
        void ShowTitleInfo(string cCounts = "0")
        {
            switch (m_HistoryReportType)
            {
                case HistoryType.PACK_IIS:
                    this.txtTitle.Text = "U8 Android Web服务打包履历";
                    tsbRollBack.Enabled = false;
                    break;
                case HistoryType.PACK_SQL:
                    this.txtTitle.Text = "SQL脚本打包履历";
                    tsbRollBack.Enabled = false;
                    break;
                case HistoryType.DEPLOY_IIS:
                    this.txtTitle.Text = $"U8 Android Web服务部署履历【{cCounts}】";
                    break;
                case HistoryType.DEPLOY_SQL:
                    this.txtTitle.Text = $"SQL脚本部署履历【{cCounts}】";
                    break;
                default:
                    break;
            }
        }
        void InitScreen()
        {
            ShowTitleInfo();
        }

        private void tsBtnExport_Click(object sender, EventArgs e)
        {
            (new MessageDialog()).ShowError("暂不支持！");
        }

        private void tsbRollBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_HistoryReportType == HistoryType.DEPLOY_IIS ||
                    m_HistoryReportType == HistoryType.DEPLOY_SQL)
                {
                    #region IIS和SQL回滚
                    if (string.IsNullOrEmpty(@MastDeployGrid?.SelectedVersionGUID))
                    {
                        throw new Exception("请选择要回滚的行项目！");
                    }
                    if (m_HistoryObj == null)
                    {
                        throw new Exception("没有数据可供回滚！");
                    }

                    if (m_HistoryReportType == HistoryType.DEPLOY_IIS)
                    {
                        #region Web服务部署回滚
                        var vRollbackItem = ((IList<IISDeployHistoryItem>)m_HistoryObj).FirstOrDefault<IISDeployHistoryItem>((A) =>
                         {
                             return A.GUID.Equals(@MastDeployGrid?.SelectedVersionGUID);
                         });

                        if (!System.IO.File.Exists(vRollbackItem?.HistoryPackageFileFullName))
                        {
                            throw new Exception($"待会滚的包备份文件【{vRollbackItem?.HistoryPackageFileFullName}】已经丢失！无法回滚！");
                        }
                        if ((new MessageDialog()).ShowConfirm($"确认回滚部署包【{Path.GetFileNameWithoutExtension(vRollbackItem?.HistoryPackageFileFullName)}】？") == DialogResult.No)
                        {
                            SelectedRollbackHistoryItem = null;
                            return;
                        }
                        else
                        {
                            SelectedRollbackHistoryItem = vRollbackItem;
                            DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        #endregion
                    }
                    else if (m_HistoryReportType == HistoryType.DEPLOY_SQL)
                    {
                        #region SQL部署回滚

                        var vRollbackItem = ((IList<SQLDeployHistoryItem>)m_HistoryObj).FirstOrDefault<SQLDeployHistoryItem>((A) =>
                        {
                            return A.GUID.Equals(@MastDeployGrid?.SelectedVersionGUID);
                        });

                        if (!System.IO.File.Exists(vRollbackItem?.HistoryPackageFileFullName))
                        {
                            throw new Exception($"待会滚的包备份文件【{vRollbackItem?.HistoryPackageFileFullName}】已经丢失！无法回滚！");
                        }
                        if ((new MessageDialog()).ShowConfirm($"确认回滚部署包【{Path.GetFileNameWithoutExtension(vRollbackItem?.HistoryPackageFileFullName)}】？") == DialogResult.No)
                        {
                            SelectedRollbackHistoryItem = null;
                            return;
                        }
                        else
                        {
                            SelectedRollbackHistoryItem = vRollbackItem;
                            DialogResult = DialogResult.OK;
                            this.Close();
                        }

                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region 打包历史文件地址定位
                    if (string.IsNullOrEmpty(@MastDeployGrid?.SelectedVersionGUID))
                    {
                        throw new Exception("请选择行项目！");
                    }
                    var vDataTmp = m_HistoryObj as DataSet;
                    if (vDataTmp == null)
                    {
                        throw new Exception("没有内容！");
                    }
                    var vSelectedFiles = vDataTmp.Tables[0].Select($"GUID='{@MastDeployGrid?.SelectedVersionGUID}'");
                    if (vSelectedFiles.Length == 0)
                    {
                        throw new Exception("没有内容！");
                    }
                    var vSelectedPackHistoryFileName = vSelectedFiles[0]["备份目录"].ToString().Trim();

                    if (!System.IO.File.Exists(vSelectedPackHistoryFileName))
                    {
                        throw new Exception($"包备份文件【{vSelectedPackHistoryFileName}】已经丢失！");
                    }
                    FormOpUtils.OpenFolderAndSelectFile(vSelectedPackHistoryFileName);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                (new MessageDialog()).ShowError(ex.Message);
            }
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            LoadHistory();
        }
        #endregion
    }
}
