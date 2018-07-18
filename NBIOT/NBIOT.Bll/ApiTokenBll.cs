using NBIOT.Dal;
using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NBIOT.Bll
{
    public class ApiTokenBll
    {
        #region 基本增删改查
        
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Add(ApiToken model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new ApiTokenDal())
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
        public ReturnResult<bool> AddList(List<ApiToken> list)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new ApiTokenDal())
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
        public ReturnResult<bool> Update(ApiToken model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new ApiTokenDal())
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
        public ReturnResult<bool> Delete(ApiToken model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new ApiTokenDal())
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
        public ReturnResult<ApiToken> GetOne(ApiToken model)
        {
            var rst = new ReturnResult<ApiToken>();

            using (var dal = new ApiTokenDal())
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
        public ReturnResult<List<ApiToken>> GetList(ApiToken model)
        {
            var rst = new ReturnResult<List<ApiToken>>();

            using (var dal = new ApiTokenDal())
            {
                rst = dal.GetList(model);
            }

            return rst;
        }

        #endregion
    }
}