/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   IFilesPackageProvider.cs
* 功能描述： 部署文件打包接口
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-26 11:23:55
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-26 11:23:55		Myl		Create
* V1.0.1	2019-12-28          	Myl		添加接口方法GetPackagesDetailInfo
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Impl
{
    /// <summary>
    /// 部署文件打包接口
    /// </summary>
    public interface IFilesPackageProvider<T, U> : IDisposable
        where T : Impl.BasePackageZipParams<U>
        where U : BasePackageZipItem
    {
        /// <summary>
        /// 文件打包参数
        /// </summary>
        T PackParams { get; set; }
        /// <summary>
        /// 文件打包项
        /// </summary>
        U PackData { get; set; }
        /// <summary>
        /// 执行文件打包
        /// </summary>
        /// <returns></returns>
        Tuple<bool, string> ExecPack();

        /// <summary>
        /// 文件打包前校验
        /// </summary>
        /// <returns></returns>
        Tuple<bool, string> ExecPackParamsCheck();

        /// <summary>
        /// 生成打包文件信息
        /// （包含打包类型，打包人，打包时间，打包版本，版本描述等）
        /// </summary>
        /// <returns>
        /// T1：是否生成成功
        /// T2：生成成功的打包信息文件名称或者失败的文本描述信息
        /// </returns>
        Tuple<bool, string> GeneratePackageInfoFile();

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
        Tuple<bool, string, IList<IDictionary<string, U>>> GetPackagesDetailInfo(string cPathOrFileName = null);
        /// <summary>
        /// 执行文件解压
        /// </summary>
        /// <param name="cSourcePackageFileFullName">
        /// 待解压的源文件包目录
        /// </param>
        /// <param name="cDestinationDirectoryName">
        /// 文件解压目的目录
        /// </param>
        /// <param name="bDeleteDestDirAllFiles">
        /// 是否需要删除目标目录下的所有文件（默认删除）
        /// </param>
        /// <returns></returns>
        Tuple<bool, string> ExecUnPack(string cSourcePackageFileFullName,string cDestinationDirectoryName, bool bDeleteDestDirAllFiles = true);
    }
}
