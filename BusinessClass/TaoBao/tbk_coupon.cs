using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessClass.TaoBao
{
    /// <summary>
    /// 阿里妈妈推广券信息查询。传入商品ID+券ID，或者传入me参数，均可查询券信息。
    /// </summary>
    public class tbk_coupon
    {
        public string coupon_activity_id { get; set; }
        public decimal coupon_amount { get; set; }
        public string coupon_end_time { get; set; }
        public int coupon_remain_count { get; set; }
        public int coupon_src_scene { get; set; }
        public decimal coupon_start_fee { get; set; }
        public string coupon_start_time { get; set; }
        public int coupon_total_count { get; set; }
        public int coupon_type { get; set; }
        public static tbk_coupon get(long GoodsId, string ActivityId, string Me)
        {
            Top.Api.ITopClient client = new Top.Api.DefaultTopClient(Global.TB_api_urls, Global.TB_appkey, Global.TB_secret, "json");
            Top.Api.Request.TbkCouponGetRequest req = new Top.Api.Request.TbkCouponGetRequest();
            req.ItemId = Convert.ToInt64(GoodsId);
            req.ActivityId = ActivityId;
            req.Me = Me;
            Top.Api.Response.TbkCouponGetResponse rsp = client.Execute(req);
            string Body = rsp.Body;
            JObject jo = JObject.Parse(rsp.Body);
            var date = jo["tbk_coupon_get_response"]?["data"];
            if (date == null) {
                throw new Exception("数据获取失败");
            }
            return date.ToObject<tbk_coupon>();
        }
    }
}
