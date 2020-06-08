/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   IISHelper.cs
* 功能描述： IIS操作帮助类
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-24 11:13:48
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-24 11:13:48		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using Microsoft.Web.Administration;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;
using TechScan.Tool.U8.ServiceDeploy.Base.Exceptions;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils;
using TechScan.Tool.U8.ServiceDeploy.IIS.Config;

namespace TechScan.Tool.U8.ServiceDeploy.IIS.Impl
{
    /// <summary>
    /// IIS操作帮助类
    /// </summary>
    public static class IISHelper
    {
        #region Methods

        /// <summary>
        /// 获取服务器IIS版本号
        /// </summary>
        /// <returns>返回IIS版本号</returns>
        public static int GetIISVersion()
        {
            try
            {
                RegistryKey parameters = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\W3SVC\\Parameters");
                int MajorVersion = (int)parameters.GetValue("MajorVersion");
                return MajorVersion;
            }
            catch (Exception ex)
            {
                //return -1;

                throw new IISDeployBaseException($"获取IIS版本时发生错误:{ex.Message}");
            }
        }

        /// <summary>
        /// 检查应用程序池是否为空
        /// </summary>
        /// <param name="applicationPoolName">应用程序池名称</param>
        /// <returns>
        /// true：应用程序池已经存在
        /// false：应用程序池不存在
        /// </returns>
        public static bool IsApplicationPoolExist(string applicationPoolName)
        {
            try
            {
                using (ServerManager iis = new ServerManager())
                {
                    return iis.ApplicationPools[applicationPoolName ?? ConstantValues.DEFAULT_IIS_APPLICATIONPOOL_NAME] != null;
                }
            }
            catch (Exception ex)
            {
                throw new IISDeployRuntimeException(ex.Message, ex);
            }
        }

        public static void installsite()
        {
            using (ServerManager iisManager = new ServerManager())
            {
                var app = iisManager.Sites;
            }
        }

        public static string ApplicationPoolStart(string applicationPoolName)
        {
            try
            {
                using (ServerManager iis = new ServerManager())
                {
                    var pool = iis.ApplicationPools[applicationPoolName];

                    if (pool == null || pool.State == ObjectState.Started)
                    {
                        return String.Empty;
                    }

                    pool.Start();
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static bool InstallWebSite(out string errMsg, IISConfig iisConfig = null,
            Action<DeployLogType, string, bool, string> Log = null,
            Action<string> act = null)
        {
            try
            {
                errMsg = string.Empty;

                #region 添加虚拟目录用户权限
                if (iisConfig?.VirtualSiteUserAuth?.IsAddUserAuth ?? true)
                {
                    Log(DeployLogType.Info, $"开始添加虚拟目录用户[{iisConfig?.VirtualSiteUserAuth?.ToString()}]的完全控制权限", false, "");
                    act($"开始添加虚拟目录用户控制权限");
                    var vAddDirUserAuth = FilesHelper.AddSecurityControll2Folder(iisConfig?.PhysicalPath, iisConfig?.VirtualSiteUserAuth?.Users);
                    if (!vAddDirUserAuth.Item1)
                    {
                        Log(DeployLogType.Warn, $"添加虚拟目录{iisConfig?.PhysicalPath}用户控制权限失败,可尝试手动添加！错误原因：{vAddDirUserAuth.Item2}", false, "");
                    }
                    else
                    {
                        Log(DeployLogType.Info, $"添加虚拟目录用户控制权限成功！", false, "");
                    }
                }
                #endregion

                #region 是否需要重注册IIS
                #endregion

                using (ServerManager iisManager = new ServerManager())
                {
                    #region 创建应用程序池

                    ApplicationPoolConfig poolConfig = (iisConfig?.AppPoolConfig) ?? new Config.ApplicationPoolConfig() { };
                    if (IsApplicationPoolExist(poolConfig?.PoolName))
                    {
                        #region 已经存在则修改程序池属性
                        Log(DeployLogType.Info, $"修改应用程序池{poolConfig?.PoolName}", false, "");
                        act($"修改应用程序池{poolConfig?.PoolName}");
                        ApplicationPool appPool = iisManager.ApplicationPools[poolConfig.PoolName];
                        appPool.ManagedRuntimeVersion = poolConfig.ManagedRuntimeVersion;
                        appPool.ManagedPipelineMode = poolConfig.ManagedPipelineMode;
                        appPool.Enable32BitAppOnWin64 = poolConfig.Enable32BitAppOnWin64;
                        if (appPool.State != ObjectState.Started)
                        {
                            appPool.Start();
                        }
                        #endregion
                    }
                    else
                    {
                        #region 如果不存在则创建应用程序池
                        Log(DeployLogType.Info, $"开始创建应用程序池{poolConfig?.PoolName}", false, "");
                        act($"开始创建应用程序池{poolConfig?.PoolName}");
                        ApplicationPool newPool = iisManager.ApplicationPools.Add(poolConfig.PoolName);
                        newPool.ManagedRuntimeVersion = poolConfig.ManagedRuntimeVersion;
                        newPool.ManagedPipelineMode = poolConfig.ManagedPipelineMode;
                        newPool.Enable32BitAppOnWin64 = poolConfig.Enable32BitAppOnWin64;
                        newPool.AutoStart = true;

                        #endregion
                    }

                    #region 启动程序池

                    var retryStartPool = 0;
                ReTryStartPool:
                    retryStartPool++;
                    var poolstartReult = ApplicationPoolStart(poolConfig.PoolName);
                    if (!string.IsNullOrEmpty(poolstartReult))
                    {
                        if (retryStartPool >= 3)
                        {
                            throw new IISDeployRuntimeException($"无法启动程序池{poolConfig.PoolName}！错误为:{poolstartReult}");
                        }

                        Thread.Sleep(5000);
                        goto ReTryStartPool;
                    }
                    Log(DeployLogType.Info, $"应用程序池{poolConfig?.PoolName}启动成功！", false, "");
                    #endregion

                    #endregion

                    #region 添加端口号
                    Log(DeployLogType.Info, "开始配置网站端口！", false, "");
                    //act("开始配置网站端口");

                    var mainSite = iisManager.Sites[iisConfig?.SiteName];
                    if (mainSite == null)
                    {
                        iisManager.Sites.Add(ConstantValues.DEFAULT_IIS_SITE_NAME, @"%SystemDrive%\inetpub\wwwroot", ConstantValues.DEFAULT_IIS_PORT);
                    }

                    foreach (var item in iisConfig?.Ports)
                    {
                        Binding bindFind = mainSite.Bindings?.FirstOrDefault((A) =>
                        {
                            //bool? bEndPointExist = A?.EndPoint?.Port.Equals(item);
                            return (A?.EndPoint?.Port.Equals(item)) ?? false;
                        });
                        if (bindFind == null)
                        {
                            Binding bindTemp = mainSite.Bindings.CreateElement();
                            bindTemp.SetAttributeValue("protocol", "http");
                            //bindTemp.SetAttributeValue("bindingInformation", $"*:{item}:{iisConfig?.VirtualSiteName}");
                            bindTemp.SetAttributeValue("bindingInformation", $"*:{item}:");
                            mainSite.Bindings.Add(bindTemp);
                        }
                    }
                    Log(DeployLogType.Info, "网站端口配置成功！", false, "");
                    act("网站端口配置成功");

                    #endregion

                    #region 添加MIME
                    //docs.microsoft.com/en-us/iis/configuration/system.webserver/staticcontent/mimemap
                    Configuration webConfig = iisManager.GetWebConfiguration(iisConfig?.SiteName);
                    ConfigurationSection staticContentSection = webConfig.GetSection("system.webServer/staticContent");
                    ConfigurationElementCollection staticContentCollection = staticContentSection.GetCollection();

                    foreach (KeyValuePair<string, string> kvp in iisConfig?.MIME_Types)
                    {
                        //检查是否已经存在相应内容
                        ConfigurationElement mimeMapElement = staticContentCollection?.FirstOrDefault(A =>
                        {
                            return A.GetAttribute("fileExtension")?.Value?.Equals(kvp.Key) ?? false;
                        });
                        if (mimeMapElement == null)
                        {
                            mimeMapElement = staticContentCollection.CreateElement("mimeMap");
                            mimeMapElement["fileExtension"] = kvp.Key;
                            mimeMapElement["mimeType"] = kvp.Value;
                            staticContentCollection.Add(mimeMapElement);
                        }
                        else
                        {
                            mimeMapElement["mimeType"] = kvp.Value;
                        }
                    }
                    Log(DeployLogType.Info, "MIME映射配置成功！", false, "");
                    act("MIME映射成功");
                    #endregion

                    #region 创建虚拟站点

                    var apps = iisManager.Sites[iisConfig?.SiteName]?.Applications;

                    //Application newApp = apps?[iisConfig?.VirtualSiteName];
                    Application newApp = apps.FirstOrDefault(A =>
                    {
                        return A.Path.Equals($"/{iisConfig?.VirtualSiteName}");
                    });
                    if (newApp != null)
                    {
                        //if (!apps.AllowsRemove)
                        //{
                        //    //apps.AllowsRemove = true;
                        //}
                        apps.Remove(newApp);
                    }
                    newApp = apps.Add($"/{iisConfig?.VirtualSiteName}", iisConfig?.PhysicalPath);
                    newApp.ApplicationPoolName = poolConfig.PoolName;

                    iisManager.CommitChanges();
                    Log(DeployLogType.Info, $"虚拟站点{iisConfig?.VirtualSiteName}创建成功！", false, "");
                    act($"虚拟站点{iisConfig?.VirtualSiteName}创建成功！");
                    #endregion

                    return true;
                }
            }
            catch (ServerManagerException smEx)
            {
                Log(DeployLogType.Error, $"Message：{smEx.Message}{System.Environment.NewLine}Source：{smEx.Source}{System.Environment.NewLine}StackTrace：{smEx.StackTrace}", false, "");
                errMsg = smEx.Message;
                throw new IISDeployRuntimeException(smEx.Message, smEx);
            }
            catch (Exception ex)
            {
                Log(DeployLogType.Error, $"Message：{ex.Message}{System.Environment.NewLine}Source：{ex.Source}{System.Environment.NewLine}StackTrace：{ex.StackTrace}", false, "");
                errMsg = ex.Message;
                throw new IISDeployRuntimeException(ex.Message, ex);
            }
        }

        /// <summary>
        /// 检查IIS端口是否被占用
        /// （暂时无法判断端口是否是IIS占用的或者其他程序占用的）
        /// </summary>
        /// <param name="lstPorts"></param>
        /// <returns></returns>
        public static bool IISPortIsOccupied(IList<int> lstPorts = null)
        {
            //https://blog.csdn.net/jayzai/article/details/8182418

            //下面有问题
            if (lstPorts == null)
            {
                return false;
            }

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();
            foreach (var eachPort in lstPorts)
            {
                foreach (IPEndPoint endPoint in ipEndPoints)
                {
                    if (endPoint.Port == eachPort)
                    {
                        throw new IISDeployBaseException($"端口：{eachPort}已经被占用！");
                    }
                }
            }
            return false;
        }

        public static bool ExistSqlServerService(string tem)
        {
            // https://www.cnblogs.com/applebox/p/11611305.html
            //https://www.cnblogs.com/wolf-sun/p/4591734.html
            //https://www.cnblogs.com/yplong/p/3731118.html
            bool ExistFlag = false;
            ServiceController[] service = ServiceController.GetServices();
            for (int i = 0; i < service.Length; i++)
            {
                if (service[i].ServiceName.ToString() == tem)
                {
                    ExistFlag = true;
                }
            }
            return ExistFlag;

            /*
             * 
             * 
             * *:80:
808:*
*
localhost
localhost
*:81:


Protocol
Host
EndPoint Port


iisManager.GetWebConfiguration("Default Web Site").GetSection("system.webServer/staticContent").GetCollection()[0]["fileExtension"]
             * 
             */

        }

        /// <summary>
        /// 得到网站的物理路径
        /// </summary>
        /// <param name="rootEntry">网站节点</param>
        /// <returns></returns>
        public static string GetWebsitePhysicalPath(DirectoryEntry rootEntry)
        {
            //https://www.cnblogs.com/hnsongbiao/p/7435745.html
            string physicalPath = "";
            foreach (DirectoryEntry childEntry in rootEntry.Children)
            {
                if ((childEntry.SchemaClassName == "IIsWebVirtualDir") && (childEntry.Name.ToLower() == "root"))
                {
                    if (childEntry.Properties["Path"].Value != null)
                    {
                        physicalPath = childEntry.Properties["Path"].Value.ToString();
                    }
                    else
                    {
                        physicalPath = "";
                    }
                }
            }
            return physicalPath;
        }

        public static List<ApplicationPool> GetServerCurrentAppPools(Func<ApplicationPool, bool> predicate = null)
        {
            try
            {
                using (ServerManager iis = new ServerManager())
                {
                    return iis.ApplicationPools?.Where(predicate).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new IISDeployRuntimeException(ex.Message, ex);
            }
        }

        #region IIS重注册

        /// <summary>
        /// IIS异步重注册（.Net,WCF）
        /// </summary>
        /// <param name="cmds"></param>
        /// <returns></returns>
        private static async Task<bool> IISReReg(BaseCommandItem cmds)
        {
            try
            {
                if ((cmds?.IsCmdRequired) ?? false)
                {
                    foreach (var eachCommand in cmds?.Commands)
                    {
                        if (string.IsNullOrEmpty(eachCommand))
                        {
                            continue;
                        }
                        //这里暂时没有记录判断命令行执行结果，统一返回true的，后期再说了 20200108
                        var vCmdExecRtn = await ExecuteCmd.ExecCmdAsync(eachCommand);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                //Log(DeployLogType.Warn, $"IIS注册失败!错误原因:{ex.Message}", true);
                throw ex;
            }
        }
        public static async Task<bool> IISReRegNet(IISRegNet cmds)
        {
            return await IISReReg(cmds);
        }
        public static async Task<bool> IISReRegWcf(IISRegWCF cmds)
        {
            return await IISReReg(cmds);
        }
        #endregion

        #endregion
    }

}
