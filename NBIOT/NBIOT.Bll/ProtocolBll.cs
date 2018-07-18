using NBIOT.Dal;
using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NBIOT.Bll
{
    public class ProtocolBll
    {
        #region 基本增删改查
        
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Add(Protocol model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new ProtocolDal())
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
        public ReturnResult<bool> AddList(List<Protocol> list)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new ProtocolDal())
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
        public ReturnResult<bool> Update(Protocol model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new ProtocolDal())
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
        public ReturnResult<bool> Delete(Protocol model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new ProtocolDal())
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
        public ReturnResult<Protocol> GetOne(Protocol model)
        {
            var rst = new ReturnResult<Protocol>();

            using (var dal = new ProtocolDal())
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
        public ReturnResult<List<Protocol>> GetList(Protocol model)
        {
            var rst = new ReturnResult<List<Protocol>>();

            using (var dal = new ProtocolDal())
            {
                rst = dal.GetList(model);
            }

            return rst;
        }

        #endregion

        public ReturnResult<Protocol> GetOneByDeviceId(string deviceId)
        {
            var rst = new ReturnResult<Protocol>();

            using (var dal = new ProtocolDal())
            {
                rst = dal.GetOneByDeviceId(deviceId);
            }

            return rst;
        }
    }
}