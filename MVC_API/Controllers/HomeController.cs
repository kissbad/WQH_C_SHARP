using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC_API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        public ActionResult Login() {
            ViewBag.Title = "登录页";
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Form(FormCollection collection)
        {
            string s = collection["title"];
            return Json(new { error = 0, msg = "用户信息失效，请重新登录" });
        }

        public JsonResult ImageUpload()
        {
            string domain_name = Request.Url.ToString().Replace(Request.RawUrl.ToString(), "");
            string id = Guid.NewGuid().ToString();
            string path = HttpRuntime.AppDomainAppPath;

            string data = new StreamReader(Request.InputStream, Encoding.UTF8).ReadToEnd();
            data = HttpUtility.UrlDecode(data);

            string Image_data = data.Split(',')[1];

            byte[] buffer = Convert.FromBase64String(Image_data);
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                using (Bitmap bmp = new Bitmap(ms))
                {
                    bmp.Save(path + "upload\\" + id + ".png", ImageFormat.Png);
                }
            }
            return Json(new { error = 0, data = domain_name + "/upload/" + id + ".png" });
        }

        public object ss() {
            return new { };
        }
    }
}
