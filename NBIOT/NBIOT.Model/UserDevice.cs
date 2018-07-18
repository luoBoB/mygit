using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBIOT.Model
{
    public class UserDevice                                                                                                                                  
    {
        public UserDevice()
        {
            
        }
        
        public int Id { get; set; }
            
        public string DeviceId { get; set; }
            
        public string DeviceName { get; set; }
            
        public string VerifyCode { get; set; }
            
        public string NodeId { get; set; }
            
        public Nullable<int> ProfileId { get; set; }
            
        public Nullable<int> UserId { get; set; }
            
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public string ManufacturerId { get; set; }

        public string ManufacturerName { get; set; }

        public string DeviceType { get; set; }

        public string Model { get; set; }

        public string ProtocolType { get; set; }

        public string Phone { get; set; }
    }
}