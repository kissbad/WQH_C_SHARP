using Dapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace BusinessClass.TaoBao
{
    /// <summary>
    /// 淘宝客商品查询
    /// </summary>
    public class tbk_item_get
    {
        public string item_url;
        /// <summary>
        /// 商品编号
        /// </summary>
        public long num_iid;
        public string title;
        public string pict_url;
        public decimal reserve_price;
        /// <summary>
        /// 折扣价
        /// </summary>
        public decimal zk_final_price;
        public string nick;
        public string provcity;
        public long seller_id;
        public int user_type;
        /// <summary>
        /// 30天销售量
        /// </summary>
        public int volume;

        public static List<tbk_item_get> get(string key)
        {
            ITopClient client = new DefaultTopClient(Global.TB_api_urls, Global.TB_appkey, Global.TB_secret, "json");
            TbkItemGetRequest req = new TbkItemGetRequest();
            req.Fields = "num_iid,title,pict_url,small_images,reserve_price,zk_final_price,user_type,provcity,item_url,seller_id,volume,nick";
            req.Q = key;
            req.Sort = "total_sales_des";
            req.PageNo = 1L;
            req.PageSize = 100L;
            TbkItemGetResponse rsp = client.Execute(req);
            JObject jo = JObject.Parse(rsp.Body);

            var jt = jo["tbk_item_get_response"]?["results"]?["n_tbk_item"];
            if (jt == null)
            {
                throw new Exception(rsp.Body);
            }
            return jt.ToObject<List<tbk_item_get>>();
        }

        public bool savsToGoods()
        {
            var jo = new
            {
                goods_id = num_iid,
                goods_name = title,
                goods_url = item_url,
                pict_url,
                price = reserve_price,
                final_price = zk_final_price,
                sellCount = volume,
                crate_time = DateTime.Now
            };
            using (SqlConnection conn = new SqlConnection(Global.ConnectionString))
            {
                string sql = @"delete goods where goods_id = @goods_id;
insert into goods(goods_id,goods_name,goods_url,pict_url,price,final_price,sellCount,crate_time)
values(@goods_id,@goods_name,@goods_url,@pict_url,@price,@final_price,@sellCount,@crate_time)";
                return conn.Execute(sql, jo) > 0;
            }
        }
    }
}
