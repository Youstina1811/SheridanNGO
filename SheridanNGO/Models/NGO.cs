namespace SheridanNGO.Models{
    public class NGO
    {
        public int NGOID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Mission { get; set; }
        public string Website { get; set; }
        public string ContactEmail { get; set; }

        // Navigation property for campaigns
        public ICollection<Campaign> Campaigns { get; set; }
        public ICollection<Donation> Donations { get; set; }
    }

}