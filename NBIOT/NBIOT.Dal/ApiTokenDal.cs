using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;

namespace NBIOT.Dal
{
    public class ApiTokenDal : BaseDal
    {
        #region 基本增删改查

        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Add(ApiToken model)
        {
            var rst = new ReturnResult<bool>();

            try
            {
                var check = GetOne(model);
                if (check.Result)
                {
                    return Update(model);
                }
                else
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into api_token(");
                    strSql.Append("AppId,AccessToken,RefreshToken,AccessTokenExpiresIn,AccessTokenCreateTime,RefreshTokenExpiresTime) ");
                    strSql.Append("values (");
                    strSql.Append("@AppId,@AccessToken,@RefreshToken,@AccessTokenExpiresIn,@AccessTokenCreateTime,@RefreshTokenExpiresTime)");
                    conn.Execute(strSql.ToString(), model);

                    rst.Result = true;
                    rst.Message = "添加成功";
                }
            }
            catch (Exception ex)
            {
                rst.Message = "添加失败：" + ex.Message;
                Log4netUtil.Error("ApiTokenDal/Add", ex);
            }

            return rst;
        }

        /// <summary>
        /// 新增多条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> AddList(List<ApiToken> list)
        {
            var rst = new ReturnResult<bool>();
            var tran = conn.BeginTransaction();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into api_token(");
                strSql.Append("AppId,AccessToken,RefreshToken,AccessTokenExpiresIn,AccessTokenCreateTime,RefreshTokenExpiresTime) ");
                strSql.Append("values (");
                strSql.Append("@AppId,@AccessToken,@RefreshToken,@AccessTokenExpiresIn,@AccessTokenCreateTime,@RefreshTokenExpiresTime)");
                conn.Execute(strSql.ToString(), list, tran);
                tran.Commit();

                rst.Result = true;
                rst.Message = "批量添加成功";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                rst.Message = "批量添加失败：" + ex.Message;
                Log4netUtil.Error("ApiTokenDal/AddList", ex);
            }

            return rst;
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Update(ApiToken model)
        {
            var rst = new ReturnResult<bool>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update api_token set ");
                strSql.Append("AccessToken=@AccessToken,");
                strSql.Append("RefreshToken=@RefreshToken,");
                strSql.Append("AccessTokenExpiresIn=@AccessTokenExpiresIn,");
                strSql.Append("AccessTokenCreateTime=@AccessTokenCreateTime,");
                strSql.Append("RefreshTokenExpiresTime=@RefreshTokenExpiresTime ");
                strSql.Append("where AppId=@AppId");
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
                Log4netUtil.Error("ApiTokenDal/Update", ex);
            }

            return rst;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Delete(ApiToken model)
        {
            var rst = new ReturnResult<bool>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from api_token ");
                strSql.Append("where AppId=@AppId");
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
                Log4netUtil.Error("ApiTokenDal/Delete", ex);
            }

            return rst;
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<ApiToken> GetOne(ApiToken model)
        {
            var rst = new ReturnResult<ApiToken>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select Id,AppId,AccessToken,RefreshToken,AccessTokenExpiresIn,AccessTokenCreateTime,RefreshTokenExpiresTime from api_token ");
                strSql.Append("where 1=1 ");

                if (model != null)
                {
                    if (model.Id != 0)
                    {
                        strSql.Append("and Id = @Id ");
                    }
                    if (model.AppId > 0)
                    {
                        strSql.Append("and AppId = @AppId ");
                    }
                }
                
                var query = conn.Query<ApiToken>(strSql.ToString(), model).FirstOrDefault();
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
                Log4netUtil.Error("ApiTokenDal/GetOne", ex);
            }

            return rst;
        }

        /// <summary>
        /// 获取数据列表（带分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<List<ApiToken>> GetList(ApiToken model)
        {
            var rst = new ReturnResult<List<ApiToken>>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select Id,AppId,AccessToken,RefreshToken,AccessTokenExpiresIn,AccessTokenCreateTime,RefreshTokenExpiresTime from api_token ");
                strSql.Append("where 1=1 ");

                if (model != null)
                {
                    if (model.Id != 0)
                    {
                        strSql.Append("and Id = @Id ");
                    }
                }
                var query = conn.Query<ApiToken>(strSql.ToString(), model).ToList();

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
                Log4netUtil.Error("ApiTokenDal/GetList", ex);
            }

            return rst;
        }

        #endregion
    }
}