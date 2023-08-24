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
        public ICollection<BasketItem> Items { get; set; }
        public string UserId { get; set; }
        public double CartPrice { get; set; }
    }
}
