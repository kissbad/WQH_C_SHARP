using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using WQH.Data;


namespace ASP.net.WX_Pay
{
    /// <summary>
    /// notify 的摘要说明
    /// </summary>
    public class notify : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            StreamReader Reader = new StreamReader(context.Request.InputStream, Encoding.UTF8);
            string StrData = Reader.ReadToEnd();


            string sql = @"insert into notify_temp(return_msg,createtime)values(@return_msg,convert(varchar(19),getdate(),120))";
            System.Data.SqlClient.SqlParameter[] Paramet = new System.Data.SqlClient.SqlParameter[]{
                new System.Data.SqlClient.SqlParameter("@return_msg",StrData)
            };
            SqlHelper.ExecuteNonQuery(CommandType.Text, sql, Paramet);

            //string test = "<xml><appid><![CDATA[wxecb19ba014a17249]]></appid> <bank_type><![CDATA[CFT]]></bank_type> <cash_fee><![CDATA[1]]></cash_fee> <fee_type><![CDATA[CNY]]></fee_type> <is_subscribe><![CDATA[N]]></is_subscribe> <mch_id><![CDATA[1502239331]]></mch_id> <nonce_str><![CDATA[26DACC1FFEA84C5BB4B44DCEC85DFC9C]]></nonce_str> <openid><![CDATA[ohhel5CLw_2Jo8gCTc95fbjpCkvo]]></openid> <out_trade_no><![CDATA[XSD00000335]]></out_trade_no> <result_code><![CDATA[SUCCESS]]></result_code> <return_code><![CDATA[SUCCESS]]></return_code> <sign><![CDATA[966E2CB8FE25E7E40AC12DF688036FED]]></sign> <time_end><![CDATA[20180426094842]]></time_end> <total_fee>1</total_fee> <trade_type><![CDATA[JSAPI]]></trade_type> <transaction_id><![CDATA[4200000061201804266182981952]]></transaction_id> </xml>";

            StringReader sr = new StringReader(StrData);
            XmlTextReader xr = new XmlTextReader(sr);
            DataSet ds = new DataSet();
            ds.ReadXml(xr);


            string appid = ds.Tables["xml"].Rows[0]["appid"].ToString();
            string sql1 = @"select api_Secret from MiniProgramInfo where appId= @appid ";
            System.Data.SqlClient.SqlParameter[] Paramet1 = new System.Data.SqlClient.SqlParameter[]{
                new System.Data.SqlClient.SqlParameter("@appid",appid)
            };
            string key = SqlHelper.ExecuteScalar(CommandType.Text, sql1, Paramet1) as string;


            SortedDictionary<string, string> myDictionary = new SortedDictionary<string, string>();


            if (ds.Tables["xml"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["xml"].Columns.Count; i++)
                {
                    if (ds.Tables["xml"].Columns[i].ToString() != "sign")
                    {
                        myDictionary.Add(ds.Tables["xml"].Columns[i].ToString(), ds.Tables["xml"].Rows[0][i].ToString());
                    }
                }
            }

            string str1 = "";
            foreach (var item in myDictionary)
            {
                str1 += item.Key + "=" + item.Value + "&";
            };
            str1 += "key=" + key;


            if (ds.Tables["xml"].Rows[0]["sign"].ToString() == WQH.String.CreateMD5Hash(str1))
            {

                string sql2 = @"update a set is_pay = 1 from orders a where orderNo = @orderNo";

                System.Data.SqlClient.SqlParameter[] Paramet2 = new System.Data.SqlClient.SqlParameter[]{
                new System.Data.SqlClient.SqlParameter("@orderNo",ds.Tables["xml"].Rows[0]["out_trade_no"].ToString()),
                };
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql2, Paramet2);

                context.Response.ContentType = "application/json";
                context.Response.Write("<xml><return_code><![CDATA[SUCCESS]]></return_code><return_msg><![CDATA[OK]]></return_msg></xml>");
            }
            else
            {
                context.Response.ContentType = "application/json";
                context.Response.Write("<xml><return_code><![CDATA[FAIL]]></return_code><return_msg><![CDATA[签名校验失败]]></return_msg></xml>");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}