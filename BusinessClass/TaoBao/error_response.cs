using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessClass.TaoBao
{
    public class error_response
    {
        public int code { get; set; }
        public string msg { get; set; }
        public string sub_code { get; set; }
        public string sub_msg { get; set; }
        public string request_id { get; set; }
        public static error_response get(JObject jo) {
            var error_response = jo["error_response"];
            if (error_response == null) { return null; }
            return error_response.ToObject<error_response>();
        }
    }
}
