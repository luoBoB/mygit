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
    public class AppsController : ControllerBase
    {
        [HttpPost]
        [Route("GetNew")]
        [AdminRequired]
        public string GetNew()
        {
            var rst = new ReturnResult<Apps>
            {
                Data = new Apps(),
                Message = "获取成功"
            };

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("Add")]
        [AdminRequired]
        public string Add(Apps model)
        {
            var rst = new ReturnResult<bool>();
            var bll = new AppsBll();
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
        public string Del(Apps model)
        {
            var rst = new ReturnResult<bool>();
            var bll = new AppsBll();
            rst = bll.Delete(model);

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("GetOne")]
        [AdminRequired]
        public string GetOne(Apps model)
        {
            var rst = new ReturnResult<Apps>();
            var bll = new AppsBll();
            rst = bll.GetOne(model);

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("GetList")]
        [AdminRequired]
        public string GetList(Apps model)
        {
            var rst = new ReturnResult<List<Apps>>();
            var bll = new AppsBll();
            rst = bll.GetList(model);

            return JsonHelper.Instance.Serialize(rst);
        }
    }
}