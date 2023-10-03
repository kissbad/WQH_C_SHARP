using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;

namespace WQH.Print
{
    public class Printer
    {
        /// <summary>
        /// 打印机名称
        /// </summary>
        public string Printer_name { get; set; }
        /// <summary>
        /// 双面打印设置
        /// </summary>
        public Duplex duplex { get; set; } = Duplex.Default;
        /// <summary>
        /// 是否彩色打印
        /// </summary>
        public bool coler { get; set; } = false;
        public Printer(string Printer_name) {
            this.Printer_name = Printer_name;
        }
    }
}
