using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPITests
{
    public class CurrencyQuery
    {
        public decimal amount { get; set; }
        public string from { get; set; }
        public string to { get; set; }

    }

    public class CurrencyInfo
    {
        public long timestamp { get; set; }
        public decimal rate { get; set; }
    }

    public class CurrencyConvertResponse
    {
        public bool success { get; set; }
        public CurrencyQuery query { get; set; }
        public decimal result { get; set; }
        public CurrencyInfo info { get; set; }
    }

}