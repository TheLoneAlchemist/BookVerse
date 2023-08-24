using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.Models
{
    public class BasketItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Product Product { get; set; }
        [Required]
        public int Quantity { get; set; } = 1;
        public  DateTime AddedOn { get; set; }

        public double NetPrice { get; set; }
        public bool Status { get; set; } = false;
    }
}
