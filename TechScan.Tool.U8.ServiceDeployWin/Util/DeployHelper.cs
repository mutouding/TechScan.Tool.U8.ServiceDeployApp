/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   DeployHelper.cs
* 功能描述： 部署帮助类
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-23 17:02:07
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-23 17:02:07		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using TechScan.Tool.U8.ServiceDeploy.Base.Config;
using TechScan.Tool.U8.ServiceDeploy.Base.Encrypt;
using TechScan.Tool.U8.ServiceDeploy.Base.Exceptions;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils;
using TechScan.Tool.U8.ServiceDeploy.IIS.Config;
using TechScan.Tool.U8.ServiceDeploy.SQL.Config;

namespace TechScan.Tool.U8.ServiceDeployWin.Util
{
    /// <summary>
    /// 部署帮助类
    /// </summary>
    internal static class DeployHelper
    {
        #region Property

        #region 部署参数静态实例

        /// <summary>
        /// 部署参数静态实例
        /// </summary>
        private static DeployConfig m_DeployConfigCacheInstance;

        /// <summary>
        /// 部署参数静态实例
        /// </summary>
        public static DeployConfig DeployConfigCacheInstance
        {
            get
            {
                if (m_DeployConfigCacheInstance == null)
                {
                    m_DeployConfigCacheInstance = ReadConfig();
                }
                return m_DeployConfigCacheInstance;
            }
            set
            {
                if (value == null)
                {
                    throw new DeployBaseException("配置参数为NULL错误！");
                }
                m_DeployConfigCacheInstance = value;
                m_DeployConfigCacheInstance.SaveConfig();
            }
        }

        #endregion

        /// <summary>
        /// 部署过程产生的数据文件存放的根目录(暂时默认放在用户公共数据目录下，后期如果有需要再做成配置项)
        /// （配置文件、历史数据等）
        /// 例如“C:\Users\Myl\AppData\Roaming”
        /// </summary>
        public static string BaseDeployDataPath;

        /// <summary>
        /// 本系统数据目录路径
        /// 例如“C:\Users\Myl\AppData\Roaming\TechScanServiceDeploy”
        /// </summary>
        public static string DeployDataPath;

        /// <summary>
        /// IIS发布历史数据保存目录
        /// 例如“C:\Users\Myl\AppData\Roaming\TechScanServiceDeploy\IIS_DeployHistory”
        /// </summary>
        public static string DeployData_IISHistoryPath;
        /// <summary>
        /// SQL发布历史数据保存目录
        /// 例如“C:\Users\Myl\AppData\Roaming\TechScanServiceDeploy\SQL_DeployHistory”
        /// </summary>
        public static string DeployData_SQLHistoryPath;
        /// <summary>
        /// 打包历史目录
        /// 例如“C:\Users\Myl\AppData\Roaming\TechScanServiceDeploy\Package_History”
        /// </summary>
        public static string PackData_HistoryPath;
        #region Log（Myl 20200106 这里不做配置了）

        /// <summary>
        /// 基本Log目录
        /// 例如“C:\Users\Myl\AppData\Roaming\TechScanServiceDeploy\Logs”
        /// </summary>
        public static string BaseLogPath;
        /// <summary>
        /// SQL脚本部署日志目录
        /// </summary>
        public static string Deploy_SQL_LogPath;
        /// <summary>
        /// IISWeb服务部署日志目录
        /// </summary>
        public static string Deploy_IIS_LogPath;
        /// <summary>
        /// 打包日志文件目录
        /// </summary>
        public static string Deploy_Package_LogPath;

        #region 首次自动化部署目录相关
        /// <summary>
        /// 默认的内嵌包文件目录
        /// </summary>
        public static string FirstDeploy_Base_Path;
        /// <summary>
        /// 默认的内嵌包-IISWeb包
        /// </summary>
        public static string FirstDeploy_IIS_Pkg_FileName;
        /// <summary>
        /// 默认的内嵌包-SQL脚本包
        /// </summary>
        public static string FirstDeploy_SQL_Pkg_FileName;
        #endregion

        #endregion

        #endregion

        static DeployHelper()
        {
            BaseDeployDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            DeployDataPath = Path.Combine(BaseDeployDataPath, ConstantValues.DEFAULT_DEPLOY_CONFIG_BASE_PATH_NAME);
            DeployData_IISHistoryPath = Path.Combine(DeployDataPath, ConstantValues.DEFAULT_DEPLOY_IIS_HISTORY_PATH_NAME);
            DeployData_SQLHistoryPath = Path.Combine(DeployDataPath, ConstantValues.DEFAULT_DEPLOY_SQL_HISTORY_PATH_NAME);

            //当前存放在程序根目录下
            BaseLogPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Logs");
            Deploy_SQL_LogPath= Path.Combine(BaseLogPath, ConstantValues.DEFAULT_SQL_DEPLOY_LOG_PATH_NAME);
            Deploy_IIS_LogPath = Path.Combine(BaseLogPath, ConstantValues.DEFAULT_IIS_DEPLOY_LOG_PATH_NAME);
            Deploy_Package_LogPath = Path.Combine(BaseLogPath, ConstantValues.DEFAULT_PACKAGE_LOG_PATH_NAME);

            PackData_HistoryPath= Path.Combine(DeployDataPath, ConstantValues.DEFAULT_PACKAGE_HISTORY_PATH_NAME);

            #region 首次自动化部署目录相关
            FirstDeploy_Base_Path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "DefaultPkgs");
            FirstDeploy_IIS_Pkg_FileName = Path.Combine(FirstDeploy_Base_Path, ConstantValues.DEFAULT_FIRST_DEPLOY_IISPKG_FILE_NAME);
            FirstDeploy_SQL_Pkg_FileName = Path.Combine(FirstDeploy_Base_Path, ConstantValues.DEFAULT_FIRST_DEPLOY_SQLPKG_FILE_NAME);
            #endregion

            Init();
        }

        private static void Init()
        {
            try
            {
                #region 初始化几个目录
                //数据根目录

                if (!Directory.Exists(DeployDataPath))
                {
                    Directory.CreateDirectory(DeployDataPath);
                }

                //IIS历史数据目录
                if (!Directory.Exists(DeployData_IISHistoryPath))
                {
                    Directory.CreateDirectory(DeployData_IISHistoryPath);
                }

                //SQL历史数据目录
                if (!Directory.Exists(DeployData_SQLHistoryPath))
                {
                    Directory.CreateDirectory(DeployData_SQLHistoryPath);
                }

                if (!Directory.Exists(BaseLogPath))
                {
                    Directory.CreateDirectory(BaseLogPath);
                }
                if (!Directory.Exists(Deploy_SQL_LogPath))
                {
                    Directory.CreateDirectory(Deploy_SQL_LogPath);
                }
                if (!Directory.Exists(Deploy_IIS_LogPath))
                {
                    Directory.CreateDirectory(Deploy_IIS_LogPath);
                }
                if (!Directory.Exists(Deploy_Package_LogPath))
                {
                    Directory.CreateDirectory(Deploy_Package_LogPath);
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new DeployBaseException(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取配置数据文件存放目录
        /// </summary>
        /// <returns></returns>
        public static string GetDeployConfigPath()
        {
            try
            {
                //目前配置文件&历史日志存放在用户公共数据目录，例如“C:\Users\Myl\AppData\Roaming”
                //var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var folderName = Path.Combine(BaseDeployDataPath, ConstantValues.DEFAULT_DEPLOY_CONFIG_BASE_PATH_NAME);
                if (!string.IsNullOrEmpty(folderName))
                {
                    if (!Directory.Exists(folderName))
                    {
                        Directory.CreateDirectory(folderName);
                    }
                    return Path.Combine(folderName, ConstantValues.DEFAULT_DEPLOY_CONFIG_FILE_NAME);
                }
                return string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static DeployConfig ReadConfig(string deployConfigPath = null)
        {
            DeployConfig config;
            if (string.IsNullOrEmpty(deployConfigPath))
            {
                config = new DeployConfig()
                {
                    Global_Config = new GlobalConfig(),
                    IIS_Config = new IISConfig(),
                    SQL_Config = new SQLConfig()
                };
                if (config.SaveConfig().Item1)
                {
                    return config;
                }
                else
                {
                    return null;
                }
            }

            if (File.Exists(deployConfigPath))
            {
                var configString = DESEncrypt.Decrypt(File.ReadAllText(deployConfigPath, Encoding.UTF8));
                if (!string.IsNullOrEmpty(configString))
                {
                    try
                    {
                        config = JsonConvert.DeserializeObject<DeployConfig>(configString);
                        return config;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    //return new DeployConfig()
                    //{
                    //    Global_Config = new GlobalConfig(),
                    //    IIS_Config = new IISConfig(),
                    //    SQL_Config = new SQLConfig()
                    //};
                    config = new DeployConfig()
                    {
                        Global_Config = new GlobalConfig(),
                        IIS_Config = new IISConfig(),
                        SQL_Config = new SQLConfig()
                    };
                    if (config.SaveConfig().Item1)
                    {
                        return config;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {
                //return new DeployConfig()
                //{
                //    Global_Config = new GlobalConfig(),
                //    IIS_Config = new IISConfig(),
                //    SQL_Config = new SQLConfig()
                //};
                config = new DeployConfig()
                {
                    Global_Config = new GlobalConfig(),
                    IIS_Config = new IISConfig(),
                    SQL_Config = new SQLConfig()
                };
                if (config.SaveConfig().Item1)
                {
                    return config;
                }
                else
                {
                    return null;
                }
            }
        }
        public static DeployConfig ReadConfig()
        {
            return ReadConfig(GetDeployConfigPath());
        }

        /// <summary>
        /// 首次部署的环境及参数检查
        /// </summary>
        public static void CheckFirstDeployEnv()
        {
            if (!System.IO.File.Exists(DeployHelper.FirstDeploy_IIS_Pkg_FileName))
            {
                throw new DeployBaseException("默认首次部署的Android Web服务包已损坏或者丢失！");
            }
            if (!System.IO.File.Exists(DeployHelper.FirstDeploy_SQL_Pkg_FileName))
            {
                throw new DeployBaseException("默认首次部署的SQL脚本包已损坏或者丢失！");
            }
        }
    }
}
