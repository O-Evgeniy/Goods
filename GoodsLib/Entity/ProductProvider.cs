using System;
using System.Collections.Generic;
using System.Text;

namespace GoodsLib.Entity
{
    public class ProductProvider
    {
        public ProductProviderEnum Id { get; set; }
        public string Name { get; set; }
        public ProductProvider(ProductProviderEnum id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
