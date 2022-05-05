using System;
using System.Collections.Generic;
using System.Text;
using GoodsLib.Interface;

namespace GoodsLib.Entity
{
    public class ResultTable
    {
        public List<ProductBase> Products { get; set; }
        public ResultTable()
        {
            Products = new List<ProductBase>();
        }
    }
}
