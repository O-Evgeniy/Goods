using System;
using System.Collections.Generic;
using System.Text;

namespace GoodsLib.Interface
{
    public interface IExcelParser
    {
        public IConsignment<T> Parse<T>(string filename, ExcelFormat format, double markup, int round);
    }
}
