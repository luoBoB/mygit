using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using NBIOT.Common;
using NBIOT.Model;

namespace NBIOT.Dal
{
    public class BaseDal : IDisposable
    {
        public static string ConnectionString
        {
            get
            {
                var config = XmlHelper.DeserializeToObject<AppSettings>(AppDomain.CurrentDomain.BaseDirectory + "Config.xml");

                return config.ConnectionString;
            }
        }
        public MySqlConnection conn;
        public BaseDal()
        {
            try
            {
                conn = new MySqlConnection(ConnectionString);
                conn.Open();
            }
            catch (Exception ex)
            {
                Log4netUtil.Error("在BaseDal中打开连接时出错：" + ex);
            }
        }

        public void Dispose()
        {
            try
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
                conn?.Dispose();
            }
            catch (Exception ex)
            {
                Log4netUtil.Error("在BaseDal中关闭连接时出错：" + ex);
            }
        }
    }
}
