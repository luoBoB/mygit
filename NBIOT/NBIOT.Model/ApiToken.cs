using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBIOT.Model
{
    public class ApiToken                                                                                                                                  
    {
        public ApiToken()
        {
            
        }
        
        public int Id { get; set; }
            
        public Nullable<int> AppId { get; set; }
            
        public string AccessToken { get; set; }
            
        public string RefreshToken { get; set; }
            
        public Nullable<int> AccessTokenExpiresIn { get; set; }
            
        public Nullable<DateTime> AccessTokenCreateTime { get; set; }
            
        public string AccessTokenCreateTimeToString => AccessTokenCreateTime?.ToString("yyyy-MM-dd HH:mm:ss");
        
        public Nullable<DateTime> RefreshTokenExpiresTime { get; set; }
            
        public string RefreshTokenExpiresTimeToString => RefreshTokenExpiresTime?.ToString("yyyy-MM-dd HH:mm:ss");
        
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}