using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.Models
{
    public class Checkout
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Mobile is Required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter a valid phone number")]
        public string Phone { get; set; }

        public int AddressId { get; set; }
        [ValidateNever] 
        public Address Address { get; set; }

        public string UserId { get; set; }

        public double TotalPrice { get; set; }

        public int ShippingPrice { get; set; } = 0;

        public int CouponDiscount { get; set; } = 0;

        public bool Status { get; set; } = false;

        public int CartId { get; set; }

        [ValidateNever]
        public Cart Cart { get; set; }


        public Checkout(double TotalPrice)
        {
            this.TotalPrice = TotalPrice;
        }


        public Checkout()
        {

        }
    }
}
