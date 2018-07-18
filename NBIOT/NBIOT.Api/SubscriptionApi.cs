using NBIOT.Bll;
using NBIOT.Common;
using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NBIOT.Api
{
    public class SubscriptionApi : BaseApi
    {
        public ReturnResult<SubScriptionBusinessRequest> SubscriptionBusinessData(int appId,string notifyType, string callbackUrl, bool? ownerFlag = true)
        {
            var rst = new ReturnResult<SubScriptionBusinessRequest>();
            //根据AppId获取对应的应用服务器地址
            var appRst = new AppsBll().GetOne(new Apps() { Id = appId });
            if (appRst.Result)
            {
                var apps = appRst.Data;

                var tokenRst = new TokenApi().GetAccessToken(appId);
                var token = tokenRst.Data.AccessToken;
                if (string.IsNullOrEmpty(token))
                {
                    return rst;
                }
                WebHeaderCollection headers = new WebHeaderCollection();
                headers.Add("app_key", apps.AppId);
                headers.Add("Authorization", "Bearer " + token);
                rst = GetDataFromAPI<SubScriptionBusinessRequest>(new RequestPara()
                {
                    ServerUrl = apps.AppServer,
                    ApiPath = $"/iocm/app/sub/v1.2.0/subscriptions?ownerFlag={ownerFlag}",
                    Method = HttpClientActionMethod.POST,
                    ContentType = "application/json",
                    Headers = headers,
                    PostData = JsonHelper.Instance.Serialize(new
                    {
                        appId = apps.AppId,
                        notifyType,
                        callbackUrl
                    }),
                    CertFile = ApiConfig.CertFile,
                    CertPassword = ApiConfig.CertPassword
                });
            }

            return rst;
        }

        public ReturnResult<SubScriptionBusinessRequest> DelSubscription(string subscriptionId, int appId)
        {
            var rst = new ReturnResult<SubScriptionBusinessRequest>();
            //根据AppId获取对应的应用服务器地址
            var appRst = new AppsBll().GetOne(new Apps() { Id = appId });
            if (appRst.Result)
            {
                var apps = appRst.Data;

                var tokenRst = new TokenApi().GetAccessToken(appId);
                var token = tokenRst.Data.AccessToken;
                if (string.IsNullOrEmpty(token))
                {
                    return rst;
                }
                WebHeaderCollection headers = new WebHeaderCollection();
                headers.Add("app_key", apps.AppId);
                headers.Add("Authorization", "Bearer " + token);
                rst = GetDataFromAPI<SubScriptionBusinessRequest>(new RequestPara()
                {
                    ServerUrl = apps.AppServer,
                    ApiPath = $"/iocm/app/sub/v1.2.0/subscriptions/{subscriptionId}?appId={apps.AppId}",
                    Method = HttpClientActionMethod.DELETE,
                    ContentType = "application/json",
                    Headers = headers,
                    CertFile = ApiConfig.CertFile,
                    CertPassword = ApiConfig.CertPassword
                });
            }

            return rst;
        }

    }
}
