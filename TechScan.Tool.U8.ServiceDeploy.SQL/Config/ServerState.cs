/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   ServerState.cs
* 功能描述： 数据库服务器状态
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-09 20:39:51
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-09 20:39:51		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;

namespace TechScan.Tool.U8.ServiceDeploy.SQL.Config
{
    /// <summary>
    /// 数据库服务器状态
    /// </summary>
    [Serializable]
    public class ServerState : DbServerInfo
    {
        public string Key { get; set; }
        public bool IsReady { get; set; }
        public bool State { get; set; }
    }
}
