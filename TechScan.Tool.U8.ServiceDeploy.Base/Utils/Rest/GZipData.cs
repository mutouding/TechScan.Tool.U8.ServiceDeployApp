/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   GZipData.cs
* 功能描述： N/A
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-07 15:00:20
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-07 15:00:20		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechScan.BarCode.U8.Android.BLL.Common;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Utils.Rest
{
    public sealed class GZipData
    {
        #region Property
        /// <summary>
        /// 设置或者获取API接口经过GZIP压缩并Base64转码后的字符串
        /// </summary>
        [JsonProperty("zippeddata", NullValueHandling = NullValueHandling.Ignore)]
        public string ZippedData { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// 获取压缩数据GZIP解压后的JSON字符串
        /// </summary>
        /// <returns></returns>
        public string GetJsonDeCompressionString()
        {
            if (string.IsNullOrEmpty(ZippedData))
            {
                throw new Exception("GZIP字符串为空！");
            }
            return GZipHelper.GZipDecompressString(ZippedData);
        }

        public object DeserializeJsonObject(Type t)
        {
            return
            JsonConvert.DeserializeObject(GetJsonDeCompressionString(), t);
        }

        public string SerializeObject(object obj)
        {
            return
            JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
        #endregion
    }
}
