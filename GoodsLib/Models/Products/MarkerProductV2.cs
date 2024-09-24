using System;
using System.Collections.Generic;

namespace GoodsLib.Models.Products
{
    public class MarkerProductV2 : ProductBase
    {
        public string Brand { get; }
        public string Country { get; }
        public double NoCostPricePerUnit { get; }
        public string AdditionalBarcode { get; }
        public string HsCode { get; }
        
        
        public MarkerProductV2(List<string> list, double markup,int round)
            : base(markup,round)
        {
            Id = (int)TryParse(list[0], typeof(int));
            Barcode = Split(list[1]);
            ExternalCode = list[2];
            ArticleNumber = list[3];
            HsCode = list[4];
            Brand = list[5];
            Country = list[6];
            Name = list[7];
            Count = (int)TryParse(list[8], typeof(int));
            NoCostPricePerUnit = (double)TryParse(list[9], typeof(double));
            PurchasePricePerUnit = (double)TryParse(list[10], typeof(double));
            if (double.IsNaN(PurchasePricePerUnit))
                PurchasePricePerUnit = NoCostPricePerUnit;
            TotalPurchasePrice = (double)TryParse(list[11], typeof(double));
            AdditionalBarcode = list[12];
            SalePrice = Convert.ToInt32(Round(markup * PurchasePricePerUnit,round));
            Description = "маркер_" + GetTime();
        }
    }
}