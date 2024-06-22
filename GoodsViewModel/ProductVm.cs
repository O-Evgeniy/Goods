using System;
using GoodsLib.Models.Products;

namespace GoodsViewModel
{
    public class ProductVm : BaseVm
    {
        private readonly ProductBase _product;

        public int Id => _product.Id;
        public string Barcode => _product.Barcode;
        public string ExternalCode => _product.ExternalCode;
        public string Name => _product.Name;
        public int Count => _product.Count;
        public double PurchasePricePerUnit => _product.PurchasePricePerUnit;
        public double TotalPurchasePrice => _product.TotalPurchasePrice;
        public string ArticleNumber => _product.ArticleNumber;

        public int SalePrice
        {
            get => _product.SalePrice;
            set
            {
                _product.SalePrice = value;
                Notify();
            }
        }

        public string Description => _product.Description;

        public ProductVm(ProductBase product)
        {
            this._product = product;
        }

        public void Update(double markup, int round)
        {
            SalePrice = Convert.ToInt32(Round(markup * PurchasePricePerUnit, round));
        }

        private static double Round(double val, int round)
        {
            var res = Math.Ceiling(val / round) * round;
            return res;
        }
    }
}