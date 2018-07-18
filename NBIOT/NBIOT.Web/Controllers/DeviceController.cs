using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NBIOT.Model;
using NBIOT.Common;
using NBIOT.Bll;
using NBIOT.Api;

namespace NBIOT.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        [HttpGet]
        [HttpPost]
        [Route("RegDevice")]
        public string RegDevice(RegDeviceRequest model)
        {
            var rst = new ReturnResult<RegDeviceResult>();

            var api = new DeviceApi();
            var apiRst = api.RegDevice(model);
            if (apiRst.Result && !string.IsNullOrEmpty(apiRst.Data.deviceId))
            {
                //根据设备型号查找profile
                var bll = new ProfileBll();
                var profileRst = bll.GetOneByModel(new Profile() { Model = model.model });
                if (profileRst.Result)
                {
                    var profile = profileRst.Data;
                    //修改设备
                    var updateRst = api.UpdateDevice(model.appId, apiRst.Data.deviceId, model.name, profile, "CoAP");
                    if (updateRst.Result && updateRst.Status == "NoContent")
                    {
                        rst.Result = true;
                        rst.Data = apiRst.Data;
                        rst.Message = "注册成功";
                    }
                    else
                    {
                        rst.Message = "注册失败";
                    }
                }
                else
                {
                    rst.Message = "设备型号不存在";
                }
                //插入用户设备表
                var device = new UserDevice()
                {
                    DeviceId = apiRst.Data.deviceId,
                    DeviceName = model.name,
                    VerifyCode = model.verifyCode,
                    NodeId = model.nodeId,
                    ProfileId = profileRst.Data?.Id,
                    UserId = model.userId
                };
                var addDeviceRst = new UserDeviceBll().AddOrUpdate(device);
                if (addDeviceRst.Result)
                {
                    rst.Result = true;
                    rst.Message = "注册成功";
                }
            }
            else
            {
                rst.Message = "注册失败";
            }

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpGet]
        [HttpPost]
        [Route("DelDevice")]
        public string DelDevice(int appId, string deviceId)
        {
            var rst = new ReturnResult<bool>();

            var api = new DeviceApi();
            var delRst = api.DelDevice(appId, deviceId);
            if (delRst.Status == "NoContent")
            {
                rst.Result = true;
                rst.Message = "删除成功";

                var bll = new UserDeviceBll();
                var del = bll.Delete(new UserDevice() { DeviceId = deviceId });
            }
            else
            {
                rst.Message = "删除失败";
            }

            return JsonHelper.Instance.Serialize(rst);
        }
    }
}