/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   BaseHistoryItem.cs
* 功能描述： 操作历史项基类
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-30 13:06:03
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-30 13:06:03		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using TechScan.Tool.U8.ServiceDeploy.Base.Enums;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Impl
{
    /// <summary>
    /// 操作历史项基类
    /// </summary>
    public class BaseHistoryItem
    {
        #region Property

        /// <summary>
        /// 操作历史类型
        /// </summary>
        public HistoryType TYPE
        {
            get; set;
        }

        /// <summary>
        /// 历史包的路径全名
        /// （打包的安装包或者部署时的部署包）
        /// </summary>
        public string HistoryPackageFileFullName
        {
            get;
            set;
        }
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string GUID
        {
            get;set;
        }
        #endregion
    }
}
