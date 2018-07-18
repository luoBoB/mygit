using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;

namespace NBIOT.Dal
{
    public class ProfileDal : BaseDal
    {
        #region 基本增删改查
        
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Add(Profile model)
        {
            var rst = new ReturnResult<bool>();

            try
            {
                //判断重复
                var checkQuery = GetOneByModel(model);
                if (checkQuery.Result)
                {
                    rst.Message = "设备型号已存在";
                    return rst;
                }
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into profile(");
                strSql.Append("ManufacturerId,ManufacturerName,DeviceType,Model,ProtocolType,AppId,ProtocolId) ");
                strSql.Append("values (");
                strSql.Append("@ManufacturerId,@ManufacturerName,@DeviceType,@Model,@ProtocolType,@AppId,@ProtocolId)");
                conn.Execute(strSql.ToString(), model);

                rst.Result = true;
                rst.Message = "添加成功";
            }
            catch (Exception ex)
            {
                rst.Message = "添加失败：" + ex.Message;
                Log4netUtil.Error("ProfileDal/Add", ex);
            }

            return rst;
        }
        
        /// <summary>
        /// 新增多条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> AddList(List<Profile> list)
        {
            var rst = new ReturnResult<bool>();
            var tran = conn.BeginTransaction();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into profile(");
                strSql.Append("ManufacturerId,ManufacturerName,DeviceType,Model,ProtocolType,AppId,ProtocolId) ");
                strSql.Append("values (");
                strSql.Append("@ManufacturerId,@ManufacturerName,@DeviceType,@Model,@ProtocolType,@AppId,@ProtocolId)");
                conn.Execute(strSql.ToString(), list, tran);
                tran.Commit();

                rst.Result = true;
                rst.Message = "批量添加成功";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                rst.Message = "批量添加失败：" + ex.Message;
                Log4netUtil.Error("ProfileDal/AddList", ex);
            }

            return rst;
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Update(Profile model)
        {
            var rst = new ReturnResult<bool>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update profile set ");
                strSql.Append("ManufacturerId=@ManufacturerId,");
                strSql.Append("ManufacturerName=@ManufacturerName,");
                strSql.Append("DeviceType=@DeviceType,");
                strSql.Append("Model=@Model,");
                strSql.Append("ProtocolType=@ProtocolType, ");
                strSql.Append("AppId=@AppId, ");
                strSql.Append("ProtocolId=@ProtocolId ");
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
                Log4netUtil.Error("ProfileDal/Update", ex);
            }

            return rst;
        }
        
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Delete(Profile model)
        {
            var rst = new ReturnResult<bool>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("delete from profile ");
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
                Log4netUtil.Error("ProfileDal/Delete", ex);
            }

            return rst;
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<Profile> GetOne(Profile model)
        {
            var rst = new ReturnResult<Profile>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select pro.Id,pro.ManufacturerId,pro.ManufacturerName,pro.DeviceType,pro.Model,pro.ProtocolType,pro.AppId,app.`Name` as AppName,pl.`Name` as ProtocolName
                                from profile as pro
                                left join apps as app on pro.AppId = app.Id 
                                left join protocol as pl on pro.ProtocolId = pl.Id ");
                strSql.Append("where Id=@Id");
                var query = conn.Query<Profile>(strSql.ToString(), model).FirstOrDefault();
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
                Log4netUtil.Error("ProfileDal/GetOne", ex);
            }

            return rst;
        }

        /// <summary>
        /// 添加时判断关键内容是否已存在
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<Profile> GetOneByModel(Profile model)
        {
            var rst = new ReturnResult<Profile>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select pro.Id,pro.ManufacturerId,pro.ManufacturerName,pro.DeviceType,pro.Model,pro.ProtocolType,pro.AppId,app.`Name`        as AppName,pl.`Name` as ProtocolName
                                from profile as pro
                                left join apps as app on pro.AppId = app.Id 
                                left join protocol as pl on pro.ProtocolId = pl.Id ");
                strSql.Append("where Model = @Model ");
                var query = conn.Query<Profile>(strSql.ToString(), model).FirstOrDefault();
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
                Log4netUtil.Error("ProfileDal/GetOneByModel", ex);
            }

            return rst;
        }

        /// <summary>
        /// 获取数据列表（带分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<List<Profile>> GetList(Profile model)
        {
            var rst = new ReturnResult<List<Profile>>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select pro.Id,pro.ManufacturerId,pro.ManufacturerName,pro.DeviceType,pro.Model,pro.ProtocolType,pro.AppId,app.`Name`        as AppName,pl.`Name` as ProtocolName
                                from profile as pro
                                left join apps as app on pro.AppId = app.Id 
                                left join protocol as pl on pro.ProtocolId = pl.Id  ");
                strSql.Append("where 1=1 ");
                
                if (model != null)
                {
                    if (model.Id != 0)
                    {
                        strSql.Append("and Id = @Id ");
                    }
                    if (!string.IsNullOrEmpty(model.Model))
                    {
                        model.Model = "%"+ model.Model + "%";
                        strSql.Append("and Model LIKE @Model ");
                    }
                }
                var query = conn.Query<Profile>(strSql.ToString(), model).ToList();

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
                Log4netUtil.Error("ProfileDal/GetList", ex);
            }

            return rst;
        }
        
        #endregion
    }
}