using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Security.Cryptography.Xml;

namespace WQH.Xml
{
    /// <summary>
    /// 签名
    /// </summary>
    public class Sign
    {
        /// <summary>
        /// RSA算法证书签名XML文件（药监项目）
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="Key"></param>
        /// <param name="MSCert"></param>
        /// <returns></returns>
        public static XmlElement SignXmlFile(XmlDocument doc, RSA Key, X509Certificate2 MSCert)
        {
            doc.PreserveWhitespace = false;

            SignedXml signedXml = new SignedXml(doc);
            signedXml.SigningKey = Key;
            Reference reference = new Reference();
            reference.Uri = "";
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);
            signedXml.AddReference(reference);


            X509IssuerSerial xserial;
            xserial.IssuerName = MSCert.IssuerName.Name;
            xserial.SerialNumber = MSCert.SerialNumber;

            KeyInfo keyInfo = new KeyInfo();
            KeyInfoX509Data kdata = new KeyInfoX509Data(MSCert);
            kdata.AddIssuerSerial(xserial.IssuerName, xserial.SerialNumber);
            keyInfo.AddClause(kdata);


            signedXml.KeyInfo = keyInfo;
            signedXml.ComputeSignature();
            XmlElement xmlDigitalSignature = signedXml.GetXml();
            return xmlDigitalSignature;
        }

        /// <summary>
        /// 未通
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] SignData(byte[] data,CngKey key) {
            using (ECDsaCng ecdsa = new ECDsaCng(key)) {
                return ecdsa.SignData(data);
            }
        }
        public static void SignXml(XmlDocument xmlDoc, RSA rsaKey)
        {
            // Check arguments.
            if (xmlDoc == null)
                throw new ArgumentException(nameof(xmlDoc));
            if (rsaKey == null)
                throw new ArgumentException(nameof(rsaKey));

            // Create a SignedXml object.
            SignedXml signedXml = new SignedXml(xmlDoc);

            // Add the key to the SignedXml document.
            signedXml.SigningKey = rsaKey;

            // Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = "";

            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            // Append the element to the XML document.
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
        }
        public static void Encrypt(XmlDocument Doc, string ElementToEncrypt, X509Certificate2 Cert)
        {
            // Check the arguments.  
            if (Doc == null)
                throw new ArgumentNullException("Doc");
            if (ElementToEncrypt == null)
                throw new ArgumentNullException("ElementToEncrypt");
            if (Cert == null)
                throw new ArgumentNullException("Cert");

            XmlElement elementToEncrypt = Doc.GetElementsByTagName(ElementToEncrypt)[0] as XmlElement;
            if (elementToEncrypt == null)
            {
                throw new XmlException("The specified element was not found");

            }

            EncryptedXml eXml = new EncryptedXml();
            EncryptedData edElement = eXml.Encrypt(elementToEncrypt, Cert);
            EncryptedXml.ReplaceElement(elementToEncrypt, edElement, false);
        }
    }
}
