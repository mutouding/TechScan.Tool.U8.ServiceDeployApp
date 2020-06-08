/*---------------------------------------------------------------------
* 			Copyright (C) 2017 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   ApiResponse
* 功能描述： 所有服务接口统一的返回消息对象
* 作者：     马永龙
* CLR版本：  4.0.30319.42000
* 创建时间： 2017/9/21 13:53:46
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 			负责人 	变更内容
* V1.0.0	2017/9/21 13:53:46	Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using Newtonsoft.Json;
using System.Runtime.Serialization;
namespace TechScan.BarCode.U8.Android.BLL.Common.Params.Out
{
    /// <summary>
    /// API接口返回消息对象
    /// </summary>
    public abstract class ApiResponse
    {
        /// <summary>
        /// API执行状态编码
        /// 0：API执行成功
        /// -1OR其他：API执行不成功
        /// </summary>
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; }

        /// <summary>
        /// API执行成功或者失败的消息
        /// </summary>
        [JsonProperty("msg", NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }

        /// <summary>
        /// 获取API调用是否成功状态
        /// </summary>
        [JsonIgnore]
        public bool IsSuccess
        {
            get
            {
                return Code.Equals("0");
            }
        }
    }
}
