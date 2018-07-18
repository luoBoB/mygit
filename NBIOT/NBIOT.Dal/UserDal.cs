using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;

namespace NBIOT.Dal
{
    public class UserDal : BaseDal
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

            try
            {
                //判断重复
                var checkQuery = Check(model);
                if (checkQuery.Result)
                {
                    rst.Message = "手机号已存在";
                    return rst;
                }
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into user(");
                strSql.Append("Phone,Password,AppId,CreateTime) ");
                strSql.Append("values (");
                strSql.Append("@Phone,@Password,@AppId,@CreateTime)");
                conn.Execute(strSql.ToString(), model);

                rst.Result = true;
                rst.Message = "添加成功";
            }
            catch (Exception ex)
            {
                rst.Message = "添加失败：" + ex.Message;
                Log4netUtil.Error("UserDal/Add", ex);
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
            var tran = conn.BeginTransaction();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into user(");
                strSql.Append("Phone,Password,AppId,CreateTime) ");
                strSql.Append("values (");
                strSql.Append("@Phone,@Password,@AppId,@CreateTime)");
                conn.Execute(strSql.ToString(), list, tran);
                tran.Commit();

                rst.Result = true;
                rst.Message = "批量添加成功";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                rst.Message = "批量添加失败：" + ex.Message;
                Log4netUtil.Error("UserDal/AddList", ex);
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update user set ");
                strSql.Append("Phone=@Phone,");
                strSql.Append("Password=@Password,");
                strSql.Append("AppId=@AppId,");
                strSql.Append("CreateTime=@CreateTime ");
                strSql.Append("where Id=@Id");
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
                Log4netUtil.Error("UserDal/Update", ex);
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from user ");
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
                Log4netUtil.Error("UserDal/Delete", ex);
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select u.Id,u.Phone,u.Password,app.`Name` as AppName,u.AppId,u.CreateTime 
                                from user as u
                                left join apps as app on u.AppId = app.Id ");
                strSql.Append("where Id=@Id");

                var query = conn.Query<User>(strSql.ToString(), model).FirstOrDefault();
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
                Log4netUtil.Error("UserDal/GetOne", ex);
            }

            return rst;
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<User> Check(User model)
        {
            var rst = new ReturnResult<User>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select u.Id,u.Phone,u.Password,app.`Name` as AppName,u.AppId,u.CreateTime 
                                from user as u
                                left join apps as app on u.AppId = app.Id ");
                strSql.Append("where Phone=@Phone");

                var query = conn.Query<User>(strSql.ToString(), model).FirstOrDefault();
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
                Log4netUtil.Error("UserDal/Check", ex);
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select u.Id,u.Phone,u.Password,app.`Name` as AppName,u.AppId,u.CreateTime 
                                from user as u
                                left join apps as app on u.AppId = app.Id ");
                strSql.Append("where 1=1 ");

                if (model != null)
                {
                    if (model.Id != 0)
                    {
                        strSql.Append("and Id = @Id ");
                    }
                    if (!string.IsNullOrEmpty(model.Phone))
                    {
                        model.Phone = "%" + model.Phone + "%";
                        strSql.Append("and Phone LIKE @Phone ");
                    }
                }
                var query = conn.Query<User>(strSql.ToString(), model).ToList();

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
                Log4netUtil.Error("UserDal/GetList", ex);
            }

            return rst;
        }

        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<User> UserLogin(User model)
        {
            var rst = new ReturnResult<User>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select Id,Phone,Password from user ");
                strSql.Append("where Phone=@Phone ");
                strSql.Append("and Password=@Password ");

                var query = conn.Query<User>(strSql.ToString(), model).FirstOrDefault();
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
                Log4netUtil.Error("UserDal/GetOne", ex);
            }

            return rst;
        }

        #endregion
    }
}