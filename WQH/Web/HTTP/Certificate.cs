using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace WQH.Web.HTTP
{
    public class Certificate
    {
        public static X509Certificate get_Certificates(string cert_path, string password)
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            X509Certificate cer = new X509Certificate(cert_path, password);
            return cer;
        }

        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            if (errors == SslPolicyErrors.None)
                return true;
            return false;
        }

        public static X509Certificate2 get_Certificates2(string cert_path, string password)
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            X509Certificate2 cer2 = new X509Certificate2(cert_path, password);
            return cer2;
        }

        /// <summary>
        /// 获取 X.509 证书
        /// </summary>
        /// <param name="storeNmae">指定 X.509 证书存储的名称的枚举值之一</param>
        /// <param name="CurrentUser">指定 X.509 证书存储位置的枚举值之一</param>
        /// <param name="certName">证书名称</param>
        /// <returns></returns>
        public static X509Certificate2 GetCertificateFromStore(StoreName storeNmae, StoreLocation CurrentUser, string certName)
        {
            X509Store store = new X509Store(storeNmae, CurrentUser);
            try
            {
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection certCollection = store.Certificates;
                X509Certificate2Collection currentCerts = certCollection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                X509Certificate2Collection signingCert = currentCerts.Find(X509FindType.FindBySubjectName, certName, false);
                if (signingCert.Count == 0)
                    return null;
                return signingCert[0];
            }
            finally
            {
                store.Close();
            }

        }

        public static X509Certificate2 GetCertificateFromFriendlyName(StoreName storeNmae, StoreLocation CurrentUser, string FriendlyName)
        {
            X509Store store = new X509Store(storeNmae, CurrentUser);
            X509Certificate2 cer = null;
            try
            {
                store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
                X509Certificate2Collection certCollection = store.Certificates;
                X509Certificate2Collection currentCerts = certCollection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
                int c = currentCerts.Count;
                foreach (var i in currentCerts)
                {
                    if (i.FriendlyName == FriendlyName)
                        cer = i;
                }
                return cer;
            }
            finally
            {
                store.Close();
            }

        }
    }
}
