using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechScan.Tool.Controls;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;
using TechScan.Tool.U8.ServiceDeploy.Base.Exceptions;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils;
using TechScan.Tool.U8.ServiceDeploy.IIS.Impl;
using TechScan.Tool.U8.ServiceDeploy.SQL.Impl;
using TechScan.Tool.U8.ServiceDeployWin.Exceptions;
using TechScan.Tool.U8.ServiceDeployWin.Models;
using TechScan.Tool.U8.ServiceDeployWin.Util;

//缺少记住最近的版本号功能 191226

namespace TechScan.Tool.U8.ServiceDeployWin.Deploy
{
    [Obsolete("已经停用", true)]
    public partial class uclZipPackage : UserControl
    {
        #region Fields & Property

        #endregion

        #region Ctor
        public uclZipPackage()
        {
            InitializeComponent();

            pnlDirectory.DragEnter += PnlDirectory_DragEnter;
            pnlDirectory.DragDrop += PnlDirectory_DragDrop;

            cmbPackageType.DataSource = CommUtils.GetPkgTypes();
        }

        private void PnlDirectory_DragDrop(object sender, DragEventArgs e)
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
                MessageBox.Show(ex.Message);
            }
        }

        private void PnlDirectory_DragEnter(object sender, DragEventArgs e)
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

        #region Methods

        /// <summary>
        /// 判断path是否为目录
        /// </summary>
        public bool IsDirectory(String path)
        {
            System.IO.FileInfo info = new System.IO.FileInfo(path);
            return (info.Attributes & System.IO.FileAttributes.Directory) != 0;
        }

        private void btnAddDirectory_Click(object sender, EventArgs e)
        {
            try
            {
                #region 检查是否有多个文件夹[目前只允许一个文件夹打包]

                if (GetDirectoryControlCount() >= 1)
                {
                    throw new Exception("目前只支持单个文件夹打包！");
                }

                #endregion

                #region 选择文件夹
                var cSelectedDirectoryPath = "";
                using (var fsd = new FolderSelectDialog())
                {
                    fsd.Title = "选择待打包的文件夹";
                    if (fsd.ShowDialog(this.Handle))
                    {
                        cSelectedDirectoryPath = fsd.FileName;
                    }
                }
                if (string.IsNullOrWhiteSpace(cSelectedDirectoryPath) || !Directory.Exists(cSelectedDirectoryPath))
                {
                    return;
                }
                #endregion

                ClearCtls();

                uclDirectory ucDic = new uclDirectory();
                ucDic.SelectedDirectoryPath = cSelectedDirectoryPath;
                pnlDirectory.Controls.Add(ucDic);
                pnlDirectory.Controls.Add(btnAddDirectory);
                pnlDirectory.Visible = true;
            }
            catch (Exception ex)
            {
                MessagesHelper.ShowMessage(ex.Message, MessageBoxIcon.Error);
            }
        }

        private void ClearCtls()
        {
            pnlDirectory.Controls.Clear();
        }

        private int GetDirectoryControlCount()
        {
            int iCount = 0;
            foreach (var item in pnlDirectory.Controls)
            {
                if (item is uclDirectory)
                {
                    iCount++;
                }
            }
            return iCount;
        }

        private void Zip<T>()
        {

        }

        private async void btnStartPack_Click(object sender, EventArgs e)
        {
            try
            {
                #region 数据校验

                if (string.IsNullOrEmpty(txtPackVersionCode.Text))
                {
                    throw new InvalidControlDataException("版本号不能为空！") { };
                }
                if (string.IsNullOrEmpty(txtDestinationDirectory.Text))
                {
                    throw new InvalidControlDataException("目标文件夹不能为空！") { };
                }
                #endregion

                picLoading.Visible = true;
                var tRtn =
               await Task.Run<Tuple<bool, string>>(() =>
                 {

                     Tuple<bool, string> tRtnTmp;
                     if (rboIIS.Checked)
                     {
                         var packObj = new IISPackageRepository();
                         packObj.SourceDirectory = GetSourceDirectoryPath();
                         packObj.DestinationDirectory = txtDestinationDirectory.Text;
                         if (this.InvokeRequired)
                         {
                             this.BeginInvoke(new Action(() => {
                                 packObj.PackParams = new IISPackageZipParams(new IISPackageZipItem((PackageType)Enum.Parse(typeof(PackageType), cmbPackageType.SelectedValue?.ToString() ?? "Release"))
                                 {
                                     PackType = DeployTypes.IIS,
                                     Description = txtPackVersionDescription.Text ?? string.Empty,
                                     Packager = txtPackager.Text ?? string.Empty,
                                     Version = txtPackVersionCode.Text ?? "1.0",
                                     PackageDateTime = dtpPackDateTime.Value,
                                     PkgType = (PackageType)Enum.Parse(typeof(PackageType), cmbPackageType.Text)
                                 });
                             }));
                         }
                         else
                         {
                             packObj.PackParams = new IISPackageZipParams(new IISPackageZipItem((PackageType)Enum.Parse(typeof(PackageType), cmbPackageType.SelectedValue?.ToString() ?? "Release"))
                             {
                                 PackType = DeployTypes.IIS,
                                 Description = txtPackVersionDescription.Text ?? string.Empty,
                                 Packager = txtPackager.Text ?? string.Empty,
                                 Version = txtPackVersionCode.Text ?? "1.0",
                                 PackageDateTime = dtpPackDateTime.Value,
                                 PkgType = (PackageType)Enum.Parse(typeof(PackageType), cmbPackageType.Text)
                             });
                         }
                         tRtnTmp = packObj.ExecPack();
                     }
                     else
                     {
                         var packObj = new SQLPackageRepository();
                         packObj.SourceDirectory = GetSourceDirectoryPath();
                         packObj.DestinationDirectory = txtDestinationDirectory.Text;
                         if (this.InvokeRequired)
                         {
                             this.BeginInvoke(new Action(() =>
                             {
                                 packObj.PackParams = new SQLPackageZipParams(new SQLPackageZipItem((PackageType)Enum.Parse(typeof(PackageType), cmbPackageType.SelectedValue?.ToString() ?? "Release"))
                                 {
                                     PackType = DeployTypes.SQL,
                                     Description = txtPackVersionDescription.Text ?? string.Empty,
                                     Packager = txtPackager.Text ?? string.Empty,
                                     Version = txtPackVersionCode.Text ?? "1.0",
                                     PackageDateTime = dtpPackDateTime.Value,
                                     PkgType = (PackageType)Enum.Parse(typeof(PackageType), cmbPackageType.Text)
                                 });
                             }));
                         }
                         else
                         {
                             packObj.PackParams = new SQLPackageZipParams(new SQLPackageZipItem((PackageType)Enum.Parse(typeof(PackageType), cmbPackageType.SelectedValue?.ToString() ?? "Release"))
                             {
                                 PackType = DeployTypes.SQL,
                                 Description = txtPackVersionDescription.Text ?? string.Empty,
                                 Packager = txtPackager.Text ?? string.Empty,
                                 Version = txtPackVersionCode.Text ?? "1.0",
                                 PackageDateTime = dtpPackDateTime.Value,
                                 PkgType = (PackageType)Enum.Parse(typeof(PackageType), cmbPackageType.Text)
                             });
                         }
                         tRtnTmp = packObj.ExecPack();
                     }
                     return tRtnTmp;
                 });

                if (tRtn.Item1)
                {
                    MessagesHelper.ShowMessage("文件打包成功！");
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
                picLoading.Visible = false;
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

        private string GetSourceDirectoryPath()
        {
            string cPath = "";
            foreach (var item in pnlDirectory.Controls)
            {
                if (item is uclDirectory)
                {
                    cPath = ((uclDirectory)item).SelectedDirectoryPath;
                }
            }
            return cPath;
        }

        #endregion
    }
}
