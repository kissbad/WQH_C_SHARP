using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessClass
{
    public class user
    {
        /// <summary>
        /// 查询方法托管
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public delegate _user From(string key);

        public static _user get_info(string key, From f)
        {
            return f(key);
        }

        public static _user from_id(string key)
        {
            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                string sql = @"select * from _user where user_id = @user_id";
                return conn.QueryFirstOrDefault<_user>(sql, new { user_id = key });
            }
        }

        public static _user from_login_name(string key)
        {
            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                string sql = @"select * from _user where login_name = @login_name";
                return conn.QueryFirstOrDefault<_user>(sql, new { login_name = key });
            }
        }
    }
    /// <summary>
    /// 数据库表_user
    /// </summary>
    public class _user
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string login_name { get; set; }
        public string pass_word { get; set; }
        public string mobile_phone_number { get; set; }
        public string image_url { get; set; }
        public string crate_time { get; set; }
        public string user_state { get; set; }
    }
}
