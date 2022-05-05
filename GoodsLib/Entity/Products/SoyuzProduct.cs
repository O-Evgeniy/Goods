using System;
using System.Collections.Generic;
using System.Text;

namespace GoodsLib.Entity.Products
{
    public class SoyuzProduct : ProductBase
    {
        public double Volume { get; set; }

        public SoyuzProduct(List<string> list, double markup,int round) 
            : base(markup,round)
        {
            Id = (int)TryParse(list[0], typeof(int));
            ArticleNumber = (string)list[1];
            Barcode = list[2];
            Name = list[3];
            Count = (int)TryParse(list[6], typeof(int));
            Volume = (double)TryParse(list[7], typeof(double));
            PurchasePricePerUnit = (double)TryParse(list[8], typeof(double));
            TotalPurchasePrice = (double)TryParse(list[9], typeof(double));
            SalePrice = Convert.ToInt32(Round(markup * PurchasePricePerUnit, round));
            Description = "союз_" + GetTime();
        }
    }
}
