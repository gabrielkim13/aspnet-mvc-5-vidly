using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Vidly.DAL;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private readonly VidlyContext _context;

        public CustomersController()
        {
            _context = new VidlyContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            
            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(x => x.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult New()
        {
            var customerFormViewModel = new CustomerFormViewModel()
            {
                Customer = new Customer(),
                MembershipTypes = _context.MembershipTypes.ToList(),
            };

            return View("CustomerForm", customerFormViewModel);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(x => x.Id == id);

            if (customer == null)
                return HttpNotFound();

            var customerFormViewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList(),
            };

            return View("CustomerForm", customerFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var customerFormViewModel = new CustomerFormViewModel()
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList(),
                };

                return View("CustomerForm", customerFormViewModel);
            }

            if (customer.Id > 0)
            {
                var updateCustomer = _context.Customers.Single(c => c.Id == customer.Id);

                updateCustomer.Name = customer.Name;
                updateCustomer.BirthDate = customer.BirthDate;
                updateCustomer.MembershipTypeId = customer.MembershipTypeId;
                updateCustomer.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            else
            {
                _context.Customers.Add(customer);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}