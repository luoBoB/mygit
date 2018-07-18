using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NBIOT.Model;
using NBIOT.Common;
using NBIOT.Bll;
using NBIOT.Web.Filters;

namespace NBIOT.Web.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class HistoryDataController : ControllerBase                                                                                                                                         
    {
        [HttpPost]
        [Route("GetNew")]
        [AdminRequired]
        public string GetNew()
        {
            var rst = new ReturnResult<HistoryData>
            {
                Data = new HistoryData(),
                Message = "获取成功"
            };
            
            return JsonHelper.Instance.Serialize(rst);
        }
    
        [HttpPost]
        [Route("Add")]
        [AdminRequired]
        public string Add(HistoryData model)
        {
            var rst = new ReturnResult<bool>();
            var bll = new HistoryDataBll();
            if (model.Id == 0)
            {
                rst = bll.Add(model);
            }
            else
            {
                rst = bll.Update(model);
            }
            
            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("Del")]
        [AdminRequired]
        public string Del(HistoryData model)
        {
            var rst = new ReturnResult<bool>();
            var bll = new HistoryDataBll();
            rst = bll.Delete(model);
            
            return JsonHelper.Instance.Serialize(rst);
        }
        
        [HttpPost]
        [Route("GetOne")]
        [AdminRequired]
        public string GetOne(HistoryData model)
        {
            var rst = new ReturnResult<HistoryData>();
            var bll = new HistoryDataBll();
            rst = bll.GetOne(model);
            
            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("GetList")]
        [AdminRequired]
        public string GetList(HistoryData model)
        {
            var rst = new ReturnResult<List<HistoryData>>();
            var bll = new HistoryDataBll();
            rst = bll.GetList(model);
            
            return JsonHelper.Instance.Serialize(rst);
        }
    }
}