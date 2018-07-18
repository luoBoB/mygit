using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;
using NBIOT.Common;

namespace NBIOT.Dal
{
    public class AdminDal : BaseDal
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

            try
            {
                //判断重复
                var checkQuery = Check(model);
                if (checkQuery.Result)
                {
                    rst.Message = "账号已存在";
                    return rst;
                }

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into admin(");
                strSql.Append("UserName,Password,LastLoginTime,CreateTime) ");
                strSql.Append("values (");
                strSql.Append("@UserName,@Password,@LastLoginTime,@CreateTime)");
                conn.Execute(strSql.ToString(), model);

                rst.Result = true;
                rst.Message = "添加成功";
            }
            catch (Exception ex)
            {
                rst.Message = "添加失败：" + ex.Message;
                Log4netUtil.Error("AdminDal/Add", ex);
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
            var tran = conn.BeginTransaction();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into admin(");
                strSql.Append("UserName,Password,LastLoginTime,CreateTime) ");
                strSql.Append("values (");
                strSql.Append("@UserName,@Password,@LastLoginTime,@CreateTime)");
                conn.Execute(strSql.ToString(), list, tran);
                tran.Commit();

                rst.Result = true;
                rst.Message = "批量添加成功";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                rst.Message = "批量添加失败：" + ex.Message;
                Log4netUtil.Error("AdminDal/AddList", ex);
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update admin set ");
                strSql.Append("UserName=@UserName,");
                strSql.Append("Password=@Password,");
                strSql.Append("LastLoginTime=@LastLoginTime,");
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
                Log4netUtil.Error("AdminDal/Update", ex);
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                if (model != null)
                {
                    if (model.Id != 0)
                    {
                        strSql.Append("delete from admin ");
                        strSql.Append("where Id = @Id ");
                    }
                    if (!string.IsNullOrEmpty(model.Token))
                    {
                        strSql.Append("delete from admin ");
                        strSql.Append("where Token = @Token ");
                    }
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
                else
                {
                    rst.Message = "参数不能为空";
                    return rst;
                }
            }
            catch (Exception ex)
            {
                rst.Message = "删除失败：" + ex.Message;
                Log4netUtil.Error("AdminDal/Delete", ex);
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select t1.*,t2.Token from admin t1 ");
                strSql.Append(@"inner join admin_token t2 on t1.Id = t2.AdminId ");
                strSql.Append("where 1=1 ");
                if (model != null)
                {
                    if (model.Id != 0)
                    {
                        strSql.Append("and t1.Id = @Id ");
                    }
                    if (!string.IsNullOrEmpty(model.Token))
                    {
                        strSql.Append("and t2.Token = @Token ");
                    }
                }
                var query = conn.Query<Admin>(strSql.ToString(), model).FirstOrDefault();
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
                Log4netUtil.Error("AdminDal/GetOne", ex);
            }

            return rst;
        }

        public ReturnResult<Admin> Check(Admin model)
        {
            var rst = new ReturnResult<Admin>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select Id,UserName,Password,LastLoginTime,CreateTime from admin ");
                strSql.Append("where UserName=@UserName");
                var query = conn.Query<Admin>(strSql.ToString(), model).FirstOrDefault();
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
                Log4netUtil.Error("AdminDal/Check", ex);
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select Id,UserName,Password,LastLoginTime,CreateTime from admin ");
                strSql.Append("where 1=1 ");

                if (model != null)
                {
                    if (model.Id != 0)
                    {
                        strSql.Append("and Id = @Id ");
                    }
                    if (!string.IsNullOrEmpty(model.UserName))
                    {
                        model.UserName = "%" + model.UserName + "%";
                        strSql.Append("and UserName like @UserName ");
                    }
                }
                var query = conn.Query<Admin>(strSql.ToString(), model).ToList();

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
                Log4netUtil.Error("AdminDal/GetList", ex);
            }

            return rst;
        }

        #endregion

        public ReturnResult<Admin> Login(Admin model)
        {
            var rst = new ReturnResult<Admin>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select Id,UserName,Password,LastLoginTime,CreateTime from admin ");
                strSql.Append("where UserName=@UserName ");
                strSql.Append("and Password=@Password");

                model.Password = EncryptHelper.MD5Encrypt(model.Password);
                var query = conn.Query<Admin>(strSql.ToString(), model).FirstOrDefault();
                if (query == null)
                {
                    rst.Message = "用户名或密码不正确";
                }
                else
                {
                    var token = new AdminTokenDal().GetToken(new AdminToken() { AdminId = query.Id, IsGetNew = true });
                    if (token.Result)
                    {
                        rst.Result = true;
                        rst.Data = query;
                        rst.Data.Token = token.Data.Token;
                        rst.Message = "获取成功";
                    }
                    else
                    {
                        rst.Message = "获取Token失败";
                    }
                }
            }
            catch (Exception ex)
            {
                rst.Message = "查询失败：" + ex.Message;
                Log4netUtil.Error("AdminDal/Login", ex);
            }

            return rst;
        }
    }
}