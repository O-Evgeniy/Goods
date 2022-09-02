using System;
using System.Collections.Generic;
using System.IO;
using GoodsLib.Entity;
using GoodsLib.Interface;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace GoodsLib.Parser
{
    public class SimalandParser_V2
    {
         public IConsignment<SimaLandProduct> Parse(Stream stream, ExcelFormat format, double markup, int round)
        {
            IConsignment<SimaLandProduct> consignment = new Consignment<SimaLandProduct>();
            ISheet sheet;
            try
            {
                IWorkbook woorkbook;
                if (format == ExcelFormat.XLSX)
                    woorkbook = new XSSFWorkbook(stream);
                else
                    woorkbook = new HSSFWorkbook(stream);
                sheet = woorkbook.GetSheetAt(0);
                IRow headerRow = sheet.GetRow(18);
                int cellCount = headerRow.LastCellNum;

                for (int i = 19; i < sheet.LastRowNum; i++)
                {
                    List<string> list = new List<string>();
                    IRow row = sheet.GetRow(i);
                    var cell = row.GetCell(row.FirstCellNum);
                    if (cell == null)
                        return consignment;
                    if (string.IsNullOrEmpty(cell.ToString()))
                        return consignment;
                    if (cell.CellType == CellType.Error)
                        break;
                    foreach (var num in ValueableCellsNumber)
                    {
                        cell = row.GetCell(num - 1);
                        if (cell != null)
                        {
                            var value = ParseCell(cell);
                            list.Add(value);
                        }
                    }
                    consignment.Products.Add(new SimaLandProduct(list, markup, round));
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

        List<int> ValueableCellsNumber = new List<int>()
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
            55,  // Сумма
            62,  // Объем
            70   // Номер заказа
        };

        private static string ParseCell(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Numeric:
                    return cell.NumericCellValue.ToString();
                default:
                    return cell.CellType == CellType.Error ? string.Empty : cell.StringCellValue;
            }
        }
    }
}