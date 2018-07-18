using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NBIOT.Model;
using NBIOT.Common;
using NBIOT.Bll;

namespace NBIOT.Web.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class AdminTokenController : ControllerBase                                                                                                                                         
    {
        [HttpPost]
        [Route("Add")]
        public string Add(AdminToken model)
        {
            var rst = new ReturnResult<bool>();
            var bll = new AdminTokenBll();
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
        public string Del(AdminToken model)
        {
            var rst = new ReturnResult<bool>();
            var bll = new AdminTokenBll();
            rst = bll.Delete(model);
            
            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("GetOne")]
        public string GetOne(AdminToken model)
        {
            var rst = new ReturnResult<AdminToken>();
            var bll = new AdminTokenBll();
            rst = bll.GetOne(model);

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("GetList")]
        public string GetList(AdminToken model)
        {
            var rst = new ReturnResult<List<AdminToken>>();
            var bll = new AdminTokenBll();
            rst = bll.GetList(model);
            
            return JsonHelper.Instance.Serialize(rst);
        }
    }
}