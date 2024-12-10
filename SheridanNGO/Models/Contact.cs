using System.ComponentModel.DataAnnotations;

namespace SheridanNGO.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
