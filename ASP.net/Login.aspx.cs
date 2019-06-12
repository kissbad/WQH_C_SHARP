using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Helpers;
using WQH.Data;
using BusinessClass;

namespace ASP.net
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string Username = this.Username.Text;
            string Password = this.Password.Text;

            user.From f = user.from_login_name;
            _user u = user.get_info(Username, f);

            if (u == null || u?.pass_word != WQH.String.CreateMD5Hash(Password))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script language=javascript>alert('帐号或密码错误!')</script>");
                return;
            }

            Response.Cookies.Remove(AntiForgeryConfig.CookieName);
            DateTime expTime = DateTime.Now.AddDays(1);

            var ticket = new FormsAuthenticationTicket(1,
                string.Format("{0}|{1}|{2}", u.user_id, u.login_name, u.pass_word), DateTime.Now, expTime, true,
                "9", FormsAuthentication.FormsCookiePath);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket))
            {
                HttpOnly = true,
                Expires = expTime
            };

            Response.Cookies.Set(cookie);
            Response.Redirect("Default.aspx", false);
        }
    }
}