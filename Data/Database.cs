using System.Collections.Generic;
using Bogus;

namespace HtmxDemo.Data
{
    public class Database
    {
        public List<Customer> Customers { get; set; }

        public static Database Create()
        {
            var customerFaker = new Faker<Customer>()
                .RuleFor(c => c.Id, (f, c) => f.IndexFaker)
                .RuleFor(c => c.FirstName, (f, c) => f.Name.FirstName())
                .RuleFor(c => c.LastName, (f, c) => f.Name.LastName())
                .RuleFor(c => c.EmailAddress, (f, c) => f.Internet.ExampleEmail(c.FirstName, c.LastName))
                .RuleFor(c => c.Avatar, (f, c) => f.Internet.Avatar());

            return new Database
            {
                Customers = customerFaker.Generate(20)
            };
        }
    }
}