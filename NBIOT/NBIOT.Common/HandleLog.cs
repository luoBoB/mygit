using System;
using System.Collections.Generic;
using System.Text;

namespace NBIOT.Common
{
    public class HandleLog
    {
        public delegate void HandleLogInfo(string log);

        public static event HandleLogInfo WriteServerLog;
        public static void WriteLog(string log)
        {
            if (WriteServerLog != null)
            {
                WriteServerLog.Invoke(log);//同步打印服务器运行日志
            }
        }
    }
}
