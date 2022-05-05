using GoodsLib.Entity;
using GoodsLib.Interface;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GoodsLib.Parser
{
    public class TousParser
    {
        public IConsignment<TousProduct> Parse(string filename, ExcelFormat format, double markup, int round)
        {
            IConsignment<TousProduct> consignment = new Consignment<TousProduct>();
            ISheet sheet;
            try
            {
                using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook woorkbook;
                    if (format == ExcelFormat.XLSX)
                        woorkbook = new XSSFWorkbook(stream);
                    else
                        woorkbook = new HSSFWorkbook(stream);
                    sheet = woorkbook.GetSheetAt(0);
                    IRow headerRow = sheet.GetRow(12);
                    int cellCount = headerRow.LastCellNum;

                    for (int i = 13; i < sheet.LastRowNum; i++)
                    {
                        List<string> list = new List<string>();
                        IRow row = sheet.GetRow(i);
                        var cell = row.GetCell(row.FirstCellNum);
                        if (string.IsNullOrEmpty(cell.ToString()))
                            return consignment;
                        string prevValue = null;
                        ICell first = row.GetCell(row.FirstCellNum);
                        if (first.CellType == CellType.Error)
                            break;
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            cell = row.GetCell(j);

                            if (cell != null)
                            {
                                var value = ParseCell(cell);
                                if (value == string.Empty)
                                    continue;
                                if (cell.Address.Equals(first.Address) && value == string.Empty)
                                    break;
                                list.Add(value);
                                prevValue = value;
                            }
                        }
                        if (list.Count == 0)
                            break;
                        consignment.Products.Add(new TousProduct(list, markup, round));
                    }
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
