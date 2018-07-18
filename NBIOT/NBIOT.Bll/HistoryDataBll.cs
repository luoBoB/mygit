using NBIOT.Dal;
using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NBIOT.Bll
{
    public class HistoryDataBll
    {
        #region 基本增删改查
        
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Add(HistoryData model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new HistoryDataDal())
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
        public ReturnResult<bool> AddList(List<HistoryData> list)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new HistoryDataDal())
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
        public ReturnResult<bool> Update(HistoryData model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new HistoryDataDal())
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
        public ReturnResult<bool> Delete(HistoryData model)
        {
            var rst = new ReturnResult<bool>();

            using (var dal = new HistoryDataDal())
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
        public ReturnResult<HistoryData> GetOne(HistoryData model)
        {
            var rst = new ReturnResult<HistoryData>();

            using (var dal = new HistoryDataDal())
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
        public ReturnResult<List<HistoryData>> GetList(HistoryData model)
        {
            var rst = new ReturnResult<List<HistoryData>>();

            using (var dal = new HistoryDataDal())
            {
                rst = dal.GetList(model);
            }

            return rst;
        }

        #endregion
    }
}