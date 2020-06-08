/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   FormOpUtils.cs
* 功能描述： 界面层通用操作类
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-06 10:44:24
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-06 10:44:24		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;

namespace TechScan.Tool.U8.ServiceDeployWin.Util
{
    /// <summary>
    /// 界面层通用操作类
    /// </summary>
    public static class FormOpUtils
    {
        internal const int EmptyIndex = -1;
        internal const string FileExtenionSql = ".sql";

        #region Methods
        /// <summary>
        /// 初始化控件显示
        /// </summary>
        /// <param name="lstCtls">控件列表</param>
        /// <param name="act">初始化规则</param>
        public static void InitCtls(List<System.Windows.Forms.Control> lstCtls, Predicate<System.Windows.Forms.Control> act = null)
        {
            lstCtls?.ForEach(A =>
            {
                A.Controls.OfType<System.Windows.Forms.Control>().ToList().ForEach(B =>
                {
                    act = act ?? ((C) => { return C.Name.StartsWith("lbl"); });
                    if (act(B))
                    {
                        B.Text = string.Empty;
                    }
                });
            });
        }

        /// <summary>
        /// 获取通用Log对象执行方式
        /// </summary>
        /// <param name="log">Log对象</param>
        /// <returns></returns>
        public static Action<DeployLogType, string, bool/*是否有Log链接显示*/, string /*连接字符串*/> GetCommLog(NLog.Logger log)
        {
            return (A, B, C, D) =>
            {
                LogEventInfo publisEvent = new LogEventInfo(LogLevel.Info, "", " ==> ");
                publisEvent.LoggerName = "rich_iis_log";
                //Myl 20200114 Modify
                //publisEvent.Properties["ShowLink"] = "file://" + B.Replace("\\", "\\\\");
                publisEvent.Properties["ShowLink"] = string.IsNullOrEmpty(D) ? "file://" + B.Replace("\\", "\\\\") : $"{D}://" + B.Replace("\\", "\\\\");
                switch (A)
                {
                    case ServiceDeploy.Base.Enums.DeployLogType.Trace:
                        if (C)
                        {
                            log.Log(publisEvent);
                        }
                        else
                        {
                            log?.Trace(B);
                        }
                        break;
                    case ServiceDeploy.Base.Enums.DeployLogType.Debug:
                        if (C)
                        {
                            log.Log(publisEvent);
                        }
                        else
                        {
                            log?.Debug(B);
                        }
                        break;
                    case ServiceDeploy.Base.Enums.DeployLogType.Info:
                        if (C)
                        {
                            log.Log(publisEvent);
                        }
                        else
                        {
                            log?.Info(B);
                        }
                        break;
                    case ServiceDeploy.Base.Enums.DeployLogType.Warn:
                        if (C)
                        {
                            log.Log(publisEvent);
                        }
                        else
                        {
                            log?.Warn(B);
                        }
                        break;
                    case ServiceDeploy.Base.Enums.DeployLogType.Error:

                        if (C)
                        {
                            log.Log(publisEvent);
                        }
                        else
                        {
                            log?.Error(B);
                        }
                        break;
                    case ServiceDeploy.Base.Enums.DeployLogType.Fatal:
                        if (C)
                        {
                            log.Log(publisEvent);
                        }
                        else
                        {
                            log?.Fatal(B);
                        }
                        break;
                    default:
                        break;
                }
            };
        }

        internal static void SelectText(TextEditorControl editor, string text)
        {
            var offset = editor.Text.IndexOf(text);
            var endOffset = offset + text.Length;
            editor.ActiveTextAreaControl.TextArea.Caret.Position = editor.ActiveTextAreaControl.TextArea.Document.OffsetToPosition(endOffset);
            editor.ActiveTextAreaControl.TextArea.SelectionManager.ClearSelection();
            var document = editor.ActiveTextAreaControl.TextArea.Document;
            var selection = new DefaultSelection(document, document.OffsetToPosition(offset), document.OffsetToPosition(endOffset));
            editor.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(selection);
        }
        internal static void SetDragDropContent(TextBox editor, DragEventArgs e)
        {
            var result = GetDragDropContent(e);
            if (!string.IsNullOrEmpty(result))
                editor.Text = result;
        }
        internal static void HandleContentDragEnter(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat)
                || e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }
        internal static string GetDragDropContent(DragEventArgs e)
        {
            string result = null;
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
                result = e.Data.GetData(DataFormats.StringFormat).ToString();
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = e.Data.GetData(DataFormats.FileDrop) as string[];
                result = File.ReadAllText(files.First());
            }
            return result;
        }
        internal static void SetTextBoxStyle(TextEditorControl editor)
        {
            editor.ShowEOLMarkers = false;
            editor.ShowHRuler = false;
            editor.ShowInvalidLines = false;
            editor.ShowSpaces = false;
            editor.ShowTabs = false;
            editor.ShowMatchingBracket = true;
            editor.AllowCaretBeyondEOL = false;
            editor.ShowVRuler = false;
            editor.ImeMode = ImeMode.On;
            editor.SetHighlighting("TSQL");
        }

        #region 文件定位

        internal static void OpenFolderAndSelectFile(String fileFullName)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
                psi.Arguments = "/e,/select," + fileFullName;
                System.Diagnostics.Process.Start(psi);
            }
            catch
            { }
        }

        #endregion

        #endregion
    }
}
