/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   MD5Encrypt.cs
* 功能描述： MD5加密/解密
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-26 10:33:54
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-26 10:33:54		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Text;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Encrypt
{
    /// <summary>
    /// MD5加密/解密
    /// </summary>
    public sealed class MD5Encrypt
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string getMd5Hash(string str)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(str));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            if (sBuilder.ToString().Length > 5)
            {
                return sBuilder.ToString().Substring(5) + sBuilder.ToString().Substring(0, 5);
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// MD5验证
        /// </summary>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool verifyMd5Hash(string input, string hash)
        {
            string hashOfInput = getMd5Hash(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
