using System;

namespace GoodsLib
{
    public class ParseException : Exception
    {
        public ParseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}