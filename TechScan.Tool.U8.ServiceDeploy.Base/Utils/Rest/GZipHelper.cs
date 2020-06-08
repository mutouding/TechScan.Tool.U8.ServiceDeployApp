/*---------------------------------------------------------------------
* 			Copyright (C) 2017 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   GZipHelper
* 功能描述： JSON字符串压缩操作类
* 作者：     马永龙
* CLR版本：  4.0.30319.42000
* 创建时间： 2017/9/23 13:44:25
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 			负责人 	变更内容
* V1.0.0	2017/9/23 13:44:25	Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.IO.Compression;
using TechScan.BarCode.U8.Android.BLL.Common.Params.Out;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils.Rest;

namespace TechScan.BarCode.U8.Android.BLL.Common
{
    /// <summary>
    /// JSON字符串压缩
    /// </summary>
    public class GZipHelper
    {
        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static DataSet GetDatasetByString(string Value)
        {
            DataSet ds = new DataSet();
            string CC = GZipDecompressString(Value);
            System.IO.StringReader Sr = new StringReader(CC);
            ds.ReadXml(Sr);
            return ds;
        }
        /// <summary>
        /// 根据DATASET压缩字符串
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static string GetStringByDataset(string ds)
        {
            return GZipCompressString(ds);
        }
        /// <summary>
        /// 将传入字符串以GZip算法压缩后，返回Base64编码字符
        /// </summary>
        /// <param name="rawString">需要压缩的字符串</param>
        /// <returns>压缩后的Base64编码的字符串</returns>
        public static string GZipCompressString(string rawString)
        {
            if (string.IsNullOrEmpty(rawString) || rawString.Length == 0)
            {
                return "";
            }
            else
            {
                byte[] rawData = System.Text.Encoding.UTF8.GetBytes(rawString.ToString());
                byte[] zippedData = Compress(rawData);
                return Convert.ToBase64String(zippedData);
            }

        }
        /// <summary>
        /// GZip压缩
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] rawData)
        {
            MemoryStream ms = new MemoryStream();
            GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Compress, true);
            compressedzipStream.Write(rawData, 0, rawData.Length);
            compressedzipStream.Close();
            return ms.ToArray();
        }
        /// <summary>
        /// 将传入的二进制字符串资料以GZip算法解压缩
        /// </summary>
        /// <param name="zippedString">经GZip压缩后的二进制字符串</param>
        /// <returns>原始未压缩字符串</returns>
        public static string GZipDecompressString(string zippedString)
        {
            if (string.IsNullOrEmpty(zippedString) || zippedString.Length == 0)
            {
                return "";
            }
            else
            {
                byte[] zippedData = Convert.FromBase64String(zippedString.ToString());
                return (string)(System.Text.Encoding.UTF8.GetString(Decompress(zippedData)));
            }
        }
        /// <summary>
        /// ZIP解压
        /// </summary>
        /// <param name="zippedData"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] zippedData)
        {
            MemoryStream ms = new MemoryStream(zippedData);
            GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Decompress);
            MemoryStream outBuffer = new MemoryStream();
            byte[] block = new byte[1024];
            while (true)
            {
                int bytesRead = compressedzipStream.Read(block, 0, block.Length);
                if (bytesRead <= 0)
                    break;
                else
                    outBuffer.Write(block, 0, bytesRead);
            }
            compressedzipStream.Close();
            return outBuffer.ToArray();
        }

        /// <summary>
        /// 获取U8Android服务部署好后自动测试的结果
        /// </summary>
        /// <param name="cResultMsg"></param>
        /// <returns></returns>
        public static Tuple<bool, DataPostApiResponse> AnalysisRestMsg(string cResultMsg)
        {
            try
            {
                GZipData gzip = JsonConvert.DeserializeObject<GZipData>(cResultMsg);
                var zippedMsg = GZipHelper.GZipDecompressString(gzip.ZippedData);
                var dataResponse = JsonConvert.DeserializeObject<DataPostApiResponse>(zippedMsg);
                return Tuple.Create<bool, DataPostApiResponse>(true, dataResponse);
            }
            catch (Exception ex)
            {
                return Tuple.Create<bool, DataPostApiResponse>(false, new DataPostApiResponse() { Code = "-1", Msg = ex.Message });
            }
        }
    }
}
