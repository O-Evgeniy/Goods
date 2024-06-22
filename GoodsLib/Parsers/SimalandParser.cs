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
    public class SimalandParser : IExcelParser<SimaLandProduct>
    {
        public IEnumerable<SimaLandProduct> Parse(Stream stream, ExcelFormat format, double markup, int round)
        {
            var consignment = new List<SimaLandProduct>();
            try
            {
                var woorkbook = format == ExcelFormat.Xlsx ? (IWorkbook)new XSSFWorkbook(stream) : new HSSFWorkbook(stream);
                var sheet = woorkbook.GetSheetAt(0);

                for (var i = 15; i < sheet.LastRowNum; i++)
                {
                    var list = new List<string>();
                    var row = sheet.GetRow(i);
                    var cell = row.GetCell(row.FirstCellNum);
                    if (cell == null)
                        return consignment;
                    if (string.IsNullOrEmpty(cell.ToString()))
                        return consignment;
                    if (cell.CellType == CellType.Error)
                        break;
                    foreach (var num in _valueableCellsNumber)
                    {
                        cell = row.GetCell(num - 1);
                        if (cell != null)
                        {
                            list.Add( cell.Parse());
                        }
                    }
                    consignment.Add(new SimaLandProduct(list, markup, round));
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
            2,   // №
            4,   // Штрихкод
            9,   // Код
            13,  // Товары (работы,услуги)
            29,  // Инфо
            32,  // Количество
            37,  // Розничная цена
            38,  // Цена
            42,  // Сумма (без скидки)
            47,  // Скидка
            51,  // Цена (со скидкой)
            52,  // Сумма
            56,  // Объем
            73   // Номер заказа
        };
    }
}
