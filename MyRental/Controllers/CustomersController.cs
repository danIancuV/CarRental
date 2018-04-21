using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MyRental.Models;
using MyRental.ViewModels;
using System.Collections.Generic;

namespace MyRental.Controllers
{
    public class CustomersController : Controller
    {
        private LocalDatabaseEntities db = new LocalDatabaseEntities();

        // GET: Customers
        public ActionResult Index(string sortBy)
        {
            ViewBag.NameSort = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";

            //get list of customers from db
            var customers = db.Customers.ToList();

            // create list of customers from viewmodel
            var customersVM = new List<CustomerVM>();

            foreach (var customer in customers)
            {
                customersVM.Add(CustomerVM.MapTo(customer));
            }
            
            switch (sortBy)
            {
                case "Name desc":
                    var clients = customersVM.OrderByDescending(x => x.Name);
                    return View(clients.ToList());
                default:
                    clients = customersVM.OrderBy(x => x.Name);
                    return View(clients.ToList());
            }
        }    

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            var customerVM = CustomerVM.MapTo(customer);

            return View(customerVM);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,Name,SSN,Town")] CustomerVM customerVM)
        {
            if (ModelState.IsValid)
            {
                var customer = CustomerVM.MapTo(customerVM);
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customerVM);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            var customerVM = CustomerVM.MapTo(customer);

            return View(customerVM);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,Name,SSN,Town")] CustomerVM customerVM)
        {
            if (ModelState.IsValid)
            {
                var customer = CustomerVM.MapTo(customerVM);
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerVM);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            var customerVM = CustomerVM.MapTo(customer);

            return View(customerVM);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Customer customer = db.Customers.Find(id);
                db.Customers.Remove(customer);
                db.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return RedirectToAction("ExceptionHandling", "Home");
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
