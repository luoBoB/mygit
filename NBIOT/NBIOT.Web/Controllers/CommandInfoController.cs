using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NBIOT.Model;
using NBIOT.Common;
using NBIOT.Bll;
using NBIOT.Web.Filters;
using NBIOT.Api;

namespace NBIOT.Web.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class CommandInfoController : ControllerBase
    {
        [HttpPost]
        [Route("GetNew")]
        [AdminRequired]
        public string GetNew()
        {
            var rst = new ReturnResult<CommandInfo>
            {
                Result = true,
                Data = new CommandInfo(),
                Message = "获取成功"
            };

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("Add")]
        [AdminRequired]
        public string Add(CommandInfo model)
        {
            var rst = new ReturnResult<bool>();
            var bll = new CommandInfoBll();
            if (model.Id == 0)
            {
                rst = bll.Add(model);
            }
            else
            {
                rst = bll.Update(model);
            }

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("Del")]
        [AdminRequired]
        public string Del(CommandInfo model)
        {
            var rst = new ReturnResult<bool>();
            var bll = new CommandInfoBll();
            rst = bll.Delete(model);

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpGet]
        [HttpPost]
        [Route("GetOne")]
        [AdminRequired]
        public string GetOne(CommandInfo model)
        {
            var rst = new ReturnResult<CommandInfo>();
            var bll = new CommandInfoBll();
            rst = bll.GetOne(model);

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpGet]
        [HttpPost]
        [Route("GetCommand")]
        [AdminRequired]
        public string GetCommand(string commandId)
        {
            var rst = new ReturnResult<CommandInfo>();
            var bll = new CommandInfoBll();
            rst = bll.GetOne(new CommandInfo() { CommandId = commandId });

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("GetList")]
        [AdminRequired]
        public string GetList(CommandInfo model)
        {
            var rst = new ReturnResult<List<CommandInfo>>();
            var bll = new CommandInfoBll();
            rst = bll.GetList(model);

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpGet]
        [HttpPost]
        [Route("DeviceCommands")]
        public string DeviceCommands(int appId, string deviceId, string serviceId, string method, string paras, int expireTime = 1200, int maxRetransmit = 3)
        {
            var rst = new ReturnResult<DeviceCommandResp>();

            var api = new CommandApi();
            rst = api.DeviceCommands(new SendCommandRequest
            {
                deviceId = deviceId,
                command = new CommandDetail
                {
                    serviceId = serviceId,
                    method = method,
                    paras = JsonHelper.Instance.Deserialize<object>(paras)
                },
                callbackUrl = "http://120.76.164.210:9999/commandRsp",
                expireTime = expireTime,
                maxRetransmit = maxRetransmit
            }, appId);

            if (rst.Result)
            {
                var data = rst.Data;
                var bll = new CommandInfoBll();

                var addRst = bll.Add(new CommandInfo()
                {
                    CommandId = data.commandId,
                    AppId = data.appId,
                    DeviceId = data.deviceId,
                    Command = JsonHelper.Instance.Serialize(data.command),
                    CallbackUrl = data.callbackUrl,
                    ExpireTime = data.expireTime,
                    Status = data.status,
                    Result = JsonHelper.Instance.Serialize(data.result),
                    CreationTime = FunHelper.UTCToDateTime(data.creationTime),
                    ExecuteTime = FunHelper.UTCToDateTime(data.executeTime),
                    PlatformIssUedTime = FunHelper.UTCToDateTime(data.platformIssuedTime),
                    DeliveredTime = FunHelper.UTCToDateTime(data.deliveredTime),
                    IssuedTimes = data.issuedTimes,
                    MaxRetransMit = data.maxRetransmit,
                });
                if (!addRst.Result)
                {
                    rst.Result = false;
                    rst.Message = addRst.Message;
                }
            }

            return JsonHelper.Instance.Serialize(rst);
        }
    }
}