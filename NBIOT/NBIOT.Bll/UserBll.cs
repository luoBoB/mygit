using NBIOT.Dal;
using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NBIOT.Bll
{
    public class UserBll
    {
        #region 基本增删改查
        
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Add(User model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new UserDal())
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
        public ReturnResult<bool> AddList(List<User> list)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new UserDal())
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
        public ReturnResult<bool> Update(User model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new UserDal())
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
        public ReturnResult<bool> Delete(User model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new UserDal())
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
        public ReturnResult<User> GetOne(User model)
        {
            var rst = new ReturnResult<User>();

            using (var dal = new UserDal())
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
        public ReturnResult<List<User>> GetList(User model)
        {
            var rst = new ReturnResult<List<User>>();

            using (var dal = new UserDal())
            {
                rst = dal.GetList(model);
            }

            return rst;
        }

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<User> UserLogin(User model)
        {
            var rst = new ReturnResult<User>();

            using (var dal = new UserDal())
            {
                rst = dal.UserLogin(model);
            }

            return rst;
        }

        #endregion
    }
}