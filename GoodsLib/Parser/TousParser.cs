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
        public IConsignment<TousProduct> Parse(Stream stream, ExcelFormat format, double markup, int round)
        {
            IConsignment<TousProduct> consignment = new Consignment<TousProduct>();
            ISheet sheet;
            try
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
                    consignment.Products.Add(new TousProduct(list, markup, round));
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

        //public IConsignment<TousProduct> Parse(string filename, ExcelFormat format, double markup, int round)
        //{
        //    IConsignment<TousProduct> consignment = new Consignment<TousProduct>();
        //    ISheet sheet;
        //    try
        //    {
        //        using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
        //        {
        //            IWorkbook woorkbook;
        //            if (format == ExcelFormat.XLSX)
        //                woorkbook = new XSSFWorkbook(stream);
        //            else
        //                woorkbook = new HSSFWorkbook(stream);
        //            sheet = woorkbook.GetSheetAt(0);
        //            IRow headerRow = sheet.GetRow(12);
        //            int cellCount = headerRow.LastCellNum;

        //            for (int i = 13; i < sheet.LastRowNum; i++)
        //            {
        //                List<string> list = new List<string>();
        //                IRow row = sheet.GetRow(i);
        //                var cell = row.GetCell(row.FirstCellNum);
        //                if (string.IsNullOrEmpty(cell.ToString()))
        //                    return consignment;
        //                if (cell.CellType == CellType.Error)
        //                    break;
        //                foreach (var num in ValueableCellsNumber)
        //                {
        //                    cell = row.GetCell(num - 1);
        //                    if (cell != null)
        //                    {
        //                        var value = ParseCell(cell);
        //                        list.Add(value);
        //                    }
        //                }
        //                consignment.Products.Add(new TousProduct(list, markup, round));

        //            }
        //        }
        //        return consignment;
        //    }
        //    catch (IOException e)
        //    {
        //        throw new ParseException("Файл занят другим приложением", e);
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ParseException("Данные повреждены или выбран неправильный поставщик", e);
        //    }
        //}

        List<int> ValueableCellsNumber = new List<int>()
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
