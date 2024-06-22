using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GoodsLib;
using System.IO;
using GoodsLib.Models.Enum;
using GoodsLib.Models.Products;
using GoodsLib.Parsers;
using ProductProvider = GoodsLib.Models.Enum.ProductProvider;

namespace GoodsViewModel
{
    public class MainVm : BaseVm
    {
        private readonly ResultTableVm _tableVm = new ResultTableVm();

        private ObservableCollection<ProductVm> _products = new ObservableCollection<ProductVm>();

        public ObservableCollection<ProductVm> Products
        {
            get => _products;
            set
            {
                _products = value;
                Notify();
            }
        }

        private ProductProvider _productProvider;

        public ProductProvider SelectedProvider
        {
            get => _productProvider;
            set
            {
                _productProvider = value;
                Notify();
            }
        }

        public List<int> Rounding { get; } = new List<int>
        {
            1,
            10
        };

        public int Round { get; set; } = 1;

        private double _markup = 1.55;

        public int Markup
        {
            get => (int)(_markup * 100 - 100);
            set => _markup = (value * 0.01) + 1;
        }

        public Dictionary<string, ProductProvider> ProductProviders { get; } =
            new Dictionary<string, ProductProvider>
            {
                ["Маркер"] = ProductProvider.Marker,
                ["Тойс"] = ProductProvider.Tous,
                ["Союз"] = ProductProvider.Souyz,
                ["Сималэнд"] = ProductProvider.Simaland,
                ["Сималэнд_v2"] = ProductProvider.SimalandV2,
                ["Маркер_v2"] = ProductProvider.MarkerV2
            };

        private void AddProducts(IEnumerable<ProductBase> products)
        {
            _tableVm.Products.AddRange(products);
            Products = new ObservableCollection<ProductVm>(_tableVm.Products.Select(p => new ProductVm(p)));
        }

        public void UpdateProducts()
        {
            Products.ForEach(p => p.Update(_markup, Round));
        }

        public void ClearProducts()
        {
            _tableVm.Products.Clear();
            Products?.Clear();
        }

        public void SaveProducts(string path)
        {
            ExcelBuilder.Save(path, _tableVm.Products);
        }

        public void LoadFile(string path)
        {
            var format = Path.GetExtension(path).ToLower() == ".xlsx" ? ExcelFormat.Xlsx : ExcelFormat.Xls;
            var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            var parserFactory = new ParserFactory();
            var products = parserFactory.Build(SelectedProvider).Parse(stream, format, _markup, Round);
            AddProducts(products);

            SelectedProvider = ProductProvider.None;
        }
    }
}