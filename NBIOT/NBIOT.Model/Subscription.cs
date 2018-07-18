using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBIOT.Model
{
    public class Subscription                                                                                                                                  
    {
        public Subscription()
        {
            
        }
        public int Id { get; set; }
            
        public string SubscriptionId { get; set; }
            
        public string NotifyType { get; set; }
            
        public string CallbackUrl { get; set; }
        public Nullable<int> AppId { get; set; }

        public string AppServer { get; set; }
        public string AppType { get; set; }
        public string DeviceServer { get; set; }
        public string AppName { get; set; }
        public string Secret { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }


    }
}