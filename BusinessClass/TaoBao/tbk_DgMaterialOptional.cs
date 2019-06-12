using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace BusinessClass.TaoBao
{
    /// <summary>
    /// 通用物料搜索API（导购）
    /// </summary>
    public class tbk_DgMaterialOptional
    {
        public string category_id { get; set; }
        public string category_name { get; set; }
        public int commission_rate { get; set; }
        public string commission_type { get; set; }
        public string coupon_id { get; set; }
        public string coupon_info { get; set; }
        public int coupon_remain_count { get; set; }
        public int coupon_total_count { get; set; }
        public bool include_dxjh { get; set; }
        public bool include_mkt { get; set; }
        public string item_url { get; set; }
        public int level_one_category_id { get; set; }
        public string level_one_category_name { get; set; }
        public long num_iid { get; set; }
        public string pict_url { get; set; }
        public string provcity { get; set; }
        public string reserve_price { get; set; }
        public long seller_id { get; set; }
        public int shop_dsr { get; set; }
        public string shop_title { get; set; }
        public string short_title { get; set; }
        public string title { get; set; }
        public string tk_total_commi { get; set; }
        public string tk_total_sales { get; set; }
        public static tbk_DgMaterialOptional get(string goodsLink)
        {
            string appkey = "24784901";
            string secret = "a821644ecc9212bbdf03d65eacd29b18";

            ITopClient client = new DefaultTopClient(Global.TB_api_urls, appkey, secret, "json");
            TbkDgMaterialOptionalRequest req = new TbkDgMaterialOptionalRequest();

            req.AdzoneId = 15659700253;
            req.Q = goodsLink;

            TbkDgMaterialOptionalResponse rsp = client.Execute(req);
            JObject jo = JObject.Parse(rsp.Body);

            var map_date = jo["tbk_dg_material_optional_response"]?["result_list"]?["map_data"]?[0];
            if (map_date == null) { throw new Exception("数据获取失败"); }
            return map_date.ToObject<tbk_DgMaterialOptional>();
        }
    }
}
