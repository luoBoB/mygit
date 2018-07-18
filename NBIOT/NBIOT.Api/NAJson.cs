using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBIOT.Api
{

    public class ApiResult
    {
        public int statusCode;
        public string result;
        public string errcode;
        public string memo;
    }

    /// <summary>
    /// 设备profile：data1 int   data2 string(8,32)  ret1 int ret2 string(8,32) para1 para2 para12 para22  
    /// 0xf0上报0xff命令相应0x91 cmd1 0x92 cmd2
    /// </summary>
    public class TokenResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string accessToken { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tokenType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string refreshToken { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int expiresIn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scope { get; set; }
    }

    public class RegDeviceRequest
    {
        public int? userId { get; set; }
        public int appId { get; set; }
        public string name { get; set; }
        public string model { get; set; }
        public string endUserId { get; set; }
        public string psk { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string verifyCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string nodeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int timeout { get; set; } = 0;
    }

    public class RegDeviceResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string deviceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string verifyCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int timeout { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string psk { get; set; }
    }


    public class ModDeviceRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string endUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string manufacturerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string model { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string protocolType { get; set; }
    }





    public class DeviceDataHistoryDTOsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string deviceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gatewayId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string appId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string serviceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> data { get; set; }
        public string DataString
        {
            get
            {
                string result = "";
                foreach (KeyValuePair<string, string> item in data)
                {
                    result += item.Key + ":" + item.Value + "\r\n";
                }
                return result;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string timestamp { get; set; }
    }

    public class HistoryDataResult
    {
        /// <summary>
        /// 
        /// </summary>
        public int totalCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DeviceDataHistoryDTOsItem> deviceDataHistoryDTOs { get; set; }
    }


    public class CommandPara
    {
        public string paraName { get; set; }
        public string paraValue { get; set; }
        public bool isNum { get; set; }
    }
    public class CommandDetail
    {
        /// <summary>
        /// 
        /// </summary>
        public string serviceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string method { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object paras { get; set; }
    }

    public class SendCommandRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public string deviceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CommandDetail command { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string callbackUrl { get; set; }
        public int expireTime { get; set; }
        public int maxRetransmit { get; set; }
    }

    public class CommandResult<T>
    {
        public string deviceId { get; set; }
        public string commandId { get; set; }
        public CommandResultDetail<T> result { get; set; }

    }

    public class CommandResultDetail<T>
    {
        public string resultCode { get; set; }
        public T resultDetail { get; set; }
    }

    #region 命令下发
    public class Pagination
    {
        //页码
        public long pageNo { get; set; }
        //每页信息数量
        public long pageSize { get; set; }
        //记录总数
        public long totalSize { get; set; }
    }
    public class DeviceCommandResp
    {
        /// <summary>
        /// 设备命令 ID。
        /// </summary>
        public string commandId { get; set; }
        public string appId { get; set; }
        /// <summary>
        /// 下发命令的设备 ID，用于唯一标识一个设备。
        /// </summary>
        public string deviceId { get; set; }
        /// <summary>
        /// 下发命令的信息
        /// </summary>
        public CommandDetail command { get; set; }
        public string callbackUrl { get; set; }
        public int expireTime { get; set; }
        /// <summary>
        /// SUCCESSFUL 表示命令已经成功执行; FAILED 表示命令执行失败
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 下发命令执行的详细结果。
        /// </summary>
        public Object result { get; set; }
        /// <summary>
        /// 命令的创建时间。
        /// </summary>
        public string creationTime { get; set; }
        /// <summary>
        /// 命令执行的时间。
        /// </summary>
        public string executeTime { get; set; }
        /// <summary>
        /// 平台发送命令的时间。
        /// </summary>
        public string platformIssuedTime { get; set; }
        /// <summary>
        /// 平台将命令送达到设备的时间。
        /// </summary>
        public string deliveredTime { get; set; }
        /// <summary>
        /// 平台发送命令的次数。
        /// </summary>
        public int issuedTimes { get; set; }
        /// <summary>
        /// 命令下发最大重传次数。
        /// </summary>
        public int maxRetransmit { get; set; }
    }
    //查询设备命令的响应参数
    public class SelectDeviceResult : ApiResult
    {
        public Pagination pagination { get; set; }

        public DeviceCommandResp data { get; set; }
    }

    //创建设备命令撤销任务的响应参数
    public class CreateDevCommCancel : ApiResult
    {
        public string taskId { get; set; }
        public string appId { get; set; }
        public string deviceId { get; set; }
        public string status { get; set; }
        public int totalCount { get; set; }
        public List<DeviceCommandResp> deviceCommands { get; set; }

    }

    //查询设备命令撤销任务的响应参数
    public class SelectDevCommCancel : ApiResult
    {
        public Pagination pagination { get; set; }
        public List<DeviceCommandCancelTaskResp> data { get; set; }
    }
    public class DeviceCommandCancelTaskResp
    {
        public string taskId { get; set; }
        public string appId { get; set; }
        public string deviceId { get; set; }
        public string status { get; set; }
        /// <summary>
        /// 下发命令执行的详细结果。
        /// </summary>
        public Object result { get; set; }
        /// <summary>
        /// 命令的创建时间。
        /// </summary>
        public string creationTime { get; set; }
        /// <summary>
        /// 命令执行的时间。
        /// </summary>
        public string executeTime { get; set; }
        /// <summary>
        /// 平台发送命令的时间。
        /// </summary>
        public string platformIssuedTime { get; set; }
        /// <summary>
        /// 平台将命令送达到设备的时间。
        /// </summary>
        public string deliveredTime { get; set; }
        /// <summary>
        /// 平台发送命令的次数。
        /// </summary>
        public int issuedTimes { get; set; }
        /// <summary>
        /// 命令下发最大重传次数。
        /// </summary>
        public int maxRetransmit { get; set; }
    }


    #endregion


    /// <summary>
    /// Subscribe to the platform's business data
    /// autor:zjd
    /// date:2018/7/4 11:04
    /// </summary>
    public class SubScriptionBusinessRequest
    {

        /// <summary>
        /// 
        /// </summary>
        public string subscriptionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string notifyType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string callbackUrl { get; set; }
    }


}
