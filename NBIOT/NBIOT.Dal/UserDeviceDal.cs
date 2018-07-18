using NBIOT.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;

namespace NBIOT.Dal
{
    public class UserDeviceDal : BaseDal
    {
        #region 基本增删改查
        
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Add(UserDevice model)
        {
            var rst = new ReturnResult<bool>();

            try
            {
                //判断重复
                var checkQuery = Check(model);
                if (checkQuery.Result)
                {
                    rst.Message = "设备名称已存在";
                    return rst;
                }

                //获取profileid
                //获取userid
                

                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into user_device(");
                strSql.Append("DeviceId,DeviceName,VerifyCode,NodeId,ProfileId,UserId) ");
                strSql.Append("values (");
                strSql.Append("@DeviceId,@DeviceName,@VerifyCode,@NodeId,@ProfileId,@UserId)");
                conn.Execute(strSql.ToString(), model);

                rst.Result = true;
                rst.Message = "添加成功";
            }
            catch (Exception ex)
            {
                rst.Message = "添加失败：" + ex.Message;
                Log4netUtil.Error("UserDeviceDal/Add", ex);
            }

            return rst;
        }
        
        /// <summary>
        /// 新增多条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> AddList(List<UserDevice> list)
        {
            var rst = new ReturnResult<bool>();
            var tran = conn.BeginTransaction();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into user_device(");
                strSql.Append("DeviceId,DeviceName,VerifyCode,NodeId,ProfileId,UserId) ");
                strSql.Append("values (");
                strSql.Append("@DeviceId,@DeviceName,@VerifyCode,@NodeId,@ProfileId,@UserId)");
                conn.Execute(strSql.ToString(), list, tran);
                tran.Commit();

                rst.Result = true;
                rst.Message = "批量添加成功";
            }
            catch (Exception ex)
            {
                tran.Rollback();
                rst.Message = "批量添加失败：" + ex.Message;
                Log4netUtil.Error("UserDeviceDal/AddList", ex);
            }

            return rst;
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Update(UserDevice model)
        {
            var rst = new ReturnResult<bool>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update user_device set ");
                strSql.Append("DeviceId=@DeviceId,");
                strSql.Append("DeviceName=@DeviceName,");
                strSql.Append("VerifyCode=@VerifyCode,");
                strSql.Append("NodeId=@NodeId,");
                strSql.Append("ProfileId=@ProfileId,");
                strSql.Append("UserId=@UserId ");
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
                Log4netUtil.Error("UserDeviceDal/Update", ex);
            }

            return rst;
        }
        
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<bool> Delete(UserDevice model)
        {
            var rst = new ReturnResult<bool>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                if(model.Id > 0)
                {
                    strSql.Append("delete from user_device ");
                    strSql.Append("where Id=@Id");
                }
                if (!string.IsNullOrEmpty(model.DeviceId))
                {
                    strSql.Append("delete from user_device ");
                    strSql.Append("where DeviceId=@DeviceId");
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
            catch (Exception ex)
            {
                rst.Message = "删除失败：" + ex.Message;
                Log4netUtil.Error("UserDeviceDal/Delete", ex);
            }

            return rst;
        }

        /// <summary>
        /// 获取一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<UserDevice> GetOne(UserDevice model)
        {
            var rst = new ReturnResult<UserDevice>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"SELECT ud.DeviceId,ud.DeviceName,ud.VerifyCode,ud.NodeId,pro.*,u.Phone
                                FROM user_device as ud 
                                LEFT JOIN `user` as u ON ud.UserId = u.Id
                                LEFT JOIN `profile` as pro ON ud.ProfileId = pro.Id 
                            ");
                strSql.Append("where Id=@Id");
                var query = conn.Query<UserDevice>(strSql.ToString(), model).FirstOrDefault();
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
                Log4netUtil.Error("UserDeviceDal/GetOne", ex);
            }

            return rst;
        }

        /// <summary>
        /// 添加时判断关键内容是否已存在
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<UserDevice> Check(UserDevice model)
        {
            var rst = new ReturnResult<UserDevice>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"SELECT ud.DeviceId,ud.DeviceName,ud.VerifyCode,ud.NodeId,pro.*,u.Phone
                                FROM user_device as ud 
                                LEFT JOIN `user` as u ON ud.UserId = u.Id
                                LEFT JOIN `profile` as pro ON ud.ProfileId = pro.Id 
                            ");
                strSql.Append("where DeviceName=@DeviceName");
                var query = conn.Query<UserDevice>(strSql.ToString(), model).FirstOrDefault();
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
                Log4netUtil.Error("UserDeviceDal/Check", ex);
            }

            return rst;
        }

        /// <summary>
        /// 获取数据列表（带分页）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ReturnResult<List<UserDevice>> GetList(UserDevice model)
        {
            var rst = new ReturnResult<List<UserDevice>>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"SELECT ud.DeviceId,ud.DeviceName,ud.VerifyCode,ud.NodeId,pro.*,ud.UserId,u.Phone
                                FROM user_device as ud 
                                LEFT JOIN `user` as u ON ud.UserId = u.Id
                                LEFT JOIN `profile` as pro ON ud.ProfileId = pro.Id 
                            ");
                strSql.Append("where 1=1 ");

                if (model != null)
                {
                    if (model.Id != 0)
                    {
                        strSql.Append("and Id = @Id ");
                    }
                    if (model.UserId > 0)
                    {
                        strSql.Append("and u.Id=@UserId ");
                    }
                    if (!string.IsNullOrEmpty(model.DeviceName)) 
                    {
                        model.DeviceName = "%"+ model.DeviceName + "%";
                        strSql.Append("and DeviceName LIKE @DeviceName ");
                    }
                }
                var query = conn.Query<UserDevice>(strSql.ToString(), model).ToList();

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
                Log4netUtil.Error("UserDeviceDal/GetList", ex);
            }

            return rst;
        }

        #endregion

        public ReturnResult<UserDevice> GetOneByVerifyCode(UserDevice model)
        {
            var rst = new ReturnResult<UserDevice>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"select Id,DeviceId,DeviceName,VerifyCode,NodeId,ProfileId,UserId from user_device ");
                strSql.Append("where VerifyCode=@VerifyCode");
                var query = conn.Query<UserDevice>(strSql.ToString(), model).FirstOrDefault();
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
                Log4netUtil.Error("UserDeviceDal/GetOneByVerifyCode", ex);
            }

            return rst;
        }

        public ReturnResult<bool> UpdateByVerifyCode(UserDevice model)
        {
            var rst = new ReturnResult<bool>();

            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update user_device set ");
                strSql.Append("DeviceId=@DeviceId,");
                strSql.Append("DeviceName=@DeviceName,");
                strSql.Append("ProfileId=@ProfileId,");
                strSql.Append("UserId=@UserId ");
                strSql.Append("where VerifyCode=@VerifyCode");
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
                Log4netUtil.Error("UserDeviceDal/UpdateByVerifyCode", ex);
            }

            return rst;
        }

        public ReturnResult<bool> AddOrUpdate(UserDevice model)
        {
            var rst = new ReturnResult<bool>();

            try
            {
                var checkQuery = GetOneByVerifyCode(model);
                if (checkQuery.Result)
                {
                    return UpdateByVerifyCode(model);
                }
                else
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into user_device(");
                    strSql.Append("DeviceId,DeviceName,VerifyCode,NodeId,ProfileId,UserId) ");
                    strSql.Append("values (");
                    strSql.Append("@DeviceId,@DeviceName,@VerifyCode,@NodeId,@ProfileId,@UserId)");
                    conn.Execute(strSql.ToString(), model);

                    rst.Result = true;
                    rst.Message = "添加成功";
                }
            }
            catch (Exception ex)
            {
                rst.Message = "添加失败：" + ex.Message;
                Log4netUtil.Error("UserDeviceDal/AddOrUpdate", ex);
            }

            return rst;
        }
    }
}