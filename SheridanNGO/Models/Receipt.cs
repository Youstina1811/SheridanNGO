using Microsoft.AspNet.Identity.EntityFramework;

namespace SheridanNGO.Models
{
    public class Receipt
    {
        public int ReceiptID { get; set; }
        public int DonationID { get; set; }
        public DateTime ReceiptDate { get; set; }
        public decimal AmountDonated { get; set; }

        // Navigation property
        public Donation Donation { get; set; }
    }

}
