using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using WQH.Data;


namespace ASP.net.Interface
{
    /// <summary>
    /// GoodsSaleLog 的摘要说明
    /// </summary>
    public class GoodsSaleLog : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string GoodsId = (context.Request.Params["item"]??"").ToString();
            if (string.IsNullOrWhiteSpace(GoodsId))
            {
                context.Response.ContentType = "application/json";
                context.Response.Write(JObject.FromObject(new { error = 1, msg = "参数item不能为空" }));
                return;
            }
            string StrData = new StreamReader(context.Request.InputStream, Encoding.UTF8).ReadToEnd();
            JObject jo = (JObject)JsonConvert.DeserializeObject(StrData);
            string pageIndex = jo["pageIndex"].ToString() ?? "1";

            string sql = @"exec date_sale_list @GoodsId,@pageIndex";
            DataTable dt = SqlHelper.ExecuteReader(CommandType.Text, sql, new System.Data.SqlClient.SqlParameter[] {
                new System.Data.SqlClient.SqlParameter("@GoodsId",GoodsId),
                new System.Data.SqlClient.SqlParameter("@pageIndex",pageIndex)
            });

            JArray ja = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(dt)) as JArray;

            StringBuilder sql1 = new StringBuilder();
            if (ja.Count > 0)
            {
                foreach (JObject joi in ja)
                {
                    sql1.Clear();
                    sql1.Append(@"select a.GoodsId,a.CrateDateTime,a.previousTime,a.SaleCount,a.rise,sum(case when b.id is null then 0 else 1 end) collect_count
from dbo.GoodsSaleLog a
left join collectLog b on a.GoodsId = b.GoodsId and b.CrateDateTime between a.previousTime and a.CrateDateTime
where a.GoodsId = @GoodsId and convert(char(10),a.CrateDateTime,120) = @date
group by a.GoodsId,a.CrateDateTime,a.previousTime,a.SaleCount,a.rise
order by CrateDateTime desc");

                    DataTable dt1 = SqlHelper.ExecuteReader(CommandType.Text, sql1.ToString(), new SqlParameter[]
                    {
                        new System.Data.SqlClient.SqlParameter("@GoodsId",joi["GoodsId"].ToString()),
                        new System.Data.SqlClient.SqlParameter("@date",joi["date"].ToString())
                    });

                    JToken jt = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(dt1)) as JToken;
                    joi.Add("Details", jt);
                };
            }
            else
            {
                context.Response.ContentType = "application/json";
                context.Response.Write(JObject.FromObject(new { error = 1, msg = "查无数据" }));
                return;
            }

            context.Response.ContentType = "application/json";
            context.Response.Write(JObject.FromObject(new { error = 0, data = ja }));
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