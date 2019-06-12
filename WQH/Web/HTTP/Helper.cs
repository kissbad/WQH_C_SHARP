using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace WQH.Web.HTTP
{
    public class Helper
    {
        public static string FromXml(XmlDocument xml) {
            return xml.InnerXml.ToString();
        }

        public static string ToJson(SortedDictionary<string, string> p)
        {
            StringBuilder sb = new StringBuilder("{");
            foreach (var item in p)
            {
                sb.Append("\"" + item.Key + "\":\"" + item.Value + "\",");
            };
            return sb.Remove(sb.Length - 1, 1).Append("}").ToString();
        }

        public static string ToJson(Dictionary<string, string> p)
        {
            StringBuilder sb = new StringBuilder("{");
            foreach (var item in p)
            {
                sb.Append("\"" + item.Key + "\":\"" + item.Value + "\",");
            };
            return sb.Remove(sb.Length - 1, 1).Append("}").ToString();
        }

        public static string ToXML(SortedDictionary<string, string> p)
        {
            StringBuilder sb = new StringBuilder("<xml>");
            foreach (var item in p)
            {
                sb.Append("<" + item.Key + ">" + item.Value + "</" + item.Key + ">");
            };
            return sb.Append("</xml>").ToString();
        }

        //public static string ToURL<T>(T p)
        //{

        //    StringBuilder sb = new StringBuilder("<xml>");
        //    foreach (var item in p)
        //    {
        //        sb.Append("<" + item.Key + ">" + item.Value + "</" + item.Key + ">");
        //    };
        //    return sb.Append("</xml>").ToString();
        //}

        public static string ToXML(Dictionary<string, string> p)
        {
            StringBuilder sb = new StringBuilder("<xml>");
            foreach (var item in p)
            {
                sb.Append("<" + item.Key + ">" + item.Value + "</" + item.Key + ">");
            };
            return sb.Append("</xml>").ToString();
        }

        public static string Default(string p)
        {
            return p;
        }

        public static string GetString<T>(HttpWebRequest webrequest, T par, Func<T, string> p)
        {
            if (par != null)
            {
                string data = p(par);
                webrequest.ContentLength = Encoding.UTF8.GetBytes(data).Length;

                using (StreamWriter mywriter = new StreamWriter(webrequest.GetRequestStream()))
                {
                    mywriter.Write(data);
                }
            }

            string resp = string.Empty;
            using (HttpWebResponse webreponse = (HttpWebResponse)webrequest.GetResponse())
            {
                Encoding ecode = Encoding.Default;
                string ct = webreponse.ContentType;
                if (ct.Contains("UTF-8")|| ct.Contains("utf-8"))
                {
                    ecode = Encoding.UTF8;
                }
                using (Stream stream = webreponse.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, ecode))
                    {
                        resp = reader.ReadToEnd();
                    }
                }
            }
            return resp;
        }

        public static Bitmap GetBitmap<T>(HttpWebRequest webrequest, T par, Func<T, string> p)
        {
            Bitmap img = null;
            if (par != null)
            {
                string data = p(par);
                webrequest.ContentLength = Encoding.UTF8.GetBytes(data).Length;

                using (StreamWriter mywriter = new StreamWriter(webrequest.GetRequestStream()))
                {
                    mywriter.Write(data);
                }
            }

            using (HttpWebResponse webreponse = (HttpWebResponse)webrequest.GetResponse())
            {
                using (Stream stream = webreponse.GetResponseStream())
                {
                    img = new Bitmap(stream);
                }
            }
            return img;
        }
    }
}

