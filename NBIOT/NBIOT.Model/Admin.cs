using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBIOT.Model
{
    public class Admin                                                                                                                                  
    {
        public Admin()
        {
            
        }
        
        public int Id { get; set; }
            
        public string UserName { get; set; }
            
        public string Password { get; set; }
            
        public Nullable<DateTime> LastLoginTime { get; set; }
            
        public string LastLoginTimeToString => LastLoginTime?.ToString("yyyy-MM-dd HH:mm:ss");
        
        public Nullable<DateTime> CreateTime { get; set; }
            
        public string CreateTimeToString => CreateTime?.ToString("yyyy-MM-dd HH:mm:ss");
        
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Token { get; set; }
    }
}