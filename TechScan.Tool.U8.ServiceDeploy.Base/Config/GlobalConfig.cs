/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   SystemLanguages
* 功能描述： 系统全局配置
* 作者：     马永龙
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-23 14:28:37
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 			负责人 	变更内容
* V1.0.0	2019-12-23 14:28:37	Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using TechScan.Tool.U8.ServiceDeploy.Base.Enums;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Config
{
    /// <summary>
    /// 全局配置
    /// </summary>
    public class GlobalConfig
        : BaseConfig
    {
        #region Property

        /// <summary>
        /// 当前系统语言
        /// </summary>
        public SystemLanguages @SystemLanguage
        {
            get; set;
        }

        #endregion

        #region Ctor
        public GlobalConfig(SystemLanguages sysLang = SystemLanguages.zh_CN)
        {
            @SystemLanguage = sysLang;
        }
        #endregion
    }
}
