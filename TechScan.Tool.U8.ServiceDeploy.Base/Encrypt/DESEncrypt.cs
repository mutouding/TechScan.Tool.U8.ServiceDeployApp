/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   DESEncrypt.cs
* 功能描述： DES加密/解密类
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-26 10:32:41
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-26 10:32:41		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Security.Cryptography;
using System.Text;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Encrypt
{
    /// <summary>
    /// DES加密/解密类
    /// </summary>
    public sealed class DESEncrypt
    {
        private static readonly string passKey = "^2018#!_@MaYonglong@_!#2018^";

        #region Ctor
        /// <summary>
        /// Ctor
        /// </summary>
        public DESEncrypt()
        {
        }
        #endregion

        #region ========加密========

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text">待加密文本</param>
        /// <returns>返回加密后的文本</returns>
        public static string Encrypt(string Text)
        {
            return Encrypt(Text, passKey);
        }
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text">待加密文本</param> 
        /// <param name="sKey">密钥</param> 
        /// <returns>返回加密后的文本</returns> 
        public static string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        #endregion

        #region ========解密========

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text">待解密文本</param>
        /// <returns>返回解密后的文本</returns>
        public static string Decrypt(string Text)
        {
            return Decrypt(Text, passKey);
        }
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text">待解密文本</param> 
        /// <param name="sKey">密钥</param> 
        /// <returns>返回解密后的文本</returns> 
        public static string Decrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion
    }
}
