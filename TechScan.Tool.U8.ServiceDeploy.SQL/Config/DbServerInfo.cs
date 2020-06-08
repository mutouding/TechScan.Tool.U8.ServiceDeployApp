/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   DbServerInfo.cs
* 功能描述： 数据库服务器信息类
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-04 21:55:02
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-04 21:55:02		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils;
using TechScan.Tool.U8.ServiceDeploy.SQL.Enums;

namespace TechScan.Tool.U8.ServiceDeploy.SQL.Config
{
    /// <summary>
    /// 数据库服务器信息类
    /// </summary>
    [Serializable]
    public class DbServerInfo
    {
        #region Property

        /// <summary>
        /// SQL认证方式
        /// </summary>
        public AuthTypes AuthType { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 是否加密
        /// </summary>
        public bool IsEncrypted { get; set; }
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// 数据库
        /// </summary>
        public string Database { get; set; }
        /// <summary>
        /// 是否云数据库
        /// （扩展用，暂时无用）
        /// </summary>
        public bool IsCloud { get; set; }

        /// <summary>
        /// 数据库连接超时时间（秒）
        /// 默认30秒
        /// </summary>
        public int ConnectionTimeout { get; set; }

        #endregion

        #region Ctor

        public DbServerInfo()
        {
            IsEncrypted = false;
            ConnectionTimeout = ConstantValues.DEFAULT_SQL_CONNECTION_TIMEOUT;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"{Server},{Database},{User}";
        }

        public DbServerInfo Clone()
        {
            //return new DbServerInfo
            //{
            //    IsCloud = this.IsCloud,
            //    Server = this.Server,
            //    AuthType = this.AuthType,
            //    Database = this.Database,
            //    User = this.User,
            //    Password = this.Password,
            //    IsEncrypted = this.IsEncrypted
            //};

            return CommUtils.CloneObject<DbServerInfo>(this);
        }

        public string GetDbShortName()
        {
            return $"服务器[{Server}]-数据库[{Database}]";
        }

        #endregion
    }
}
