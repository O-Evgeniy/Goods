using GoodsLib.Entity;
using System;

namespace GoodsViewModel
{
    public class ProductVM : BaseVM
    {
        ProductBase product;

        public int Id => product.Id;
        public string Barcode => product.Barcode;
        public string ExternalCode => product.ExternalCode;
        public string Name => product.Name;
        public int Count => product.Count;
        public double PurchasePricePerUnit => product.PurchasePricePerUnit;
        public double TotalPurchasePrice => product.TotalPurchasePrice;
        public string ArticleNumber => product.ArticleNumber;
        public int SalePrice
        {
            get { return product.SalePrice; }
            set { product.SalePrice = value; Notify(); }
        }
        public string Description => product.Description;

        public ProductVM(ProductBase product)
        {
            this.product = product;
        }

        public void Update(double markup, int round)
        {
            SalePrice = Convert.ToInt32(Round(markup * PurchasePricePerUnit, round));
        }

        internal protected double Round(double val, int round)
        {
            var res = Math.Ceiling(val / round) * round;
            return res;
        }
    }
}
