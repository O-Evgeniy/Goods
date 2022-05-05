using System;
using System.Collections.Generic;
using System.Text;
using GoodsLib.Interface;

namespace GoodsLib.Entity
{
    public class Consignment<T> : IConsignment<T>
    {
        public IList<T> Products { get; set; }

        public Consignment()
        {
            Products = new List<T>();
        }
    }
}
