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

        public int ProductId { get; set; }
        [ForeignKey("Product")]
        public Product Product { get; set; }

        public int UserId { get; set; }
        [ForeignKey("ApplicationUser")]
        public ApplicationUser User { get; set; }
    }
}
