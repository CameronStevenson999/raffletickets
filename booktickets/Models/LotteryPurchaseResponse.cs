using System;

namespace booktickets.Models
{

    public class LotteryPurchaseResponse
    {
        public Guid PurchaseId { get; set; }
        public decimal TotalCost { get; set; }
    }
}
