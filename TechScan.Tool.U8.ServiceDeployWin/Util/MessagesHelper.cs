/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   MessagesHelper.cs
* 功能描述： 通用消息弹框
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-04 21:37:37
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-04 21:37:37		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Reflection;
using System.Windows.Forms;

namespace TechScan.Tool.U8.ServiceDeployWin.Util
{
    /// <summary>
    /// 通用消息弹框
    /// </summary>
    internal class MessagesHelper
    {
        /// <summary>
        /// 弹框标题
        /// </summary>
        internal static string MessageTitle
        {
            get { return ((Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0]) as AssemblyTitleAttribute).Title; }
        }

        internal static void ShowMessage(string message)
        {
            ShowMessage(message, MessageBoxIcon.Information);
        }

        internal static void ShowMessage(string message, MessageBoxIcon icon)
        {
            ShowMessage(message, MessageTitle, icon);
        }

        internal static void ShowMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        internal static bool ShowQuestion(string message)
        {
            return MessageBox.Show(message, MessageTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        internal static Tuple<bool, string> ShowInputMsgBox(string title, string message, string defaultValue = null)
        {
            InputDialog dialog = new InputDialog(message, title, defaultValue);
            dialog.SetInputLength(0, 200);
            var result = dialog.ShowDialog();
            if (result == DialogResult.Cancel)
            {
                return new Tuple<bool, string>(false, string.Empty);
            }
            return new Tuple<bool, string>(true, dialog.Input);
        }
    }
}
