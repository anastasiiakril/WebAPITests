using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPITests
{
    //-------CONVERT
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
    //-------CHANGE

    public class CurrencyQuotes
    {
        public decimal change { get; set; }
        public decimal change_pct { get; set; }
        public decimal end_rate { get; set; }
        public decimal start_rate { get; set; }


    }

    public class CurrencyChangeResponse
    {
        public bool success { get; set; }
        public Dictionary<string, CurrencyQuotes> quotes { get; set; }
        public decimal result { get; set; }
        public string end_date { get; set; }
        public string start_date { get; set; }

        public string source { get; set; }

    }



}