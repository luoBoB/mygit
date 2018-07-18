using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;

namespace NBIOT.Dal
{
    public class SubscriptionDal : BaseDal
    {
        #region 基本增删改查
        
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Add(Subscription model)
        {
            var rst = new ReturnResult<bool>();

            try
            {
            
                //判断重复
                var checkQuery = Check(model);
                if (checkQuery.Result)
                {
                    rst.Message = "已存在";
                    return rst;
                }
            
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into subscription(");
                strSql.Append("SubscriptionId,NotifyType,CallbackUrl,AppId) ");
                strSql.Append("values (");
                strSql.Append("@SubscriptionId,@NotifyType,@CallbackUrl,@AppId)");
                conn.Execute(strSql.ToString(), model);

                rst.Result = true;
                rst.Message = "添加成功";
            }
            catch (Exception ex)
            {
                rst.Message = "添加失败：" + ex.Message;
                Log4netUtil.Error("SubscriptionDal/Add", ex);
            }

            return rst;
        }
        
        /// <summary>
        /// 新增多条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> AddList(List<Subscription> list)
        {
            var rst = new ReturnResult<bool>();
            var tran = conn.BeginTransaction();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into subscription(");
                strSql.Append("SubscriptionId,NotifyType,CallbackUrl,AppId) ");
                strSql.Append("values (");
                strSql.Append("@SubscriptionId,@NotifyType,@CallbackUrl,@AppId)");
                conn.Execute(strSql.ToString(), list, tran);
                tran.Commit();

                rst.Result = true;
                rst.Message = "批量添加成功";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                rst.Message = "批量添加失败：" + ex.Message;
                Log4netUtil.Error("SubscriptionDal/AddList", ex);
            }

            return rst;
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Update(Subscription model)
        {
            var rst = new ReturnResult<bool>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update subscription set ");
                strSql.Append("SubscriptionId=@SubscriptionId,");
                strSql.Append("NotifyType=@NotifyType,");
                strSql.Append("CallbackUrl=@CallbackUrl,");
                strSql.Append("AppId=@AppId ");
                strSql.Append("where Id=@Id");
                int count = conn.Execute(strSql.ToString(), model);

                if(count > 0)
                {
                    rst.Result = true;
                    rst.Message = "更新成功";
                }
                else
                {
                    rst.Message = "更新失败";
                }
            }
            catch (Exception ex)
            {
                rst.Message = "更新失败：" + ex.Message;
                Log4netUtil.Error("SubscriptionDal/Update", ex);
            }

            return rst;
        }
        
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Delete(Subscription model)
        {
            var rst = new ReturnResult<bool>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from subscription ");
                strSql.Append("where Id=@Id");
                int count = conn.Execute(strSql.ToString(), model);

                if (count > 0)
                {
                    rst.Result = true;
                    rst.Message = "删除成功";
                }
                else
                {
                    rst.Message = "删除失败";
                }
            }
            catch (Exception ex)
            {
                rst.Message = "删除失败：" + ex.Message;
                Log4netUtil.Error("SubscriptionDal/Delete", ex);
            }

            return rst;
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<Subscription> GetOne(Subscription model)
        {
            var rst = new ReturnResult<Subscription>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"SELECT sub.Id,sub.SubscriptionId,sub.NotifyType,sub.CallbackUrl,sub.AppId,
                                ap.AppServer,ap.AppType,ap.DeviceServer,ap.`Name` AS AppName,ap.Secret 
                                FROM subscription AS sub
                                LEFT JOIN apps as ap ON sub.AppId = ap.Id ");
                strSql.Append("where Id=@Id");
                var query = conn.Query<Subscription>(strSql.ToString(), model).FirstOrDefault();
                if (query == null)
                {
                    rst.Message = "数据不存在";
                }
                else
                {
                    rst.Result = true;
                    rst.Data = query;
                    rst.Message = "查询成功";
                }
            }
            catch (Exception ex)
            {
                rst.Message = "查询失败：" + ex.Message;
                Log4netUtil.Error("SubscriptionDal/GetOne", ex);
            }

            return rst;
        }
         /// <summary>
        /// 添加时判断关键内容是否已存在
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<Subscription> Check(Subscription model)
        {
            var rst = new ReturnResult<Subscription>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"SELECT sub.Id,sub.SubscriptionId,sub.NotifyType,sub.CallbackUrl,sub.AppId,
                                ap.AppServer,ap.AppType,ap.DeviceServer,ap.`Name` AS AppName,ap.Secret 
                                FROM subscription AS sub
                                LEFT JOIN apps as ap ON sub.AppId = ap.Id ");
                strSql.Append("where CallbackUrl=@CallbackUrl");
                var query = conn.Query<Subscription>(strSql.ToString(), model).FirstOrDefault();
                if (query == null)
                {
                    rst.Message = "数据不存在";
                }
                else
                {
                    rst.Result = true;
                    rst.Data = query;
                    rst.Message = "查询成功";
                }
            }
            catch (Exception ex)
            {
                rst.Message = "查询失败：" + ex.Message;
                Log4netUtil.Error("SubscriptionDal/Check", ex);
            }

            return rst;
        }

        /// <summary>
        /// 获取数据列表（带分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<List<Subscription>> GetList(Subscription model)
        {
            var rst = new ReturnResult<List<Subscription>>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"SELECT sub.Id,sub.SubscriptionId,sub.NotifyType,sub.CallbackUrl,sub.AppId,
                                ap.AppServer,ap.AppType,ap.DeviceServer,ap.`Name` AS AppName,ap.Secret 
                                FROM subscription AS sub
                                LEFT JOIN apps as ap ON sub.AppId = ap.Id ");
                strSql.Append("where 1=1 ");
                
                if (model != null)
                {
                    if (model.Id != 0)
                    {
                        strSql.Append("and Id = @Id ");
                    }
                }
                var query = conn.Query<Subscription>(strSql.ToString(), model).ToList();

                if (model.PageIndex > 0)
                {
                    var pageList = query.OrderBy(c => c.Id).Skip(model.PageSize * (model.PageIndex - 1)).Take(model.PageSize).ToList();

                    rst.Data = pageList;
                    rst.PageInfo = new PageInfo()
                    {
                        PageIndex = model.PageIndex,
                        PageSize = model.PageSize,
                        TotalCount = query.Count
                    };
                }
                else
                {
                    rst.Data = query;
                }
                rst.Result = true;
                rst.Message = "查询成功";
            }
            catch (Exception ex)
            {
                rst.Message = "查询失败：" + ex.Message;
                Log4netUtil.Error("SubscriptionDal/GetList", ex);
            }

            return rst;
        }
        
        #endregion
    }
}