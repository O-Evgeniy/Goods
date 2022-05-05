using System;
using System.Collections.Generic;
using System.Text;

namespace GoodsLib.Interface
{
    public interface IConsignment<T>
    {
        public IList<T> Products { get; }
    }
}
