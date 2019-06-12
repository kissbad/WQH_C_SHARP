using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace MVC_API.Controllers
{
    public class HelpController : ApiController
    {
        /// <summary>
        /// 登录接口
        /// </summary>
        [HttpPost]
        public object Login(Models.Request.Login l)
        {
            BusinessClass.user.From f = BusinessClass.user.from_login_name;
            var user = BusinessClass.user.get_info(l.logen_name, f);
            if (user == null)
            {
                return new { error = 1, msg = "用户不存在" };
            }
            if (user.pass_word != WQH.String.CreateMD5Hash(l.pass_word))
            {
                return new { error = 1, msg = "密码不正确" };
            }
            else
            {
                return new { error = 0, msg = "登录成功", data = user };
            }
        }
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="l"></param>
        /// <returns></returns>
        [HttpPost]
        public object getGoodsList(Models.Request.getGoodsList ggl)
        {
            try
            {
                var data = ggl.get();
                return new { error = 0, msg = "查询成功", data };
            }
            catch (Exception e)
            {
                return new { error = 1, msg = e.Message };
            }

        }
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object Upload()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent("form-data"))
                {
                    return "数据类型错误";
                }
                HttpFileCollection fc = HttpContext.Current.Request.Files;
                var Keys = fc.AllKeys;
                List<string> data = new List<string>();
                for (int i = 0; i < fc.Count; i++)
                {
                    Stream s = fc[i].InputStream;
                    string extension = Path.GetExtension(fc[i].FileName).ToLower();
                    if (".jpg|.gif|.png|.bmp|.psd|.svg|".IndexOf(extension) == -1)
                    {
                        throw new Exception("图片格式错误");
                    }
                    var fileName = "upload/" + Guid.NewGuid().ToString().ToUpper() + extension;
                    var oss = new BusinessClass.TaoBao.OSS();
                    var x = oss.Upload(s, fileName).ToString();
                    data.Add(x);
                }
                return data;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
