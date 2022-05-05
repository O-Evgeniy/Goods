using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using NPOI;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using GoodsLib.Interface;
using GoodsLib.Entity;
namespace GoodsLib.Parser
{
    public class MarkerParser
    {
        public IConsignment<MarkerProduct> Parse(string filename, ExcelFormat format, double markup, int round)
        {
            IConsignment<MarkerProduct> consignment = new Consignment<MarkerProduct>();
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
                    IRow headerRow = sheet.GetRow(7);
                    int cellCount = headerRow.LastCellNum;

                    for (int i = 9; i < sheet.LastRowNum; i++)
                    {
                        List<string> list = new List<string>();
                        IRow row = sheet.GetRow(i);
                        var cell = row.GetCell(row.FirstCellNum);
                        if (string.IsNullOrEmpty(cell.ToString()))
                            return consignment;

                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                            {
                                if (j == 5)
                                    continue;
                                list.Add(ParseCell(row.GetCell(j)));
                            }
                        }
                        consignment.Products.Add(new MarkerProduct(list, markup, round));
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
                throw new ParseException("Данные повреждены или выбран неправильный поставщик",e);
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
                    return cell.StringCellValue;
            }
        }
    }
}