using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.Models.ViewModels
{
    public class MyOrderVM
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double NetPrice { get; set; }
        public bool DeliveryStatus { get; set; }
    }
}
