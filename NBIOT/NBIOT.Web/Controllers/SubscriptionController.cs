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
using NBIOT.Api;

namespace NBIOT.Web.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        [HttpPost]
        [Route("GetNew")]
        [AdminRequired]
        public string GetNew()
        {
            var rst = new ReturnResult<Subscription>
            {
                Result = true,
                Data = new Subscription(),
                Message = "获取成功"
            };

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("AddSubscription")]
        [AdminRequired]
        public string AddSubscription(Subscription model)
        {
            var rst = new ReturnResult<bool>();
            var bll = new SubscriptionBll();
            if (model.Id == 0)
            {
                var api = new SubscriptionApi();
                var apiRst = api.SubscriptionBusinessData((int)model.AppId, model.NotifyType, model.CallbackUrl);
                if (apiRst.Result)
                {
                    model.SubscriptionId = apiRst.Data?.subscriptionId;
                    model.NotifyType = apiRst.Data?.notifyType;
                    model.CallbackUrl = apiRst.Data?.callbackUrl;
                    rst = bll.Add(model);
                }
                else
                {
                    rst.Message = apiRst.Message;
                }
            }
            else
            {
                rst = bll.Update(model);
            }

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("DelSubscription")]
        [AdminRequired]
        public string DelSubscription(Subscription model)
        {
            var rst = new ReturnResult<bool>();
            var api = new SubscriptionApi();
            var delRst = api.DelSubscription(model.SubscriptionId, 1);
            if (delRst.Status == "NoContent")
            {
                rst.Result = true;
                rst.Message = "删除成功";

                var bll = new SubscriptionBll();
                var del = bll.Delete(model);
            }
            else
            {
                rst.Message = "删除失败";
            }

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("GetOne")]
        [AdminRequired]
        public string GetOne(Subscription model)
        {
            var rst = new ReturnResult<Subscription>();
            var bll = new SubscriptionBll();
            rst = bll.GetOne(model);

            return JsonHelper.Instance.Serialize(rst);
        }

        [HttpPost]
        [Route("GetList")]
        [AdminRequired]
        public string GetList(Subscription model)
        {
            var rst = new ReturnResult<List<Subscription>>();
            var bll = new SubscriptionBll();
            rst = bll.GetList(model);

            return JsonHelper.Instance.Serialize(rst);
        }
    }
}