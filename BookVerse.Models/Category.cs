using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookVerse.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30,ErrorMessage ="Max character is 30")]
        public string Name { get; set; }
        [DisplayName("Category Description")]
        public string Description { get; set; }
        [Range(1,100,ErrorMessage ="Range should be 1 to 100!")]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
    }
}
