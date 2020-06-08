/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   SqlFilesSearch.cs
* 功能描述： SQL脚本文件搜索
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-10 20:51:39
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-10 20:51:39		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TechScan.Tool.U8.ServiceDeploy.SQL.Impl
{
    /// <summary>
    /// SQL脚本文件搜索
    /// </summary>
    public static class SqlFilesSearch
    {
        static readonly List<string> Files = new List<string>();

        public static List<string> GetFiles(string dir)
        {
            Files.Clear();
            DirSearch(dir);

            return Files;
        }

        private static void DirSearch(string dir)
        {
            try
            {
                //现在脚本需要命名为数字，这样命名数字小的脚本先执行
                //foreach (var f in Directory.GetFiles(dir).Where(x => x.Contains(".sql")).OrderBy(x => float.Parse(Path.GetFileName(x).Replace(".sql", ""))))
                foreach (var f in Directory.GetFiles(dir).Where(x => x.ToUpper().Contains(".SQL")).OrderBy(x => Path.GetFileName(x)))
                {
                    Files.Add(f);
                }

                //foreach (var d in Directory.GetDirectories(dir).OrderBy(x => float.Parse(Path.GetFileName(x).Substring(/*Configuration.Configuration.Pproperties.ScriptVersionPrefix.Length*/3))))
                foreach (var d in Directory.GetDirectories(dir).OrderBy(x => Path.GetFileName(x)))
                {
                    DirSearch(d);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
