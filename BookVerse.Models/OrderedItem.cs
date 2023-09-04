using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.Models
{
    public class OrderedItem
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public double NetPrice { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
