/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   BasePackageZipParams.cs
* 功能描述： 打包参数基类
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-26 10:38:02
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-26 10:38:02		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Impl
{
    /// <summary>
    /// 打包参数基类
    /// </summary>
    /// <typeparam name="T">打包数据项</typeparam>
    public class BasePackageZipParams<T> : BaseParams
        where T : BasePackageZipItem
    {
        #region Property

        /// <summary>
        /// 打包类型（目前是U8Android服务的IIS打包和SQL脚本的打包）
        /// </summary>
        public DeployTypes PackType
        {
            get;set;
        }

        /// <summary>
        /// 打包参数
        /// （目前包含打包人，打包时间，打包版本，版本描述）
        /// </summary>
        public T PackParamKey
        {
            get; set;
        }

        #endregion

        #region Ctor

        public BasePackageZipParams(DeployTypes packType = DeployTypes.IIS, T zipItem = null)
        {
            PackType = packType;
            PackParamKey = zipItem;
        }
        public BasePackageZipParams(Tuple<DeployTypes, T> tData)
        {
            PackType = tData.Item1;
            PackParamKey = tData.Item2;
        }
        #endregion

        #region Methods

        #endregion
    }
}
