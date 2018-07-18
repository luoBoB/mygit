using NBIOT.Api;
using NBIOT.Bll;
using NBIOT.Common;
using NBIOT.IParse;
using NBIOT.Model;
using System;
using System.Collections.Generic;

namespace NBIOT.ParseAlarm
{
    public class ParseClass : IParseClass
    {
        public bool DeviceDataChanged(object data)
        {
            var rst = (ResponseResult<object>)data;
            //var serviceData = JsonHelper.Instance.Deserialize<Alarm>(rst.service.data.ToString());
            HandleLog.WriteLog($"服务Id：{rst.service.serviceId}");
            HandleLog.WriteLog($"服务类型：{rst.service.serviceType}");
            HandleLog.WriteLog($"服务数据：{Environment.NewLine}{rst.service.data.ToString()}");

            //HandleLog.WriteLog($"燃气值：{serviceData.FuelGas}");
            //HandleLog.WriteLog($"结束符：{serviceData.EndFlag}");

            return true;
        }

        public bool CommandRsp(object data)
        {
            var rst = (CommandResult<object>)data;
            //var serviceData = JsonHelper.Instance.Deserialize<Alarm>(rst.service.data.ToString());
            HandleLog.WriteLog($"响应状态：{rst.result.resultCode}");
            HandleLog.WriteLog($"响应结果：{Environment.NewLine}{rst.result.resultDetail}");

            var bll = new CommandInfoBll();
            var udpateRst = bll.UpdateStatusAndResult(new CommandInfo()
            {
                CommandId = rst.commandId,
                Status = rst.result.resultCode,
                Result = JsonHelper.Instance.Serialize(rst.result.resultDetail)
            });
            HandleLog.WriteLog("修改数据库状态：" + udpateRst.Message);

            return true;
        }

        public object GetCommandPara(string serviceId, string method, Dictionary<string, object> data)
        {
            var rst = new object();

            var cmd = new Command();
            if (serviceId == "OpenLock_Event")
            {
                if (method == "OpenLock")
                {
                    rst = cmd.GetOpenLockPara(data);
                }
            }

            return rst;
        }
    }
}
