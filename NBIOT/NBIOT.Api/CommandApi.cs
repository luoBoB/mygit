using NBIOT.Bll;
using NBIOT.Common;
using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NBIOT.Api
{
    public class CommandApi : BaseApi
    {
        /// <summary>
        ///  2.7.1 命令下发 
        /// </summary>
        /// <param name="scr"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public ReturnResult<DeviceCommandResp> DeviceCommands(SendCommandRequest scr, int appId)
        {
            var rst = new ReturnResult<DeviceCommandResp>();

            var tokenRst = new TokenApi().GetAccessToken(appId);
            var token = tokenRst.Data.AccessToken;
            if (string.IsNullOrEmpty(token))
            {
                rst.Message = "获取token失败";
                return rst;
            }

            //根据AppId获取对应的应用服务器地址
            var appRst = new AppsBll().GetOne(new Apps() { Id = appId });
            if (appRst.Result)
            {
                var apps = appRst.Data;
                WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
                webHeaderCollection.Add("app_key", apps.AppId);
                webHeaderCollection.Add("Authorization", "Bearer " + token);
                rst = GetDataFromAPI<DeviceCommandResp>(new RequestPara()
                {
                    ServerUrl = apps.AppServer,
                    Method = HttpClientActionMethod.POST,
                    ContentType = "application/json",
                    ApiPath = "/iocm/app/cmd/v1.4.0/deviceCommands",
                    Headers = webHeaderCollection,
                    PostData = JsonHelper.Instance.Serialize(scr),
                    CertFile = ApiConfig.CertFile,
                    CertPassword = ApiConfig.CertPassword
                });
            }

            return rst;
        }

        /// <summary>
        /// 2.7.2 查询设备指令 
        /// </summary>
        /// <param name="pageNo">（可选）查询的页码，大于等于 0，默认值：0。</param>
        /// <param name="pageSize">（可选）查询每页信息的数量</param>
        /// <param name="deviceId">（可选）指定查询命令的设备 ID</param>
        /// <param name="startTime">（可选）查询开始时间  格式： yyyyMMdd'T'HHmmss'Z'，如20151212T121212Z</param>
        /// <param name="endTime">（可选）查询结束时间</param>
        /// <param name="appId">数据库主键</param>
        /// <returns></returns>
        public ReturnResult<SelectDeviceResult> selectDeviceResult(int pageNo, int pageSize, string deviceId, string startTime, string endTime, int appId)
        {
            var sdr = new ReturnResult<SelectDeviceResult>();
            //获取token
            var tokenRst = new TokenApi().GetAccessToken(appId);
            var token = tokenRst.Data.AccessToken;
            if (string.IsNullOrEmpty(token))
            {
                sdr.Message = "获取token失败";
                return sdr;
            }

            var appRst = new AppsBll().GetOne(new Apps() { Id = appId });
            if (appRst.Result)
            {
                var apps = appRst.Data;
                WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
                webHeaderCollection.Add("app_key", apps.AppId);
                webHeaderCollection.Add("Authorization", "Bearer" + token);

                //添加可选参数
                StringBuilder param = null;
                if (pageNo >= 0 || (pageSize >= 1 && pageSize <= 1000)
                    || string.IsNullOrEmpty(deviceId) || string.IsNullOrEmpty(startTime)
                    || string.IsNullOrEmpty(endTime) || string.IsNullOrEmpty(apps.AppId))
                {
                    param.Append("?");
                    param.Append($"pageNo={pageNo}&pageSize={pageSize}&deviceId={deviceId}&startTime={startTime}&endTime={endTime}&appId={apps.AppId}");
                }
                //发送且返回结果集
                sdr = GetDataFromAPI<SelectDeviceResult>(new RequestPara()
                {
                    ServerUrl = apps.AppServer,
                    Method = HttpClientActionMethod.GET,
                    ContentType = "application/json",
                    ApiPath = "/iocm/app/cmd/v1.4.0/deviceCommands" + param,
                    Headers = webHeaderCollection,
                    CertFile = ApiConfig.CertFile,
                    CertPassword = ApiConfig.CertPassword
                });
            }
            return sdr;
        }
        /// <summary>
        /// 2.7.3 修改设备命令（当前仅支持修改的命令状态为撤销， 即将命令撤销）
        /// </summary>
        /// <param name="appid">表主键</param>
        /// <param name="deviceCommandId">要修改的命令ID， 在调用创建设备命令接口后获得</param>
        /// <returns></returns>
        public ReturnResult<DeviceCommandResp> alterDeviceResult(int appid, string deviceCommandId)
        {
            var dcr = new ReturnResult<DeviceCommandResp>();
            //获取token
            var tokenRst = new TokenApi().GetAccessToken(appid);
            var token = tokenRst.Data.AccessToken;
            if (string.IsNullOrEmpty(token))
            {
                dcr.Message = "获取token失败";
                return dcr;
            }
            var appRst = new AppsBll().GetOne(new Apps() { Id = appid });
            if (appRst.Result)
            {
                var apps = appRst.Data;
                WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
                webHeaderCollection.Add("app_key", apps.AppId);
                webHeaderCollection.Add("Authorization", "Bearer" + token);

                //添加可选参数
                StringBuilder param = new StringBuilder();
                if (!string.IsNullOrEmpty(appRst.Data.AppId))
                {
                    param.Append($"?appId={appRst.Data.AppId}");
                }
                //发送并返回结果集
                dcr = GetDataFromAPI<DeviceCommandResp>(new RequestPara()
                {
                    ServerUrl = apps.AppServer,
                    Method = HttpClientActionMethod.PUT,
                    ContentType = "application/json",
                    ApiPath = $"/iocm/app/cmd/v1.4.0/deviceCommands/{deviceCommandId}" + param,
                    Headers = webHeaderCollection,
                    PostData = JsonHelper.Instance.Serialize(new
                    {
                        status = "CANCELED", //撤销
                    })
                });
            }
            return dcr;
        }
        /// <summary>
        /// 2.7.4 创建设备命令撤销任务
        /// </summary>
        /// <param name="appid">表主键</param>
        /// <param name="deviceId">待撤销设备命令的设备 ID</param>
        /// <returns></returns>
        public ReturnResult<CreateDevCommCancel> createDeviceCommandCancel(int appid, string deviceId)
        {
            var ccc = new ReturnResult<CreateDevCommCancel>();
            //获取token
            var tokenRst = new TokenApi().GetAccessToken(appid);
            var token = tokenRst.Data.AccessToken;
            if (string.IsNullOrEmpty(token))
            {
                ccc.Message = "获取token失败";
                return ccc;
            }
            var appRst = new AppsBll().GetOne(new Apps() { Id = appid });

            if (appRst.Result)
            {
                var apps = appRst.Data;
                WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
                webHeaderCollection.Add("app_key", apps.AppId);
                webHeaderCollection.Add("Authorization", "Bearer" + token);

                //添加可选参数
                StringBuilder param = new StringBuilder();
                if (!string.IsNullOrEmpty(appRst.Data.AppId))
                {
                    param.Append($"?appId={appRst.Data.AppId}");
                }
                //发送并返回结果集
                ccc = GetDataFromAPI<CreateDevCommCancel>(new RequestPara()
                {
                    ServerUrl = apps.AppServer,
                    Method = HttpClientActionMethod.PUT,
                    ContentType = "application/json",
                    ApiPath = "/iocm/app/cmd/v1.4.0/deviceCommandCancelTasks" + param,
                    Headers = webHeaderCollection,
                    PostData = JsonHelper.Instance.Serialize(new
                    {
                        deviceId, //撤销
                    })
                });
            }
            return ccc;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="taskId"></param>
        /// <param name="deviceId"></param>
        /// <param name="status"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ReturnResult<SelectDevCommCancel> selectDevCommCancel(int appid, int pageNo, int pageSize, string taskId, string deviceId, string status, string startTime, string endTime)
        {
            var sdcc = new ReturnResult<SelectDevCommCancel>();

            //获取token
            var tokenRst = new TokenApi().GetAccessToken(appid);
            var token = tokenRst.Data.AccessToken;
            if (string.IsNullOrEmpty(token))
            {
                sdcc.Message = "获取token失败";
                return sdcc;
            }
            var appRst = new AppsBll().GetOne(new Apps() { Id = appid });

            if (appRst.Result)
            {
                var apps = appRst.Data;
                //构造请求头
                WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
                webHeaderCollection.Add("app_key", apps.AppId);
                webHeaderCollection.Add("Authorization", "Bearer" + token);

                //添加可选参数
                StringBuilder param = null;
                if (pageNo >= 0 || (pageSize >= 1 && pageSize <= 1000)
                    || string.IsNullOrEmpty(deviceId) || string.IsNullOrEmpty(startTime)
                    || string.IsNullOrEmpty(endTime) || string.IsNullOrEmpty(apps.AppId))
                {
                    param.Append("?");
                    param.Append($"pageNo={pageNo}&pageSize={pageSize}&deviceId={deviceId}&startTime={startTime}&endTime={endTime}&appId={apps.AppId}");
                }
                //发送并返回结果集
                sdcc = GetDataFromAPI<SelectDevCommCancel>(new RequestPara()
                {
                    ServerUrl = apps.AppServer,
                    Method = HttpClientActionMethod.GET,
                    ContentType = "application/json",
                    ApiPath = "/iocm/app/cmd/v1.4.0/deviceCommandCancelTasks" + param,
                    Headers = webHeaderCollection

                });
            }
            return sdcc;

        }

    }


}
