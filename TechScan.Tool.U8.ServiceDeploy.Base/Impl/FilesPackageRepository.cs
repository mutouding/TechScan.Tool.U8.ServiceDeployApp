/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   FilesPackageRepository.cs
* 功能描述： 文件打包仓库
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-26 11:33:20
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-26 11:33:20		Myl		Create
*                                           暂时这里的数据结构设计的不好，如果必要后期再说
*
======================================================================
//--------------------------------------------------------------------*/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using TechScan.Tool.U8.ServiceDeploy.Base.Encrypt;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Impl
{
    /// <summary>
    /// 文件打包仓库
    /// </summary>
    /// <typeparam name="TParams"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    public abstract class FilesPackageRepository<TParams, TItem> :
        IFilesPackageProvider<TParams, TItem>
        where TParams : Impl.BasePackageZipParams<TItem>
        where TItem : BasePackageZipItem
    {
        #region Property

        /// <summary>
        /// 打包参数对象
        /// </summary>
        public TParams PackParams
        {
            get;
            set;
        }

        /// <summary>
        /// 待打包的源目录（目前只针对文件夹目录）
        /// </summary>
        public string SourceDirectory
        {
            get; set;
        }
        /// <summary>
        /// 打包生成的目的文件夹目录
        /// </summary>
        public string DestinationDirectory
        {
            get; set;
        }
        /// <summary>
        /// 打包项
        /// </summary>
        public TItem PackData
        {
            get;
            set;
        }

        #region 文件包数据获取规则委托
        private Func<ZipArchive, bool> m_FilePackageGetRule;
        /// <summary>
        /// 文件包数据获取规则委托
        /// </summary>
        /// <returns>
        /// 返回规则委托
        /// </returns>
        public virtual Func<ZipArchive, bool> FilePackageGetRule
        {
            //目前都只是简单的校验，后期如果有需要再说了
            get
            {
                if (m_FilePackageGetRule == null)
                {
                    m_FilePackageGetRule = (A) =>
                    {

                        #region 基础 校验

                        int iFileExtensionLength = ConstantValues.DEFAULT_FILE_PACKINFO_EXTENTION_NAME.Length;
                        ZipArchiveEntry zaeTemp = A.Entries.FirstOrDefault(B =>
                        {
                            string pkFileName = B?.Name.Trim();
                            if (string.IsNullOrEmpty(pkFileName) || pkFileName.Length <= iFileExtensionLength)
                            {
                                return false;
                            }

                            return string.Compare(pkFileName.Substring(pkFileName.Length - iFileExtensionLength, iFileExtensionLength).ToUpper(), ConstantValues.DEFAULT_FILE_PACKINFO_EXTENTION_NAME, true) == 0;
                        });
                        if (zaeTemp == null)
                        {
                            throw new Exception($"读取包文件时出错！没有找到包信息文件[{ConstantValues.DEFAULT_FILE_PACKINFO_EXTENTION_NAME}]！");
                        }

                        #endregion

                        /*
                        zaeTemp = A.Entries.FirstOrDefault(B =>
                        {
                            string pkFileName = B?.Name.Trim();
                            iFileExtensionLength = 4;//".SVC或者.SQL"
                            if (string.IsNullOrEmpty(pkFileName) || pkFileName.Length <= iFileExtensionLength)
                            {
                                return false;
                            }
                            return string.Compare(pkFileName.Substring(pkFileName.Length - iFileExtensionLength, iFileExtensionLength).ToUpper(), PackParams.PackType == Enums.DeployTypes.IIS ? ".SVC" : ".SQL", true) == 0;
                        });
                        if (zaeTemp == null)
                        {
                            return false;
                        }
                        */

                        return true;
                    };
                }
                return m_FilePackageGetRule;
            }
            set
            {
                m_FilePackageGetRule = value;
            }
        }
        #endregion

        #endregion

        #region Methods

        public void Dispose()
        {
            return;
        }

        /// <summary>
        /// 执行文件打包
        /// </summary>
        /// <returns></returns>
        public virtual Tuple<bool, string> ExecPack()
        {
            try
            {
                //Step1：参数校验
                Tuple<bool, string> tRtn = ExecPackParamsCheck();
                if (!tRtn.Item1)
                {
                    return tRtn;
                }
                //Step2：生成打包信息文件.PKey
                Tuple<bool, string> tGetPackInfoFileRtn = GeneratePackageInfoFile();
                if (!tGetPackInfoFileRtn.Item1)
                {
                    return tGetPackInfoFileRtn;
                }
                //Step3：生成打包文件.Zip
                //打包源目录
                byte[] btPackDatas = ZipHelper.DoCreateFromDirectory(SourceDirectory, CompressionLevel.Optimal, false);
                var cPackedFileFullName = Path.Combine(DestinationDirectory, GetNewPublishPackFileName());
                /*
                 * Q：191226
                 * 这里没有判断目标文件夹是否有写权限，如果没有，则对目标文件夹添加写权限
                 * 后期todo
                 */
                using (FileStream fs = new FileStream(cPackedFileFullName, FileMode.Create))
                {
                    fs.Write(btPackDatas, 0, btPackDatas.Length);
                    fs.Flush();
                    fs.Close();
                }

                #region 删除源目录下Key文件 -Myl 2020-01-13

                var vSourceKeyFileName = Path.Combine(SourceDirectory, tGetPackInfoFileRtn.Item2);
                if (System.IO.File.Exists(vSourceKeyFileName))
                {
                    System.IO.File.Delete(vSourceKeyFileName);
                }

                #endregion

                #region 数据备份，写入打包历史 Myl 20200115

                var vBasePackHisDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                vBasePackHisDir = Path.Combine(vBasePackHisDir, ConstantValues.DEFAULT_DEPLOY_CONFIG_BASE_PATH_NAME);
                vBasePackHisDir = Path.Combine(vBasePackHisDir, ConstantValues.DEFAULT_PACKAGE_HISTORY_PATH_NAME);
                if (!System.IO.Directory.Exists(vBasePackHisDir))
                {
                    System.IO.Directory.CreateDirectory(vBasePackHisDir);
                }
                var vePackHisFileName = CopyHelper.FileCopy(cPackedFileFullName, vBasePackHisDir);

                HistoryType htTmp = HistoryType.PACK_IIS;
                if (PackParams.PackType == Enums.DeployTypes.SQL)
                {
                    htTmp = HistoryType.PACK_SQL;
                }
                FilePackHistoryRepository<FilePackHistoryItem<BasePackageZipItem>, BasePackageZipItem>
                    hisFactory = new Impl.FilePackHistoryRepository<Impl.FilePackHistoryItem<Impl.BasePackageZipItem>, Impl.BasePackageZipItem>(htTmp, vBasePackHisDir)
                    {
                        HistoryDir = vBasePackHisDir
                    };
                //添加包历史
                //FilePackHistoryRepository<TParams, TItem> hisFactory = new FilePackHistoryRepository<TParams, TItem>()
                //{
                //    HistoryDir = vBasePackHisDir
                //};
                Tuple<bool, string> tAddRtn = hisFactory.AddHistory(new FilePackHistoryItem<BasePackageZipItem>()
                {
                    GUID = System.Guid.NewGuid().ToString("N").ToUpper(),
                    TYPE = htTmp,
                    HistoryPackageFileFullName = vePackHisFileName,
                    PackData =this.PackData
                });

                #endregion

                return Tuple.Create(true, cPackedFileFullName);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }
        }

        /// <summary>
        /// 打包前参数检查
        /// </summary>
        /// <returns>
        /// true：参数检查完毕，符合打包条件
        /// false：参数检查失败，不符合打包条件
        /// </returns>
        public virtual Tuple<bool, string> ExecPackParamsCheck()
        {
            if (string.IsNullOrEmpty(SourceDirectory))
            {
                return Tuple.Create<bool, string>(false, "没有传入有效的源文件目录！");
            }
            if (!Directory.Exists(SourceDirectory))
            {
                return Tuple.Create(false, $"不存在的文件目录{SourceDirectory}！");
            }
            if (Directory.GetFiles(SourceDirectory, PackParams?.PackType == Enums.DeployTypes.IIS ? "*.svc" : "*.sql", SearchOption.AllDirectories).Length == 0)
            {
                return Tuple.Create(false, $"目录[{SourceDirectory}]下缺少{(PackParams?.PackType == Enums.DeployTypes.IIS ? "svc" : " sql")}文件！");
            }
            if (PackParams == null && PackParams.PackParamKey == null)
            {
                return Tuple.Create(false, "缺少有效的打包参数！");
            }

            return Tuple.Create(true, "");
        }

        /// <summary>
        /// 生成打包信息文件
        /// </summary>
        /// <returns>
        /// T1：是否生成成功
        /// T2：生成成功的打包信息文件名称或者失败的文本描述信息
        /// </returns>
        public virtual Tuple<bool, string> GeneratePackageInfoFile()
        {
            try
            {
                //删除已有的打包信息文件.PKey
                DeleteCurrentPKeyFiles();
                var cPackageInfoString = JsonConvert.SerializeObject(PackParams, Formatting.Indented);
                cPackageInfoString = DESEncrypt.Encrypt(cPackageInfoString);
                var cPackInfoFileName = $"PACKAGEINFO_{System.Guid.NewGuid().ToString("N").ToUpper()}{ConstantValues.DEFAULT_FILE_PACKINFO_EXTENTION_NAME}";

                //向源目录写入发版文件
                File.WriteAllText(Path.Combine(SourceDirectory, cPackInfoFileName), cPackageInfoString, Encoding.UTF8);

                return Tuple.Create(true, cPackInfoFileName);
            }
            catch (Exception ex)
            {
                //throw new FilesPackException(ex.Message, ex);
                return Tuple.Create(false, ex.Message);
            }
        }

        /// <summary>
        /// 获取ZIP打包文件名称
        /// </summary>
        /// <returns></returns>
        public virtual string GetNewPublishPackFileName()
        {
            try
            {
                return $"{PackParams.PackType.ToString()}_Publish_{DateTime.Now.ToString("yyyyMMddHHmmss")}.zip";
            }
            catch (Exception)
            {
                return System.Guid.NewGuid().ToString("N").ToUpper();
            }
        }

        /// <summary>
        /// 删除已有的打包信息文件.PKey
        /// </summary>
        private void DeleteCurrentPKeyFiles()
        {
            string[] cPKeyFiles = Directory.GetFiles(SourceDirectory, $"*{ConstantValues.DEFAULT_FILE_PACKINFO_EXTENTION_NAME}", SearchOption.TopDirectoryOnly);
            foreach (var item in cPKeyFiles)
            {
                File.Delete(item);
            }
        }

        /// <summary>
        /// 获取包的详细信息
        /// </summary>
        /// <param name="cPathOrFileName">文件夹路径或者单个包文件的路径名称</param>
        /// <returns>
        /// 返回所有包的明细信息Tuple
        /// Item1：是否获取成功
        /// Item2：获取失败的错误消息
        /// Item3：包明细列表（列表由字典组成（Key-包文件路径名称：Value-包明细信息对象））
        /// </returns>
        public virtual Tuple<bool, string, IList<IDictionary<string, TItem>>> GetPackagesDetailInfo(string cPathOrFileName = null)
        {
            #region 校验
            if (string.IsNullOrEmpty(cPathOrFileName))
            {
                return Tuple.Create<bool, string, IList<IDictionary<string, TItem>>>(false, "没有指定包文件或者路径参数！", null);
            }
            #endregion

            if (CopyHelper.PathIsDirectory(cPathOrFileName))
            {
                string[] cPKeyFiles = Directory.GetFiles(cPathOrFileName, "*.zip", SearchOption.AllDirectories);
                if (cPKeyFiles.Length == 0)
                {
                    throw new Exception($"目录[{cPathOrFileName}]下没有任何包文件！");
                }
                IList<IDictionary<string, TItem>> iPkgs = new List<IDictionary<string, TItem>>();
                foreach (var item in cPKeyFiles)
                {
                    iPkgs.Add(new Dictionary<string, TItem>()
                    {
                        {
                            item,GetPackageFileInfo(item)
                        }
                    });
                }
                return Tuple.Create<bool, string, IList<IDictionary<string, TItem>>>(true, "", iPkgs);
            }
            else
            {
                #region 返回单个包文件
                return GetSinglePackageDetailInfo(cPathOrFileName);
                #endregion
            }
        }

        public Tuple<bool, string, IList<IDictionary<string, TItem>>> GetSinglePackageDetailInfo(string cSinglePckageFileName = null)
        {
            if (!File.Exists(cSinglePckageFileName))
            {
                throw new Exception($"包文件[{cSinglePckageFileName}]不存在！");
            }

            return new Tuple<bool, string, IList<IDictionary<string, TItem>>>(
                true,
                "",
                new List<IDictionary<string, TItem>>()
                {
                        new  Dictionary<string, TItem>()
                        {
                            {
                                cSinglePckageFileName,
                                GetPackageFileInfo(cSinglePckageFileName)
                            }
                        }
                });
        }

        /// <summary>
        /// 获取包发布信息
        /// </summary>
        /// <param name="cSinglePackageZipFileName"></param>
        /// <returns></returns>
        public TItem GetPackageFileInfo(string cSinglePackageZipFileName)
        {
            try
            {
                //目前做法：解压zip中的PKey包信息文件到一个临时目录，反出信息对象，并删除临时文件 Myl 191228
                ZipArchive zaTemp = ZipFile.OpenRead(cSinglePackageZipFileName);
                if (zaTemp == null)
                {
                    throw new Exception($"读取包文件[{cSinglePackageZipFileName}]时发生未知异常！");
                }

                #region 包校验

                if (FilePackageGetRule != null)
                {
                    if (!FilePackageGetRule(zaTemp))
                    {
                        throw new Exception($"包文件[{cSinglePackageZipFileName}]不是有效的{PackParams.PackType.ToString()}部署包！");
                    }
                }

                #endregion

                int iFileExtensionLength = ConstantValues.DEFAULT_FILE_PACKINFO_EXTENTION_NAME.Length;
                ZipArchiveEntry zaeTemp = zaTemp.Entries.FirstOrDefault(A =>
                  {
                      string pkFileName = A?.Name.Trim();
                      if (string.IsNullOrEmpty(pkFileName) || pkFileName.Length <= iFileExtensionLength)
                      {
                          return false;
                      }

                      return string.Compare(pkFileName.Substring(pkFileName.Length - iFileExtensionLength, iFileExtensionLength).ToUpper(), ConstantValues.DEFAULT_FILE_PACKINFO_EXTENTION_NAME, true) == 0;
                  });
                if (zaeTemp == null)
                {
                    throw new Exception($"读取包文件[{cSinglePackageZipFileName}]时出错！没有找到包信息文件[{ConstantValues.DEFAULT_FILE_PACKINFO_EXTENTION_NAME}]！");
                }

                //解压Key文件到临时目录
                string cTempDirectory = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), ConstantValues.DEFAULT_PACKAGEFILE_EXACT_DIRECTORY_NAME);

                if (!Directory.Exists(cTempDirectory))
                {
                    Directory.CreateDirectory(cTempDirectory);
                }
                //解压的临时包内信息文件名称【类似：c:\IIS_Publish_20191226192521.zip】
                string cExtractedTempFileName = System.IO.Path.Combine(cTempDirectory, zaeTemp.Name);
                zaeTemp.ExtractToFile(cExtractedTempFileName, true);
                //读取临时文件
                TItem rtnItem = GetPackageParams(cExtractedTempFileName)?.PackParamKey;

                #region 删除临时解压的包信息文件
                if (System.IO.File.Exists(cExtractedTempFileName))
                {
                    System.IO.File.Delete(cExtractedTempFileName);
                }
                #endregion

                return rtnItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取包参数（包含包类型，打包信息等）
        /// </summary>
        /// <param name="cPackageFilePath"></param>
        /// <returns></returns>
        private TParams GetPackageParams(string cPackageFilePath)
        {
            //包信息文件内字符
            string cPackageInfoString = File.ReadAllText(cPackageFilePath, Encoding.UTF8);
            cPackageInfoString = DESEncrypt.Decrypt(cPackageInfoString);
            var cPackageInfoObj = JsonConvert.DeserializeObject<TParams>(cPackageInfoString);
            return cPackageInfoObj;
        }

        public Tuple<bool, string> ExecUnPack(string cSourcePackageFileFullName, string cDestinationDirectoryName, bool bDeleteDestDirAllFiles = true)
        {
            try
            {
                #region 目录&目录文件预处理 Myl 191230 Add

                if (string.IsNullOrEmpty(cSourcePackageFileFullName) || string.IsNullOrEmpty(cDestinationDirectoryName))
                {
                    return Tuple.Create(false, "没有有效的源和目的参数！");
                }
                var vRetryCounts = 0;
                if (Directory.Exists(cDestinationDirectoryName))
                {
                ReTryDeleteDir:
                    vRetryCounts++;
                    try
                    {
                        //if (bDeleteDestDirAllFiles)
                        //{
                        //    //DirectoryInfo di = new DirectoryInfo(cDestinationDirectoryName);
                        //    //di.Delete(true);
                        //    CopyHelper.DeleteDirAllFiles(cDestinationDirectoryName, false);
                        //}
                        DirectoryInfo dir = new DirectoryInfo(cDestinationDirectoryName);
                        dir.Delete(true);
                    }
                    catch (Exception ex)
                    {
                        if (vRetryCounts >= 3)
                        {
                            throw ex;
                        }
                        Thread.Sleep(5000);
                        goto ReTryDeleteDir;
                    }
                }
                else
                {
                    Directory.CreateDirectory(cDestinationDirectoryName);
                }

                #endregion

                if (ZipHelper.UnZipToDirectory(cSourcePackageFileFullName, cDestinationDirectoryName))
                {
                    return Tuple.Create(true, "");
                }
                else
                {
                    return Tuple.Create(false, "包解压失败！未知错误！");
                }
            }
            catch (Exception ex)
            {
                return Tuple.Create(false, $"包解压失败！{ex.Message}");
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }


        //protected virtual bool GetPackagesDetailRule



        #endregion
    }
}
