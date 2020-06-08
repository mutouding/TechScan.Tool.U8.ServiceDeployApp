/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   InvalidControlDataException.cs
* 功能描述： 界面控件操作异常
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-26 17:45:50
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-26 17:45:50		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;

namespace TechScan.Tool.U8.ServiceDeployWin.Exceptions
{
    /// <summary>
    /// 界面控件操作异常
    /// </summary>
    public class InvalidControlDataException : System.ApplicationException
    {
        #region 属性

        public System.Windows.Forms.TextBox Owner
        {
            get;
            set;
        }

        #endregion

        #region 构造函数
        public InvalidControlDataException()
            : this("缺少有效的数据录入！")
        {
        }

        public InvalidControlDataException(string message)
            : base(message)
        {
        }

        public InvalidControlDataException(string message, Exception e)
            : base(message, e)
        { }

        #endregion
    }
}
