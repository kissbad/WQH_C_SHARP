using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace BusinessClass
{
    public class Global
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["connectionString"]?.ConnectionString;
        /// <summary>
        /// 淘宝APP Key
        /// </summary>
        public static readonly string TB_appkey = "24942701";
        /// <summary>
        /// 淘宝APP Key对应的秘钥
        /// </summary>
        public static readonly string TB_secret = "34ba12d1d9af80f839171e2d1f3884d8";
        /// <summary>
        /// 淘宝接口地址
        /// </summary>
        public static readonly string TB_api_url = "http://gw.api.taobao.com/router/rest";
        /// <summary>
        /// 淘宝接口地址s
        /// </summary>
        public static readonly string TB_api_urls = "https://eco.taobao.com/router/rest";

    }
}
