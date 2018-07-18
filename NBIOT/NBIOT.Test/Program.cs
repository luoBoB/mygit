using NBIOT.Api;
using NBIOT.Bll;
using NBIOT.Common;
using NBIOT.Model;
using NBIOT.Web.Controllers;
using System;
using System.Text;

namespace NBIOT.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var dt = FunHelper.UTCToDateTime("20180706T061635Z");

            var data = "{\"姓名\":\"罗报道\",\"手机\":\"13266513170\",\"年龄\":\"18\",\"性别\":\"女\",\"学车类型\":\"C1\",\"手动挡\":\"是\"}";

 


            Console.ReadKey();
            //while (true)
            //{
            //    Console.WriteLine("请输入任意键发送：");

            //    Console.ReadKey();

            //    CommandApi api = new CommandApi();

            //    var cmd = new CommandDetail()
            //    {
            //        serviceId = "AutoAlarm",
            //        method = "CancelAlarm",
            //        paras = new
            //        {
            //            //SyncByte = 0x5A,
            //            //BodyLen = 0x1B,
            //            //CmdType = 0x01,
            //            //Status = 0x04,
            //            //Flags = 0x00,
            //            //AppId = DataHelper.HexToInt32("0384"),
            //            //OrderNo = 0x14,
            //            //SDID1 = DataHelper.HexToInt32("A137"),
            //            //SDID2 = DataHelper.HexToInt32("8101"),
            //            //Pwd = DataHelper.HEXToASCII("313233343536000000000000"),
            //            //DelayLockSeconds = DataHelper.HexToInt32("000000FF"),
            //            //CheckSum = 0xD1

            //            FuncCode = 0x01,
            //            EndFlag = 0x0D
            //        }
            //    };

            //    var rst = api.DeviceCommands(new SendCommandRequest()
            //    {
            //        deviceId = "73f8bb24-f29f-4d8b-a026-7b2f18270fdb", // "906d511e-9da1-4023-8297-4c1c6ecb78d2",
            //        command = cmd,
            //        callbackUrl = "http://120.76.164.210:9999/commandRsp",
            //        expireTime = 60000
            //    }, 1);
            //    var data = rst.Data;
            //    var bll = new CommandInfoBll();

            //    var addRst = bll.Add(new CommandInfo() {
            //        CommandId = data.commandId,
            //        AppId = data.appId,
            //        DeviceId = data.deviceId,
            //        Command = JsonHelper.Instance.Serialize(data.command),
            //        CallbackUrl = data.callbackUrl,
            //        ExpireTime = data.expireTime,
            //        Status = data.status,
            //        Result = JsonHelper.Instance.Serialize(data.result),
            //        CreationTime = FunHelper.UTCToDateTime(data.creationTime),
            //        ExecuteTime = FunHelper.UTCToDateTime(data.executeTime),
            //        PlatformIssUedTime = FunHelper.UTCToDateTime(data.platformIssuedTime),
            //        DeliveredTime = FunHelper.UTCToDateTime(data.deliveredTime),
            //        IssuedTimes = data.issuedTimes,
            //        MaxRetransMit = data.maxRetransmit,
            //    });

            //    Console.WriteLine(addRst.Message);
            //}
        }



    }
}
