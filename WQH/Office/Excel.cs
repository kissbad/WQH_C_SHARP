using System.Reflection;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.InteropServices;
using System;
using System.Text;
using System.Collections.Generic;

namespace WQH.Office
{
    public class Excel
    {
        public static string Logath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Excel\\";

        public static void cExcel<T>(List<T> list) {
            string FileName = Guid.NewGuid().ToString() + ".xlsx";
            if (!Directory.Exists(Logath))
            {
                Directory.CreateDirectory(Logath);
            }

            Application excelApp = new Application();
            if (excelApp == null)
            {
                throw (new Exception("抱歉在你的电脑上没有安装Excel"));
            }

            //判断指定路径中是否存在要创建的Excel
            Workbook workBook;
            if (File.Exists(Logath + FileName))
            {
                workBook = excelApp.Workbooks.Open(Logath + FileName);
            }
            else
            {
                workBook = excelApp.Workbooks.Add(true);
            }

            Worksheet workSheet = workBook.ActiveSheet as Worksheet;

            
            Type tp = typeof(T);
            PropertyInfo[] proInfos = tp.GetProperties();
            var i = 1;
            foreach (var item in proInfos)
            {
                Range a = workSheet.Cells[i++][1];
                a.Value = item.Name;
            }
            var x = 2;
            foreach (var item in list)
            {
                var y = 1;
                foreach (var proInfo in proInfos)
                {
                    object obj = proInfo.GetValue(item, null);
                    if (obj == null)
                    {
                        continue;
                    }
                    Range a = workSheet.Cells[y++][x];
                    a.Value = obj;
                }
                x++;
            }

            excelApp.Visible = false;
            excelApp.DisplayAlerts = false;

            workBook.SaveAs(Logath + FileName);
            workBook.Close(false, Missing.Value, Missing.Value);

            excelApp.Quit();
            GC.Collect();
        }

        public static void SetData(object data) {
            
        }
    }
}
