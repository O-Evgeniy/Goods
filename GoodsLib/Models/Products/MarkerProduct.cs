using System;
using System.Collections.Generic;

namespace GoodsLib.Models.Products
{
    public class MarkerProduct : ProductBase
    {
        public double NoCostPricePerUnit { get; }
        public string AdditionalBarcode { get; }
        
        
        public MarkerProduct(List<string> list, double markup,int round)
            : base(markup,round)
        {
            Id = (int)TryParse(list[0], typeof(int));
            Barcode = Split(list[1]);
            ExternalCode = list[2];
            ArticleNumber = list[3];
            Name = list[4];
            Count = (int)TryParse(list[5], typeof(int));
            NoCostPricePerUnit = (double)TryParse(list[6], typeof(double));
            PurchasePricePerUnit = (double)TryParse(list[7], typeof(double));
            if (double.IsNaN(PurchasePricePerUnit))
                PurchasePricePerUnit = NoCostPricePerUnit;
            TotalPurchasePrice = (double)TryParse(list[8], typeof(double));
            AdditionalBarcode = list[9];
            SalePrice = Convert.ToInt32(Round(markup * PurchasePricePerUnit,round));
            Description = "маркер_" + GetTime();
        }
    }
}