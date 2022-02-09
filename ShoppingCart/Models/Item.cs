using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class Item
    {
        public string Description { get; set; }
        public int BulkCount { get; set; }
        public decimal Price { get; set; }
        public decimal BulkPrice { get; set; }
    }

}
