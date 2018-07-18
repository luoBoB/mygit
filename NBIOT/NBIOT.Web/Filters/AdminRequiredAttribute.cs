using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using NBIOT.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NBIOT.Common;
using Microsoft.AspNetCore.Mvc;

namespace NBIOT.Web.Filters
{
    public class AdminRequiredAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor is ControllerActionDescriptor action)
            {
                var token = filterContext.HttpContext.Request.Headers["X-Token"];
                if(token != "App")
                {
                    AdminTokenBll bll = new AdminTokenBll();
                    var checkRst = bll.CheckToken(new Model.AdminToken() { Token = token });
                    if (!checkRst.Result)
                    {
                        filterContext.Result = new JsonResult(checkRst);
                    }
                }
                base.OnActionExecuting(filterContext);
            }
        }
    }
}
