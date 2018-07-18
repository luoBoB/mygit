using NBIOT.Dal;
using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NBIOT.Bll
{
    public class AppsBll
    {
        #region 基本增删改查
        
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Add(Apps model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new AppsDal())
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
        public ReturnResult<bool> AddList(List<Apps> list)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new AppsDal())
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
        public ReturnResult<bool> Update(Apps model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new AppsDal())
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
        public ReturnResult<bool> Delete(Apps model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new AppsDal())
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
        public ReturnResult<Apps> GetOne(Apps model)
        {
            var rst = new ReturnResult<Apps>();

            using (var dal = new AppsDal())
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
        public ReturnResult<List<Apps>> GetList(Apps model)
        {
            var rst = new ReturnResult<List<Apps>>();

            using (var dal = new AppsDal())
            {
                rst = dal.GetList(model);
            }

            return rst;
        }

        #endregion
    }
}