using NBIOT.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBIOT.Model
{
    public class CommandInfo                                                                                                                                  
    {
        public CommandInfo()
        {
            
        }
        
        public int Id { get; set; }
            
        public string CommandId { get; set; }
            
        public string AppId { get; set; }
            
        public string DeviceId { get; set; }
            
        public string Command { get; set; }
            
        public string CallbackUrl { get; set; }
            
        public Nullable<int> ExpireTime { get; set; }
            
        public string Status { get; set; }
            
        public string Result { get; set; }
            
        public Nullable<DateTime> CreationTime { get; set; }
            
        public string CreationTimeToString => CreationTime?.ToString("yyyy-MM-dd HH:mm:ss");
        
        public Nullable<DateTime> ExecuteTime { get; set; }
            
        public string ExecuteTimeToString => ExecuteTime?.ToString("yyyy-MM-dd HH:mm:ss");
        
        public Nullable<DateTime> PlatformIssUedTime { get; set; }
            
        public string PlatformIssUedTimeToString => PlatformIssUedTime?.ToString("yyyy-MM-dd HH:mm:ss");
        
        public Nullable<DateTime> DeliveredTime { get; set; }
            
        public string DeliveredTimeToString => DeliveredTime?.ToString("yyyy-MM-dd HH:mm:ss");
        
        public Nullable<int> IssuedTimes { get; set; }
            
        public Nullable<int> MaxRetransMit { get; set; }

        public string DeviceName { get; set; }

        public string AppName { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string StatusName => FunHelper.GetStatus(Status);

    }
}