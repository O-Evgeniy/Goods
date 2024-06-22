using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel;
using System.Linq;
using GoodsLib.Models.Products;

namespace GoodsLib
{
    public static class ExcelBuilder
    {
        public static void Save(string filename, IList<ProductBase> products)
        {
            using var fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            var wb = new HSSFWorkbook();
            var sheet = wb.CreateSheet("Sheet1");
            var helper = wb.GetCreationHelper();
            int i = 0, j = 0;
            var row = sheet.CreateRow(i);
            var type = products.GetType().GetGenericArguments().Single();
            foreach (var p in type.GetProperties())
            {
                var attr = p.GetCustomAttributes(false).OfType<LabelAttribute>().FirstOrDefault();
                if (attr != null)
                {
                    var cell = row.CreateCell(j++);
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
                        var cell = row.CreateCell(j++);
                        var strValue = value == null ? string.Empty : value.ToString();
                        cell.SetCellValue(helper.CreateRichTextString(strValue));
                    }
                }
            }

            wb.Write(fs);
        }
    }
}