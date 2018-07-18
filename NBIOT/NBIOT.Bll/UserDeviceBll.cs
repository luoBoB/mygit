using NBIOT.Dal;
using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NBIOT.Bll
{
    public class UserDeviceBll
    {
        #region 基本增删改查
        
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Add(UserDevice model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new UserDeviceDal())
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
        public ReturnResult<bool> AddList(List<UserDevice> list)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new UserDeviceDal())
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
        public ReturnResult<bool> Update(UserDevice model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new UserDeviceDal())
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
        public ReturnResult<bool> Delete(UserDevice model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new UserDeviceDal())
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
        public ReturnResult<UserDevice> GetOne(UserDevice model)
        {
            var rst = new ReturnResult<UserDevice>();

            using (var dal = new UserDeviceDal())
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
        public ReturnResult<List<UserDevice>> GetList(UserDevice model)
        {
            var rst = new ReturnResult<List<UserDevice>>();

            using (var dal = new UserDeviceDal())
            {
                rst = dal.GetList(model);
            }

            return rst;
        }

        #endregion

        public ReturnResult<bool> AddOrUpdate(UserDevice model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new UserDeviceDal())
            {
                rst = dal.AddOrUpdate(model);
            }

            return rst;
        }
    }
}