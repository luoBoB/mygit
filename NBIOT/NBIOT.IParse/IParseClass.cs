using System;
using System.Collections.Generic;
using System.Text;

namespace NBIOT.IParse
{
    public interface IParseClass
    {
        bool DeviceDataChanged(object data);
        bool CommandRsp(object data);
        object GetCommandPara(string serviceId, string method, Dictionary<string, object> data);
    }
}
