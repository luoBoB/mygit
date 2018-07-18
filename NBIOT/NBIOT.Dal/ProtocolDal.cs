using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;

namespace NBIOT.Dal
{
    public class ProtocolDal : BaseDal
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

            try
            {

                //判断重复
                var checkQuery = Check(model);
                if (checkQuery.Result)
                {
                    rst.Message = "已存在";
                    return rst;
                }
                model.CreateTime = DateTime.Now;

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into protocol(");
                strSql.Append("Name,DllName,DllPath,CreateTime) ");
                strSql.Append("values (");
                strSql.Append("@Name,@DllName,@DllPath,@CreateTime)");
                conn.Execute(strSql.ToString(), model);

                rst.Result = true;
                rst.Message = "添加成功";
            }
            catch (Exception ex)
            {
                rst.Message = "添加失败：" + ex.Message;
                Log4netUtil.Error("ProtocolDal/Add", ex);
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
            var tran = conn.BeginTransaction();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into protocol(");
                strSql.Append("Name,DllName,DllPath,CreateTime) ");
                strSql.Append("values (");
                strSql.Append("@Name,@DllName,@DllPath,@CreateTime)");
                conn.Execute(strSql.ToString(), list, tran);
                tran.Commit();

                rst.Result = true;
                rst.Message = "批量添加成功";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                rst.Message = "批量添加失败：" + ex.Message;
                Log4netUtil.Error("ProtocolDal/AddList", ex);
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update protocol set ");
                strSql.Append("Name=@Name,");
                strSql.Append("DllName=@DllName,");
                strSql.Append("DllPath=@DllPath,");
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
                Log4netUtil.Error("ProtocolDal/Update", ex);
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from protocol ");
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
                Log4netUtil.Error("ProtocolDal/Delete", ex);
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select Id,Name,DllName,DllPath,CreateTime from protocol ");
                strSql.Append("where Id=@Id");
                var query = conn.Query<Protocol>(strSql.ToString(), model).FirstOrDefault();
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
                Log4netUtil.Error("ProtocolDal/GetOne", ex);
            }

            return rst;
        }
        /// <summary>
        /// 添加时判断关键内容是否已存在
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<Protocol> Check(Protocol model)
        {
            var rst = new ReturnResult<Protocol>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select Id,Name,DllName,DllPath,CreateTime from protocol ");
                strSql.Append("where Name=@Name");
                var query = conn.Query<Protocol>(strSql.ToString(), model).FirstOrDefault();
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
                Log4netUtil.Error("ProtocolDal/Check", ex);
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

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select Id,Name,DllName,DllPath,CreateTime from protocol ");
                strSql.Append("where 1=1 ");

                if (model != null)
                {
                    if (model.Id != 0)
                    {
                        strSql.Append("and Id = @Id ");
                    }
                    if (!string.IsNullOrEmpty(model.Name))
                    {
                        model.Name = "%" + model.Name + "%";
                        strSql.Append("and Name LIKE @Name ");
                    }
                }
                var query = conn.Query<Protocol>(strSql.ToString(), model).ToList();

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
                Log4netUtil.Error("ProtocolDal/GetList", ex);
            }

            return rst;
        }

        #endregion

        public ReturnResult<Protocol> GetOneByDeviceId(string deviceId)
        {
            var rst = new ReturnResult<Protocol>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select t3.* from user_device t1 ");
                strSql.Append(@"inner join profile t2 on t1.ProfileId=t2.Id ");
                strSql.Append(@"inner join protocol t3 on t2.ProtocolId=t3.Id ");
                strSql.Append("where t1.DeviceId=@deviceId");
                var query = conn.Query<Protocol>(strSql.ToString(), new { deviceId }).FirstOrDefault();
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
                Log4netUtil.Error("ProtocolDal/GetOne", ex);
            }

            return rst;
        }
    }
}