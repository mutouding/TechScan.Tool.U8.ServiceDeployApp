/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   ConstantValues.cs
* 功能描述： 记录系统的一些常量
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-25 13:53:28
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-25 13:53:28		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System.Collections.Generic;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Utils
{
    /// <summary>
    /// 记录系统的一些常量
    /// </summary>
    public sealed class ConstantValues
    {
        #region IIS
        /// <summary>
        /// 默认的IIS应用程序池名称
        /// </summary>
        public const string DEFAULT_IIS_APPLICATIONPOOL_NAME = "U8AndroidAppPool";
        /// <summary>
        /// 默认的IIS应用程序池NET托管版本
        /// </summary>
        public const string DEFAULT_IIS_APPLICATIONPOOL_MANAGEDRUNTIMEVERSION = "v4.0";
        /// <summary>
        /// 默认的IIS应用程序池是否支持64位
        /// </summary>
        public const bool DEFAULT_IIS_APPLICATIONPOOL_ENABLE32BITAPPONWIN64 = true;
        /// <summary>
        /// IIS虚拟目录默认发布的网站名称
        /// </summary>
        public const string DEFAULT_IIS_VIRTUALSITE_NAME = "U8Android";
        /// <summary>
        /// IIS虚拟目录发布的网站默认的物理路径
        /// </summary>
        public const string DEFAULT_IIS_VIRTUALSITE_PHYSICALPATH = @"C:\U8Android";
        /// <summary>
        /// IIS虚拟目录默认的端口号
        /// </summary>
        public const int DEFAULT_IIS_PORT = 80;
        /// <summary>
        /// 默认的IIS站点名称
        /// </summary>
        public const string DEFAULT_IIS_SITE_NAME = "Default Web Site";

        /// <summary>
        /// 缺省的MIME类型添加
        /// </summary>
        public static IDictionary<string, string> DEFAULT_IIS_MIME_TYPES =
            new System.Collections.Generic.Dictionary<string, string>()
            {
                { ".apk","application/vnd.android.package-archive"}
            };
        /// <summary>
        /// 默认的站点带添加用户权限的用户列表
        /// </summary>
        public static List<string> DEFAULT_SITE_ADDAUTH_USERS = new List<string>() { "Everyone" };
        /// <summary>
        /// IIS重新注册.NET命令行语句
        /// </summary>
        public static List<string> DEFAULT_COMMAND_REGIIS = new List<string>()
        {
            @"cd C:\Windows\Microsoft.NET\Framework\v4.0.30319",
            "aspnet_regiis.exe -i",
            @"cd C:\Windows\Microsoft.NET\Framework64\v4.0.30319",
            "aspnet_regiis.exe -i"
        };
        /// <summary>
        /// IIS重新注册WCF命令行语句
        /// </summary>
        public static List<string> DEFAULT_COMMAND_REGWCF = new List<string>()
        {
            @"cd C:\WINDOWS\Microsoft.NET\Framework\v3.0\Windows Communication Foundation",
            "ServiceModelReg.exe -i"
        };
        #region WCF接口测试相关
        /// <summary>
        /// 默认的Restful服务测试接口名称
        /// </summary>
        public const string DEFAULT_IIS_RESTFUL_TEST_INTERFACE_NAME = "U8Android/V1/Common/Version";
        /// <summary>
        /// 默认的Restful服务测试接口全名
        /// </summary>
        public const string DEFAULT_IIS_RESTFUL_TEST_INTERFACE_FULLNAME = @"http://localhost/U8Android/V1/Common/Version";
        #endregion

        /// <summary>
        /// 默认的打包信息文件扩展名
        /// </summary>
        public const string DEFAULT_FILE_PACKINFO_EXTENTION_NAME = ".PKey";

        /// <summary>
        /// 包文件默认的解压目录
        /// </summary>
        public const string DEFAULT_PACKAGEFILE_EXACT_DIRECTORY_NAME = "PackageExactTemp";

        /// <summary>
        /// 部署配置文件的默认根路径名称
        /// </summary>
        public const string DEFAULT_DEPLOY_CONFIG_BASE_PATH_NAME = "TechScanServiceDeploy";
        /// <summary>
        /// 默认的部署配置文件文件名
        /// </summary>
        public const string DEFAULT_DEPLOY_CONFIG_FILE_NAME = "ServiceDeployConfig.sdConfig";

        /// <summary>
        /// IIS部署历史的默认文件夹名称
        /// </summary>
        public const string DEFAULT_DEPLOY_IIS_HISTORY_PATH_NAME = "IIS_DeployHistory";
        /// <summary>
        /// SQL部署历史的默认文件夹名称
        /// </summary>
        public const string DEFAULT_DEPLOY_SQL_HISTORY_PATH_NAME = "SQL_DeployHistory";
        #region LOG
        /// <summary>
        /// IISWeb服务部署日志的默认文件夹名称
        /// </summary>
        public const string DEFAULT_IIS_DEPLOY_LOG_PATH_NAME = "IIS_DeployLog";
        /// <summary>
        /// SQL脚本部署日志的默认文件夹名称
        /// </summary>
        public const string DEFAULT_SQL_DEPLOY_LOG_PATH_NAME = "SQL_DeployLog";
        /// <summary>
        /// 文件打包日志的默认文件夹名称
        /// </summary>
        public const string DEFAULT_PACKAGE_LOG_PATH_NAME = "PackageLog";

        #endregion
        /// <summary>
        /// 打包历史默认文件夹名称
        /// </summary>
        public const string DEFAULT_PACKAGE_HISTORY_PATH_NAME = "Package_History";
        /// <summary>
        /// 默认的操作历史履历文件名称（非路径全名）
        /// </summary>
        public const string DEFAULT_HISTORY_LIST_FILE_NAME = "HistoryList.Json";

        #endregion

        #region SQL

        /// <summary>
        /// 默认的数据库连接超时时间为30秒
        /// </summary>
        public const int DEFAULT_SQL_CONNECTION_TIMEOUT = 30;

        /// <summary>
        /// 默认的公司名称
        /// </summary>
        public const string DEFAULT_CORP_NAME = "TechScan";


        #region 首次部署内嵌资源包名称

        /// <summary>
        /// 默认的首次部署的IISWeb包资源文件名（非路径全名）
        /// </summary>
        public const string DEFAULT_FIRST_DEPLOY_IISPKG_FILE_NAME = "IIS_Publish_First.zip";
        /// <summary>
        /// 默认的首次部署的SQL脚本包资源文件名（非路径全名）
        /// </summary>
        public const string DEFAULT_FIRST_DEPLOY_SQLPKG_FILE_NAME = "SQL_Publish_First.zip";

        #endregion

        #endregion
    }
}
