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
        public String Name { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String? Phone { get; set; }
        public String? Address { get; set; }

        public UserType UserType { get; set; }
      
        public User(string name, string email, string password, string phone, string address, UserType type = UserType.Donor)
        {
            Name = name;
            Email = email;
            Password = password;
            Phone = phone ?? "";
            Address = address ?? "";
        }

        public User(string name,string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }
        public User() { }

        public ICollection<Donation>? Donations { get; set; }
    }
}
