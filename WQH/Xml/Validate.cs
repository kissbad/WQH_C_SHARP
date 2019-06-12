using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace WQH.Xml
{
    public class Validate
    {
        /// <summary>
        /// XSD验证
        /// </summary>
        /// <param name="xmlFile">XML文件名</param>
        /// <param name="xsdFile">XSD文件名</param>
        /// <param name="namespaceUrl"></param>
        /// <returns></returns>
        public static string XmlValidationByXsd(string xmlFile, string xsdFile, string namespaceUrl = null)
        {
            StringBuilder sb = new StringBuilder();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add(namespaceUrl, xsdFile);
            settings.ValidationEventHandler += (x, y) =>
            {
                sb.AppendFormat("{0}|", y.Message);
            };
            using (XmlReader reader = XmlReader.Create(xmlFile, settings))
            {
                try
                {
                    while (reader.Read()) ;
                }
                catch (XmlException ex)
                {
                    sb.AppendFormat("{0}|", ex.Message);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// XSD验证
        /// </summary>
        /// <param name="xmlText">XML内容文本</param>
        /// <param name="schemaFile">XSD文件名</param>
        /// <returns></returns>
        public static string XmlValidationByXsd(string xmlText, string schemaFile)
        {
            StringBuilder sb = new StringBuilder();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add(null, schemaFile);
            settings.ValidationEventHandler += (x, y) =>
            {
                sb.AppendFormat("{0}\n", y.Message);
            };
            using (XmlReader reader = XmlReader.Create(new StringReader(xmlText), settings))
            {
                try
                {
                    while (reader.Read()) ;
                }
                catch (XmlException ex)
                {
                    sb.AppendFormat("{0}\n", ex.Message);
                }
            }
            return sb.ToString();
        }
    }
}
