using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace WQH.Web.FTP
{
    public class Helper
    {
        public static FtpStatusCode A1(string st, string day)
        {
            string str = ConfigurationManager.AppSettings["FTP_uri"].ToString();
            string userName = ConfigurationManager.AppSettings["FTP_um"].ToString();
            string password = ConfigurationManager.AppSettings["FTP_pw"].ToString();
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(str + "XSQD_ST_" + day + "_01.txt");
            request.Method = "STOR";
            request.Credentials = new NetworkCredential(userName, password);
            byte[] bytes = Encoding.UTF8.GetBytes(st);
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Flush();
            requestStream.Close();
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            return response.StatusCode;
        }
    }
}
