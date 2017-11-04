using MyRental.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyRental.ViewModels
{
    public class CarVM
    {
        public CarVM()
        {
            this.Reservations = new HashSet<Reservation>();
        }

        public int CarID { get; set; }

        [StringLength(20, ErrorMessage = "The {0} must be minimum {2} and maximum {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Car Make")]
        [RegularExpression("^[a-zA-Z_ ]+$", ErrorMessage = "Only alphabetical and space characters are permitted.")]
        [Required]
        public string Make { get; set; }

        
        [Display(Name = "Car Type")]
        [Required]
        public CarType Type { get; set; }

        [Display(Name = "Car Consumption[km/l]")]
        [Required]
        [Range(1, 35, ErrorMessage = "Only positive numbers are permitted.")]
        public int? Consumption_km_l_ { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

        public string TestForGit { get; set; }
		public string TestForGit2 { get; set; }

        public static CarVM MapTo(Car car)
        {
            return new CarVM
            {
                CarID = car.CarID,
                Make = car.Make,
                Consumption_km_l_ = car.Consumption_km_l_,
                Type = (CarType)car.Type
            };
        }

        public static Car MapTo(CarVM carVM)
        {
            return new Car
            {
                CarID = carVM.CarID,
                Make = carVM.Make,
                Consumption_km_l_ = carVM.Consumption_km_l_,
                Type = (int)carVM.Type
            };
        }


    }
}