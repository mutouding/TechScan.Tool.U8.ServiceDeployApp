/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   BaseDeployHistory.cs
* 功能描述： 部署历史记录基类
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-30 14:00:45
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-30 14:00:45		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System.Data;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Impl
{
    /// <summary>
    /// 部署历史记录基类
    /// </summary>
    /// <typeparam name="T">基本部署项</typeparam>
    /// <typeparam name="U">部署配置对象</typeparam>
    /// <typeparam name="J">部署参数对象</typeparam>
    public class BaseDeployHistory<T, U, J> : BaseHistory<T>
         where T : BaseDeployHistoryItem<U, J>
         where U : Config.BaseConfig
         where J : BaseDeployParams
    {
        #region Property
        #endregion

        #region Ctor

        public BaseDeployHistory(HistoryType hisType) :
            base(hisType)
        {

        }

        public override DataSet ToDataSet()
        {
            return base.ToDataSet();
        }

        protected override DataSet BuildHistoryDataConstruction()
        {
            return base.BuildHistoryDataConstruction();
        }

        #endregion
    }
}
