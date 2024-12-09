using Microsoft.AspNet.Identity.EntityFramework;

namespace SheridanNGO.Models
{

    public enum UserType
    {   Donor,
        NGO,
        Admin
    }

    public class User
    {
        public int UserID { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }  // Changed from PasswordHash to Password

        public string Name { get; set; }  // Changed from FullName to Name

        public string UserType { get; set; }  // Changed from Role to UserType ('donor', 'ngo', etc.)

        public string Phone { get; set; }  // Changed from PhoneNumber to Phone

        public string Address { get; set; }  // Changed from DateCreated (DateTime) to Address (string)

        public DateTime DateCreated { get; set; }  // This remains unchanged to track account creation date.

        // Navigation property for donations
        public ICollection<Donation> Donations { get; set; }
    }


}
