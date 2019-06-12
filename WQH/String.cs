using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WQH
{
    public class String
    {
        // Methods
        public static bool is_CN(string str)
        {
            Regex regex = new Regex("^[一-龥]$");
            return regex.IsMatch(str);
        }

        public static bool is_Numeric(string str)
        {
            Regex regex = new Regex("^[0-9]+(.[0-9]{1,3})?$");
            return regex.IsMatch(str);
        }

        public static bool is_Integer(string str)
        {
            Regex regex = new Regex(@"^[+]{0,1}(\d+)$");
            return regex.IsMatch(str);
        }

        public static bool is_Phone(string str)
        {
            if (is_Integer(str))
            {
                byte[] bytes = Encoding.Default.GetBytes(str);
                int length = bytes.Length;
                if (length == 11)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        public static string CreateMD5Hash(string input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString().ToUpper();
        }
    }
}
