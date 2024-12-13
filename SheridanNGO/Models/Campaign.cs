namespace SheridanNGO.Models
{

    public class Campaign
    {
        public int CampaignID { get; set; }

        public int NGOID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal GoalAmount { get; set; }

  /*      public string ImageUrl { get; set; }*/
        public decimal CurrentAmount { get; set; }  // Changed from RaisedAmount to CurrentAmount

        public DateTime StartDate { get; set; }  // Replaced Deadline with StartDate
        public DateTime EndDate { get; set; }    // Replaced Deadline with EndDate

        public string CauseCategory { get; set; } // Cause category (e.g., 'education', 'healthcare', etc.)

        public string imageURL { get; set; }

        // Navigation property
        public NGO NGO { get; set; }

        public Campaign(int campaignID, int nGOID, string title, string description, decimal goalAmount,  decimal currentAmount, DateTime startDate, DateTime endDate, string causeCategory, NGO nGO)
        {
            CampaignID = campaignID;
            NGOID = nGOID;
            Title = title;
            Description = description;
            GoalAmount = goalAmount;
         /*   ImageUrl = imageUrl;*/
            CurrentAmount = currentAmount;
            StartDate = startDate;
            EndDate = endDate;
            CauseCategory = causeCategory;
            NGO = nGO;
        }

        public Campaign()
        {

        }



    }
}