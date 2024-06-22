using System.Collections.Generic;
using GoodsLib.Models.Products;

namespace GoodsViewModel
{
    public class ResultTableVm
    {
        public List<ProductBase> Products { get; set; }
        public ResultTableVm()
        {
            Products = new List<ProductBase>();
        }
    }
}
