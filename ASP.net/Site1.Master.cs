using BusinessClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WQH.Data;

namespace ASP.net
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_info"] == null)
            {
                var cookies = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (cookies == null)
                {
                    Response.Redirect("Login.aspx", false);
                    return;
                }

                var ticket = FormsAuthentication.Decrypt(cookies.Value);
                var arrTicketName = ticket.Name.Split('|');
                if (ticket.Expired)
                {
                    FormsAuthentication.SignOut();
                }
                else
                {
                    user.From f = user.from_id;
                    _user u = user.get_info(arrTicketName[0], f);


                    if (u == null || u?.pass_word != arrTicketName[2])
                    {
                        FormsAuthentication.SignOut();
                        Response.Redirect("Login.aspx", false);
                        return;
                    }
                    else
                    {
                        Session["user_info"] = u;
                    }
                }
            }

            TreeView2.DataSourceID = "web";
        }
    }
}