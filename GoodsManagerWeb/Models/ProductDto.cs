using GoodsLib.Entity;
using Microsoft.EntityFrameworkCore;

namespace GoodsManagerWeb.Models
{
    public class ProductDto
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public double PurchasePricePerUnit { get; set; }

        public string Barcode { get; set; }

        public string ExternalCode { get; set; }

        public string Name { get; set; }

        public int Count { get; set; }

        public double TotalPurchasePrice { get; set; }

        public string ArticleNumber { get; set; }

        public int SalePrice { get; set; }

        public string Description { get; set; }
    }
}
