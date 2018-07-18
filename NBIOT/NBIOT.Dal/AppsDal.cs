using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;

namespace NBIOT.Dal
{
    public class AppsDal : BaseDal
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
                strSql.Append("insert into apps(");
                strSql.Append("Name,AppId,Secret,AppType,AppServer,DeviceServer,CreateTime) ");
                strSql.Append("values (");
                strSql.Append("@Name,@AppId,@Secret,@AppType,@AppServer,@DeviceServer,@CreateTime)");
                conn.Execute(strSql.ToString(), model);

                rst.Result = true;
                rst.Message = "添加成功";
            }
            catch (Exception ex)
            {
                rst.Message = "添加失败：" + ex.Message;
                Log4netUtil.Error("AppsDal/Add", ex);
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
            var tran = conn.BeginTransaction();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into apps(");
                strSql.Append("Name,AppId,Secret,AppType,AppServer,DeviceServer,CreateTime) ");
                strSql.Append("values (");
                strSql.Append("@Name,@AppId,@Secret,@AppType,@AppServer,@DeviceServer,@CreateTime)");
                conn.Execute(strSql.ToString(), list, tran);
                tran.Commit();

                rst.Result = true;
                rst.Message = "批量添加成功";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                rst.Message = "批量添加失败：" + ex.Message;
                Log4netUtil.Error("AppsDal/AddList", ex);
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update apps set ");
                strSql.Append("Name=@Name,");
                strSql.Append("AppId=@AppId,");
                strSql.Append("Secret=@Secret,");
                strSql.Append("AppType=@AppType,");
                strSql.Append("AppServer=@AppServer,");
                strSql.Append("DeviceServer=@DeviceServer,");
                strSql.Append("CreateTime=@CreateTime ");
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
                Log4netUtil.Error("AppsDal/Update", ex);
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from apps ");
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
                Log4netUtil.Error("AppsDal/Delete", ex);
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select Id,Name,AppId,Secret,AppType,AppServer,DeviceServer,CreateTime from apps ");
                strSql.Append("where Id=@Id");
                var query = conn.Query<Apps>(strSql.ToString(), model).FirstOrDefault();
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
                Log4netUtil.Error("AppsDal/GetOne", ex);
            }

            return rst;
        }
         /// <summary>
        /// 添加时判断关键内容是否已存在
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<Apps> Check(Apps model)
        {
            var rst = new ReturnResult<Apps>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select Id,Name,AppId,Secret,AppType,AppServer,DeviceServer,CreateTime from apps ");
                strSql.Append("where Name=@Id");
                var query = conn.Query<Apps>(strSql.ToString(), model).FirstOrDefault();
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
                Log4netUtil.Error("AppsDal/Check", ex);
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select Id,Name,AppId,Secret,AppType,AppServer,DeviceServer,CreateTime from apps ");
                strSql.Append("where 1=1 ");
                
                if (model != null)
                {
                    if (model.Id != 0)
                    {
                        strSql.Append("and Id = @Id ");
                    }
                }
                if (!string.IsNullOrEmpty(model.Name))
                {
                    model.Name = "%" + model.Name + "%";

                    strSql.Append(" and Name LIKE @Name  ");

                }
                var query = conn.Query<Apps>(strSql.ToString(), model).ToList();

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
                Log4netUtil.Error("AppsDal/GetList", ex);
            }

            return rst;
        }
        
        #endregion
    }
}