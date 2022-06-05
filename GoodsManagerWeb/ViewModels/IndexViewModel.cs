using GoodsLib;
using GoodsLib.Entity;
using GoodsLib.Interface;
using GoodsLib.Parser;
using GoodsViewModel;
using NPOI.SS.UserModel;

namespace GoodsManagerWeb.ViewModels
{
    public class IndexViewModel
    {
        private ResultTable table;
        public List<ProductProviderModel> Providers { get; set; }
        public List<ProductVM> Products { get; set; } = new List<ProductVM>();

        public List<int> Rounds = new() { 1, 10 };
        public IndexViewModel()
        {
            table = new ResultTable();
            Providers = new List<ProductProviderModel>
            { 
               new ProductProviderModel(ProductProviderEnum.marker, "Маркер Игрушка"),
               new ProductProviderModel(ProductProviderEnum.tous, "Ural Toys"),
               new ProductProviderModel(ProductProviderEnum.souyz, "Союз-игрушка")
            };
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
                        AddProducts(markerCons.Products);
                        break;
                    case ProductProviderEnum.tous:
                        var tousCons = new TousParser().Parse(stream, format, markup, round);
                        AddProducts(tousCons.Products);
                        break;
                    case ProductProviderEnum.souyz:
                        var soyuzCons = new SoyuzParser().Parse(stream, format, markup, round);
                        AddProducts(soyuzCons.Products);
                        break;
                    default: throw new ArgumentException("Поставщик не распознан");
                }
            }
            catch
            {
                throw;
            }
        }

        public IWorkbook GetBook()
        {
            return ExcelBuilder.GetBook(table.Products, ExcelFormat.XLS);
        }

        private void AddProducts(IEnumerable<ProductBase> products)
        {
            table.Products.AddRange(products);
            Products.AddRange(products.Select(p=>new ProductVM(p)));
        }
    }
}
