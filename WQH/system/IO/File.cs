using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace WQH.system.IO
{
    public class File
    {
        //public static readonly string Logath = ConfigurationManager.AppSettings["Logath"].ToString();
        public static readonly string Logath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/log/";
        // Methods
        public static void WriteLog(string strMsg, int is_sj)
        {
            string str = System.DateTime.Now.ToString();
            if (!Directory.Exists(Logath))
            {
                Directory.CreateDirectory(Logath);
            }
            FileStream stream = new FileStream(Logath + System.DateTime.Today.ToString("yyyyMMdd") + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            writer.BaseStream.Seek(0L, SeekOrigin.End);
            if (is_sj == 1)
            {
                writer.WriteLine(str + ": " + strMsg);
            }
            else
            {
                writer.WriteLine(strMsg);
            }
            writer.Flush();
            writer.Close();
            stream.Close();
        }
    }
}
