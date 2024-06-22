using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using GoodsLib.Entity;
using GoodsLib.Entity.Products;
using GoodsLib.Interface;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace GoodsLib.Parser
{
    public class MarkerParserV2
    {
        public IConsignment<MarkerProductV2> Parse(Stream stream, ExcelFormat format, double markup, int round)
        {
            IConsignment<MarkerProductV2> consignment = new Consignment<MarkerProductV2>();
            ISheet sheet;
            try
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
                    if (i == 64)
                    {
                    }

                    List<string> list = new List<string>();
                    IRow row = sheet.GetRow(i);
                    var firstCell = row.GetCell(row.FirstCellNum);
                    if (string.IsNullOrEmpty(firstCell.ToString()))
                        return consignment;

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        var cell = row.GetCell(j);

                        Debug.Assert(cell.CellType is CellType.Error);

                        if (j == row.FirstCellNum && cell.CellType is CellType.Error)
                        {
                            return consignment;
                        }

                        if (cell != null)
                        {
                            if (j == 8)
                                continue;
                            list.Add(ParseCell(cell));
                        }
                    }

                    consignment.Products.Add(new MarkerProductV2(list, markup, round));
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