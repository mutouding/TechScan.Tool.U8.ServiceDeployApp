/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   SQLPackageRepository.cs
* 功能描述： SQL脚本文件目录打包
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-06 21:39:47
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-06 21:39:47		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.IO;
using System.IO.Compression;
using TechScan.Tool.U8.ServiceDeploy.Base.Impl;

namespace TechScan.Tool.U8.ServiceDeploy.SQL.Impl
{
    /// <summary>
    /// SQL脚本文件目录打包
    /// </summary>
    public sealed class SQLPackageRepository :
         FilesPackageRepository<SQLPackageZipParams, SQLPackageZipItem>
    {
        #region Property
        /// <summary>
        /// SQL文件获取规则代理
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

        public SQLPackageRepository(
            SQLPackageZipParams tParams = null,
            string srcDirectory = "",
            string desDirectory = "")
        {
            PackParams = tParams;
            if (PackParams != null)
            {
                PackParams.PackType = Base.Enums.DeployTypes.SQL;
            }
            SourceDirectory = srcDirectory;
            DestinationDirectory = desDirectory;
        }

        public SQLPackageRepository(string cPackFileFullName)
        {
            PackData = GetPackageFileInfo(cPackFileFullName);
        }

        #endregion

        #region Methods

        public override Tuple<bool, string> ExecPackParamsCheck()
        {
            Tuple<bool, string> tCheck = base.ExecPackParamsCheck();
            if (!tCheck.Item1)
            {
                return tCheck;
            }

            #region 校验文件夹下面是否是U8Android的WCF服务
            //这里目前做下简单的检查（是否有.sql文件）
            if (Directory.GetFiles(SourceDirectory, "*.sql", SearchOption.AllDirectories)?.Length == 0)
            {
                return Tuple.Create(false, $"目录[{SourceDirectory}下没有SQL脚本文件！]");
            }

            #endregion

            return Tuple.Create(true, "");
        }

        public override Tuple<bool, string> ExecPack()
        {
            return base.ExecPack();
        }

        #endregion
    }
}
