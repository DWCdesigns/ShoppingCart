using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ShoppingCart.Models;
using ShoppingCart.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Terminal _terminal;

        public IndexModel(ILogger<IndexModel> logger, Terminal terminal) //
        {
            _logger = logger;
            _terminal = terminal;
        }

        public class ShoppingCart{
            public List<Item> Items { get; set; } = new List<Item>();
            public decimal Total { get; set; }
            public string Selected { get; set; }
            public string SelectedList { get; set; }
        }
        [BindProperty]
        public ShoppingCart cart { get; set; } = new ShoppingCart();

        public List<Item> Items { get; set; } = new List<Item>();
        public void OnGet()
        {

            cart.SelectedList = "";
            Items = _terminal.items;

 

    }

        public async Task<IActionResult> OnPostAddItemAsync()
        {
            // Add selected items to list
            string selected = Request.Form["cart.SelectedList"].ToString();
            string[] previous = selected.Split("|");
            foreach(string i in previous)
            {
                _terminal.Scan(i);
            }
            //Let terminal calculate new total and display on the page
            cart.Total = _terminal.Total();
            cart.SelectedList +=  "|";

            Items = _terminal.items;

            return Page();
        }
        public async Task<IActionResult> OnPostEmptyCartAsync()
        {
            // Add selected items to list
            string selected = Request.Form["cart.SelectedList"].ToString();
            
            //Let terminal calculate new total and display on the page
            cart.Total = 0.00m;
            cart.SelectedList = "";
            Items = _terminal.items;

            return Page();
        }
    }
}
