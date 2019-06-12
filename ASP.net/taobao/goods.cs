using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ASP.net.taobao
{
    public class goods
    {
        public string itemId { get; set; }
        public string title { get; set; }
        public string categoryId { get; set; }
        public string commentCount { get; set; }
        public string favcount { get; set; }



        public static string get_goods(string goods_id)
        {
            String url = "http://acs.m.taobao.com/h5/mtop.taobao.detail.getdetail/6.0/?data=%7B%22itemNumId%22%3A%22" + goods_id + "%22%7D";
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.UserAgent = "Mozilla/5.0 (iPad; U; CPU OS 3_2_2 like Mac OS X; en-us) AppleWebKit/531.21.10 (KHTML, like Gecko) Version/4.0.4 Mobile/7B500 Safari/531.21.10";
            return WQH.Web.HTTP.Helper.GetString<object>(webrequest, null, null);
        }
    }
}