using NBIOT.Dal;
using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NBIOT.Bll
{
    public class ProfileBll
    {
        #region 基本增删改查
        
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Add(Profile model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new ProfileDal())
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
        public ReturnResult<bool> AddList(List<Profile> list)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new ProfileDal())
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
        public ReturnResult<bool> Update(Profile model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new ProfileDal())
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
        public ReturnResult<bool> Delete(Profile model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new ProfileDal())
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
        public ReturnResult<Profile> GetOne(Profile model)
        {
            var rst = new ReturnResult<Profile>();

            using (var dal = new ProfileDal())
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
        public ReturnResult<List<Profile>> GetList(Profile model)
        {
            var rst = new ReturnResult<List<Profile>>();

            using (var dal = new ProfileDal())
            {
                rst = dal.GetList(model);
            }

            return rst;
        }

        #endregion

        public ReturnResult<Profile> GetOneByModel(Profile model)
        {
            var rst = new ReturnResult<Profile>();

            using (var dal = new ProfileDal())
            {
                rst = dal.GetOneByModel(model);
            }

            return rst;
        }
    }
}