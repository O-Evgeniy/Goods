using GoodsLib;
using GoodsLib.Entity;
using GoodsLib.Interface;
using GoodsLib.Parser;
using GoodsManagerWeb.Models;
using GoodsViewModel;
using NPOI.SS.UserModel;

namespace GoodsManagerWeb.ViewModels
{
    public class IndexViewModel
    {
        public List<ProductProviderModel> Providers { get; }
        public List<int> Rounds { get; }
        public List<ProductBase> Products { get; set; } = new List<ProductBase>();
        public IndexViewModel()
        {
            Providers = new List<ProductProviderModel>
            { 
               new ProductProviderModel(ProductProviderEnum.marker, "Маркер Игрушка"),
               new ProductProviderModel(ProductProviderEnum.tous, "Ural Toys"),
               new ProductProviderModel(ProductProviderEnum.souyz, "Союз-игрушка")
            };
            Rounds = new() { 1, 10 };
        }

        public void LoadFile(ProductProviderEnum provider, string path,Stream stream, double markup, int round)
        {
            ExcelFormat format = Path.GetExtension(path).ToLower() == ".xlsx" ? ExcelFormat.XLSX : ExcelFormat.XLS;
            try
            {
                switch (provider)
                {
                    case ProductProviderEnum.marker:
                        var markerCons = new MarkerParser().Parse(stream, format, markup, round);
                        Products.AddRange(markerCons.Products);
                        break;
                    case ProductProviderEnum.tous:
                        var tousCons = new TousParser().Parse(stream, format, markup, round);
                        Products.AddRange(tousCons.Products);
                        break;
                    case ProductProviderEnum.souyz:
                        var soyuzCons = new SoyuzParser().Parse(stream, format, markup, round);
                        Products.AddRange(soyuzCons.Products);
                        break;
                    default: throw new ArgumentException("Поставщик не распознан");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
