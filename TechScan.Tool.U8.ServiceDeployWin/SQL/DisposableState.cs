/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   DisposableState.cs
* 功能描述： N/A
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-09 20:46:55
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-09 20:46:55		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechScan.Tool.U8.ServiceDeployWin.SQL
{
    internal class DisposableState : IDisposable
    {
        private readonly System.Windows.Forms.Control _win;
        private readonly ToolStripItem[] _elements;

        internal DisposableState(System.Windows.Forms.Control win, ToolStripItem[] elements)
        {
            _win = win;
            _elements = elements;
            //SetState(false);
        }

        private void SetState(bool state)
        {
            if (_win.IsHandleCreated)
                _win.Invoke(
                     new Action(() =>
                    {
                        _win.Cursor = state ? Cursors.Arrow : Cursors.WaitCursor;
                        if (_elements != null)
                            _elements.ToList().ForEach((o) => { o.Enabled = state; o.Invalidate(); });
                    }));
        }

        public void Dispose()
        {
            //SetState(true);
        }
    }
}
