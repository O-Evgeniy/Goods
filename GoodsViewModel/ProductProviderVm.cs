using GoodsLib.Models.Enum;

namespace GoodsViewModel
{
    public class ProductProviderVm
    {
        public ProductProvider Id { get; set; }
        public string Name { get; set; }
        public ProductProviderVm(ProductProvider id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
