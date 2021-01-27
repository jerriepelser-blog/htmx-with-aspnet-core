using System.Collections.Generic;
using HtmxDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HtmxDemo.Pages
{
    public class DeleteItemsModel : PageModel
    {
        private readonly Database _database;

        public List<Customer> Customers { get; set; }

        public DeleteItemsModel(Database database)
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

            return new ContentResult();
        }
    }
}