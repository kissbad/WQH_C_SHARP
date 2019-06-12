using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP.net.Page
{
    public partial class QueryCoupon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var goods_id = Int64.Parse(TextBox1.Text);
            var ActivityId = TextBox2.Text;
            var x = BusinessClass.TaoBao.tbk_coupon.get(goods_id, ActivityId,null);
        }
    }
}