using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ItemCount { get; set; } = 0;

        [Required]
        public double CartPrice { get; set; }

        [Required]
        public double GrandTotalPrice { get; set; }

        [Required]
        public bool CheckOutStatus { get; set; }

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public List<BasketItem> BasketItems { get; set; }
    }
}
