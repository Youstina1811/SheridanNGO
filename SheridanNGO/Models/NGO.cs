namespace SheridanNGO.Models{
    public class NGO
    {
        public int NGOId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactInfo { get; set; }

        // Navigation Properties
        public ICollection<Campaign> Campaigns { get; set; }
    }
}