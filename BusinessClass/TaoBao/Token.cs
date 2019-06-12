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
    public class Token
    {
        public string access_token { get; set; }
        public string expire_time { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string re_expires_in { get; set; }

        public static Token get(string code)
        {
            ITopClient client = new DefaultTopClient(Global.TB_api_urls, Global.TB_appkey, Global.TB_secret, "json");
            TopAuthTokenCreateRequest req = new TopAuthTokenCreateRequest();
            req.Code = code;
            TopAuthTokenCreateResponse rsp = client.Execute(req);
            JObject jo = (JObject)JsonConvert.DeserializeObject(rsp.Body);
            var token_result = jo["top_auth_token_create_response"]?["token_result"];
            if (token_result != null)
            {
                return JsonConvert.DeserializeObject<Token>(token_result.ToString());
            }
            else
            {
                throw new Exception("获取token失败:" + jo.ToString());
            }
        }

        public static Token refresh(string refresh_token)
        {
            ITopClient client = new DefaultTopClient(Global.TB_api_urls, Global.TB_appkey, Global.TB_secret, "json");
            TopAuthTokenRefreshRequest req = new TopAuthTokenRefreshRequest();
            req.RefreshToken = refresh_token;
            TopAuthTokenRefreshResponse rsp = client.Execute(req);
            JObject jo = (JObject)JsonConvert.DeserializeObject(rsp.Body);
            var token_result = jo["top_auth_token_refresh_response"]?["token_result"];
            if (token_result != null)
            {
                return JsonConvert.DeserializeObject<Token>(token_result.ToString());
            }
            else
            {
                throw new Exception("刷新token失败:" + jo.ToString());
            }
        }
    }
}
