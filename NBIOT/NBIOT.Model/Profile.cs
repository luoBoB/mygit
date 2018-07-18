using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBIOT.Model
{
    public class Profile                                                                                                                                  
    {
        public Profile()
        {
            
        }
        
        public int Id { get; set; }
            
        public string ManufacturerId { get; set; }
            
        public string ManufacturerName { get; set; }
            
        public string DeviceType { get; set; }
            
        public string Model { get; set; }
            
        public string ProtocolType { get; set; }
        public Nullable<int> AppId { get; set; }
        public string AppName { get; set; }
        public Nullable<int> ProtocolId { get; set; }
        public string ProtocolName { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }


    }
}