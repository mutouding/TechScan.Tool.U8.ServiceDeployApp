/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   DiffListText.cs
* 功能描述： N/A
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-09 22:44:05
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-09 22:44:05		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechScan.Tool.U8.ServiceDeployWin.SQL.Diff
{
    public class DiffListText : IDiffList
    {
        private const int MaxLineLength = 1024;
        private readonly List<TextLine> _lines;

        public DiffListText(string source, bool isFile)
        {
            _lines = new List<TextLine>();
            if (isFile)
            {
                using (var sr = new StreamReader(source))
                {
                    String line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Length > MaxLineLength)
                        {
                            throw new InvalidOperationException(
                                string.Format("File contains a line greater than {0} characters.",
                                MaxLineLength.ToString()));
                        }
                        _lines.Add(new TextLine(line));
                    }
                }
            }
            else
            {
                source.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList().ForEach(l => _lines.Add(new TextLine(l)));
            }
        }
        #region IDiffList Members

        public int Count()
        {
            return _lines.Count;
        }

        public IComparable GetByIndex(int index)
        {
            return (TextLine)_lines[index];
        }

        #endregion

    }
}
