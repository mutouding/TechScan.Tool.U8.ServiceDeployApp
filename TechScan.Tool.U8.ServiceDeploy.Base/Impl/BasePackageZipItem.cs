/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   BasePackageZipItem.cs
* 功能描述： 打包数据项
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-26 10:51:55
* Client：   MYLPC
* 文件版本： V1.0.1

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-26 10:51:55		Myl		Create
* V1.0.1	2020-01-02          	Myl		添加打包类型参数
* 
======================================================================
//--------------------------------------------------------------------*/

using System;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Impl
{
    /// <summary>
    /// 打包数据项
    /// </summary>
    public class BasePackageZipItem
    {
        #region Property
        /// <summary>
        /// 部署类型
        /// </summary>
        public DeployTypes PackType
        {
            get; set;
        }
        /// <summary>
        /// 打包版本
        /// </summary>
        public string Version
        {
            get; set;
        }
        /// <summary>
        /// 版本描述
        /// </summary>
        public string Description
        {
            get; set;
        }
        /// <summary>
        /// 打包人
        /// </summary>
        public string Packager
        {
            get; set;
        }
        /// <summary>
        /// 打包时间
        /// </summary>
        public DateTime PackageDateTime
        {
            get; set;
        }

        /// <summary>
        /// 当前包类型
        /// </summary>
        public PackageType @PkgType
        {
            get;
            set;
        }

        #endregion

        #region Ctor
        [Newtonsoft.Json.JsonConstructor]
        public BasePackageZipItem(DeployTypes ptType, PackageType pkType, string version = "1.0", string description = "",
          string packager = "TechScan", DateTime? packageDateTime = null)
        {
            @PackType = ptType;
            @PkgType = pkType;
            Version = version;
            Description = description;
            Packager = packager;
            PackageDateTime = packageDateTime ?? System.DateTime.Now;
        }

        public BasePackageZipItem(Tuple<DeployTypes,PackageType, string, string, string, DateTime?> tData)
        {
            @PackType = tData.Item1;
            @PkgType = tData.Item2;
            Version = tData.Item3;
            Description = tData.Item4;
            Packager = tData.Item5;
            PackageDateTime = tData.Item6 ?? System.DateTime.Now;
        }

        #endregion
    }
}
