using GoodsLib.Entity;
using System;

namespace GoodsViewModel
{
    public class ProductVM
    {
        ProductBase product;

        public int Id =>product.Id;
        public string Barcode =>product.Barcode;
        public string ExternalCode =>product.ExternalCode;
        public string Name =>product.Name;
        public int Count =>product.Count;
        public double PurchasePricePerUnit =>product.PurchasePricePerUnit;
        public double TotalPurchasePrice =>product.TotalPurchasePrice;
        public string ArticleNumber =>product.ArticleNumber;
        public int SalePrice =>product.SalePrice;
        public string Description =>product.Description;

        public ProductVM(ProductBase product)
        {
            this.product = product;
        }
    }
}
