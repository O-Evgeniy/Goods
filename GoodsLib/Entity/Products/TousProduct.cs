using System;
using System.Collections.Generic;

namespace GoodsLib.Entity
{
    public class TousProduct : ProductBase
    {
        public TousProduct(List<string> list, double markup, int round)
            : base(markup, round)
        {
            Id = (int)TryParse(list[0], typeof(int));
            Barcode = list[1];
            ExternalCode = list[2];
            ArticleNumber = list[3];
            Name = list[4];
            Count = (int)TryParse(list[5], typeof(int));
            PurchasePricePerUnit = (double)TryParse(list[6], typeof(double));
            TotalPurchasePrice = (double)TryParse(list[7], typeof(double));
            Tax = list[8];
            CustomDeclaration = list.Count > 9 ? list[9] : string.Empty;
            OriginCountry = list.Count > 10 ? list[10] : string.Empty;
            SalePrice = Convert.ToInt32(Round(markup * PurchasePricePerUnit, round));
            Description = "тойс_" + GetTime();
        }

        // Страна происхождения
        public string OriginCountry { get; set; }

        // НДС
        public string Tax { get; set; }

        //Номер ГТД
        public string CustomDeclaration { get; set; }
    }
}