using System;

namespace GoodsLib.Models.Products
{
    public abstract class ProductBase
    {
        private readonly double _markup;
        private readonly int _round;

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

        protected ProductBase(double markup, int round)//наценка
        {
            _markup = markup;
            _round = round;
        }

        protected static object TryParse(string value, Type type)
        {
            if (type == typeof(double))
            {
                return double.TryParse(value, out var val) ? val : double.NaN;
            }

            if (type == typeof(int))
            {
                return int.TryParse(value, out var val) ? val : -1;
            }

            return value;
        }

        protected static double Round(double val, int round)
        {
            return Math.Ceiling(val / round) * round;
        }

        protected static string GetTime()
        {
            return DateTime.Now.AddDays(-1).ToString("d");
        }

        protected static string Split(string str)
        {
            return string.Join(", ", str.Split(new[] { ";", ".", ",", " " },StringSplitOptions.RemoveEmptyEntries));
        }
    }
}