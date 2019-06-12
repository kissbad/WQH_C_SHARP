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
    /// 淘宝客淘口令
    /// </summary>
    public class tbk_TpwdCreate
    {

        public static tbk_TpwdCreate get(string url, string Text, string logo)
        {
            ITopClient client = new DefaultTopClient(Global.TB_api_urls, Global.TB_appkey, Global.TB_secret, "json");
            TbkTpwdCreateRequest req = new TbkTpwdCreateRequest();
            //req.UserId = "123";
            req.Text = Text;
            req.Url = url;
            req.Logo = logo;
            req.Ext = "{}";
            TbkTpwdCreateResponse rsp = client.Execute(req);
            JObject jo = JObject.Parse(rsp.Body);

            var data = jo["tbk_tpwd_create_response"]?["data"];
            if (data == null)
            {
                throw new Exception(error_response.get(jo).sub_msg);
            }
            return data.ToObject<tbk_TpwdCreate>();
        }
    }
}
