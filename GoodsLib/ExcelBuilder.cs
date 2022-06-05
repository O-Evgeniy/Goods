using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using GoodsLib.Entity;
using System.Reflection;
using System.Linq;

namespace GoodsLib
{
    public static class ExcelBuilder
    {
        public static void Save(string filename, IList<ProductBase> products, ExcelFormat format)
        {
            using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                IWorkbook wb;
                if (format == ExcelFormat.XLS)
                    wb = new HSSFWorkbook();
                else
                    wb = new XSSFWorkbook();

                ISheet sheet = wb.CreateSheet("Sheet1");
                ICreationHelper helper = wb.GetCreationHelper();
                int i = 0, j = 0;
                IRow row = sheet.CreateRow(i);
                Type type = products.GetType().GetGenericArguments().Single();
                foreach (var p in type.GetProperties())
                {
                    var attr = p.GetCustomAttributes(false).OfType<LabelAttribute>().FirstOrDefault();
                    if (attr != null)
                    {
                        ICell cell = row.CreateCell(j++);
                        cell.SetCellValue(helper.CreateRichTextString(attr.Label));

                    }
                }

                for (i = 1; i <= products.Count; i++)
                {
                    j = 0;
                    row = sheet.CreateRow(i);
                    foreach (var p in type.GetProperties())
                    {
                        var attr = p.GetCustomAttributes(false).OfType<LabelAttribute>().FirstOrDefault();
                        if (attr != null)
                        {
                            var value = p.GetValue(products[i - 1]);
                            ICell cell = row.CreateCell(j++);
                            var strValue = value == null ? string.Empty : value.ToString();
                            cell.SetCellValue(helper.CreateRichTextString(strValue));
                        }
                    }
                }
                wb.Write(fs);
            }
        }

        public static IWorkbook GetBook(IList<ProductBase> products, ExcelFormat format)
        {

            IWorkbook wb;
            if (format == ExcelFormat.XLS)
                wb = new HSSFWorkbook();
            else
                wb = new XSSFWorkbook();

            ISheet sheet = wb.CreateSheet("Sheet1");
            ICreationHelper helper = wb.GetCreationHelper();
            int i = 0, j = 0;
            IRow row = sheet.CreateRow(i);
            Type type = products.GetType().GetGenericArguments().Single();
            foreach (var p in type.GetProperties())
            {
                var attr = p.GetCustomAttributes(false).OfType<LabelAttribute>().FirstOrDefault();
                if (attr != null)
                {
                    ICell cell = row.CreateCell(j++);
                    cell.SetCellValue(helper.CreateRichTextString(attr.Label));

                }
            }

            for (i = 1; i <= products.Count; i++)
            {
                j = 0;
                row = sheet.CreateRow(i);
                foreach (var p in type.GetProperties())
                {
                    var attr = p.GetCustomAttributes(false).OfType<LabelAttribute>().FirstOrDefault();
                    if (attr != null)
                    {
                        var value = p.GetValue(products[i - 1]);
                        ICell cell = row.CreateCell(j++);
                        var strValue = value == null ? string.Empty : value.ToString();
                        cell.SetCellValue(helper.CreateRichTextString(strValue));
                    }
                }
            }
            return wb;
        }
    }
}
