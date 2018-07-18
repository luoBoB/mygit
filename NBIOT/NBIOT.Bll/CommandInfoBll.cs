using NBIOT.Dal;
using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NBIOT.Bll
{
    public class CommandInfoBll
    {
        #region 基本增删改查
        
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Add(CommandInfo model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new CommandInfoDal())
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
        public ReturnResult<bool> AddList(List<CommandInfo> list)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new CommandInfoDal())
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
        public ReturnResult<bool> Update(CommandInfo model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new CommandInfoDal())
            {
                rst = dal.Update(model);
            }

            return rst;
        }

        public ReturnResult<bool> UpdateStatusAndResult(CommandInfo model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new CommandInfoDal())
            {
                rst = dal.UpdateStatusAndResult(model);
            }

            return rst;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Delete(CommandInfo model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new CommandInfoDal())
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
        public ReturnResult<CommandInfo> GetOne(CommandInfo model)
        {
            var rst = new ReturnResult<CommandInfo>();

            using (var dal = new CommandInfoDal())
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
        public ReturnResult<List<CommandInfo>> GetList(CommandInfo model)
        {
            var rst = new ReturnResult<List<CommandInfo>>();

            using (var dal = new CommandInfoDal())
            {
                rst = dal.GetList(model);
            }

            return rst;
        }

        #endregion
    }
}