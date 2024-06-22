using System;
using System.Collections.Generic;
using System.IO;
using GoodsLib.Interfaces;
using GoodsLib.Models.Enum;
using GoodsLib.Models.Products;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace GoodsLib.Parsers
{
    public class SoyuzParser : IExcelParser<SoyuzProduct>
    {
        public IEnumerable<SoyuzProduct> Parse(Stream stream, ExcelFormat format, double markup, int round)
        {
            var consignment = new List<SoyuzProduct>();
            try
            {
                var woorkbook = format == ExcelFormat.Xlsx ? (IWorkbook)new XSSFWorkbook(stream) : new HSSFWorkbook(stream);
                var sheet = woorkbook.GetSheetAt(0);
                var headerRow = sheet.GetRow(9);
                int cellCount = headerRow.LastCellNum;

                for (var i = 10; i < sheet.LastRowNum; i++)
                {
                    List<string> list = new List<string>();
                    var row = sheet.GetRow(i);

                    if (row.FirstCellNum == -1)
                        return consignment;
                    var cell = row.GetCell(row.FirstCellNum);
                    if (string.IsNullOrEmpty(cell.ToString()))
                        return consignment;

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            list.Add(row.GetCell(j).Parse());
                        }
                    }
                    consignment.Add(new SoyuzProduct(list, markup, round));
                }
                return consignment;
            }
            catch (IOException e)
            {
                throw new ParseException("Файл занят другим приложением", e);
            }
            catch (Exception e)
            {
                throw new ParseException("Данные повреждены или выбран неправильный поставщик", e);
            }
        }
    }
}
