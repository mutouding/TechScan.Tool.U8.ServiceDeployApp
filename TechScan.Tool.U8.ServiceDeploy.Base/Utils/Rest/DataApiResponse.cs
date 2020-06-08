/*---------------------------------------------------------------------
* 			Copyright (C) 2017 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   DataApiResponse
* 功能描述： 通用API返回数据对象
* 作者：     马永龙
* CLR版本：  4.0.30319.42000
* 创建时间： 2017-09-28 20:14:48
* 文件版本： V1.0.6

===============================版本履历===============================
* Ver 		变更日期 			负责人 	变更内容
* V1.0.0	2017-09-28 20:14:48	Myl		Create
* V1.0.1    2017-12-04 15:29:31 Myl     添加将当前数据对象统一转换成JsonObject方法
* V1.0.2    2017/12/06          Myl     所有节点改为采用小写字母传递
* V1.0.3    2017/12/11          Myl     添加构造函数，对属性初始化
* V1.0.4    2018/01/03          Myl     增加针对JArray序列化的泛型方法ToString<T>()
* V1.0.5    2018/02/01          Myl     优化ConvertToJObject方法，解决当Datas字符串无法反序列化为JObject对象时的异常报错问题
* V1.0.6    2018/03/16          Myl     修改ConvertToJObject()方法当Datas属性为空时转JObject反序列化时出现空引用的Bug
======================================================================
//--------------------------------------------------------------------*/

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace TechScan.BarCode.U8.Android.BLL.Common.Params.Out
{
    /// <summary>
    /// 通用API返回数据对象
    /// </summary>
    public class DataApiResponse : ApiResponse
    {
        #region Property
        /// <summary>
        /// API返回的数据
        /// </summary>
        [JsonProperty("datas", NullValueHandling = NullValueHandling.Ignore)]
        public string Datas { get; set; }

        /// <summary>
        /// API返回的数据条目数量
        /// </summary>
        [JsonProperty("datacount", NullValueHandling = NullValueHandling.Ignore)]
        public int DataCount { get; set; }
        #endregion

        #region Ctor

        /// <summary>
        /// 通用API返回数据对象基类Ctor
        /// </summary>
        public DataApiResponse()
        {
            Datas = string.Empty;
            DataCount = 0;
            Msg = string.Empty;
            Code = string.Empty;
        }

        #endregion

        #region Methods

        #region 将当前DataApiResponse对象转换成JObject对象
        /// <summary>
        /// 将当前DataApiResponse对象转换成JObject对象
        /// </summary>
        /// <returns>
        /// 返回对应的JObject对象
        /// </returns>
        public virtual JObject ConvertToJObject()
        {
            //API实际返回的数据Datas转换成JObject对象
            JObject jDatasObject;
            try
            {
                jDatasObject = (JObject)JsonConvert.DeserializeObject(string.IsNullOrEmpty(Datas) ? string.Empty : Datas);
            }
            catch (System.Exception ex)
            {
                #region Log Record When Exception Occured
                //log4net.ILog jsonConvertExceptionLog =
                //log4net.LogManager.GetLogger((System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));
                //jsonConvertExceptionLog.Error(string.Format("API返回消息时反序列化JSON对象时发生异常:{0}", ex.Message), ex);
                #endregion

                jDatasObject = new JObject();
                JProperty jp = new JProperty("msg", Datas);
                jDatasObject.Add(jp);
            }

            //当前DataApiResponse对象转换成JObject对象
            JObject jResponseObject = new JObject();
            dynamic dyJObject = jResponseObject;

            dyJObject.code = Code;
            dyJObject.msg = Msg;
            dyJObject.datacount = DataCount;
            dyJObject.issuccess = IsSuccess;
            if (jDatasObject != null)
            {
                dyJObject.datas = jDatasObject;
            }
            return dyJObject;
        }

        #endregion

        #region 将当前DataApiResponse对象转换成JObject对象（数据中含有数组JArray）
        /// <summary>
        /// 将当前DataApiResponse对象转换成JObject对象（数据中含有数组JArray）
        /// </summary>
        /// <typeparam name="T">泛型类型（主要为了JArray考虑，目前好像暂时用不到泛型T）</typeparam>
        /// <returns>返回对应的JObject对象</returns>
        public virtual JObject ConvertToJObject<T>()
        {
            //API实际返回的数据Datas转换成JObject对象
            //T jDatasObject = JsonConvert.DeserializeObject<T>(Datas);

            JArray jDatasObject = (JArray)JsonConvert.DeserializeObject(Datas);

            //当前DataApiResponse对象转换成JObject对象
            JObject jResponseObject = new JObject();
            dynamic dyJObject = jResponseObject;

            dyJObject.code = Code;
            dyJObject.msg = Msg;
            dyJObject.datacount = DataCount;
            dyJObject.issuccess = IsSuccess;
            if (jDatasObject != null)
            {
                dyJObject.datas = jDatasObject;
            }
            return dyJObject;
        }
        #endregion

        #region 返回JSON字符串 ToString()方法
        /// <summary>
        /// 重载ToString()
        /// </summary>
        /// <returns>
        /// 返回JObject对应的JSON字符串
        /// </returns>
        public override string ToString()
        {
            return ConvertToJObject().ToString();
        }

        /// <summary>
        /// 返回对象JSON字符串
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <returns>
        /// 返回泛型类型对应的JSON字符串
        /// </returns>
        public virtual string ToString<T>()
        {
            return ConvertToJObject<T>().ToString();
        }
        #endregion

        #endregion
    }


}
