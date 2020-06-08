/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   IHistory.cs
* 功能描述： 操作历史接口
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-30 13:10:54
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-30 13:10:54		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Impl
{
    /// <summary>
    /// 操作历史接口
    /// </summary>
    public interface IHistory<T> : IDisposable
         where T : BaseHistoryItem
    {
        /// <summary>
        /// 历史文件存放目录
        /// e.g：C:\Users\Myl\AppData\Roaming\TechScanServiceDeploy\IIS_DeployHistory
        /// </summary>
        string HistoryDir
        {
            get;
            set;
        }
        /// <summary>
        /// 历史履历文档名称
        /// e.g：IISDeployHistory.DHistory
        /// </summary>
        string HistoryListFileName
        {
            get;
            set;
        }

        /// <summary>
        /// 添加操作历史
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Tuple<bool, string> AddHistory(T t);

        /// <summary>
        /// 获取历史清单列表
        /// </summary>
        /// <returns></returns>
        IList<T> GetHistoryList();
    }
}
