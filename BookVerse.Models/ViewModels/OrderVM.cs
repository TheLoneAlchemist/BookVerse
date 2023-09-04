using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.Models.ViewModels
{
    public class OrderVM
    {
        public double TotalPrice { get; set; }

        [ValidateNever]
        public IEnumerable<string> PaymentOption { get; set; }

        public int CheckoutId { get; set; }
    }
}
