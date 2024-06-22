using System;
using System.Collections.Generic;

namespace GoodsLib.Models.Products
{
    public class SimaLandProduct : ProductBase
    {
        public double RetailPrice { get; set; }
        public double Price { get; set; }
        public double SumWithoutDiscount { get; set; }
        public double Discount { get; set; }
        public double Volume { get; set; }
        public string Information { get; set; }
        public string OrderNumber { get; set; }

        public SimaLandProduct(List<string> list,double markup, int round) 
            : base(markup, round)
        {
            Id = (int)TryParse(list[0],typeof(int));
            Barcode =  Split(list[1]);
            ExternalCode = list[2];
            Name = list[3];
            Information = list[4];
            Count = (int)TryParse(list[5],typeof(int));
            RetailPrice = (double)TryParse(list[6],typeof(double));
            Price = (double)TryParse(list[7],typeof(double));
            SumWithoutDiscount = (double)TryParse(list[8],typeof(double));
            Discount = (double)TryParse(list[9],typeof(double));
            PurchasePricePerUnit = (double)TryParse(list[10],typeof(double));
            TotalPurchasePrice = (double)TryParse(list[11],typeof(double));
            Volume = (double)TryParse(list[12],typeof(double));
            OrderNumber = list[13];
            SalePrice = Convert.ToInt32(Round(markup * PurchasePricePerUnit, round));
            Description = "сималэнд_" + GetTime();
        }
    }
}
