/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   CommUtils.cs
* 功能描述： 通用的帮助类
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-02 20:18:43
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-02 20:18:43		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Utils
{
    /// <summary>
    /// 通用的帮助类
    /// </summary>
    public static class CommUtils
    {
        #region Methods

        /// <summary>
        /// 获取当前系统支持的包版本类型字符串描述集合
        /// </summary>
        /// <returns></returns>
        public static IList<string> GetPkgTypes()
        {
            IList<string> lstPkTypes = new List<string>();
            foreach (var vPkType in Enum.GetValues(typeof(PackageType)))
            {
                lstPkTypes.Add(vPkType.ToString());
            }
            return lstPkTypes;
        }

        /// <summary>
        /// 获取托管管道模式列表
        /// 这个目前就写死两种
        /// </summary>
        /// <returns></returns>
        public static IList<string> GetManagedPipelineModeTypes()
        {
            return new List<string>()
            {
                "集成","经典"
            };
        }

        /// <summary>
        /// Framework版本（Android程序用的4.0框架，这里到时候前台默认写死v4.0）
        /// 这个目前就写死两种
        /// </summary>
        /// <returns></returns>
        public static IList<string> GetFrameworks()
        {
            return new List<string>()
            {
                "v4.0","v2.0"
            };
        }

        /// <summary>
        /// 对象复制
        /// （要求对象可序列化）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectInstance"></param>
        /// <returns></returns>
        public static T CloneObject<T>(T objectInstance)
        {
            var bFormatter = new BinaryFormatter();
            var stream = new MemoryStream();
            bFormatter.Serialize(stream, objectInstance);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)bFormatter.Deserialize(stream);
        }

        #endregion
    }
}
