using System;
using System.Linq;

namespace GoodsLib.Entity
{
    public abstract class ProductBase
    {
        private readonly double markup;
        private readonly int round;

        public int Id { get; set; }
        public double TotalPurchasePrice { get; set; }

        [Label("Штрих-код")]
        public string Barcode { get; set; }

        [Label("Код")]
        public string ExternalCode { get; set; }

        [Label("Наименование")]
        public string Name { get; set; }

        [Label("Остаток")]
        public int Count { get; set; }

        [Label("Цена закупки")]
        public double PurchasePricePerUnit { get; set; }

        [Label("Артикул")]
        public string ArticleNumber { get; set; }

        [Label("Цена")]
        public int SalePrice { get; set; }

        [Label("Описание")]
        public string Description { get; set; }

        public ProductBase(double markup, int round)//наценка
        {
            this.markup = markup;
            this.round = round;
        }

        internal protected object TryParse(string value, Type type)
        {
            if (type == typeof(double))
            {
                double val;
                return double.TryParse(value, out val) ? val : double.NaN;
            }
            else if (type == typeof(int))
            {
                int val;
                return int.TryParse(value, out val) ? val : -1;
            }
            else
                return value;
        }

        internal protected double Round(double val, int round)
        {
            var res = Math.Ceiling(val / round) * round;
            return res;
        }

        internal protected string GetTime()
        {
            var time = DateTime.Now.AddDays(-1);
            return time.ToString("d");
        }

        internal protected string Split(string str)
        {
            return string.Join(", ", str.Split(';', '.', ',', ' ').Where(s => !string.IsNullOrWhiteSpace(s)));
        }
    }
}