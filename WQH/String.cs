﻿using System;
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
        /// <summary>
        /// 手机号码11位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 日期格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        public static string FormatTime(string str)
        {
            string[] arr = { };
            if (str.Contains('-'))
            {
                arr = str.Split('-');
            }
            else if (str.Contains('.'))
            {
                arr = str.Split('.');
            }
            else if (str.Contains('/'))
            {
                arr = str.Split('/');
            }

            try
            {
                DateTime dateTime = new DateTime(int.Parse(arr[0]), int.Parse(arr[1]), int.Parse(arr[2]));
                return dateTime.ToString("yyyy-MM-dd");
            }
            catch (Exception e)
            {
                throw new ApplicationException("日期转换失败");
            }
        }

        public static List<string> Split(string st, int Length)
        {
            int i = (int)Math.Ceiling((decimal)st.Length / Length);
            WQH.system.IO.File.WriteLog(i.ToString(), 1);
            List<string> arr = new List<string>();
            for (int j = 0; j < i; j++)
            {
                if (j == i -1)
                {
                    arr.Add(st.Substring(j * Length, st.Length - j * Length));
                }
                else
                {
                    arr.Add(st.Substring(j * Length, Length));
                }
            }
            return arr;

        }
    }
}
