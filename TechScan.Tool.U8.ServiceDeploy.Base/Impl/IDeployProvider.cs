/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   IDeployProvider.cs
* 功能描述： 程序发布提供程序接口
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-24 17:22:02
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-24 17:22:02		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using TechScan.Tool.U8.ServiceDeploy.Base.Config;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Impl
{
    /// <summary>
    /// 程序发布提供程序接口
    /// </summary>
    /// <typeparam name="T">发布项配置参数</typeparam>
    /// <typeparam name="U">部署参数</typeparam>
    public interface IDeployProvider<T, U> : IDisposable
        where T : BaseConfig
        where U : BaseDeployParams
    {
        #region Property
        /// <summary>
        /// 历史数据路径
        /// </summary>
        string HistoryDataDir
        {
            get; set;
        }

        /// <summary>
        /// 类型名称
        /// </summary>
        string ProviderName { get; }

        T ConfigData
        {
            get;
            set;
        }
        /// <summary>
        /// 部署参数
        /// </summary>
        U DeployParamData { get; set; }

        #region 部署
        /// <summary>
        /// 部署
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        Tuple<bool, string> ExecDeploy(Action<string> act);

        /// <summary>
        /// 异步执行部署
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        Task<Tuple<bool, string>> ExecDeployAsync(Action<string> act);
        #endregion
        #endregion

        #region Methods

        void DataValidationBeforeDeploy();

        /// <summary>
        /// 日志对象
        /// </summary>
        Action<DeployLogType, string, bool, string> @Log
        {
            get; set;
        }
        #endregion
    }
}
