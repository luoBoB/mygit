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
    public class UserDeviceController : ControllerBase                                                                                                                                         
    {
        [HttpPost]
        [Route("GetNew")]
        [AdminRequired]
        public string GetNew()
        {
            var rst = new ReturnResult<UserDevice>
            {
                Result = true,
                Data = new UserDevice(),
                Message = "获取成功"
            };
            
            return JsonHelper.Instance.Serialize(rst);
        }
    
        [HttpPost]
        [Route("Add")]
        [AdminRequired]
        public string Add(UserDevice model)
        {
            var rst = new ReturnResult<bool>();
            var bll = new UserDeviceBll();
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
        public string Del(UserDevice model)
        {
            var rst = new ReturnResult<bool>();
            var bll = new UserDeviceBll();
            rst = bll.Delete(model);
            
            return JsonHelper.Instance.Serialize(rst);
        }
        
        [HttpPost]
        [Route("GetOne")]
        [AdminRequired]
        public string GetOne(UserDevice model)
        {
            var rst = new ReturnResult<UserDevice>();
            var bll = new UserDeviceBll();
            rst = bll.GetOne(model);
            
            return JsonHelper.Instance.Serialize(rst);
        }
        
        [HttpPost]
        [Route("GetList")]
        [AdminRequired]
        public string GetList(UserDevice model)
        {
            var rst = new ReturnResult<List<UserDevice>>();
            var bll = new UserDeviceBll();
            rst = bll.GetList(model);
            
            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpGet]
        [HttpPost]
        [Route("GetListByUserId")]
        [AdminRequired]
        public string GetListByUserId(UserDevice model)
        {
            var rst = new ReturnResult<List<UserDevice>>();
            var bll = new UserDeviceBll();
            rst = bll.GetList(model);

            return JsonHelper.Instance.Serialize(rst);
        }
    }
}