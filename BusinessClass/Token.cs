using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessClass
{
    public class Token
    {
        public string user_id { get; set; }
        public string token { get; set; }
        public DateTime expire_time { get; set; }
        public DateTime create_time { get; set; }
        private static string generateToken(string user_id, string pass_word, DateTime expire_time)
        {
            return WQH.String.CreateMD5Hash(user_id + "|" + pass_word + "|" + expire_time.ToString());
        }
        public static string create(IToken itoken, int expire)
        {
            DateTime expire_time = DateTime.Now.AddDays(expire);
            string token = generateToken(itoken.user_id, itoken.pass_word, expire_time);
            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                string sql = @"delete from token where user_id = @user_id;
insert into Live_admin_token(user_id,token,expire_time)
values(@user_id,@token,@expire_time) ";
                if (conn.Execute(sql, new { itoken.user_id, token, expire_time }) > 0)
                {
                    return token;
                }
                else
                {
                    throw new Exception("token生成失败");
                }
            }
        }
        public static Token get(string token)
        {
            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                string sql = @"select * from token where token = @token";
                return conn.QueryFirstOrDefault<Token>(sql, new { token });
            }
        }
    }
}
