using MyRental.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyRental.ViewModels
{
    public class CustomerVM
    {
        public CustomerVM()
        {
            this.Reservations = new HashSet<Reservation>();
        }

        public int CustomerID { get; set; }

        [StringLength(30, ErrorMessage = "The {0} must be minimum {2} and maximum {1} characters long.", MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z_ ]+$", ErrorMessage = "Only alphabetical and space characters are permitted.")]
        [Display(Name = "Customer Name")]
        [Required]
        public string Name { get; set; }

        [StringLength(13, ErrorMessage = "The {0} must be {1} characters long.", MinimumLength = 13)]
        [RegularExpression(@"^[0-9]{13}$", ErrorMessage = "The SSN must be 13 characters long, only numbers are permitted.")]
        [Display(Name = "Customer SSN")]
        [Required]
        public string SSN { get; set; }

        [StringLength(30, ErrorMessage = "The {0} must be minimum {2} and maximum {1} characters long.", MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z_ ]+$", ErrorMessage = "Only alphabetical and space characters are permitted.")]
        [Display(Name = "Customer Town")]
        [Required]
        public string Town { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

        public static CustomerVM MapTo(Customer customer)
        {
            return new CustomerVM
            {
                CustomerID = customer.CustomerID,
                Name = customer.Name,
                SSN = customer.SSN,
                Town = customer.Town
            };
        }

        public static Customer MapTo(CustomerVM customerVM)
        {
            return new Customer
            {
                CustomerID = customerVM.CustomerID,
                Name = customerVM.Name,
                SSN = customerVM.SSN,
                Town = customerVM.Town
            };
        }
    }
}