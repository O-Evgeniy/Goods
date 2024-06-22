using GoodsLib.Models.Enum;
using GoodsLib.Models.Products;

namespace GoodsLib.Interfaces
{
    public interface IParserFactory
    {
        public IExcelParser<ProductBase> Build(ProductProvider provider);
    }
}