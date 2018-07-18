using Nancy;
using NBIOT.Common;
using NBIOT.IParse;
using NBIOT.Model;
using NBIOT.Bll;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NBIOT.Api;

namespace NBIOT.Subscibe
{
    public class SubscibeModule : NancyModule
    {
        public SubscibeModule()
        {
            //添加设备
            Post("/deviceAdded", (x) =>
            {
                try
                {
                    StreamReader readStream = new StreamReader(Request.Body, Encoding.UTF8);
                    string data = readStream.ReadToEnd();
                    HandleLog.WriteLog("收到数据：" + data + Environment.NewLine);
                    if (!string.IsNullOrEmpty(data))
                    {
                        //打印
                        var result = JsonHelper.Instance.Deserialize<DeviceResult<object>>(data);

                        HandleLog.WriteLog("事件类型：" + result.notifyType);
                        HandleLog.WriteLog("设备Id：" + result.deviceId);
                        HandleLog.WriteLog("NodeId：" + result.deviceInfo.nodeId);

                    }
                }
                catch (Exception ex)
                {
                    HandleLog.WriteLog(ex.Message);
                }

                return "{ code: 200 }";
            });

            //设备数据变化
            Post("/deviceDataChanged", (x) =>
            {
                try
                {
                    StreamReader readStream = new StreamReader(Request.Body, Encoding.UTF8);
                    string data = readStream.ReadToEnd();
                    HandleLog.WriteLog("收到数据：" + data + Environment.NewLine);
                    if (!string.IsNullOrEmpty(data))
                    {
                        var result = JsonHelper.Instance.Deserialize<ResponseResult<object>>(data);
                        HandleLog.WriteLog("事件类型：" + result.notifyType);
                        HandleLog.WriteLog("设备Id：" + result.deviceId);

                        //查询数据库的解析dll 反射创建解析类的实例
                        var protocolRst = new ProtocolBll().GetOneByDeviceId(result.deviceId);
                        if (protocolRst.Result)
                        {
                            //IParseClass clsInterface = new NBIOT.ParseAlarm.ParseClass();

                            var protocol = protocolRst.Data;
                            ReflectionLesson refl = new ReflectionLesson(protocol.FullDllPath, protocol.DllName, "ParseClass");

                            //直接使用反射调用方法
                            IParseClass clsInterface = refl.ReflectionClass();
                            clsInterface.DeviceDataChanged(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    HandleLog.WriteLog(ex.Message);
                }

                return "{ code: 200 }";
            });

            //删除设备
            Post("/deviceDeleted", (x) =>
            {
                try
                {
                    StreamReader readStream = new StreamReader(Request.Body, Encoding.UTF8);
                    string data = readStream.ReadToEnd();
                    HandleLog.WriteLog("收到数据：" + data + Environment.NewLine);
                    if (!string.IsNullOrEmpty(data))
                    {
                        var result = JsonHelper.Instance.Deserialize<DeviceResult<object>>(data);
                        HandleLog.WriteLog("事件类型：" + result.notifyType);
                        HandleLog.WriteLog("设备Id：" + result.deviceId);
                        UserDeviceBll deviceBll = new UserDeviceBll();
                        var deviceModel = new UserDevice
                        {
                            DeviceId = result.deviceId
                        };
                        var delRst = deviceBll.Delete(deviceModel);
                        HandleLog.WriteLog("删除结果：" + delRst.Message);
                    }
                }
                catch (Exception ex)
                {
                    HandleLog.WriteLog(ex.Message);
                }

                return "{ code: 200 }";
            });

            Post("/commandRsp", (x) =>
            {
                try
                {
                    StreamReader readStream = new StreamReader(Request.Body, Encoding.UTF8);
                    string data = readStream.ReadToEnd();
                    HandleLog.WriteLog("收到数据：" + data + Environment.NewLine);
                    if (!string.IsNullOrEmpty(data))
                    {
                        var result = JsonHelper.Instance.Deserialize<CommandResult<object>>(data);
                        HandleLog.WriteLog("事件类型：commandRsp");
                        HandleLog.WriteLog("设备Id：" + result.deviceId);
                        HandleLog.WriteLog("命令Id：" + result.commandId);

                        IParseClass clsInterface = new NBIOT.ParseAlarm.ParseClass();
                        clsInterface.CommandRsp(result);
                    }
                }
                catch (Exception ex)
                {
                    HandleLog.WriteLog(ex.Message);
                }

                return "{ code: 200 }";
            });
        }
    }
}
