using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookVerse.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [PersonalData]
        public string FirstName { get; set; }
        [Required]
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public string Mobile { get; set; }
        [PersonalData]
        public string? Street { get; set; }
        [PersonalData]
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set;}
    }
}
