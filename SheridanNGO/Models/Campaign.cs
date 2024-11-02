namespace SheridanNGO.Models{

    public class Campaign
    {
        public int CampaignId { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public decimal GoalAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Foreign Keys
        public int NGOId { get; set; }
        public required NGO  NGO { get; set; }
    }


}