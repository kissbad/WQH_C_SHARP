using System.Reflection;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.InteropServices;

namespace WQH.Office
{
    public class Excel
    {
        public string FileName { get; set; }

        public Excel(string FileName) {
            this.FileName = FileName;
            Application app = new Application();
            Workbook excelDoc = null;
            Worksheet ws = (Worksheet)excelDoc.Sheets[0];
            Range r = ws.get_Range("A1", "A2");
            r.Value = "1111";
            excelDoc.SaveAs(FileName, XlFileFormat.xlWorkbookDefault, null, null, null,null, XlSaveAsAccessMode.xlExclusive);
            excelDoc.Close();
            app.Quit();
        }

        public static void SetData(object data) {
            
        }
    }
}
