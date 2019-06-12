using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace BusinessClass.TaoBao
{
    class goods
    {
    }

    public class TB_goods
    {
        public string itemId { get; set; }
        public string title { get; set; }
        public int sellCount { get; set; }
        public int vagueSellCount { get; set; }


        /// <summary>
        /// 常规获取商品信息借口，过频出错
        /// </summary>
        /// <param name="goods_id">商品ID</param>
        /// <returns></returns>
        public static TB_goods get_data(string goods_id)
        {
            try
            {
                String url = "http://acs.m.taobao.com/h5/mtop.taobao.detail.getdetail/6.0/?data=%7B%22itemNumId%22%3A%22" + goods_id + "%22%7D";
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
                webrequest.Headers.Add("UserAgent", "Mozilla/5.0 (iPad; U; CPU OS 3_2_2 like Mac OS X; en-us) AppleWebKit/531.21.10 (KHTML, like Gecko) Version/4.0.4 Mobile/7B500 Safari/531.21.10");
                string result = WQH.Web.HTTP.Helper.GetString<object>(webrequest, null, null);

                JObject jo = JObject.Parse(result);
                var value = jo["data"]?["apiStack"]?[0]["value"]?.ToString();
                if (value != null)
                {
                    var jvalue = JObject.Parse(value);
                    var item = jvalue["item"];
                    return item == null ? null : item.ToObject<TB_goods>();
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                WQH.system.IO.File.WriteLog(goods_id + "|" + "TB_goods接口错误：" + e.Message.ToString(), 1);
                return null;
            }
        }
    }
}
