using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;

namespace NBIOT.Dal
{
    public class CommandInfoDal : BaseDal
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into command_info(");
                strSql.Append("CommandId,AppId,DeviceId,Command,CallbackUrl,ExpireTime,Status,Result,CreationTime,ExecuteTime,PlatformIssUedTime,DeliveredTime,IssuedTimes,MaxRetransMit) ");
                strSql.Append("values (");
                strSql.Append("@CommandId,@AppId,@DeviceId,@Command,@CallbackUrl,@ExpireTime,@Status,@Result,@CreationTime,@ExecuteTime,@PlatformIssUedTime,@DeliveredTime,@IssuedTimes,@MaxRetransMit)");
                conn.Execute(strSql.ToString(), model);

                rst.Result = true;
                rst.Message = "添加成功";
            }
            catch (Exception ex)
            {
                rst.Message = "添加失败：" + ex.Message;
                Log4netUtil.Error("CommandInfoDal/Add", ex);
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
            var tran = conn.BeginTransaction();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into command_info(");
                strSql.Append("CommandId,AppId,DeviceId,Command,CallbackUrl,ExpireTime,Status,Result,CreationTime,ExecuteTime,PlatformIssUedTime,DeliveredTime,IssuedTimes,MaxRetransMit) ");
                strSql.Append("values (");
                strSql.Append("@CommandId,@AppId,@DeviceId,@Command,@CallbackUrl,@ExpireTime,@Status,@Result,@CreationTime,@ExecuteTime,@PlatformIssUedTime,@DeliveredTime,@IssuedTimes,@MaxRetransMit)");
                conn.Execute(strSql.ToString(), list, tran);
                tran.Commit();

                rst.Result = true;
                rst.Message = "批量添加成功";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                rst.Message = "批量添加失败：" + ex.Message;
                Log4netUtil.Error("CommandInfoDal/AddList", ex);
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update command_info set ");
                strSql.Append("CommandId=@CommandId,");
                strSql.Append("AppId=@AppId,");
                strSql.Append("DeviceId=@DeviceId,");
                strSql.Append("Command=@Command,");
                strSql.Append("CallbackUrl=@CallbackUrl,");
                strSql.Append("ExpireTime=@ExpireTime,");
                strSql.Append("Status=@Status,");
                strSql.Append("Result=@Result,");
                strSql.Append("CreationTime=@CreationTime,");
                strSql.Append("ExecuteTime=@ExecuteTime,");
                strSql.Append("PlatformIssUedTime=@PlatformIssUedTime,");
                strSql.Append("DeliveredTime=@DeliveredTime,");
                strSql.Append("IssuedTimes=@IssuedTimes,");
                strSql.Append("MaxRetransMit=@MaxRetransMit ");
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
                Log4netUtil.Error("CommandInfoDal/Update", ex);
            }

            return rst;
        }

        public ReturnResult<bool> UpdateStatusAndResult(CommandInfo model)
        {
            var rst = new ReturnResult<bool>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update command_info set ");
                strSql.Append("Status=@Status,");
                strSql.Append("Result=@Result,");
                strSql.Append("ExecuteTime=@ExecuteTime,");
                strSql.Append("PlatformIssUedTime=@PlatformIssUedTime,");
                strSql.Append("DeliveredTime=@DeliveredTime ");
                strSql.Append("where CommandId=@CommandId");
                int count = conn.Execute(strSql.ToString(), model);

                if (count > 0)
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
                Log4netUtil.Error("CommandInfoDal/Update", ex);
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from command_info ");
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
                Log4netUtil.Error("CommandInfoDal/Delete", ex);
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select ci.Id,ci.CommandId,ci.AppId,ci.DeviceId,ci.Command,ci.CallbackUrl,ci.ExpireTime,ci.Status,ci.Result,ci.CreationTime,ci.ExecuteTime,
                                ci.PlatformIssUedTime,ci.DeliveredTime,ci.IssuedTimes,ci.MaxRetransMit,app.`Name` as AppName,ud.DeviceName
                                from command_info as ci
                                left join apps as app on ci.AppId = app.AppId
                                left join user_device as ud on ci.DeviceId = ud.DeviceId ");
                strSql.Append("where 1=1 ");
                if(model != null)
                {
                    if(model.Id > 0)
                    {
                        strSql.Append("and Id=@Id ");
                    }
                    if (!string.IsNullOrEmpty(model.CommandId))
                    {
                        strSql.Append("and ci.CommandId=@CommandId ");
                    }
                }
                var query = conn.Query<CommandInfo>(strSql.ToString(), model).FirstOrDefault();
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
                Log4netUtil.Error("CommandInfoDal/GetOne", ex);
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select ci.Id,ci.CommandId,ci.AppId,ci.DeviceId,ci.Command,ci.CallbackUrl,ci.ExpireTime,ci.Status,ci.Result,ci.CreationTime,ci.ExecuteTime,
                                ci.PlatformIssUedTime,ci.DeliveredTime,ci.IssuedTimes,ci.MaxRetransMit,app.`Name` as AppName,ud.DeviceName
                                from command_info as ci
                                left join apps as app on ci.AppId = app.AppId
                                left join user_device as ud on ci.DeviceId = ud.DeviceId ");
                strSql.Append("where 1=1 ");
                
                if (model != null)
                {
                    if (model.Id != 0)
                    {
                        strSql.Append("and Id = @Id ");
                    }
                    if (!string.IsNullOrEmpty(model.DeviceName))
                    {
                        model.DeviceName = "%"+ model.DeviceName + "%";
                        strSql.Append("and ud.DeviceName LIKE @DeviceName ");

                    }
                }
                var query = conn.Query<CommandInfo>(strSql.ToString(), model).ToList();

                if (model.PageIndex > 0)
                {
                    var pageList = query.OrderByDescending(c => c.CreationTime).Skip(model.PageSize * (model.PageIndex - 1)).Take(model.PageSize).ToList();

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
                Log4netUtil.Error("CommandInfoDal/GetList", ex);
            }

            return rst;
        }
        
        #endregion
    }
}