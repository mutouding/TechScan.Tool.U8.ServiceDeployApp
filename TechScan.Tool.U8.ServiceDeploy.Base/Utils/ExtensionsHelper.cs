/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   ExtensionsHelper.cs
* 功能描述： 扩展方法
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-04 21:18:35
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-04 21:18:35		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Utils
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ExtensionsHelper
    {
        //public static void Invoke(this Control control, Action action)
        //{
        //    try
        //    {
        //        if (control != null && control.IsHandleCreated && !control.IsDisposed)
        //            control.Invoke(action);
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //public static void LogExceptions(this Task task)
        //{
        //    task.ContinueWith(t =>
        //    {
        //        var aggException = t.Exception.Flatten();
        //        foreach (var exception in aggException.InnerExceptions)
        //        {
        //            Debug.WriteLine(exception.Message);
        //        }
        //    },
        //    TaskContinuationOptions.OnlyOnFaulted);
        //}

        public static string RemoveSpace(this string content)
        {
            content = content.Trim('\t');
            while (content.IndexOf("  ") != -1)
            {
                content = content.Replace("  ", " ");
            }
            return content.Trim();
        }

        //public static string ParseObjectName(this string line)
        //{
        //    line = SubstringTill(line, Utils.MultiCommentStart);
        //    line = SubstringTill(line, Utils.SingleCommentStart);
        //    line = line.Trim('[', ']', ';', ' ');
        //    var schema = QueryEngine.DefaultSchema + QueryEngine.Dot;
        //    if (line.StartsWith(schema))
        //        line = line.Substring(schema.Length);
        //    line = SubstringTill(line, " ");
        //    line = SubstringTill(line, "(");
        //    return line.Trim();
        //}

        public static string SubstringTill(this string line, string separator)
        {
            var index = line.IndexOf(separator);
            if (index != -1)
                return line.Substring(0, index);
            else
                return line;
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            foreach (var element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static void LogExceptions(this Task task)
        {
            task.ContinueWith(t =>
            {
                var aggException = t.Exception.Flatten();
                foreach (var exception in aggException.InnerExceptions)
                {
                    Debug.WriteLine(exception.Message);
                }
            },
            TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
