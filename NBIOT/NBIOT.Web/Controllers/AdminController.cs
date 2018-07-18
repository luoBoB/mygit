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
    public class AdminController : ControllerBase
    {
        [HttpPost]
        [Route("GetNew")]
        [AdminRequired]
        public string GetNew()
        {
            var rst = new ReturnResult<Admin>
            {
                Result = true,
                Data = new Admin(),
                Message = "获取成功"
            };

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("Add")]
        [AdminRequired]
        public string Add(Admin model)
        {
            var rst = new ReturnResult<bool>();
            var bll = new AdminBll();
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
        public string Del(Admin model)
        {
            var rst = new ReturnResult<bool>();
            var bll = new AdminBll();
            rst = bll.Delete(model);

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("GetOne")]
        [AdminRequired]
        public string GetOne(Admin model)
        {
            var rst = new ReturnResult<Admin>();
            var bll = new AdminBll();
            rst = bll.GetOne(model);

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("GetList")]
        [AdminRequired]
        public string GetList(Admin model)
        {
            var rst = new ReturnResult<List<Admin>>();
            var bll = new AdminBll();
            rst = bll.GetList(model);

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("Login")]
        public string Login(Admin model)
        {
            var rst = new ReturnResult<Admin>();
            var bll = new AdminBll();
            rst = bll.Login(model);

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("GetUserInfo")]
        [AdminRequired]
        public string GetUserInfo(string token)
        {
            var rst = new ReturnResult<Admin>();
            var bll = new AdminBll();
            rst = bll.GetOne(new Admin() { Token = token });

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("Logout")]
        public string Logout(string token)
        {
            var rst = new ReturnResult<bool>();
            var bll = new AdminBll();
            rst = bll.Delete(new Admin() { Token = token });

            return JsonHelper.Instance.Serialize(rst);
        }
    }
}