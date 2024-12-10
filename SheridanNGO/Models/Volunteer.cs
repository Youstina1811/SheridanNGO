using System.ComponentModel.DataAnnotations;

namespace SheridanNGO.Models
{
    public class Volunteer
    {       
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Days { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
