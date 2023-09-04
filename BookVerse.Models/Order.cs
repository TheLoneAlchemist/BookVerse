using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public int CheckoutId { get; set; }
        [ForeignKey("CheckoutId")]
        [ValidateNever]
        public Checkout Checkout { get; set; }

        public string? PayementMode { get; set; }
        public bool OrderStatus { get; set; } = false;

        public bool PaymentStatus { get; set; }

        public List<OrderedItem> OrderedItems { get; set; }

        public bool DeliveryStatus { get; set; } = false;
    }
}
