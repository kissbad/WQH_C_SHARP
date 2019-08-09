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
            XmlDocument xml = new XmlDocument();
            XmlElement Object = xml.CreateElement("Object");
            Object.SetAttribute("Id", "DataObjectId");
            XmlElement jh = xml.CreateElement("jh");
            jh.SetAttribute("xmlns", "http://cmsland.com/yjj_qyjk_cgrk.xsd");
            Object.AppendChild(jh);
            xml.AppendChild(Object);

            string url = "https://219.135.157.154:8445/data/clientAccept/toUploadFile.do";
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(url);
            webrequest.ClientCertificates.Add(
                WQH.Web.HTTP.Certificate.GetCertificateFromStore(System.Security.Cryptography.X509Certificates.StoreName.
                My, System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine,
                "广东三丰医药有限公司"));
            webrequest.Method = "post";

            Func<XmlDocument, string> p = WQH.Web.HTTP.Helper.FromXml;
            string request = WQH.Web.HTTP.Helper.GetString<XmlDocument>(webrequest, xml, p);
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