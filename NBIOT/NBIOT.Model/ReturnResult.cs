using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBIOT.Model
{
    public class ReturnResult<T>
    {
        public ReturnResult()
        {
            Result = false;
        }
        public bool Result { get; set; }
        /// <summary>
        /// 状态码  Token验证才用到
        /// </summary>
        public string Status { get; set; }

        public T Data { get; set; }

        public string Message { get; set; }

        public PageInfo PageInfo { get; set; }
    }
    public class PageInfo
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount => Math.Max((TotalCount + PageSize - 1) / PageSize, 1);
        public int TotalCount { get; set; }
    }
}
