/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   RecentObject.cs
* 功能描述： N/A
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-09 22:21:03
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-09 22:21:03		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using TechScan.Tool.U8.ServiceDeploy.SQL.Enums;

namespace TechScan.Tool.U8.ServiceDeploy.SQL.Impl
{
    [Serializable]
    public class RecentObject
    {
        public RecentObjectTypes RecentObjectType { get; set; }
        public string Server { get; set; }
        public string User { get; set; }
        public string Database { get; set; }
        public string ObjectType { get; set; }
        public string ObjectName { get; set; }
    }
}
