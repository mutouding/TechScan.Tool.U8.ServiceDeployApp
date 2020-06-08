/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   BaseDeployParams.cs
* 功能描述： 发布运行时参数基类（包括IIS发布和SQL发布）
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-24 17:29:16
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-24 17:29:16		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Impl
{
    /// <summary>
    /// 发布运行时参数基类（包括IIS发布和SQL发布）
    /// </summary>
    public class BaseDeployParams : BaseParams
    {
        #region Fields & Property

        /// <summary>
        /// 部署时的文本描述
        /// </summary>
        private string m_DeployDescription;
        private DateTime? m_DeployDateTime;
        private string m_SourceDeployFileName;
        private DeployVersion m_DeployVersion;

        private PublishTypes m_PublishType;

        /// <summary>
        /// 部署时的文本描述
        /// </summary>
        public string DeployDescription
        {
            get
            {
                return m_DeployDescription;
            }
            set
            {
                m_DeployDescription = value;
            }
        }

        /// <summary>
        /// 部署时间
        /// </summary>
        public DateTime? DeployDateTime
        {
            get
            {
                return m_DeployDateTime ?? System.DateTime.Now;
            }
            set
            {
                m_DeployDateTime = value;
            }
        }

        /// <summary>
        /// 部署版本
        /// </summary>
        public DeployVersion @DeployVersion
        {
            get
            {
                return m_DeployVersion ?? new DeployVersion( DeployTypes.IIS, Enums.PackageType.Alpha)
                {
                    Version = "1.0",
                    Description = "首次部署",
                    Packager = "TechScan"
                };
            }
            set
            {
                m_DeployVersion = value;
            }
        }

        /// <summary>
        /// 发布的包文件名称（.ZIP包文件路径全名）
        /// </summary>
        public string SourceDeployPackageFileName
        {
            get
            {
                return m_SourceDeployFileName;
            }
            set
            {
                m_SourceDeployFileName = value;
            }
        }

        /// <summary>
        /// 发布类型（正常发布、回滚发布）
        /// </summary>
        public PublishTypes @PublishType
        {
            get
            {
                return m_PublishType;
            }
            set
            {
                m_PublishType = value;
            }
        }

        #endregion

        #region Ctor

        public BaseDeployParams(string cSourceDeployPackageFileName,PublishTypes publishType, DeployVersion dVersion = null, string deployDescription = null)
        {
            m_DeployDateTime = DateTime.Now;
            SourceDeployPackageFileName = cSourceDeployPackageFileName;
            m_PublishType = publishType;
            @DeployVersion = dVersion;
            @DeployDescription = deployDescription ?? $"{System.DateTime.Now.ToString()}部署默认描述";
        }

        #endregion
    }
}
