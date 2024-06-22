using System.Collections.Generic;
using System.IO;
using GoodsLib.Models.Enum;
using GoodsLib.Models.Products;

namespace GoodsLib.Interfaces
{
    public interface IExcelParser<out T> where T : ProductBase
    {
        public IEnumerable<T> Parse(Stream stream, ExcelFormat format, double markup, int round);
    }
}