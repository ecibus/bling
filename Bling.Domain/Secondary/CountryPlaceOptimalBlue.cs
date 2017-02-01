using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bling.Domain.Extension;

namespace Bling.Domain.Secondary
{
    public class CountryPlaceOptimalBlue
    {
        public string Product { get; set; }
        public string Rate { get; set; }
        public string Price { get; set; }
        public string Lock { get; set; }

        public CountryPlaceOptimalBlue(string product, string rate, string price, string l)
        {
            Product = product;
            Rate = rate;
            Lock = l;

            if (price != "N/A")
            {
                string p = price.Replace("(", "-").Replace(")", "");
                Price = ("100".ToDecimal() - p.ToDecimal()).ToString();
            }
            else
            {
                Price = price;
            }
        }
    }
}
