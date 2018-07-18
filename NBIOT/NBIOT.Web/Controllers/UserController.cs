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
using System.IO;
using System.Text;

namespace NBIOT.Web.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("GetNew")]
        [AdminRequired]
        public string GetNew()
        {
            var rst = new ReturnResult<User>
            {
                Result = true,
                Data = new User(),
                Message = "获取成功"
            };

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("Add")]
        [AdminRequired]
        public string Add(User model)
        {
            var rst = new ReturnResult<bool>();
            var bll = new UserBll();
            if (model.Id == 0)
            {
                model.CreateTime = DateTime.Now;
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
        public string Del(User model)
        {
            var rst = new ReturnResult<bool>();
            var bll = new UserBll();
            rst = bll.Delete(model);

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("GetOne")]
        [AdminRequired]
        public string GetOne(User model)
        {
            var rst = new ReturnResult<User>();
            var bll = new UserBll();
            rst = bll.GetOne(model);

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("GetList")]
        [AdminRequired]
        public string GetList(User model)
        {
            var rst = new ReturnResult<List<User>>();
            var bll = new UserBll();
            rst = bll.GetList(model);

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpGet]
        [HttpPost]
        [Route("UserLogin")]
        public string UserLogin(User model)
        {
            var rst = new ReturnResult<User>();
            var bll = new UserBll();
            rst = bll.UserLogin(model);

            return JsonHelper.Instance.Serialize(rst);
        }

    }
}