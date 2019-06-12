using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSOA
{
    public class PassWord
    {
        private static string[] MODS = { "ke3wms", "jnx9i8", "dpqzl6", "vyg1bh", "7a4c2f", "u5rto0" };
        private static string[] SDOM = { "wa6j1c", "fblrk7", "mg9q0s", "vu3zth", "po4xy2", "ni5d8e" };
        /// <summary>
        /// 加密（西文要小写、29位内）
        /// </summary>
        /// <param name="pw"></param>
        /// <returns></returns>
        public static string Encryptpassword(string pw)
        {
            string result = lattertosixstr(pw, MODS);
            int nLength = result.Length;
            //余数
            int remainder = 0;
            //3位六进制数
            for (int i = 0; i < 3; i++)
            {
                remainder = nLength % 6;
                nLength = nLength / 6;
                result = remainder.ToString() + result;
            }
            //随机补够60位
            Random rd = new Random();
            while (result.Length < 60)
            {
                result = result + rd.Next(0, 6).ToString();
            }
            result = sixstrtolatter(result, SDOM);
            return result;

        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="kl"></param>
        /// <returns></returns>
        public static string Discryptpassword(string kl)
        {
            string result = lattertosixstr(kl, SDOM);
            if (result.Length < 3)
            {
                return "";
            }
            else
            {
                int temp = 0;
                for (int i = 0; i < 3; i++)
                {
                    var x = int.Parse(result.Substring(i, 1));
                    temp = (temp * 6) + x;
                }
                result = result.Substring(3, temp);
                result = sixstrtolatter(result, MODS);
                return result;
            }
        }

        private static string lattertosixstr(string pw, string[] tmods)
        {
            string tmps1 = string.Empty;
            string tmps2 = string.Empty;
            for (int i = 0; i < pw.Length; i++)
            {
                for (int n = 0; n < 6; n++)
                {
                    int p = tmods[n].IndexOf(pw[i]);
                    if (p != -1)
                    {
                        tmps1 = tmps1 + n.ToString();
                        tmps2 = p.ToString() + tmps2;
                        break;
                    }
                }
            }
            return tmps1 + tmps2;
        }

        private static string sixstrtolatter(string str, string[] tmods)
        {
            int n = str.Length;
            string result = string.Empty;
            if (n % 2 != 1)
            {
                for (int i = 0; i < n / 2; i++)
                {
                    int x = int.Parse(str.Substring(i, 1));
                    int y = int.Parse(str.Substring(n - 1 - i, 1));
                    result = result + tmods[x][y];
                }
            }
            return result;
        }
    }
}
