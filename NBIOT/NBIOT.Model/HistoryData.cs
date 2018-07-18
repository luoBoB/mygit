using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBIOT.Model
{
    public class HistoryData                                                                                                                                  
    {
        public HistoryData()
        {
            
        }
        
        public int Id { get; set; }
            
        public Nullable<int> AppId { get; set; }
            
        public string DeviceId { get; set; }
            
        public string ServiceId { get; set; }
            
        public string ServiceType { get; set; }
            
        public string Data { get; set; }
            
        public Nullable<DateTime> EventTime { get; set; }
            
        public string EventTimeToString => EventTime?.ToString("yyyy-MM-dd HH:mm:ss");
        
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}