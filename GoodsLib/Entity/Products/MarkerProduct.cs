
using System;
using System.Collections.Generic;

namespace GoodsLib.Entity
{
    public class MarkerProduct : ProductBase
    {
        public double NoCostPricePerUnit { get; set; }

        public MarkerProduct(List<string> list, double markup,int round)
            : base(markup,round)
        {
            Id = (int)TryParse(list[0], typeof(int));
            Barcode = list[1];
            ExternalCode = list[2];
            Name = list[3];
            Count = (int)TryParse(list[4], typeof(int));
            PurchasePricePerUnit = (double)TryParse(list[6], typeof(double));
            NoCostPricePerUnit = (double)TryParse(list[5], typeof(double));
            if (double.IsNaN(PurchasePricePerUnit))
                PurchasePricePerUnit = NoCostPricePerUnit;
            TotalPurchasePrice = (double)TryParse(list[7], typeof(double));
            ArticleNumber = list[8];
            SalePrice = Convert.ToInt32(Round(markup * PurchasePricePerUnit,round));
            Description = "маркер_" + GetTime();
        }
    }
}