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
    public class ProfileController : ControllerBase                                                                                                                                         
    {
        [HttpPost]
        [Route("GetNew")]
        [AdminRequired]
        public string GetNew()
        {
            var rst = new ReturnResult<Profile>
            {
                Result = true,
                Data = new Profile(),
                Message = "获取成功"
            };
            
            return JsonHelper.Instance.Serialize(rst);
        }
    
        [HttpPost]
        [Route("Add")]
        [AdminRequired]
        public string Add(Profile model)
        {
            var rst = new ReturnResult<bool>();
            var bll = new ProfileBll();
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
        public string Del(Profile model)
        {
            var rst = new ReturnResult<bool>();
            var bll = new ProfileBll();
            rst = bll.Delete(model);
            
            return JsonHelper.Instance.Serialize(rst);
        }
        
        [HttpPost]
        [Route("GetOne")]
        [AdminRequired]
        public string GetOne(Profile model)
        {
            var rst = new ReturnResult<Profile>();
            var bll = new ProfileBll();
            rst = bll.GetOne(model);
            
            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("GetList")]
        [AdminRequired]
        public string GetList(Profile model)
        {
            var rst = new ReturnResult<List<Profile>>();
            var bll = new ProfileBll();
            rst = bll.GetList(model);
            
            return JsonHelper.Instance.Serialize(rst);
        }
    }
}