using GoodsLib.Entity;
using GoodsLib.Entity.Products;
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
    public class SoyuzParser
    {
        public IConsignment<SoyuzProduct> Parse(Stream stream, ExcelFormat format, double markup, int round)
        {
            IConsignment<SoyuzProduct> consignment = new Consignment<SoyuzProduct>();
            ISheet sheet;
            try
            {
                IWorkbook woorkbook;
                if (format == ExcelFormat.XLSX)
                    woorkbook = new XSSFWorkbook(stream);
                else
                    woorkbook = new HSSFWorkbook(stream);
                sheet = woorkbook.GetSheetAt(0);
                IRow headerRow = sheet.GetRow(9);
                int cellCount = headerRow.LastCellNum;

                for (int i = 10; i < sheet.LastRowNum; i++)
                {
                    List<string> list = new List<string>();
                    IRow row = sheet.GetRow(i);

                    if (row.FirstCellNum == -1)
                        return consignment;
                    var cell = row.GetCell(row.FirstCellNum);
                    if (string.IsNullOrEmpty(cell.ToString()))
                        return consignment;

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            list.Add(ParseCell(row.GetCell(j)));
                        }
                    }
                    consignment.Products.Add(new SoyuzProduct(list, markup, round));
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
                case CellType.Error:
                    return string.Empty;
                default:
                    return cell.StringCellValue;
            }
        }
    }
}
