using GoodsLib.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;
using GoodsLib;
using System.IO;
using Microsoft.Win32;
using GoodsLib.Parser;
using GoodsLib.Interface;

namespace GoodsViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        private readonly ResultTable table;

        private ObservableCollection<ProductVM> products = new ObservableCollection<ProductVM>();
        public ObservableCollection<ProductVM> Products
        {
            get { return products; }
            set { products = value; Notify(); }
        }

        //private ObservableCollection<string> loadedConsignment = new ObservableCollection<string>();
        public ObservableCollection<string> Consignment { get; } = new ObservableCollection<string>
        {
            "Все"
        };

        public Dictionary<string, ProductProviderEnum> ProductProviders { get; } = new Dictionary<string, ProductProviderEnum>
        {
            ["Маркер"] = ProductProviderEnum.marker,
            ["Тойс"] = ProductProviderEnum.tous,
            ["Союз"] = ProductProviderEnum.souyz,
        };

        private ProductProviderEnum productProvider;
        public ProductProviderEnum SelectedProvider
        {
            get { return productProvider; }
            set { productProvider = value; Notify(); }
        }

        public List<int> Rounding { get; } = new List<int>
        {
            1,
            10
        };

        private int round = 1;
        public int Round
        {
            get { return round; }
            set { round = value; }
        }

        private double markup = 1.55;
        public int Markup
        {
            get { return (int)(markup * 100 - 100); }
            set { markup = (value * 0.01) + 1; }
        }

        public MainVM()
        {
            table = new ResultTable();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Notify([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void AddProducts(IEnumerable<ProductBase> products)
        {
            table.Products.AddRange(products);
            Products = new ObservableCollection<ProductVM>(table.Products.Select(p => new ProductVM(p)));
        }

        public void Update()
        {
            Products.ForEach(p => p.Update(markup, round));
        }

        public void ClearProducts()
        {
            table.Products.Clear();
            Products?.Clear();
        }

        public void LoadFile(string path)
        {
            LoadFile(SelectedProvider, path, markup,round);
        }

        private void LoadFile(ProductProviderEnum provider, string path, double markup,int round)
        {
            ExcelFormat format = Path.GetExtension(path).ToLower() == ".xlsx" ? ExcelFormat.XLSX : ExcelFormat.XLS;
            try
            {
                var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
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
                        var soyuzCons = new SoyuzParser().Parse(stream,format, markup, round);
                        AddProducts(soyuzCons.Products);
                        break;
                    default: throw new ArgumentException("Поставщик не распознан");
                }
            }
            catch
            {
                throw;
            }
            SelectedProvider = ProductProviderEnum.none;
        }

        public void SaveProducts(string path)
        {
            ExcelBuilder.Save(path, table.Products, ExcelFormat.XLS);
        }
    }
}
