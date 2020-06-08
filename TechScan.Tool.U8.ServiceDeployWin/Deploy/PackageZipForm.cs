using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;
using TechScan.Tool.U8.ServiceDeploy.Base.Exceptions;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils;
using TechScan.Tool.U8.ServiceDeploy.IIS.Impl;
using TechScan.Tool.U8.ServiceDeploy.SQL.Impl;
using TechScan.Tool.U8.ServiceDeployWin.Exceptions;
using TechScan.Tool.U8.ServiceDeployWin.Models;
using TechScan.Tool.U8.ServiceDeployWin.Reports;
using TechScan.Tool.U8.ServiceDeployWin.Util;

namespace TechScan.Tool.U8.ServiceDeployWin.Deploy
{
    /// <summary>
    /// 文件打包窗体
    /// </summary>
    public partial class PackageZipForm : Form
    {
        #region Fileds & Property
        /// <summary>
        /// 是否正在打包中
        /// （正在部署时不允许关掉窗口）
        /// </summary>
        private bool m_IsRunning = false;
        private string SrcDir
        {
            get
            {
                return txtSrcDir.Text.Trim();
            }
        }
        private string DestDir
        {
            get
            {
                return txtDestinationDirectory.Text.Trim();
            }
        }
        #endregion

        #region Ctor
        public PackageZipForm()
        {
            InitializeComponent();
            cmbPackageType.DataSource = CommUtils.GetPkgTypes();
            this.Load += PackageZipForm_Load;
            //this.DragEnter += PackageZipForm_DragEnter;
            //this.DragDrop += PackageZipForm_DragDrop;
            this.FormClosing += PackageZipForm_FormClosing;
        }
        #endregion

        #region Methods
        private void PackageZipForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_IsRunning)
            {
                e.Cancel = true;
            }
        }

        /*
         private void PackageZipForm_DragDrop(object sender, DragEventArgs e)
         {
             try
             {
                 Array file = (System.Array)e.Data.GetData(DataFormats.FileDrop);
                 if (file.Length > 1)
                 {
                     MessageBox.Show("不可以选择多个文件或者文件夹！");
                     return;
                 }

                 if (GetDirectoryControlCount() >= 1)
                 {
                     throw new Exception("目前只支持单个文件夹打包！请先移出现有文件夹！");
                 }

                 string cFileName = file.GetValue(0).ToString();
                 System.IO.FileInfo info = new System.IO.FileInfo(cFileName);
                 //若为目录，则获取目录下所有子文件名
                 if ((info.Attributes & System.IO.FileAttributes.Directory) != 0)
                 {
                     ClearCtls();

                     uclDirectory ucDic = new uclDirectory();
                     ucDic.SelectedDirectoryPath = cFileName;
                     pnlDirectory.Controls.Add(ucDic);
                     pnlDirectory.Controls.Add(btnAddDirectory);
                     pnlDirectory.Visible = true;
                 }
                 else
                 {
                     throw new FilesPackException("暂时只支持文件夹打包！");
                 }
             }
             catch (Exception ex)
             {
                 MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Error);
             }
         }

         private void PackageZipForm_DragEnter(object sender, DragEventArgs e)
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
         */

        private void PackageZipForm_Load(object sender, EventArgs e)
        {
            try
            {
                txtPackager.Text = ConstantValues.DEFAULT_CORP_NAME;
            }
            catch (Exception ex)
            {
                MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Error);
            }
        }
        private void txtDestinationDirectory_ZoomClick(object sender, EventArgs e)
        {
            var cSelectedDirectoryPath = "";
            using (var fsd = new FolderSelectDialog())
            {
                fsd.Title = "选择目标文件夹";
                if (fsd.ShowDialog(this.Handle))
                {
                    cSelectedDirectoryPath = fsd.FileName;
                }
            }
            if (string.IsNullOrWhiteSpace(cSelectedDirectoryPath) || !Directory.Exists(cSelectedDirectoryPath))
            {
                return;
            }

            txtDestinationDirectory.Text = cSelectedDirectoryPath;
        }
        private Action<string, string> DicIsEmptyFunc()
        {
            return (A, B) =>
            {
                if (string.IsNullOrEmpty(B))
                {
                    throw new InvalidControlDataException($"请先确定{A}目录！") { };
                }
                if (!System.IO.Directory.Exists(B))
                {
                    throw new InvalidControlDataException($"{A}目录不存在！") { };
                }
                if (!A.Equals("目标"))
                {
                    var dirSrc = new DirectoryInfo(B);
                    if ((dirSrc.GetFiles("*", SearchOption.AllDirectories)?.Length ?? 0) == 0)
                    {
                        throw new InvalidControlDataException($"{A}目录为空！") { };
                    }
                }
            };
        }
        private async void tbStartPack_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                #region 数据校验

                if (string.IsNullOrEmpty(txtPackVersionCode.Text))
                {
                    throw new InvalidControlDataException("版本号不能为空！") { };
                }
                if (string.IsNullOrEmpty(txtPackVersionDescription.Text))
                {
                    throw new InvalidControlDataException("版本描述不能为空！") { };
                }
                //if (string.IsNullOrEmpty(SrcDir))
                //{
                //    throw new InvalidControlDataException("请先确定源目录！") { };
                //}
                //if (System.IO.Directory.Exists(SrcDir))
                //{
                //    throw new InvalidControlDataException("源目录不存在！") { };
                //}
                //var dirSrc = new DirectoryInfo(SrcDir);
                //if ((dirSrc.GetFiles("*", SearchOption.AllDirectories)?.Length ?? 0) == 0)
                //{
                //    throw new InvalidControlDataException("源目录为空！") { };
                //}

                ////Action<string, string> DicIsEmptyFunc = new Action<string,string>((A,B) =>
                //// {
                ////     if (string.IsNullOrEmpty(B))
                ////     {
                ////         throw new InvalidControlDataException($"请先确定{A}目录！") { };
                ////     }
                ////     if (System.IO.Directory.Exists(B))
                ////     {
                ////         throw new InvalidControlDataException($"{A}目录不存在！") { };
                ////     }
                ////     var dirSrc = new DirectoryInfo(B);
                ////     if ((dirSrc.GetFiles("*", SearchOption.AllDirectories)?.Length ?? 0) == 0)
                ////     {
                ////         throw new InvalidControlDataException($"{A}目录为空！") { };
                ////     }
                //// });
                DicIsEmptyFunc()("源", SrcDir);
                DicIsEmptyFunc()("目标", DestDir);
                //if (string.IsNullOrEmpty(DestDir))
                //{
                //    throw new InvalidControlDataException("请先确定目标目录！") { };
                //}
                #endregion

                FrozenScreenAsync(true);
                var tRtn =
               await Task.Run<Tuple<bool, string>>(() =>
               {

                   Tuple<bool, string> tRtnTmp;
                   if (rboIIS.Checked)
                   {
                       var packObj = new IISPackageRepository( );
                       packObj.SourceDirectory = txtSrcDir.Text.Trim();//GetSourceDirectoryPath();
                       packObj.DestinationDirectory = txtDestinationDirectory.Text;
                       if (this.InvokeRequired)
                       {
                           this.BeginInvoke(new Action(() =>
                           {
                               var vKey = new IISPackageZipItem((PackageType)Enum.Parse(typeof(PackageType), cmbPackageType.Text))
                               {
                                   PackType = DeployTypes.IIS,
                                   Description = txtPackVersionDescription.Text ?? string.Empty,
                                   Packager = txtPackager.Text ?? string.Empty,
                                   Version = txtPackVersionCode.Text ?? "1.0",
                                   PackageDateTime = System.DateTime.Now,
                                   PkgType = (PackageType)Enum.Parse(typeof(PackageType), cmbPackageType.Text)
                               };
                               packObj.PackParams = new IISPackageZipParams(vKey);
                               packObj.PackParams.PackParamKey = vKey;
                               packObj.PackData = packObj.PackParams.PackParamKey;
                           }));
                       }
                       else
                       {
                           var vKey = new IISPackageZipItem((PackageType)Enum.Parse(typeof(PackageType), cmbPackageType.Text))
                           {
                               PackType = DeployTypes.IIS,
                               Description = txtPackVersionDescription.Text ?? string.Empty,
                               Packager = txtPackager.Text ?? string.Empty,
                               Version = txtPackVersionCode.Text ?? "1.0",
                               PackageDateTime = System.DateTime.Now,
                               PkgType = (PackageType)Enum.Parse(typeof(PackageType), cmbPackageType.Text)
                           };
                           packObj.PackParams = new IISPackageZipParams(vKey);
                           packObj.PackParams.PackParamKey = vKey;
                           packObj.PackData = packObj.PackParams.PackParamKey;
                       }
                       tRtnTmp = packObj.ExecPack();
                   }
                   else
                   {
                       var packObj = new SQLPackageRepository();
                       packObj.SourceDirectory = txtSrcDir.Text.Trim();//GetSourceDirectoryPath();
                       packObj.DestinationDirectory = txtDestinationDirectory.Text;
                       if (this.InvokeRequired)
                       {
                           this.BeginInvoke(new Action(() =>
                           {
                               var vKey = new SQLPackageZipItem((PackageType)Enum.Parse(typeof(PackageType), cmbPackageType.SelectedValue?.ToString() ?? "Release"))
                               {
                                   PackType = DeployTypes.SQL,
                                   Description = txtPackVersionDescription.Text ?? string.Empty,
                                   Packager = txtPackager.Text ?? string.Empty,
                                   Version = txtPackVersionCode.Text ?? "1.0",
                                   PackageDateTime = System.DateTime.Now,
                                   PkgType = (PackageType)Enum.Parse(typeof(PackageType), cmbPackageType.Text)
                               };
                               packObj.PackParams = new SQLPackageZipParams(vKey);
                               packObj.PackParams.PackParamKey = vKey;
                               packObj.PackData = packObj.PackParams.PackParamKey;
                           }));
                       }
                       else
                       {
                           var vKey = new SQLPackageZipItem((PackageType)Enum.Parse(typeof(PackageType), cmbPackageType.SelectedValue?.ToString() ?? "Release"))
                           {
                               PackType = DeployTypes.SQL,
                               Description = txtPackVersionDescription.Text ?? string.Empty,
                               Packager = txtPackager.Text ?? string.Empty,
                               Version = txtPackVersionCode.Text ?? "1.0",
                               PackageDateTime = System.DateTime.Now,
                               PkgType = (PackageType)Enum.Parse(typeof(PackageType), cmbPackageType.Text)
                           };
                           packObj.PackParams = new SQLPackageZipParams(vKey);
                           packObj.PackParams.PackParamKey = vKey;
                           packObj.PackData = packObj.PackParams.PackParamKey;
                       }
                       tRtnTmp = packObj.ExecPack();
                   }
                   return tRtnTmp;
               });

                if (tRtn.Item1)
                {
                    MessagesHelper.ShowMessage("文件打包成功！");
                    InitScreenAsync();
                }
                else
                {
                    throw new FilesPackException($"文件打包异常！{System.Environment.NewLine}{tRtn.Item2}");
                }
            }
            catch (InvalidControlDataException ctlEx)
            {
                MessagesHelper.ShowMessage(ctlEx.Message, MessageBoxIcon.Error);
                ctlEx.Owner?.SelectAll();
                ctlEx.Owner?.Focus();
            }
            catch (Exception ex)
            {
                MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Error);
            }
            finally
            {
                FrozenScreenAsync(false);
            }
        }
        private void txtSrcDir_ZoomClick(object sender, EventArgs e)
        {
            var cSelectedDirectoryPath = "";
            using (var fsd = new FolderSelectDialog())
            {
                fsd.Title = "选择源文件夹";
                if (fsd.ShowDialog(this.Handle))
                {
                    cSelectedDirectoryPath = fsd.FileName;
                }
            }
            if (string.IsNullOrWhiteSpace(cSelectedDirectoryPath) || !Directory.Exists(cSelectedDirectoryPath))
            {
                return;
            }

            txtSrcDir.Text = cSelectedDirectoryPath;
        }
        private void FrozenScreen(bool bFrozen)
        {
            m_IsRunning = bFrozen;
            picLoading.Visible = bFrozen;
            tsTools.Enabled = !bFrozen;
            gbPackType.Enabled = !bFrozen;
            txtPackager.Enabled = !bFrozen;
            cmbPackageType.Enabled = !bFrozen;
            txtPackVersionCode.Enabled = !bFrozen;
            txtPackVersionDescription.Enabled = !bFrozen;
            txtSrcDir.Enabled = !bFrozen;
            txtDestinationDirectory.Enabled = !bFrozen;
        }
        private void FrozenScreenAsync(bool bFrozen)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<bool>(FrozenScreen), bFrozen);
            }
            else
            {
                FrozenScreen(bFrozen);
            }
        }
        private void InitScreen()
        {
            txtPackVersionCode.Text = string.Empty;
            txtPackVersionDescription.Text = string.Empty;
            txtSrcDir.Text = string.Empty;
            txtDestinationDirectory.Text = string.Empty;
        }
        private void InitScreenAsync()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(InitScreen));
            }
            else
            {
                InitScreen();
            }
        }

        #region 打包历史

        private void tbRecentObjects_Click(object sender, EventArgs e)
        {

        }

        #endregion

        private void web服务打包历史ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenRecentPacks(HistoryType.PACK_IIS);
        }

        private void OpenRecentPacks(HistoryType ht)
        {
            using (frmHistory mFrm = new Reports.frmHistory(ht))
            {
                mFrm.ShowDialog();
            }
        }

        private void sQL脚本打包历史ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenRecentPacks(HistoryType.PACK_SQL);
        }

        #endregion
    }
}
