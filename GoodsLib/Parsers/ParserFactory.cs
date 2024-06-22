using System;
using GoodsLib.Interfaces;
using GoodsLib.Models.Enum;
using GoodsLib.Models.Products;

namespace GoodsLib.Parsers
{
    public class ParserFactory : IParserFactory
    {
        public IExcelParser<ProductBase> Build(ProductProvider provider)
        {
            return provider switch
            {
                ProductProvider.Marker => new MarkerParser(),
                ProductProvider.Tous => new TousParser(),
                ProductProvider.Souyz => new SoyuzParser(),
                ProductProvider.Simaland => new SimalandParser(),
                ProductProvider.SimalandV2 => new SimalandParserV2(),
                ProductProvider.MarkerV2 => new MarkerParserV2(),
                _ => throw new ArgumentException("Поставщик не распознан")
            };
        }
    }
}