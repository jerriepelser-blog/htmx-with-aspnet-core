using System.Collections.Generic;
using HtmxDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;

namespace HtmxDemo.Pages
{
    public class DeleteWithRefreshModel : PageModel
    {
        private readonly Database _database;

        public List<Customer> Customers { get; set; }
        
        public DeleteWithRefreshModel(Database database)
        {
            _database = database;
        }
        
        public void OnGet()
        {
            Customers = _database.Customers;    
        }
        
        public IActionResult OnPostDelete(int id)
        {
            _database.Customers.RemoveAll(customer => customer.Id == id);

            if (_database.Customers.Count == 0)
            {
                Response.Headers.Add("HX-Refresh", "true");
            }

            return new OkResult();
        }

    }
}