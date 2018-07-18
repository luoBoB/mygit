using NBIOT.Bll;
using NBIOT.Common;
using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NBIOT.Api
{
    public class DeviceApi : BaseApi
    {
        public ReturnResult<RegDeviceResult> RegDevice(RegDeviceRequest model)
        {
            var rst = new ReturnResult<RegDeviceResult>();

            //根据AppId获取对应的应用服务器地址
            var appRst = new AppsBll().GetOne(new Apps() { Id = model.appId });
            if (appRst.Result)
            {
                var apps = appRst.Data;

                var tokenRst = new TokenApi().GetAccessToken(model.appId);
                var token = tokenRst.Data.AccessToken;
                if (string.IsNullOrEmpty(token))
                {
                    return rst;
                }
                WebHeaderCollection headers = new WebHeaderCollection();
                headers.Add("app_key", apps.AppId);
                headers.Add("Authorization", "Bearer " + token);
                rst = GetDataFromAPI<RegDeviceResult>(new RequestPara()
                {
                    ServerUrl = apps.AppServer,
                    ApiPath = $"/iocm/app/reg/v1.2.0/devices?appId={apps.AppId}",
                    Method = HttpClientActionMethod.POST,
                    ContentType = "application/json",
                    Headers = headers,
                    PostData = JsonHelper.Instance.Serialize(new
                    {
                        //endUserId,
                        model.verifyCode,
                        model.nodeId,
                        //psk,
                        model.timeout
                    }),
                    CertFile = ApiConfig.CertFile,
                    CertPassword = ApiConfig.CertPassword
                });
            }

            return rst;
        }

        public ReturnResult<ApiResult> UpdateDevice(int appId, string deviceId, string name, Profile profile, string protocolType)
        {
            var rst = new ReturnResult<ApiResult>();

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
                rst = GetDataFromAPI<ApiResult>(new RequestPara()
                {
                    ServerUrl = apps.AppServer,
                    ApiPath = $"/iocm/app/dm/v1.4.0/devices/{deviceId}?appId={apps.AppId}",
                    Method = HttpClientActionMethod.PUT,
                    ContentType = "application/json",
                    Headers = headers,
                    PostData = JsonHelper.Instance.Serialize(new
                    {
                        appId = apps.AppId,
                        deviceId,
                        name,
                        manufacturerId = profile.ManufacturerId,
                        manufacturerName = profile.ManufacturerName,
                        model = profile.Model,
                        deviceType = profile.DeviceType,
                        protocolType
                    }),
                    CertFile = ApiConfig.CertFile,
                    CertPassword = ApiConfig.CertPassword
                });
            }

            return rst;
        }

        public ReturnResult<ApiResult> DelDevice(int appId, string deviceId)
        {
            var rst = new ReturnResult<ApiResult>();

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
                rst = GetDataFromAPI<ApiResult>(new RequestPara()
                {
                    ServerUrl = apps.AppServer,
                    ApiPath = $"/iocm/app/dm/v1.4.0/devices/{deviceId}?appId={apps.AppId}",
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
