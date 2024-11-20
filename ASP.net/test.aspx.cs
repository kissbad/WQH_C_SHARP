using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace ASP.net
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label1.Text = KSOA.PassWord.Encryptpassword(TextBox1.Text);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Label2.Text = KSOA.PassWord.Discryptpassword(TextBox2.Text);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            var a = BusinessClass.TaoBao.tbk_DgMaterialOptional.get("https://detail.tmall.com/item.htm?id=525706128826&spm=a230r.1.1997525049.3.29d13ad1T7p6yi");

        }

        public class test_o
        {
            public string A1 { get; set; } = "1";
            public string A2 { get; set; } = "2";
            public string A3 { get; set; } = "3";
            public string A4 { get; set; } = "4";
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {
            List<test_o> list = new List<test_o>();
            list.Add(new test_o());
            WQH.Office.Excel.cExcel<test_o>(list);
        }
    }
}