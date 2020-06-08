/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   TextLine.cs
* 功能描述： N/A
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-09 22:43:49
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-09 22:43:49		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechScan.Tool.U8.ServiceDeployWin.SQL.Diff
{
    public class TextLine : IComparable
    {
        public string Line;
        public int Hash;

        public TextLine(string str)
        {
            Line = str.Replace("\t", "    ");
            Hash = str.GetHashCode();
        }
        #region IComparable Members

        public int CompareTo(object obj)
        {
            return Hash.CompareTo(((TextLine)obj).Hash);
        }

        #endregion
    }
}
