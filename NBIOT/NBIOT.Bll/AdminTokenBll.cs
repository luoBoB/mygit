using NBIOT.Dal;
using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NBIOT.Bll
{
    public class AdminTokenBll
    {
        #region 基本增删改查
        
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Add(AdminToken model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new AdminTokenDal())
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
        public ReturnResult<bool> AddList(List<AdminToken> list)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new AdminTokenDal())
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
        public ReturnResult<bool> Update(AdminToken model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new AdminTokenDal())
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
        public ReturnResult<bool> Delete(AdminToken model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new AdminTokenDal())
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
        public ReturnResult<AdminToken> GetOne(AdminToken model)
        {
            var rst = new ReturnResult<AdminToken>();

            using (var dal = new AdminTokenDal())
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
        public ReturnResult<List<AdminToken>> GetList(AdminToken model)
        {
            var rst = new ReturnResult<List<AdminToken>>();

            using (var dal = new AdminTokenDal())
            {
                rst = dal.GetList(model);
            }

            return rst;
        }

        #endregion

        public ReturnResult<AdminToken> CheckToken(AdminToken model)
        {
            var rst = new ReturnResult<AdminToken>();

            using (var dal = new AdminTokenDal())
            {
                rst = dal.CheckToken(model);
            }

            return rst;
        }
    }
}