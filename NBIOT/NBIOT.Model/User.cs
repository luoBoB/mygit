using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBIOT.Model
{
    public class User                                                                                                                                  
    {
        public User()
        {
            
        }
        
        public int Id { get; set; }
            
        public string Phone { get; set; }
            
        public string Password { get; set; }

        public Nullable<int> AppId { get; set; }

        public Nullable<DateTime> CreateTime { get; set; }
            
        public string CreateTimeToString => CreateTime?.ToString("yyyy-MM-dd HH:mm:ss");

        public string AppName { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}