using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;

namespace NBIOT.Dal
{
    public class AdminTokenDal : BaseDal
    {
        #region 基本增删改查

        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Add(AdminToken model)
        {
            var rst = new ReturnResult<bool>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into admin_token(");
                strSql.Append("AdminId,Token,TimeOut) ");
                strSql.Append("values (");
                strSql.Append("@AdminId,@Token,@TimeOut)");
                conn.Execute(strSql.ToString(), model);

                rst.Result = true;
                rst.Message = "添加成功";
            }
            catch (Exception ex)
            {
                rst.Message = "添加失败：" + ex.Message;
                Log4netUtil.Error("AdminTokenDal/Add", ex);
            }

            return rst;
        }

        /// <summary>
        /// 新增多条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> AddList(List<AdminToken> list)
        {
            var rst = new ReturnResult<bool>();
            var tran = conn.BeginTransaction();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into admin_token(");
                strSql.Append("AdminId,Token,TimeOut) ");
                strSql.Append("values (");
                strSql.Append("@AdminId,@Token,@TimeOut)");
                conn.Execute(strSql.ToString(), list, tran);
                tran.Commit();

                rst.Result = true;
                rst.Message = "批量添加成功";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                rst.Message = "批量添加失败：" + ex.Message;
                Log4netUtil.Error("AdminTokenDal/AddList", ex);
            }

            return rst;
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Update(AdminToken model)
        {
            var rst = new ReturnResult<bool>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update admin_token set ");
                strSql.Append("AdminId=@AdminId,");
                strSql.Append("Token=@Token,");
                strSql.Append("TimeOut=@TimeOut ");
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
                Log4netUtil.Error("AdminTokenDal/Update", ex);
            }

            return rst;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Delete(AdminToken model)
        {
            var rst = new ReturnResult<bool>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from admin_token ");
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
                Log4netUtil.Error("AdminTokenDal/Delete", ex);
            }

            return rst;
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<AdminToken> GetOne(AdminToken model)
        {
            var rst = new ReturnResult<AdminToken>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select Id,AdminId,Token,TimeOut from admin_token ");
                strSql.Append("where Id=@Id");
                var query = conn.Query<AdminToken>(strSql.ToString(), model).FirstOrDefault();
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
                Log4netUtil.Error("AdminTokenDal/GetOne", ex);
            }

            return rst;
        }

        /// <summary>
        /// 获取数据列表（带分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<List<AdminToken>> GetList(AdminToken model)
        {
            var rst = new ReturnResult<List<AdminToken>>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select Id,AdminId,Token,TimeOut from admin_token ");
                strSql.Append("where 1=1 ");

                if (model != null)
                {
                    if (model.Id != 0)
                    {
                        strSql.Append("and Id = @Id ");
                    }
                }
                var query = conn.Query<AdminToken>(strSql.ToString(), model).ToList();

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
                Log4netUtil.Error("AdminTokenDal/GetList", ex);
            }

            return rst;
        }

        #endregion

        public ReturnResult<AdminToken> GetToken(AdminToken model)
        {
            var rst = new ReturnResult<AdminToken>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select Id,AdminId,Token,TimeOut from admin_token ");
                strSql.Append("where 1=1 ");

                if (model != null)
                {
                    if (model.AdminId > 0)
                    {
                        strSql.Append("and AdminId=@AdminId ");
                    }
                    if (!string.IsNullOrEmpty(model.Token))
                    {
                        strSql.Append("and Token=@Token ");
                    }
                }

                var query = conn.Query<AdminToken>(strSql.ToString(), model).FirstOrDefault();
                if (query == null)
                {
                    //创建新的token
                    model.Token = Guid.NewGuid().ToString().Replace("-", "");
                    model.TimeOut = DateTime.Now.AddHours(2);
                    var add = Add(model);
                    if (add.Result)
                    {
                        rst.Data = model;
                    }
                    else
                    {
                        throw new Exception("获取Token失败");
                    }
                }
                else
                {
                    if (DateTime.Compare((DateTime)query.TimeOut, DateTime.Now) > 0)
                    {
                        rst.Data = query;
                        rst.Result = true;
                        rst.Message = "获取成功";
                    }
                    else
                    {
                        if (model.IsGetNew)
                        {
                            model.Token = Guid.NewGuid().ToString().Replace("-", "");
                            model.TimeOut = DateTime.Now.AddHours(2);
                            var updateRst = UpdateToken(model);
                            if (updateRst.Result)
                            {
                                rst.Data = model;
                                rst.Result = true;
                                rst.Message = "获取成功";
                            }
                            else
                            {
                                throw new Exception("获取Token失败");
                            }
                        }
                        else
                        {
                            //否则直接返回false 过期
                            rst.Message = "Token过期，请重新登录。";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                rst.Message = ex.Message;
                Log4netUtil.Error("AdminTokenDal/GetToken", ex);
            }

            return rst;
        }

        public ReturnResult<bool> UpdateToken(AdminToken model)
        {
            var rst = new ReturnResult<bool>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update admin_token set ");
                strSql.Append("Token=@Token,");
                strSql.Append("TimeOut=@TimeOut ");
                strSql.Append("where AdminId=@AdminId");
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
                Log4netUtil.Error("AdminTokenDal/UpdateToken", ex);
            }

            return rst;
        }

        public ReturnResult<AdminToken> CheckToken(AdminToken model)
        {
            var rst = new ReturnResult<AdminToken>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select Id,AdminId,Token,TimeOut from admin_token ");
                strSql.Append("where Token=@Token ");

                var query = conn.Query<AdminToken>(strSql.ToString(), model).FirstOrDefault();
                if (query == null)
                {
                    throw new Exception("Token不存在");
                }
                else
                {
                    if (DateTime.Compare((DateTime)query.TimeOut, DateTime.Now) > 0)
                    {
                        rst.Data = query;
                        rst.Result = true;
                        rst.Message = "Token有效";
                    }
                    else
                    {
                        //否则直接返回false 过期
                        rst.Status = "10000";
                        rst.Message = "会话超时，请重新登录";
                    }
                }

            }
            catch (Exception ex)
            {
                rst.Message = ex.Message;
                Log4netUtil.Error("AdminTokenDal/CheckToken", ex);
            }

            return rst;
        }
    }
}