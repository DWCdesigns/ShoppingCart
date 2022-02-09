using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Services
{

    public interface ITerminal
    {
        void Scan(string item);
        decimal Total();
    }
    public class Terminal : ITerminal
    {

        public Terminal() { }
        //A $2.00 each or 4 for $7.00
        //B	$12.00
        //C	$1.25 or $6 for a six pack
        //D	$0.15
        public List<Item> scanned { get; set; } = new List<Item>();
        public List<Item> items { get; } = new List<Item>() {
        new Item(){ Description = "A", BulkCount = 4 , Price = 2.00m , BulkPrice = 7.00m},
        new Item(){ Description = "B", Price = 12.00m },
        new Item(){ Description = "C", BulkCount = 6 , Price = 1.25m , BulkPrice = 6.00m},
        new Item(){ Description = "D", Price = 0.15m }
        };
        //public List<Item> ItemList()
        //{
        //    return items;
        //}

        //public List<Item> ScannedItems()
        //{
        //    return scanned;
        //}
        public void Scan(string item)
        {

            //Add item to the list
            Item i = items.First(m => m.Description == item);
            scanned.Add(i);
        }

        public decimal Total()
        {
            decimal result = 0.00m;
            // loop through scanned and implemment business rules
            List<string> types = new List<string>() { "A", "B", "C", "D" };
            foreach (string d in types)
            {
                // calculate total for each type
                int CurrentCount = scanned.Where(m => m.Description == d).Count();
                Item Current = items.First(m => m.Description == d);
                if (Current.BulkCount > 0 && CurrentCount >= Current.BulkCount)
                {
                    //Calculate bulk pricing
                    int BulkCounts = CurrentCount / Current.BulkCount;
                    result += BulkCounts * Current.BulkPrice;
                    result += (CurrentCount % Current.BulkCount) * Current.Price;
                }
                else
                {
                    //Use full price.
                    result += CurrentCount * Current.Price;
                };
            }

            return result;

        }




    }
}
