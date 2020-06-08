/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   RestfulHelper.cs
* 功能描述： Rest服务帮助类
*            U8Android服务发布完成后，进行测试
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-07 14:05:08
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-07 14:05:08		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TechScan.BarCode.U8.Android.BLL.Common;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils;

namespace TechScan.Tool.U8.ServiceDeploy.IIS.Impl.Rest
{
    public enum HttpVerb
    {
        GET,
        POST,
        PUT,
        DELETE
    }

    /// <summary>
    /// Rest服务帮助类
    /// </summary>
    public class RestfulHelper : IDisposable
    {
        public string EndPoint { get; set; }
        public HttpVerb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }

        public RestfulHelper()
            : this(ConstantValues.DEFAULT_IIS_RESTFUL_TEST_INTERFACE_FULLNAME)
        { }
        public RestfulHelper(string endpoint)
        {
            EndPoint = endpoint;
            Method = HttpVerb.GET;
            ContentType = "text/xml";
            ContentType = "application/json";
            PostData = "";
        }
        public RestfulHelper(HttpVerb method)
            : this(ConstantValues.DEFAULT_IIS_RESTFUL_TEST_INTERFACE_FULLNAME, method)
        { }
        public RestfulHelper(string endpoint, HttpVerb method)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/xml";
            ContentType = "application/json";
            PostData = "";
        }

        public RestfulHelper(HttpVerb method, string postData)
            : this(ConstantValues.DEFAULT_IIS_RESTFUL_TEST_INTERFACE_FULLNAME, method, postData)
        {
        }
        public RestfulHelper(string endpoint, HttpVerb method, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/xml";
            ContentType = "application/json";
            PostData = postData;
        }

        public async Task<RestCallResult> MakeRequestAsync()
        {
            return await MakeRequestAsync("");
        }

        //public static byte[] AddZip(byte[] needAdd)
        //{
        //    System.IO.MemoryStream ms = new System.IO.MemoryStream();
        //    try
        //    {
        //        ICSharpCode.SharpZipLib.GZip.GZipOutputStream zipOut =
        //            new ICSharpCode.SharpZipLib.GZip.GZipOutputStream(ms);

        //        zipOut.Write(needAdd, 0, needAdd.Length);
        //        zipOut.Finish();
        //        zipOut.Close();

        //        return ms.ToArray();
        //    }
        //    finally
        //    {
        //        ms.Close();
        //    }
        //}

        public async Task<RestCallResult> MakeRequestAsync(string parameters)
        {
            try
            {
                #region 异步调用

                //var requesttm = (HttpWebRequest)WebRequest.Create("http://nsha4wsaintf01/B0000D0000N0001N0000F0000S0000R0004/10.251.253.102/http://mytracking.chinacloudsites.cn/api/orderImport");
                //var requesttm = (HttpWebRequest)WebRequest.Create(System.Configuration.ConfigurationSettings.AppSettings["RestfulCAUrl"].Trim());
                //NetworkCredential cr = new NetworkCredential();
                //cr.Domain = System.Configuration.ConfigurationSettings.AppSettings["RestfulCADomain"].Trim();//"cardinalhealth";
                //cr.UserName = System.Configuration.ConfigurationSettings.AppSettings["RestfulCAUserName"].Trim();//"pa-xiaoming.yu";
                //cr.Password = System.Configuration.ConfigurationSettings.AppSettings["RestfulCAPassword"].Trim();//"Cardinal7";
                //requesttm.Credentials = cr;
                //try
                //{
                //    WebResponse rp = requesttm.GetResponse();
                //    rp.Close();
                //}
                //catch (Exception)
                //{
                //}

                var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

                request.Method = Method.ToString();
                request.ContentLength = 0;
                request.ContentType = ContentType;

                #region 设置超时属性
                 
                request.Timeout = int.Parse(System.Configuration.ConfigurationManager.AppSettings["RestfulTimeout"].Trim());
                #endregion

                #region 设置身份验证-Myl20170613
                request.Credentials = CredentialCache.DefaultCredentials;
                string restfulAccount = System.Configuration.ConfigurationManager.AppSettings["RestfulAccount"].Trim();
                string restfulPassword = System.Configuration.ConfigurationManager.AppSettings["RestfulPwd"].Trim();
                string authorizationCode = Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", restfulAccount, restfulPassword)));
                request.Headers.Add("Authorization", "Basic " + authorizationCode);
                #endregion

                return await Task<RestCallResult>.Run<RestCallResult>(() =>
                 {
                     #region 写入请求数据
                     if (!string.IsNullOrEmpty(PostData))// && Method == HttpVerb.POST)
                     {
                         var encoding = new UTF8Encoding();
                         //var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
                         var bytes = UTF8Encoding.UTF8.GetBytes(PostData);

                         // bytes=  AddZip(bytes);

                         request.ContentLength = bytes.Length;

                         using (var writeStream = request.GetRequestStream())
                         {
                             writeStream.Write(bytes, 0, bytes.Length);
                         }
                     }
                     #endregion

                     #region 获取返回结果
                     using (var response = (HttpWebResponse)request.GetResponse())
                     {
                         var responseValue = string.Empty;

                         if (response.StatusCode != HttpStatusCode.OK)
                         {
                             var message = String.Format("Request Failed. Received HTTP {0}", response.StatusCode);
                             throw new ApplicationException(message);
                         }

                         // grab the response
                         using (var responseStream = response.GetResponseStream())
                         {
                             if (responseStream != null)
                             {
                                 using (var reader = new StreamReader(responseStream, Encoding.UTF8))
                                 {
                                     responseValue = reader.ReadToEnd();
                                 }
                             }
                         }

                         #region 内部错误处理

                         if (responseValue.Contains("<!DOCTYPE html>")
                              && responseValue.Contains("出错啦 （@_@）!")
                              && responseValue.Contains("请稍后在重新尝试执行"))
                         {
                             throw new Exception(responseValue);
                         }

                         #endregion

                         #region 解析接口返回

                         var vServiceTest = GZipHelper.AnalysisRestMsg(responseValue);
                         if (!vServiceTest.Item1)
                         {
                             return new RestCallResult() { IsOk = false, Msg = vServiceTest.Item2.Msg };
                         }
                         else
                         {
                             return new RestCallResult() { IsOk = vServiceTest.Item2.IsSuccess, Msg = vServiceTest.Item2.Msg };
                         }
                         #endregion
                     }
                     #endregion

                 });

                #endregion
            }
            catch (Exception ex)
            {
                return new RestCallResult() { IsOk = false, Msg = ex.Message };
            }
        }

        public void Dispose()
        {
            return;
        }
    }
}

