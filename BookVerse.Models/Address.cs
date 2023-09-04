using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]

        public string? Street { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public string? ZipCode { get; set; }
        [Required]
        public string? State { get; set; }

        [Required]
        public string? Country { get; set; }
        [Required]
        public string? UserId { get; set; }
    }
}
