/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   FilesHelper.cs
* 功能描述： 文件操作帮助类
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-07 12:06:55
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-07 12:06:55		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Utils
{
    /// <summary>
    /// 文件操作帮助类
    /// </summary>
    public static class FilesHelper
    {
        #region 文件&文件夹权限相关

        /// <summary>
        /// 为文件添加用户组的完全控制权限
        /// </summary>
        /// <param name="cFilePath">文件路径</param>
        /// <param name="users">用户列表</param>
        public static Tuple<bool, string> AddSecurityControll2File(string cFilePath, List<string> users = null)
        {
            if (users == null)
            {
                users = ConstantValues.DEFAULT_SITE_ADDAUTH_USERS;
            }
            try
            {
                //获取文件信息
                FileInfo fileInfo = new FileInfo(cFilePath);
                //获得该文件的访问权限
                System.Security.AccessControl.FileSecurity fileSecurity = fileInfo.GetAccessControl();
                users.ForEach(A =>
                {
                    fileSecurity.AddAccessRule(new FileSystemAccessRule(A, FileSystemRights.FullControl, AccessControlType.Allow));
                });
                ////添加ereryone用户组的访问权限规则 完全控制权限
                //fileSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                ////添加Users用户组的访问权限规则 完全控制权限
                //fileSecurity.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));
                //设置访问权限
                fileInfo.SetAccessControl(fileSecurity);
                return Tuple.Create<bool, string>(true, "");
            }
            catch (Exception ex)
            {
                return Tuple.Create<bool, string>(false, ex.Message);
            }
        }

        /// <summary>
        /// 为文件夹添加用户组的完全控制权限
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="users">用户列表</param>
        public static Tuple<bool, string> AddSecurityControll2Folder(string dirPath, List<string> users = null)
        {
            if (users == null)
            {
                users = ConstantValues.DEFAULT_SITE_ADDAUTH_USERS;
            }
            try
            {
                //获取文件夹信息
                DirectoryInfo dir = new DirectoryInfo(dirPath);
                //获得该文件夹的所有访问权限
                System.Security.AccessControl.DirectorySecurity dirSecurity = dir.GetAccessControl(AccessControlSections.All);
                //设定文件ACL继承
                InheritanceFlags inherits = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;
                users.ForEach(A =>
                {
                    var oFileSystemAccessRule = new FileSystemAccessRule(A, FileSystemRights.FullControl, inherits, PropagationFlags.None, AccessControlType.Allow);
                    var bModified = false;
                    dirSecurity.ModifyAccessRule(AccessControlModification.Add, oFileSystemAccessRule, out bModified);
                });

                ////添加ereryone用户组的访问权限规则 完全控制权限
                //FileSystemAccessRule everyoneFileSystemAccessRule = new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, inherits, PropagationFlags.None, AccessControlType.Allow);
                ////添加Users用户组的访问权限规则 完全控制权限
                //FileSystemAccessRule usersFileSystemAccessRule = new FileSystemAccessRule("Users", FileSystemRights.FullControl, inherits, PropagationFlags.None, AccessControlType.Allow);
                //bool isModified = false;
                //dirSecurity.ModifyAccessRule(AccessControlModification.Add, everyoneFileSystemAccessRule, out isModified);
                //dirSecurity.ModifyAccessRule(AccessControlModification.Add, usersFileSystemAccessRule, out isModified);
                //设置访问权限
                dir.SetAccessControl(dirSecurity);
                return Tuple.Create<bool, string>(true, "");
            }
            catch (Exception ex)
            {
                return Tuple.Create<bool, string>(false, ex.Message);
            }
        }

        #endregion
    }
}
