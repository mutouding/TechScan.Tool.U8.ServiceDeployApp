/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   Parser.cs
* 功能描述： SQL语法校验
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-10 20:23:01
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-10 20:23:01		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using Microsoft.Data.Schema.ScriptDom;
using Microsoft.Data.Schema.ScriptDom.Sql;
using System.Collections.Generic;
using System.IO;

namespace TechScan.Tool.U8.ServiceDeploy.SQL.Impl
{
    /// <summary>
    /// SQL语法校验
    /// </summary>
    public static class Parser
    {
        private static IList<ParseError> m_Errors;
        public static IList<ParseError> SqlErrors
        {
            get
            {
                return m_Errors;
            }
            private set
            {
                m_Errors = value;
            }
        }

        static Parser()
        {
            m_Errors = new List<ParseError>();
        }

        public static bool ParseSqlFile(string file)
        {
            bool hasErros = false;
            //if (m_Errors != null)
            //{
            //    m_Errors.Clear();
            //}
            using (TextReader reader = File.OpenText(file))
            {
                var parser = new TSql100Parser(true);
                var script = parser.Parse(reader, out m_Errors) as TSqlScript;

                hasErros = m_Errors.Count > 0;

                //foreach (var parseError in m_Errors)
                //{
                //    Errors.ProcessErrors(file, parseError);
                //}
            }

            return hasErros;
        }
    }
}
