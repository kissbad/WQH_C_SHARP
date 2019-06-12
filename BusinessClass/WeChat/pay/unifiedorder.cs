using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using WQH.Data;

namespace BusinessClass.WeChat.pay
{
    public class unifiedorder
    {
        public static void to_unifiedorder(string appid, string orderNo)
        {

            string sql1 = @"select a.appId,a.openid,a.amountPay,b.mch_id,b.appName,b.api_Secret
from orders a
join dbo.MiniProgramInfo b on a.appId = b.appId
where a.orderNo = @orderNo and a.appId= @appid";

            System.Data.SqlClient.SqlParameter[] a = new System.Data.SqlClient.SqlParameter[]{
                new System.Data.SqlClient.SqlParameter("@appId",appid),
                new System.Data.SqlClient.SqlParameter("@orderNo",orderNo),
            };
            DataTable dt = SqlHelper.ExecuteReader(CommandType.Text, sql1, a);


            string appId = dt.Rows[0]["appId"].ToString();
            string openid = dt.Rows[0]["openid"].ToString();
            string amountPay = dt.Rows[0]["amountPay"].ToString();
            string mch_id = dt.Rows[0]["mch_id"].ToString();
            string appName = dt.Rows[0]["appName"].ToString();
            string api_Secret = dt.Rows[0]["api_Secret"].ToString();

            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";

            SortedDictionary<string, string> myDictionary = new SortedDictionary<string, string>();
            myDictionary.Add("appid", appId);
            myDictionary.Add("body", appName + "购物单");
            myDictionary.Add("mch_id", mch_id);
            myDictionary.Add("nonce_str", Guid.NewGuid().ToString().Replace("-", "").ToUpper());
            myDictionary.Add("notify_url", "https://snait.cn/WX_pay/notify.ashx");
            myDictionary.Add("openid", openid);
            myDictionary.Add("out_trade_no", orderNo);
            myDictionary.Add("spbill_create_ip", "111.230.176.187");
            myDictionary.Add("total_fee", Math.Round(decimal.Parse(amountPay) * 100, 0).ToString());
            myDictionary.Add("trade_type", "JSAPI");

            StringBuilder str1 = new StringBuilder();
            StringBuilder payStaff = new StringBuilder("<xml>");

            foreach (var item in myDictionary)
            {
                str1.Append(item.Key + "=" + item.Value + "&");
                payStaff.Append("<" + item.Key + ">" + item.Value + "</" + item.Key + ">");
            };
            str1.Append("key=" + api_Secret);
            payStaff.Append("<sign>" + WQH.String.CreateMD5Hash(str1.ToString()) + "</sign></xml>");


            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.ClientCertificates.Add(WQH.Web.HTTP.Certificate.get_Certificates("cert_path", "password"));
            webrequest.Method = "post";
            webrequest.ContentType = "text/xml";//发送内容格式
            webrequest.ContentLength = Encoding.UTF8.GetBytes(payStaff.ToString()).Length;

            using (StreamWriter mywriter = new StreamWriter(webrequest.GetRequestStream()))
            {
                mywriter.Write(payStaff);
            }

            string resp = string.Empty;
            using (HttpWebResponse webreponse = (HttpWebResponse)webrequest.GetResponse())
            {
                using (Stream stream = webreponse.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        resp = reader.ReadToEnd();
                    }
                }
            }

            StringReader sr = new StringReader(resp);
            XmlTextReader xr = new XmlTextReader(sr);
            DataSet ds = new DataSet();
            ds.ReadXml(xr);

            if (ds.Tables[0].Rows[0][0].ToString() == "SUCCESS")
            {

                string sql = @"insert into unifiedorder(orderNo,createtime,appid,return_code,return_msg,mch_id,nonce_str,[sign],result_code,prepay_id,trade_type)
values(@orderNo,convert(varchar(19),getdate(),120),@appid,@return_code,@return_msg,@mch_id,@nonce_str,@sign,@result_code,@prepay_id,@trade_type)";

                System.Data.SqlClient.SqlParameter[] Paramet = new System.Data.SqlClient.SqlParameter[]{
                new System.Data.SqlClient.SqlParameter("@orderNo",orderNo),
                new System.Data.SqlClient.SqlParameter("@return_code",ds.Tables[0].Rows[0][0].ToString()),
                new System.Data.SqlClient.SqlParameter("@return_msg",ds.Tables[0].Rows[0][1].ToString()),
                new System.Data.SqlClient.SqlParameter("@appid",ds.Tables[0].Rows[0][2].ToString()),
                new System.Data.SqlClient.SqlParameter("@mch_id",ds.Tables[0].Rows[0][3].ToString()),
                new System.Data.SqlClient.SqlParameter("@nonce_str",ds.Tables[0].Rows[0][4].ToString()),
                new System.Data.SqlClient.SqlParameter("@sign",ds.Tables[0].Rows[0][5].ToString()),
                new System.Data.SqlClient.SqlParameter("@result_code",ds.Tables[0].Rows[0][6].ToString()),
                new System.Data.SqlClient.SqlParameter("@prepay_id",ds.Tables[0].Rows[0][7].ToString()),
                new System.Data.SqlClient.SqlParameter("@trade_type",ds.Tables[0].Rows[0][8].ToString()),
                new System.Data.SqlClient.SqlParameter("@openid",openid)
                };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql, Paramet);
            }
        }
    }
}
