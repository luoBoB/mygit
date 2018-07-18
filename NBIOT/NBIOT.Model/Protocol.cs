using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBIOT.Model
{
    public class Protocol                                                                                                                                  
    {
        public Protocol()
        {
            
        }
        
        public int Id { get; set; }
            
        public string Name { get; set; }
            
        public string DllName { get; set; }
            
        public string DllPath { get; set; }
            
        public Nullable<DateTime> CreateTime { get; set; }
            
        public string CreateTimeToString => CreateTime?.ToString("yyyy-MM-dd HH:mm:ss");
        
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string FullDllPath => AppDomain.CurrentDomain.BaseDirectory + DllPath;

    }
}