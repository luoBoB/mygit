using NBIOT.Common;
using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace NBIOT.Api
{
    public class BaseApi
    {
        protected ReturnResult<T> GetDataFromAPI<T>(RequestPara para)
        {
            var rst = new ReturnResult<T>();

            try
            {
                string url = string.Format("https://{0}{1}", para.ServerUrl, para.ApiPath);
                if (para.IsHttp)
                {
                    url = string.Format("http://{0}{1}", para.ServerUrl, para.ApiPath);
                }
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(url);

                X509Certificate2 cerCaiShang = new X509Certificate2(para.CertFile, para.CertPassword, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);

                if (!para.IsHttp)
                {
                    httpRequest.ClientCertificates.Add(cerCaiShang);
                }
                httpRequest.Method = para.Method.ToString();
                httpRequest.ContentType = para.ContentType;
                httpRequest.Referer = para.Referer;
                httpRequest.AllowAutoRedirect = para.AllowAutoRedirect;
                httpRequest.UserAgent = para.UserAgent;
                httpRequest.Accept = para.Accept;
                if (para.Headers != null)
                {
                    for (int i = 0; i < para.Headers.Count; i++)
                    {
                        for (int j = 0; j < para.Headers.GetValues(i).Length; j++)
                        {
                            httpRequest.Headers.Add(para.Headers.Keys[i], para.Headers.GetValues(i)[j]);
                        }
                    }
                }

                if (para.IsHttp)
                {
                    httpRequest.ServicePoint.Expect100Continue = false;
                }

                if (para.Method != HttpClientActionMethod.GET && !string.IsNullOrEmpty(para.PostData))
                {
                    Stream requestStem = httpRequest.GetRequestStream();
                    StreamWriter sw = new StreamWriter(requestStem);
                    sw.Write(para.PostData);
                    sw.Close();
                }

                HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                Stream receiveStream = httpResponse.GetResponseStream();
                string result = string.Empty;
                using (StreamReader sr = new StreamReader(receiveStream))
                {
                    result = sr.ReadToEnd();
                }

                rst.Result = true;
                rst.Status = httpResponse.StatusCode.ToString();
                rst.Data = JsonHelper.Instance.Deserialize<T>(result);
                rst.Message = "执行成功";
            }
            catch (Exception ex)
            {
                Log4netUtil.Error(ex);
                rst.Message = ex.Message;
            }

            return rst;
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;
            return true;
        }

    }

    public class RequestPara
    {
        public bool IsHttp { get; set; } = false;
        public string ServerUrl { get; set; }
        public HttpClientActionMethod Method { get; set; }
        public string ApiPath { get; set; }
        public string PostData { get; set; }
        public string CertFile { get; set; }
        public string CertPassword { get; set; }
        public WebHeaderCollection Headers { get; set; }
        public string ContentType { get; set; }
        public string Referer { get; set; } = null;
        public bool AllowAutoRedirect { get; set; } = true;
        public string UserAgent { get; set; } = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.2; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        public string Accept { get; set; } = "*/*";
    }
}
