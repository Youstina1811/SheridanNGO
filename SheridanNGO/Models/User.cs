namespace SheridanNGO.Models
{

    public enum UserType
    {   Donor,
        NGO,
        Admin
    }

    public class User
    {
        private String Name { get; set; }
        private String Email { get; set; }
        private String Password { get; set; }
        private String Phone { get; set; }
        private String Address { get; set; }
      
        public User(string name, string email, string password, string phone, string address)
        {
            Name = name;
            Email = email;
            Password = password;
            Phone = phone;
            Address = address;
        }

        public User() { }

        public ICollection<Donation> Donations { get; set; }
    }
}
