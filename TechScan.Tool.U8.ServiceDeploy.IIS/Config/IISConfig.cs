/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   IISConfig.cs
* 功能描述： IIS虚拟目录配置
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-23 15:15:51
* Client：   MYLPC
* 文件版本： V1.0.1

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-23 15:15:51		Myl		Create
* V1.0.1	2020-01-07      		Myl		添加用户权限相关
* 
======================================================================
//--------------------------------------------------------------------*/

using System.Collections.Generic;
using TechScan.Tool.U8.ServiceDeploy.Base.Config;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils;

namespace TechScan.Tool.U8.ServiceDeploy.IIS.Config
{
    /// <summary>
    /// IIS虚拟目录配置
    /// </summary>
    public class IISConfig : BaseConfig
    {
        #region Property

        /// <summary>
        /// 应用程序池配置项
        /// </summary>
        public ApplicationPoolConfig AppPoolConfig
        {
            get;
            set;
        }

        /// <summary>
        /// 虚拟站点名称
        /// </summary>
        public string SiteName
        {
            get;
            set;
        }

        /// <summary>
        /// 虚拟目录名称
        /// </summary>
        public string VirtualSiteName
        {
            get;
            set;
        }

        /// <summary>
        /// 虚拟目录物理路径
        /// </summary>
        public string PhysicalPath
        {
            get;
            set;
        }

        /// <summary>
        /// 端口号（默认系统80端口）
        /// </summary>
        public IList<int> Ports
        {
            get;
            set;
        }
        /// <summary>
        /// 需要添加的MIME类型
        /// （这里其实只要是添加U8Android的.APK文件即可，为了后期扩展，防止其他类型添加，这里用字典来存放）
        /// </summary>
        public IDictionary<string, string> MIME_Types
        {
            get;
            set;
        }

        /// <summary>
        /// 虚拟站点目录用户权限相关
        /// </summary>
        public IISWebSiteUserAuth @VirtualSiteUserAuth
        {
            get;set;
        }

        #region IIS重注册相关
        public IISRegNet @RegNet
        {
            get;set;
        }
        public IISRegWCF @RegWCF
        {
            get; set;
        }
        #endregion

        #endregion

        #region Ctor

        public IISConfig(string webSiteName = ConstantValues.DEFAULT_IIS_VIRTUALSITE_NAME)
        {
            VirtualSiteName = webSiteName;
            AppPoolConfig = new Config.ApplicationPoolConfig();
            PhysicalPath = ConstantValues.DEFAULT_IIS_VIRTUALSITE_PHYSICALPATH;
            Ports = new List<int>() { ConstantValues.DEFAULT_IIS_PORT};
            SiteName = ConstantValues.DEFAULT_IIS_SITE_NAME;
            MIME_Types = ConstantValues.DEFAULT_IIS_MIME_TYPES;
            @VirtualSiteUserAuth = new IISWebSiteUserAuth(true);
            @RegNet = new Config.IISRegNet(false);
            @RegWCF = new Config.IISRegWCF(false);
        }

        #endregion

        #region Methods

        public override bool IsNullOrEmpty()
        {
            return (AppPoolConfig?.IsNullOrEmpty() == null)
                || (string.IsNullOrEmpty(VirtualSiteName))
                || (string.IsNullOrEmpty(PhysicalPath))
                || (Ports == null || Ports.Count == 0);
        }

        #endregion
    }
}
