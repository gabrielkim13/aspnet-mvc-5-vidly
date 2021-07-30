using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private List<Customer> Customers = new List<Customer>
        {
            new Customer() { Id = 1, Name = "John Smith" },
            new Customer() { Id = 2, Name = "Mary Williams" },
        };

        public ActionResult Index()
        {
            return View(Customers);
        }

        public ActionResult Details(int id)
        {
            var customer = Customers.Find(x => x.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }
    }
}