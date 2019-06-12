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
    /// 淘宝客推广
    /// </summary>
    public class tbk_privilege
    {
        public long category_id { get; set; }
        public string coupon_click_url { get; set; }
        public string coupon_end_time { get; set; }
        public string coupon_info { get; set; }
        public long coupon_remain_count { get; set; }
        public string coupon_start_time { get; set; }
        public long coupon_total_count { get; set; }
        public int coupon_type { get; set; }
        public long item_id { get; set; }
        public string item_url { get; set; }
        public double max_commission_rate { get; set; }

        public static tbk_privilege get(long goods_id, string token, string pid)
        {
            string[] Array = pid.Split('_');
            if (!pid.Contains("mm_") || Array.Count() < 4)
            {
                throw new Exception("pid错误");
            }
            var AdzoneId = Convert.ToInt64(Array[3]);
            var SiteId = Convert.ToInt64(Array[2]);
            var sessionKey = token;

            ITopClient client = new DefaultTopClient(Global.TB_api_urls, Global.TB_appkey, Global.TB_secret, "json");
            TbkPrivilegeGetRequest req = new TbkPrivilegeGetRequest();
            req.ItemId = goods_id;
            req.AdzoneId = AdzoneId;
            req.SiteId = SiteId;
            TbkPrivilegeGetResponse rsp = client.Execute(req, sessionKey);
            var result = rsp.Body;
            JObject json = JObject.Parse(result);

            var a = json["tbk_privilege_get_response"]?["result"]?["data"];
            if (a == null)
            {
                throw new Exception("转链失败：" + result);
            }
            return a.ToObject<tbk_privilege>();
        }
    }
}
