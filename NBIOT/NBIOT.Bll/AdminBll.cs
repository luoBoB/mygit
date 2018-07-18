using NBIOT.Dal;
using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NBIOT.Bll
{
    public class AdminBll
    {
        #region 基本增删改查
        
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Add(Admin model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new AdminDal())
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
        public ReturnResult<bool> AddList(List<Admin> list)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new AdminDal())
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
        public ReturnResult<bool> Update(Admin model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new AdminDal())
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
        public ReturnResult<bool> Delete(Admin model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new AdminDal())
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
        public ReturnResult<Admin> GetOne(Admin model)
        {
            var rst = new ReturnResult<Admin>();

            using (var dal = new AdminDal())
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
        public ReturnResult<List<Admin>> GetList(Admin model)
        {
            var rst = new ReturnResult<List<Admin>>();

            using (var dal = new AdminDal())
            {
                rst = dal.GetList(model);
            }

            return rst;
        }

        #endregion

        public ReturnResult<Admin> Login(Admin model)
        {
            var rst = new ReturnResult<Admin>();

            using (var dal = new AdminDal())
            {
                rst = dal.Login(model);
            }

            return rst;
        }
    }
}