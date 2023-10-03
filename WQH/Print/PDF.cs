using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spire.Pdf;
using Spire.Pdf.Print;

namespace WQH.Print
{
    public class PDF
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string file_path  {get;set;}
        public void Print(Printer printer) {

            PdfDocument doc = new PdfDocument();
            doc.LoadFromFile(file_path);
            //doc.PrintSettings.SelectPageRange(1, 5); //设置文档打印页码范围
            //doc.PrintSettings.SelectSomePages(newint[]{1,3,5,7}); //打印不连续的页面 

            doc.PrintSettings.PrinterName = printer.Printer_name;

            //彩色打印
            doc.PrintSettings.Color = printer.coler;

            //打印份数
            doc.PrintSettings.Copies = 1;

            //A4纸的标准尺寸595pt*842pt对PDF页面进行拆分打印
            //doc.PrintSettings.SelectSplitPageLayout();

            //获取原文档第一页的纸张大小,这里的单位是Point
            //SizeF size = doc.Pages[0].Size;
            //需要特别注意的是这里涉及到单位的转换，PaperSize的宽高参数默认单位是百英寸
            //PaperSize paper = new PaperSize("Custom", (int)size.Width / 72 * 100, (int)size.Height / 72 * 100);
            //设置打印的纸张大小为原来文档的大小
            //doc.PrintSettings.PaperSize = paper;

            //设置缩放模式、是否自动横纵向
            doc.PrintSettings.SelectSinglePageLayout(PdfSinglePageScalingMode.ActualSize, true);

            //如果支持则设置双面打印模式，可选：Default/Simplex/Horizontal/Vertical
            if (doc.PrintSettings.CanDuplex)                 
            { 
                doc.PrintSettings.Duplex = printer.duplex;
            }
            doc.Print();
        }
        
    }
}
