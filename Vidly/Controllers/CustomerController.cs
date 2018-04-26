using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;

        public CustomerController()
        {
                this._context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            this._context.Dispose();
        }
           
        //private  readonly List<Customer> customers = new List<Customer>
        //{
        //    new Customer { Id = 1, Name = "John Smith"},
        //    new Customer { Id = 2, Name = "Mary Williams"}
        //};

        // GET: Customer
        public ActionResult Index()
        {
            //var count = this._context.Customers.Count();
            //if(count > 0)
            //    return View(this._context.Customers.Include(c => c.MembershipType).ToList());

            return View();
        }

        // GET: Customer/Detail/id
        public ActionResult Detail(int? id)
        {
            Customer customer = null;

            if (!id.HasValue || id <= 0)
            {
                return HttpNotFound();
            }

            customer = this._context.Customers.Include(c => c.MembershipType).SingleOrDefault(x => x.Id == id); //.Find(x => x.Id.Equals(id));

            if (customer == null) return HttpNotFound();

            return View(customer);
        }
        
        public ActionResult New()
        {
            var membershipTypes = this._context.MembershipType.ToList();
            var customerEditViewModel = new CustomerEditViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            //return View(customerEditViewModel);
            return View("CustomerForm", customerEditViewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (!id.HasValue || id <= 0)
            {
                return HttpNotFound();
            }

            var membershipTypes = this._context.MembershipType.ToList();
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var customerEditViewModel = new CustomerEditViewModel
            {
                MembershipTypes = membershipTypes,
                Customer = customer
            };
            return View("CustomerForm", customerEditViewModel);
        }

        [HttpPost]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerEditViewModel
                {
                    Customer = customer,
                    MembershipTypes = this._context.MembershipType.ToList()
                };
                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
                this._context.Customers.Add(customer);
            else
            {
                var customerDatabase = this._context.Customers.Single(c => c.Id == customer.Id);
                customerDatabase.Name = customer.Name;
                customerDatabase.BirthDate = customer.BirthDate;
                customerDatabase.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
                customerDatabase.MembershipTypeId = customer.MembershipTypeId;
            }
            this._context.SaveChanges();

            return RedirectToAction("Index", "Customer");
        }

        
    }
}