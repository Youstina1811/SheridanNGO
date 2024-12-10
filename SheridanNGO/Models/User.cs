using Microsoft.AspNet.Identity.EntityFramework;
using System.Numerics;

namespace SheridanNGO.Models
{
    public class User
    {
        // Properties of the User model
        public int UserID { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }  // Changed from PasswordHash to Password

        public string Name { get; set; }  // Changed from FullName to Name

        public string UserType { get; set; }  // Changed from Role to UserType ('donor', 'ngo', etc.)

        public string Phone { get; set; }  // Changed from PhoneNumber to Phone

        public string Address { get; set; }  // Changed from DateCreated (DateTime) to Address (string)

        // Constructor to initialize properties
        public User(string email, string password, string name, string userType, string phone, string address)
        {
            Email = email;
            Password = password;
            Name = name;
            UserType = userType;
            Phone = phone;
            Address = address;
        }

        // Parameterless constructor for Entity Framework and other uses
        public User() { }

        public ICollection<Donation> Donations { get; set; }
    }
}


/*
 * 
 
 * 
 */