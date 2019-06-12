using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP.net.Page
{
    public partial class QueryGoods : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string key = TextBox1.Text;
            BusinessClass.TaoBao.tbk_DgMaterialOptional goods = BusinessClass.TaoBao.tbk_DgMaterialOptional.get(key);
        }
    }
}