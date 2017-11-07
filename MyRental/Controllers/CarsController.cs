using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MyRental.Models;
using MyRental.ViewModels;
using System.Collections.Generic;

namespace MyRental.Controllers
{
    public class CarsController : Controller
    {
        private Entities db = new Entities();

        // GET: Cars
        public ActionResult Index(string search)
        {
            //get list of cars from db
            var cars = db.Cars.ToList();
            
            // create list of cars from viewmodel
            var carsVM = new List<CarVM>();
            //create list for filtered cars
            var carsSearchVM = new List<CarVM>();

            foreach (var car in cars)
            {
                carsVM.Add(CarVM.MapTo(car));
            }

            if (search == null) {
                return View(carsVM);
            }
            //populating list with filtered cars
         
            carsSearchVM = carsVM.Where(x => x.Make.Contains(search)).ToList<CarVM>(); 

            return View(carsSearchVM);
        }
     
            // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);

            if (car == null)
            {
                return HttpNotFound();
            }
            var carVM = CarVM.MapTo(car);

            return View(carVM);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarVM carVM)
        {
            if (ModelState.IsValid)
            {
                var car = CarVM.MapTo(carVM);
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carVM);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);

            if (car == null)
            {
                return HttpNotFound();
            }
            var carsVM = CarVM.MapTo(car);

            return View(carsVM);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarVM carVM)
        {
            if (ModelState.IsValid)
            {
                var car = CarVM.MapTo(carVM);
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carVM);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }           
            Car car = db.Cars.Find(id);

            if (car == null)
            {
                return HttpNotFound();
            }
            var carsVM = CarVM.MapTo(car);

            return View(carsVM);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Car car = db.Cars.Find(id);
                db.Cars.Remove(car);
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
