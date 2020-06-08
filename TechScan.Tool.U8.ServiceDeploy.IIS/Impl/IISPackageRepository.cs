/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   IISPackageRepository.cs
* 功能描述： U8Android服务文件目录打包
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-26 11:53:06
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-26 11:53:06		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.IO;
using System.IO.Compression;
using TechScan.Tool.U8.ServiceDeploy.Base.Impl;

namespace TechScan.Tool.U8.ServiceDeploy.IIS.Impl
{
    /// <summary>
    /// U8Android服务文件目录打包
    /// </summary>
    public sealed class IISPackageRepository :
         FilesPackageRepository<IISPackageZipParams, IISPackageZipItem>
    {
        #region Property

        /// <summary>
        /// 打包文件获取规则代理
        /// </summary>
        public override Func<ZipArchive, bool> FilePackageGetRule
        {
            get
            {
                return base.FilePackageGetRule;
            }

            set
            {
                base.FilePackageGetRule = value;
            }
        }
        #endregion

        #region Ctor

        public IISPackageRepository(
            IISPackageZipParams tParams = null,
            string srcDirectory = "",
            string desDirectory = "")
        {
            PackParams = tParams;
            if (PackParams != null)
            {
                PackParams.PackType = Base.Enums.DeployTypes.IIS;
            }
            SourceDirectory = srcDirectory;
            DestinationDirectory = desDirectory;
        }

        public IISPackageRepository(string cPackFileFullName)
        {
            PackData= GetPackageFileInfo(cPackFileFullName);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Web部署包打包前参数校验
        /// </summary>
        /// <returns></returns>
        public override Tuple<bool, string> ExecPackParamsCheck()
        {
            Tuple<bool, string> tCheck = base.ExecPackParamsCheck();
            if (!tCheck.Item1)
            {
                return tCheck;
            }

            #region 校验文件夹下面是否是U8Android的WCF服务
            //这里目前做下简单的检查（是否有.svc文件和web.config文件）
            if (Directory.GetFiles(SourceDirectory, "*.svc", SearchOption.AllDirectories)?.Length == 0)
            {
                return Tuple.Create(false, $"目录[{SourceDirectory}下没有U8Android的服务文件！]");
            }
            if (Directory.GetFiles(SourceDirectory, "Web.config", SearchOption.AllDirectories)?.Length == 0)
            {
                return Tuple.Create(false, $"目录[{SourceDirectory}下没有U8Android的服务文件！]");
            }
            #endregion

            return Tuple.Create(true, "");
        }
        /// <summary>
        /// 执行Web部署包打包动作
        /// </summary>
        /// <returns></returns>
        public override Tuple<bool, string> ExecPack()
        {
            return base.ExecPack();
        }
        #endregion
    }
}
