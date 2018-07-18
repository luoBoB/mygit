using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBIOT.Model
{
    public class Apps                                                                                                                                  
    {
        public Apps()
        {
            
        }
        
        public int Id { get; set; }
            
        public string Name { get; set; }
            
        public string AppId { get; set; }
            
        public string Secret { get; set; }
            
        public string AppType { get; set; }
            
        public string AppServer { get; set; }
            
        public string DeviceServer { get; set; }
            
        public Nullable<DateTime> CreateTime { get; set; }
            
        public string CreateTimeToString => CreateTime?.ToString("yyyy-MM-dd HH:mm:ss");
        
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}