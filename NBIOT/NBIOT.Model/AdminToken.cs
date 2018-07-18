using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBIOT.Model
{
    public class AdminToken                                                                                                                                  
    {
        public AdminToken()
        {
            
        }
        
        public int Id { get; set; }
            
        public Nullable<int> AdminId { get; set; }
            
        public string Token { get; set; }
            
        public Nullable<DateTime> TimeOut { get; set; }
            
        public string TimeOutToString => TimeOut?.ToString("yyyy-MM-dd HH:mm:ss");

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool IsGetNew { get; set; }
    }
}