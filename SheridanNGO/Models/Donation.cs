namespace SheridanNGO.Models{
    public class Donation
    {
        public int DonationID { get; set; }
        public int DonorID { get; set; }
        public decimal Amount { get; set; }
        public DateTime DonationDate { get; set; }
        public bool Recurring { get; set; }
        public int NGOID { get; set; }

        // Navigation properties
        public User Donor { get; set; }
        public NGO NGO { get; set; }
    }

}