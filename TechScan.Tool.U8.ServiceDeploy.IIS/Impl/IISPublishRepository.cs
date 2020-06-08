/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   IISPublishRepository.cs
* 功能描述： IISWeb服务部署实现
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-24 17:25:49
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-24 17:25:49		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;
using TechScan.Tool.U8.ServiceDeploy.Base.Exceptions;
using TechScan.Tool.U8.ServiceDeploy.Base.Impl;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils;
using TechScan.Tool.U8.ServiceDeploy.IIS.Config;
using TechScan.Tool.U8.ServiceDeploy.IIS.Impl.Rest;

namespace TechScan.Tool.U8.ServiceDeploy.IIS.Impl
{
    /// <summary>
    /// IIS服务部署实现
    /// </summary>
    public class IISPublishRepository
        : IDeployProvider<IISConfig, IISDeployParams>
    {
        #region Property
        
        /// <summary>
        /// IIS配置
        /// </summary>
        public IISConfig ConfigData
        {
            get;
            set;
        }
        /// <summary>
        /// Web服务发布配置
        /// </summary>
        public IISDeployParams DeployParamData
        {
            get;
            set;
        }
        /// <summary>
        /// 历史数据目录
        /// </summary>
        public string HistoryDataDir
        {
            get;
            set;
        }
        /// <summary>
        /// 日志对象
        /// </summary>
        public Action<DeployLogType, string, bool, string> Log
        {
            get;
            set;
        }
        public string ProviderName
        {
            get
            {
                return "IIS";
            }
        }

        #endregion

        #region Ctor
        #endregion

        #region Methods
        #region 确认是否已经安装U8

        /// <summary>  
        /// 确认是否已经安装了U8
        /// 暂时注册表里面没有找到U8的安装信息，暂时检查是否安装了U8的服务的办法
        /// </summary>  
        /// <returns>true: 有安裝, false:沒有安裝</returns>  
        private bool CheckU8Installed()
        {
            try
            {
                #region 注册表检查
                /*注册表暂时不好用
                Microsoft.Win32.RegistryKey uninstallNode = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Installer\UserData");
                foreach (string subKeyName in uninstallNode.GetSubKeyNames())
                {
                    Microsoft.Win32.RegistryKey subKey = uninstallNode.OpenSubKey(subKeyName);
                    object displayName = subKey.GetValue("DisplayName");
                    if (displayName != null)
                    {
                        if (displayName.ToString().Contains("EnterprisePortal.exe"))
                        {
                            return true;
                        }
                    }
                }
                return false;
                */
                #endregion

                #region 检查U8服务是否安装

                return IsU8WindowsServiceInstalled(new List<string>()
                {
                    "U8DispatchService","U8EncryptService","U8KeyManagePool","U8SCMPool","U8TaskService","UFAllNet"
                });

                #endregion
            }
            catch (Exception ex)
            {
                Log(DeployLogType.Warn, $"检查U8安装环境时发生异常错误{ex.Message}", false, "");
                return false;
            }
        }

        /// <summary>
        /// 检查U8服务是否已经安装
        /// （目前只要规定的U8应用服务器上指定的几个服务有一个安装了，我就认为U8应用服务已经安装了）这个不太完善，后期有必要再修改
        /// </summary>
        /// <param name="serviceNameList">U8服务名称集合</param>
        /// <returns></returns>
        private bool IsU8WindowsServiceInstalled(List<string> serviceNameList)
        {
            ServiceController[] vServiceList = ServiceController.GetServices();
            //var vt =
            //vServiceList.ToLookup(P => P.DisplayName);
            //vServiceList.ToLookup(P =>{ return P.ServiceType;  });
            return vServiceList.FirstOrDefault(A =>
             {
                 return serviceNameList.FirstOrDefault(B => { return B.ToUpper().Equals(A.ServiceName.ToUpper()); }) != null;
             }) != null;
        }

        #endregion

        /// <summary>
        /// 发布前数据校验
        /// </summary>
        /// <returns></returns>
        public void DataValidationBeforeDeploy()
        {
            #region 判断服务器IIS版本（系统要求7.0或者以上）

            int currentIISVersion = IISHelper.GetIISVersion();
            if (currentIISVersion <= 6)
            {
                throw new IISDeployBaseException($"当前服务器IIS版本为{currentIISVersion},版本过低，系统要求IIS 7.0或者以上！");
            }

            #endregion

            #region 检查是否已经安装了U8

            if (!CheckU8Installed())
            {
                throw new IISDeployBaseException("当前服务器还未安装U8系统，请确保U8应用服务已经安装！");
            }

            #endregion

            if (string.IsNullOrEmpty(DeployParamData?.SourceDeployPackageFileName))
            {
                throw new IISDeployBaseException($"请先指定服务安装包！");
            }
            if (string.IsNullOrEmpty(HistoryDataDir))
            {
                throw new IISDeployBaseException("请先指定历史数据目录！");
            }

            /*
             * Others todo
             */

            return;
        }
        public void Dispose()
        {
            return;
            //throw new NotImplementedException();
        }

        #region U8Android服务发布
        public /*async */Tuple<bool, string> ExecDeploy(Action<string> act)
        {
            Log(DeployLogType.Info, "开始基础环境检测", false, "");
            act("开始基础环境检测");
            #region 基础环境数据校验（IIS版本/U8环境等）
            DataValidationBeforeDeploy();
            Log(DeployLogType.Info, "基础环境检测通过!", false, "");
            act("基础环境检测通过!");
            #endregion

            #region  IIS重注册
            //这里有一个问题，重注册时，没有对执行结果进行检查，统一认为是成功的，先不管，后期再说
            //后期todo
            if (ConfigData?.RegNet?.IsCmdRequired ?? false)
            {
                Log(DeployLogType.Info, "正在对IIS重注册.Net环境!", false, "");
                act("正在重注册.Net!");
                IISHelper.IISReRegNet(ConfigData?.RegNet);
                Log(DeployLogType.Info, "IIS重注册.Net环境成功!", false, "");
                act("重注册.Net成功!");
            }
            if (ConfigData?.RegWCF?.IsCmdRequired ?? false)
            {
                Log(DeployLogType.Info, "正在对IIS重注册WCF环境!", false, "");
                act("正在重注册WCF!");
                IISHelper.IISReRegWcf(ConfigData?.RegWCF);
                Log(DeployLogType.Info, "IIS重注册WCF环境成功!", false, "");
                act("重注册WCF成功!");
            }

            #endregion

            //throw new Exception("完犊子");
            #region Step1：解压包到发布目录
            Log(DeployLogType.Info, $"开始解压部署包{DeployParamData?.SourceDeployPackageFileName}到IIS物理路径{ConfigData?.PhysicalPath}!", false, "");
            Log(DeployLogType.Info, $"{ConfigData?.PhysicalPath}", true, "");

            act("部署包解压中");
            var sourcePackage = new IISPackageRepository(DeployParamData?.SourceDeployPackageFileName);
            var vUnZipRtn = sourcePackage.ExecUnPack(DeployParamData?.SourceDeployPackageFileName, ConfigData?.PhysicalPath);
            if (!vUnZipRtn.Item1)
            {
                Log(DeployLogType.Error, $"部署包[{DeployParamData?.SourceDeployPackageFileName}]{vUnZipRtn.Item2}!", false, "");
                return vUnZipRtn;
            }

            DeployParamData.DeployVersion = new DeployVersion(DeployTypes.IIS, sourcePackage?.PackData?.PkgType ?? Base.Enums.PackageType.Release)
            {
                PackType = sourcePackage?.PackData?.PackType ?? DeployTypes.IIS,
                Version = sourcePackage?.PackData?.Version,
                PkgType = sourcePackage?.PackData?.PkgType ?? PackageType.Release,
                Description = sourcePackage?.PackData?.Description,
                PackageDateTime = (sourcePackage?.PackData?.PackageDateTime == null) ? DateTime.MinValue : sourcePackage.PackData.PackageDateTime,
                Packager = sourcePackage?.PackData?.Packager
            };

            Log(DeployLogType.Info, $"部署包{DeployParamData?.SourceDeployPackageFileName}解压成功!", false, "");
            act("部署包解压成功");

            #endregion
            Log(DeployLogType.Debug, $"开始执行IIS站点{ConfigData.VirtualSiteName}{(DeployParamData?.PublishType == PublishTypes.General ? "发布" : "回滚")}!", false, "");
            act($"开始站点{(DeployParamData?.PublishType == PublishTypes.General ? "发布" : "回滚")}");
            //throw new IISDeployRuntimeException("完犊子错误测试！");
            #region Step2：执行发布
            string errMsg = string.Empty;
            if (!IISHelper.InstallWebSite(out errMsg, ConfigData, Log, act))
            {
                throw new IISDeployRuntimeException(errMsg);
            }
            #endregion

            #region 执行服务测试 Myl 20200107Add
            Log(DeployLogType.Info, $"开始执行服务接口测试!", false, "");
            act("开始执行服务接口测试");
            var vServiceTest = TestRestServiceAsync();
            if (!vServiceTest.Result.IsOk)
            {
                Log(DeployLogType.Warn, $"服务接口测试失败!错误原因:{vServiceTest.Result.Msg}", false, "");
            }
            else
            {
                act("服务接口测试成功");
                Log(DeployLogType.Info, $"服务接口测试成功!当前U8Android后台服务接口版本为:{vServiceTest.Result.Msg}", false, "");
            }
            #endregion

            Log(DeployLogType.Info, $"IIS站点{ConfigData.VirtualSiteName}{(DeployParamData?.PublishType == PublishTypes.General ? "发布" : "回滚")}成功!", false, "");
            act($"站点发布{(DeployParamData?.PublishType == PublishTypes.General ? "发布" : "回滚")}");
            Log(DeployLogType.Info, $"开始执行数据备份!", false, "");
            act("开始执行数据备份");

            #region Step3：发布完成后数据处理

            //拷贝包到历史记录所在目录
            var cDestBackFileName = string.Empty;
            if (DeployParamData?.PublishType == PublishTypes.General)//Myl 2020-01-06 Add
            {
                //正常发布时需要备份，回滚则不需要备份了，本来就是备份zip包
                cDestBackFileName = CopyHelper.FileCopy(DeployParamData?.SourceDeployPackageFileName, HistoryDataDir);
            }
            else
            {
                cDestBackFileName = DeployParamData?.SourceDeployPackageFileName;
            }
            //添加包历史
            IISDeployHistoryRepository hisFactory = new IISDeployHistoryRepository(Base.Enums.HistoryType.DEPLOY_IIS, HistoryDataDir)
            {
                HistoryDir = HistoryDataDir
            };

            Tuple<bool, string> tAddRtn = hisFactory.AddHistory(new IISDeployHistoryItem()
            {
                GUID = System.Guid.NewGuid().ToString("N").ToUpper(),
                TYPE = Base.Enums.HistoryType.DEPLOY_IIS,
                DeployParam = this.DeployParamData,
                ConfigData = this.ConfigData,

                HistoryPackageFileFullName = cDestBackFileName

            });
            if (tAddRtn.Item1)
            {
                Log(DeployLogType.Info, $"数据备份成功!{ Path.Combine(HistoryDataDir, Path.GetFileName(DeployParamData?.SourceDeployPackageFileName))}", true, "");
                act("数据备份成功");

                #region Web服务地址

                //http://localhost:8082/U8AndroidServiceTEST/Common/Common.svc
                //Func<string> fGetvPortTmp = () =>
                //{
                //    var vPorts = "";
                //    if (ConfigData?.Ports != null && ConfigData?.Ports.Count > 0)
                //    {
                //        var portNo80 = ConfigData?.Ports.FirstOrDefault((A) => { return !A.Equals(80); });
                //        if (portNo80 != null)
                //        {
                //            vPorts = $":{portNo80.ToString()}";
                //        }
                //    }
                //    return vPorts;
                //};

                #endregion

                return Tuple.Create<bool, string>
                    (true,
                    $"localhost{FuncGetSvcAddress()}/{ConfigData?.VirtualSiteName}/Common/Common.svc"
                    );
            }
            else
            {
                return tAddRtn;
            }
            #endregion
        }

        /// <summary>
        /// 异步执行U8Android服务发布
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        public async Task<Tuple<bool, string>> ExecDeployAsync(Action<string> act)
        {
            Log(DeployLogType.Info, "开始基础环境检测", false, "");
            act("开始基础环境检测");
            #region 基础环境数据校验（IIS版本/U8环境等）
            DataValidationBeforeDeploy();
            Log(DeployLogType.Info, "基础环境检测通过!", false, "");
            act("基础环境检测通过!");
            #endregion

            #region  IIS重注册
            //这里有一个问题，重注册时，没有对执行结果进行检查，统一认为是成功的，先不管，后期再说
            //后期todo
            if (ConfigData?.RegNet?.IsCmdRequired ?? false)
            {
                Log(DeployLogType.Info, "正在对IIS重注册.Net环境!", false, "");
                act("正在重注册.Net!");
                await IISHelper.IISReRegNet(ConfigData?.RegNet);
                Log(DeployLogType.Info, "IIS重注册.Net环境成功!", false, "");
                act("重注册.Net成功!");
            }
            if (ConfigData?.RegWCF?.IsCmdRequired ?? false)
            {
                Log(DeployLogType.Info, "正在对IIS重注册WCF环境!", false, "");
                act("正在重注册WCF!");
                await IISHelper.IISReRegWcf(ConfigData?.RegWCF);
                Log(DeployLogType.Info, "IIS重注册WCF环境成功!", false, "");
                act("重注册WCF成功!");
            }

            #endregion

            //throw new Exception("完犊子");
            #region Step1：解压包到发布目录
            Log(DeployLogType.Info, $"开始解压部署包{DeployParamData?.SourceDeployPackageFileName}到IIS物理路径{ConfigData?.PhysicalPath}!", false, "");
            Log(DeployLogType.Info, $"{ConfigData?.PhysicalPath}", true, "");

            act("部署包解压中");
            var sourcePackage = new IISPackageRepository(DeployParamData?.SourceDeployPackageFileName);
            var vUnZipRtn = sourcePackage.ExecUnPack(DeployParamData?.SourceDeployPackageFileName, ConfigData?.PhysicalPath);
            if (!vUnZipRtn.Item1)
            {
                Log(DeployLogType.Error, $"部署包[{DeployParamData?.SourceDeployPackageFileName}]{vUnZipRtn.Item2}!", false, "");
                return vUnZipRtn;
            }

            DeployParamData.DeployVersion = new DeployVersion(DeployTypes.IIS, sourcePackage?.PackData?.PkgType ?? Base.Enums.PackageType.Release)
            {
                Version = sourcePackage?.PackData?.Version,
                PkgType = sourcePackage?.PackData?.PkgType ?? PackageType.Release,
                Description = sourcePackage?.PackData?.Description,
                PackageDateTime = (sourcePackage?.PackData?.PackageDateTime == null) ? DateTime.MinValue : sourcePackage.PackData.PackageDateTime,
                Packager = sourcePackage?.PackData?.Packager
            };

            Log(DeployLogType.Info, $"部署包{DeployParamData?.SourceDeployPackageFileName}解压成功!", false, "");
            act("部署包解压成功");

            #endregion
            Log(DeployLogType.Debug, $"开始执行IIS站点{ConfigData.VirtualSiteName}{(DeployParamData?.PublishType == PublishTypes.General ? "发布" : "回滚")}!", false, "");
            act($"开始站点{(DeployParamData?.PublishType == PublishTypes.General ? "发布" : "回滚")}");
            //throw new IISDeployRuntimeException("完犊子错误测试！");
            #region Step2：执行发布
            string errMsg = string.Empty;
            if (!IISHelper.InstallWebSite(out errMsg, ConfigData, Log, act))
            {
                throw new IISDeployRuntimeException(errMsg);
            }
            #endregion

            #region 执行服务测试 Myl 20200107Add
            Log(DeployLogType.Info, $"开始执行服务接口测试!", false, "");
            act("开始执行服务接口测试");
            var vServiceTest = await TestRestServiceAsync();
            if (!vServiceTest.IsOk)
            {
                Log(DeployLogType.Warn, $"服务接口测试失败!错误原因:{vServiceTest.Msg}", false, "");
            }
            else
            {
                act("服务接口测试成功");
                Log(DeployLogType.Info, $"服务接口测试成功!当前U8Android后台服务接口版本为:{vServiceTest.Msg}", false, "");
            }
            #endregion

            Log(DeployLogType.Info, $"IIS站点{ConfigData.VirtualSiteName}{(DeployParamData?.PublishType == PublishTypes.General ? "发布" : "回滚")}成功!", false, "");
            act($"站点发布{(DeployParamData?.PublishType == PublishTypes.General ? "发布" : "回滚")}");
            Log(DeployLogType.Info, $"开始执行数据备份!", false, "");
            act("开始执行数据备份");

            #region Step3：发布完成后数据处理

            //拷贝包到历史记录所在目录
            var cDestBackFileName = string.Empty;
            if (DeployParamData?.PublishType == PublishTypes.General)//Myl 2020-01-06 Add
            {
                //正常发布时需要备份，回滚则不需要备份了，本来就是备份zip包
                cDestBackFileName = CopyHelper.FileCopy(DeployParamData?.SourceDeployPackageFileName, HistoryDataDir);
            }
            else
            {
                cDestBackFileName = DeployParamData?.SourceDeployPackageFileName;
            }
            //添加包历史
            IISDeployHistoryRepository hisFactory = new IISDeployHistoryRepository(Base.Enums.HistoryType.DEPLOY_IIS, HistoryDataDir)
            {
                HistoryDir = HistoryDataDir
            };

            Tuple<bool, string> tAddRtn = hisFactory.AddHistory(new IISDeployHistoryItem()
            {
                GUID = System.Guid.NewGuid().ToString("N").ToUpper(),
                TYPE = Base.Enums.HistoryType.DEPLOY_IIS,
                DeployParam = this.DeployParamData,
                ConfigData = this.ConfigData,
                HistoryPackageFileFullName = cDestBackFileName
            });
            if (tAddRtn.Item1)
            {
                Log(DeployLogType.Info, $"数据备份成功!{ Path.Combine(HistoryDataDir, Path.GetFileName(DeployParamData?.SourceDeployPackageFileName))}", true, "");
                act("数据备份成功");

                #region Web服务地址

                //http://localhost:8082/U8AndroidServiceTEST/Common/Common.svc
                //Func<string> fGetvPortTmp = () =>
                //{
                //    var vPorts = "";
                //    if (ConfigData?.Ports != null && ConfigData?.Ports.Count > 0)
                //    {
                //        var portNo80 = ConfigData?.Ports.FirstOrDefault((A) => { return !A.Equals(80); });
                //        if (portNo80 != null)
                //        {
                //            vPorts = $":{portNo80.ToString()}";
                //        }
                //    }
                //    return vPorts;
                //};

                #endregion

                return Tuple.Create<bool, string>
                    (true,
                    $"localhost{FuncGetSvcAddress()}/{ConfigData?.VirtualSiteName}/Common/CommonService.svc"
                    );
            }
            else
            {
                return tAddRtn;
            }
            #endregion
        }
        #endregion

        #region 服务测试
        private string FuncGetSvcAddress()
        {
            Func<string> fGetvPortTmp = (() =>
            {
                var vPorts = "";
                if (ConfigData?.Ports != null && ConfigData?.Ports.Count > 0)
                {
                    var portNo80 = ConfigData?.Ports.FirstOrDefault((A) => { return !A.Equals(80); });
                    if (portNo80 != null && portNo80 != 0)
                    {
                        vPorts = $":{portNo80.ToString()}";
                    }
                }
                return vPorts;
            });
            return fGetvPortTmp();
        }

        private async Task<RestCallResult> TestRestServiceAsync()
        {
            try
            {
                return await Task.Run<RestCallResult>(async () =>
                {
                    using (RestfulHelper rService = new Rest.RestfulHelper($"http://localhost{FuncGetSvcAddress()}/{ConfigData?.VirtualSiteName}/V1/Common/Version"
    , HttpVerb.GET, ""))
                    {
                        return await rService.MakeRequestAsync();
                    }
                });
            }
            catch (Exception ex)
            {
                //Log(DeployLogType.Warn, $"服务接口测试失败!错误原因:{ex.Message}", true);
                return new RestCallResult() { IsOk = false, Msg = ex.Message };
            }
        }
        #endregion

        #endregion
    }
}
