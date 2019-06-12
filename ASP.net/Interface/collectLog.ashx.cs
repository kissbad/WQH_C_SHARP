using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using WQH.Data;


namespace ASP.net.Interface
{
    /// <summary>
    /// collectLog 的摘要说明
    /// </summary>
    public class collectLog : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string type = context.Request.Params["type"]?.ToString();
            if (string.IsNullOrWhiteSpace(type)) {
                context.Response.ContentType = "text/plain";
                context.Response.Write(JObject.FromObject(new { error = 1, msg ="参数type无效"}));
                return;
            }

            string StrData = new StreamReader(context.Request.InputStream, Encoding.UTF8).ReadToEnd();
            JObject jo = (JObject)JsonConvert.DeserializeObject(StrData);

            if (jo.Count == 0)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write(JObject.FromObject(new { error = 1, msg = "数据无效" }));
                return;
            }

            string sql = string.Empty;
            System.Data.SqlClient.SqlParameter[] Paramet = null;

            if (type == "date")
            {
                sql = @"select a.QQgroupId,b.qunNum,b.qunName,a.CrateDateTime 
from collectLog a join collectQun b on a.QQgroupNum = b.qunNum
where a.GoodsId = @GoodsId and convert(char(10),a.CrateDateTime,120) = @date
order by a.CrateDateTime";
                Paramet = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@GoodsId",jo["GoodsId"].ToString()),
                    new System.Data.SqlClient.SqlParameter("@date",jo["date"].ToString()),
                };
            }
            else
            {
                sql = @"select a.QQgroupId,b.qunNum,b.qunName,a.CrateDateTime 
from collectLog a join collectQun b on a.QQgroupNum = b.qunNum
where a.GoodsId = @GoodsId and a.CrateDateTime between @begin_time and @end_time 
order by a.CrateDateTime";

                Paramet = new System.Data.SqlClient.SqlParameter[]
                {
                    new System.Data.SqlClient.SqlParameter("@GoodsId",jo["GoodsId"].ToString()),
                    new System.Data.SqlClient.SqlParameter("@begin_time",jo["begin_time"].ToString()),
                    new System.Data.SqlClient.SqlParameter("@end_time",jo["end_time"].ToString()),
                };
            }
            DataTable dt = SqlHelper.ExecuteReader(CommandType.Text, sql, Paramet);

            context.Response.ContentType = "application/json";
            context.Response.Write(JObject.FromObject(new { error = 1, data = dt }));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}