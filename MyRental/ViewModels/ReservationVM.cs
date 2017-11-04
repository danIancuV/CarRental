using MyRental.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyRental.ViewModels
{
    public class ReservationVM
    {
        public int ReservationID { get; set; }
        public int CarID { get; set; }
        public int CustomerID { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Please enter the date in the following format: dd mon yyyy")]
        public Nullable<System.DateTime> StartDate { get; set; }


        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Please enter the date in the following format: dd mon yyyy")]
        public Nullable<System.DateTime> EndDate { get; set; }

        public virtual CarVM Car { get; set; }
        public virtual CustomerVM Customer { get; set; }

        public static ReservationVM MapTo(Reservation reservation)
        {
            return new ReservationVM
            {
                ReservationID = reservation.ReservationID,
                CarID = reservation.CarID,
                CustomerID = reservation.CustomerID,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
            };
        }

        public static Reservation MapTo(ReservationVM reservationVM)
        {
            return new Reservation
            {
                ReservationID = reservationVM.ReservationID,
                CarID = reservationVM.CarID,
                CustomerID = reservationVM.CustomerID,
                StartDate = reservationVM.StartDate,
                EndDate = reservationVM.EndDate,
            };
        }
    }
}