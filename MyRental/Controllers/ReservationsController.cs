using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MyRental.Models;
using MyRental.ViewModels;

namespace MyRental.Controllers
{
    public class ReservationsController : Controller
    {
        private LocalDatabaseEntities db = new LocalDatabaseEntities();

        // GET: Reservations
        public ActionResult Index()
        {
            //get list of reservations from db
            var reservations = db.Reservations.Include(r => r.Car).Include(r => r.Customer).ToList();

            // create list of reservations from viewmodel
            var reservationsVM = new List<ReservationVM>();

            foreach (var reservation in reservations)
            {
                reservationsVM.Add(ReservationVM.MapTo(reservation));
            }

            //adding Car Make and Customer Name in Reservation Index
            foreach (var reservationVM in reservationsVM)
            {
                reservationVM.Car = CarVM.MapTo(db.Cars.Find(reservationVM.CarID));
                reservationVM.Customer = CustomerVM.MapTo(db.Customers.Find(reservationVM.CustomerID));
            }
            return View(reservationsVM);
        }


        // GET: IndexBook
        [Authorize]
        public ActionResult IndexBook(string sortBy, string search)
        {
            ViewBag.StartDateSort = string.IsNullOrEmpty(sortBy) ? "StartDate desc" : "";

            //get list of reservations from db
            var reservations = db.Reservations.Include(r => r.Car).Include(r => r.Customer).ToList();

            var SSNSearchVM = new List<ReservationVM>();
            // create list of reservations from viewmodel
            var reservationsVM = new List<ReservationVM>();

            foreach (var reservation in reservations)
            {
                reservationsVM.Add(ReservationVM.MapTo(reservation));
            }

            //adding Car Make and Customer Name in Reservation Index
            foreach (var reservationVM in reservationsVM)
            {
                reservationVM.Car = CarVM.MapTo(db.Cars.Find(reservationVM.CarID));
                reservationVM.Customer = CustomerVM.MapTo(db.Customers.Find(reservationVM.CustomerID));
            }

            if (search == null)
            {
                switch (sortBy)
                {
                    case "StartDate desc":
                        var bookings = reservationsVM.OrderByDescending(x => x.StartDate);
                        return View(bookings.ToList());
                    default:
                        bookings = reservationsVM.OrderBy(x => x.StartDate);
                        return View(bookings.ToList());
                }
            }
            else
            {
                SSNSearchVM = reservationsVM.Where(x => x.Customer.SSN == search).ToList<ReservationVM>();
             
                return View(SSNSearchVM);
            }       
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);

            if (reservation == null)
            {
                return HttpNotFound();
            }
            var reservationVM = ReservationVM.MapTo(reservation);

            //adding Car Make and Customer Name in Reservation Details
            reservationVM.Car = CarVM.MapTo(db.Cars.Find(reservationVM.CarID));
            reservationVM.Customer = CustomerVM.MapTo(db.Customers.Find(reservationVM.CustomerID));
            
            return View(reservationVM);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.CarID = new SelectList(db.Cars.GroupBy(e => e.Make).Select(g => g.FirstOrDefault()).ToList(), "CarID", "Make");
            ViewBag.CustomerID = new SelectList(db.Customers.GroupBy(f => f.Name).Select(g => g.FirstOrDefault()).ToList(), "CustomerID", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationID,CarID,CustomerID,StartDate,EndDate")] ReservationVM reservationVM)
        {
            if (ModelState.IsValid)
            {
                var reservation = ReservationVM.MapTo(reservationVM);
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarID = new SelectList(db.Cars, "CarID", "Make", reservationVM.CarID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", reservationVM.CustomerID);

            return View(reservationVM);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);

            if (reservation == null)
            {
                return HttpNotFound();
            }

            ViewBag.CarID = new SelectList(db.Cars, "CarID", "Make", reservation.CarID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", reservation.CustomerID);
            var reservationVM = ReservationVM.MapTo(reservation);

            return View(reservationVM);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationID,CarID,CustomerID,StartDate,EndDate")] ReservationVM reservationVM)
        {
            if (ModelState.IsValid)
            {
                var reservation = ReservationVM.MapTo(reservationVM);
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarID = new SelectList(db.Cars, "CarID", "Make", reservationVM.CarID);
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Name", reservationVM.CustomerID);
            return View(reservationVM);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            var reservationVM = ReservationVM.MapTo(reservation);

            //adding Car Make and Customer Name in Reservation Delete
           
            reservationVM.Car = CarVM.MapTo(db.Cars.Find(reservationVM.CarID));
            reservationVM.Customer = CustomerVM.MapTo(db.Customers.Find(reservationVM.CustomerID));
            
            return View(reservationVM);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
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
