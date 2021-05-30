using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HtmxDemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HtmxDemo.Pages
{
    // PLEASE NOTE: This is not production-ready code as I do not do any sort of error checking at any point
    public class InlineEditModel : PageModel
    {
        public class CustomerEditModel
        {
            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            public string EmailAddress { get; set; }
        }
        
        private readonly Database _database;

        public List<Customer> Customers { get; set; }

        public InlineEditModel(Database database)
        {
            _database = database;
        }

        public void OnGet()
        {
            Customers = _database.Customers;
        }

        public IActionResult OnPostEdit(int id)
        {
            var customer = _database.Customers.First(c => c.Id == id);

            return Partial("_CustomerForm", customer);
        }

        public IActionResult OnPostUpdate(int id, [FromForm] CustomerEditModel model)
        {
            var customer = _database.Customers.First(c => c.Id == id);
            if (!ModelState.IsValid)
            {
                return Partial("_CustomerForm", customer); 
            }
            
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.EmailAddress = model.EmailAddress;

            return Partial("_CustomerDetail", customer);
        }

        public IActionResult OnGetCancel(int id)
        {
            var customer = _database.Customers.First(c => c.Id == id);

            return Partial("_CustomerDetail", customer);
        }
    }
}