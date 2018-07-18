using NBIOT.Dal;
using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NBIOT.Bll
{
    public class SubscriptionBll
    {
        #region 基本增删改查
        
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Add(Subscription model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new SubscriptionDal())
            {
                rst = dal.Add(model);
            }

            return rst;
        }
        
        /// <summary>
        /// 新增多条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> AddList(List<Subscription> list)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new SubscriptionDal())
            {
                rst = dal.AddList(list);
            }

            return rst;
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Update(Subscription model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new SubscriptionDal())
            {
                rst = dal.Update(model);
            }

            return rst;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Delete(Subscription model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new SubscriptionDal())
            {
                rst = dal.Delete(model);
            }

            return rst;
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<Subscription> GetOne(Subscription model)
        {
            var rst = new ReturnResult<Subscription>();

            using (var dal = new SubscriptionDal())
            {
                rst = dal.GetOne(model);
            }

            return rst;
        }

        /// <summary>
        /// 获取数据列表（带分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<List<Subscription>> GetList(Subscription model)
        {
            var rst = new ReturnResult<List<Subscription>>();

            using (var dal = new SubscriptionDal())
            {
                rst = dal.GetList(model);
            }

            return rst;
        }

        #endregion
    }
}