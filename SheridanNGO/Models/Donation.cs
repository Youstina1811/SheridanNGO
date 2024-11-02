namespace SheridanNGO.Models{
    public class Donation
    {
        public int DonationId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        // Foreign Keys
        public int UserId { get; set; }
        public User User { get; set; }
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }
    }
}