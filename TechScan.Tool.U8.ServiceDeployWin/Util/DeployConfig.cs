/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   DeployConfig.cs
* 功能描述： 静态部署配置类
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-23 17:36:28
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-23 17:36:28		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
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
    /// 静态部署配置类
    /// </summary>
    public class DeployConfig
    {
        #region Property

        /// <summary>
        /// IIS配置项
        /// </summary>
        public IISConfig @IIS_Config
        {
            get; set;
        }
        /// <summary>
        /// SQL配置项
        /// </summary>
        public SQLConfig @SQL_Config
        {
            get; set;
        }
        /// <summary>
        /// 全局配置项
        /// </summary>
        public GlobalConfig @Global_Config
        {
            get;
            set;
        }

        #endregion

        #region Ctor

        public DeployConfig()
        {

        }

        #endregion

        #region Methohds

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> SaveConfig()
        {
            try
            {
                var configInfoString = JsonConvert.SerializeObject(this, Formatting.Indented);
                configInfoString = DESEncrypt.Encrypt(configInfoString);
                var configFileName =
                    Path.Combine(Path.Combine(DeployHelper.BaseDeployDataPath, ConstantValues.DEFAULT_DEPLOY_CONFIG_BASE_PATH_NAME),
                    ConstantValues.DEFAULT_DEPLOY_CONFIG_FILE_NAME);
                //向源目录写入发版文件
                File.WriteAllText(configFileName, configInfoString, Encoding.UTF8);

                return Tuple.Create(true, configFileName);
            }
            catch (Exception ex)
            {
                //return Tuple.Create(false, ex.Message);
                throw new DeployBaseException(ex.Message, ex);

            }
        }

        #region SQL 相关
        public DbServerInfo FindServer(string server, string user)
        {
            return @SQL_Config?.DbServers?.FirstOrDefault(
                (s) => s.Server.ToLower() == server.ToLower() && s.User == user);
        }
        public DbServerInfo FindServer(string server)
        {
            return @SQL_Config?.DbServers?.FirstOrDefault((s) => s.Server.ToLower() == server.ToLower());
        }
        #endregion

        #endregion
    }
}
