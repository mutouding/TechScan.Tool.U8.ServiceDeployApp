/*---------------------------------------------------------------------
* 			Copyright (C) 2018 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   DataPostApiResponse
* 功能描述： 单据提交API返回数据结构
* 作者：     马永龙
* CLR版本：  4.0.30319.42000
* 创建时间： 2018-04-10 16:34:55
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 			负责人 	变更内容
* V1.0.0	2018-04-10 16:34:55	Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace TechScan.BarCode.U8.Android.BLL.Common.Params.Out
{
    /// <summary>
    /// 单据提交API返回数据结构
    /// </summary>
    public class DataPostApiResponse : DataApiResponse
    {
        #region Property
        /// <summary>
        /// API返回的数据
        /// </summary>
        [JsonProperty("datas", NullValueHandling = NullValueHandling.Ignore)]
        public new DataPostEntity Datas { get; set; }
        /// <summary>
        /// 获取API调用是否成功状态
        /// </summary>
        [JsonProperty("issuccess", NullValueHandling = NullValueHandling.Ignore)]
        public new bool IsSuccess
        {
            get
            {
                return Code.Equals("0");
            }
        }
        #endregion

        #region Ctor

        /// <summary>
        /// 通用API返回数据对象基类Ctor
        /// </summary>
        public DataPostApiResponse()
        {
            Datas = new DataPostEntity() { Msg = string.Empty };
            DataCount = 0;
            Msg = string.Empty;
            Code = string.Empty;
        }

        #endregion

        #region Methods
        public override JObject ConvertToJObject()
        {
            return base.ConvertToJObject();
        }
        /// <summary>
        /// 重载ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
        #endregion
    }

    /// <summary>
    /// API返回的消息对象实体
    /// </summary>
    public class DataPostEntity
    {
        #region Property
        /// <summary>
        /// API返回的消息
        /// </summary>
        [JsonProperty("msg", NullValueHandling = NullValueHandling.Ignore)]
        public string Msg { get; set; }
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DataPostEntity()
        {
            Msg = string.Empty;
        }

        #endregion
    }
}
