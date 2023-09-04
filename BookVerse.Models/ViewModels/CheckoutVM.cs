using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.Models.ViewModels
{
    public class CheckoutVM
    {
        public Checkout Checkout { get; set; }

        public Address Address { get; set; }
        [ValidateNever]
        public Cart Cart { get; set; }
    }
}
