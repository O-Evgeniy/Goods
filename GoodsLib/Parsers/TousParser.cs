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
    public class TousParser : IExcelParser<TousProduct>
    {
        public IEnumerable<TousProduct> Parse(Stream stream, ExcelFormat format, double markup, int round)
        {
            var consignment = new List<TousProduct>();
            try
            {
                var woorkbook = format == ExcelFormat.Xlsx ? (IWorkbook)new XSSFWorkbook(stream) : new HSSFWorkbook(stream);
                var sheet = woorkbook.GetSheetAt(0);

                for (var i = 13; i < sheet.LastRowNum; i++)
                {
                    var list = new List<string>();
                    var row = sheet.GetRow(i);
                    var cell = row.GetCell(row.FirstCellNum);
                    if (string.IsNullOrEmpty(cell.ToString()))
                        return consignment;
                    if (cell.CellType == CellType.Error)
                        break;
                    foreach (var num in _valueableCellsNumber)
                    {
                        cell = row.GetCell(num - 1);
                        if (cell != null)
                        {
                            list.Add(cell.Parse());
                        }
                    }
                    consignment.Add(new TousProduct(list, markup, round));
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

        private readonly List<int> _valueableCellsNumber = new List<int>()
        {
            2,
            4,
            12,
            19,
            30,
            52,
            56,
            60,
            66,
            69,
            74
        };
    }
}
