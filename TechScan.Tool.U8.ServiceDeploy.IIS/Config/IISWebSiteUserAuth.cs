/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   IISWebSiteUserAuth.cs
* 功能描述： IIS虚拟目录用户权限（添加目录用户读写权限）
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-07 11:27:45
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-07 11:27:45		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils;

namespace TechScan.Tool.U8.ServiceDeploy.IIS.Config
{
    /// <summary>
    /// IIS虚拟目录用户权限
    /// （添加目录用户读写权限）
    /// </summary>
    public sealed class IISWebSiteUserAuth
    {
        #region Property

        /// <summary>
        /// 是否添加用户权限
        /// </summary>
        public bool IsAddUserAuth
        {
            get; set;
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        public List<string> Users
        {
            get;
            set;
        }

        #endregion

        #region Ctor

        public IISWebSiteUserAuth(bool isAddUserAuth, List<string> users = null)
        {
            IsAddUserAuth = isAddUserAuth;
            Users = users ?? ConstantValues.DEFAULT_SITE_ADDAUTH_USERS;
        }

        #endregion

        #region Methods
        public override string ToString()
        {
            return new Func<string>(
                   () =>
                   {
                       if (Users == null)
                       {
                           return string.Empty;
                       }
                       string cUserAuthTmp = string.Empty;
                       Users?.ForEach((S) => { cUserAuthTmp += $"{S.ToString()},"; });
                       return cUserAuthTmp.TrimEnd(new char[] { ',' });
                   })();
        }
        #endregion
    }
}
