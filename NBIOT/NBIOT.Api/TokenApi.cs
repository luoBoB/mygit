using NBIOT.Bll;
using NBIOT.Common;
using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NBIOT.Api
{
    public class TokenApi : BaseApi
    {
        public ReturnResult<ApiToken> Login(int appId)
        {
            var rst = new ReturnResult<ApiToken>();
            //根据AppId获取对应的应用服务器地址
            var appRst = new AppsBll().GetOne(new Apps() { Id = appId });
            if (appRst.Result)
            {
                var apps = appRst.Data;

                var tokenRst = GetDataFromAPI<TokenResult>(new RequestPara()
                {
                    ServerUrl = apps.AppServer,
                    ApiPath = "/iocm/app/sec/v1.1.0/login",
                    Method = HttpClientActionMethod.POST,
                    ContentType = "application/x-www-form-urlencoded",
                    PostData = $"appId={apps.AppId}&secret={apps.Secret}",
                    CertFile = ApiConfig.CertFile,
                    CertPassword = ApiConfig.CertPassword
                });

                if (tokenRst.Result && !string.IsNullOrEmpty(tokenRst.Data.accessToken))
                {
                    //添加到数据库
                    var model = new ApiToken()
                    {
                        AppId = appId,
                        AccessToken = tokenRst.Data.accessToken,
                        AccessTokenCreateTime = DateTime.Now.AddMinutes(-2),
                        AccessTokenExpiresIn = tokenRst.Data.expiresIn,
                        RefreshToken = tokenRst.Data.refreshToken,
                        RefreshTokenExpiresTime = DateTime.Now.AddMonths(1).AddDays(-1)
                    };
                    var bll = new ApiTokenBll();
                    var addRst = bll.Add(model);
                    if (addRst.Result)
                    {
                        //添加成功
                        rst.Result = true;
                        rst.Data = model;
                        rst.Message = tokenRst.Message;
                    }
                }
            }

            return rst;
        }

        public ReturnResult<ApiToken> RefreshAccessToken(int appId, string refreshToken)
        {
            var rst = new ReturnResult<ApiToken>();

            //根据AppId获取对应的应用服务器地址
            var appRst = new AppsBll().GetOne(new Apps() { Id = appId });
            if (appRst.Result)
            {
                var apps = appRst.Data;
                WebHeaderCollection headers = new WebHeaderCollection();
                headers.Add("app_key", apps.AppId);
                var tokenRst = GetDataFromAPI<TokenResult>(new RequestPara()
                {
                    ServerUrl = apps.AppServer,
                    ApiPath = "/iocm/app/sec/v1.1.0/refreshToken",
                    Method = HttpClientActionMethod.POST,
                    ContentType = "application/json",
                    Headers = headers,
                    PostData = JsonHelper.Instance.Serialize(new
                    {
                        appId = apps.AppId,
                        secret = apps.Secret,
                        refreshToken
                    }),
                    CertFile = ApiConfig.CertFile,
                    CertPassword = ApiConfig.CertPassword
                });

                if (tokenRst.Result && !string.IsNullOrEmpty(tokenRst.Data.accessToken))
                {
                    //添加到数据库
                    var model = new ApiToken()
                    {
                        AppId = appId,
                        AccessToken = tokenRst.Data.accessToken,
                        AccessTokenCreateTime = DateTime.Now.AddMinutes(-2),
                        AccessTokenExpiresIn = tokenRst.Data.expiresIn,
                        RefreshToken = tokenRst.Data.refreshToken,
                        RefreshTokenExpiresTime = DateTime.Now.AddDays(1)
                    };
                    var bll = new ApiTokenBll();
                    var addRst = bll.Update(model);
                    if (addRst.Result)
                    {
                        //刷新成功
                        rst.Result = true;
                        rst.Data = model;
                        rst.Message = tokenRst.Message;
                    }
                }
            }

            return rst;
        }

        public ReturnResult<ApiToken> GetAccessToken(int appId)
        {
            var rst = new ReturnResult<ApiToken>();

            var tokenRst = new ApiTokenBll().GetOne(new ApiToken() { AppId = appId });
            if (tokenRst.Result)
            {
                var data = tokenRst.Data;
                TimeSpan diff = DateTime.Now - data.AccessTokenCreateTime.Value;
                double diffSecond = diff.TotalSeconds;
                if (diffSecond >= data.AccessTokenExpiresIn)
                {
                    var loginRst = Login(appId);
                    if (loginRst.Result && !string.IsNullOrEmpty(loginRst.Data.AccessToken))
                    {
                        rst = loginRst;
                    }


                    //RefreshToken过期了从NBIOT接口重新鉴权获取Token
                    //if (DateTime.Now > data.RefreshTokenExpiresTime)
                    //{
                    //    var loginRst = Login(appId);
                    //    if (loginRst.Result && !string.IsNullOrEmpty(loginRst.Data.AccessToken))
                    //    {
                    //        rst = loginRst;
                    //    }
                    //}
                    //else
                    //{
                    //    //RefreshToken没过期使用刷新接口 
                    //    var refreshRst = RefreshAccessToken(appId, data.RefreshToken);
                    //    if (refreshRst.Result && !string.IsNullOrEmpty(refreshRst.Data.AccessToken))
                    //    {
                    //        rst = refreshRst;
                    //    }
                    //}
                }
                else
                {
                    rst.Result = true;
                    rst.Data = data;
                    rst.Message = tokenRst.Message;
                }
            }
            else
            {
                //数据库没有的 从NBIOT接口获取
                var loginRst = Login(appId);
                if (loginRst.Result && !string.IsNullOrEmpty(loginRst.Data.AccessToken))
                {
                    rst = loginRst;
                }
            }

            return rst;
        }
    }
}
