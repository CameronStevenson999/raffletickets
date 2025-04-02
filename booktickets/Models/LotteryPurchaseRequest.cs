using System;

namespace booktickets.Models
{

    public class LotteryPurchaseRequest
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int DrawId { get; set; }
        public int NumberOfTickets { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
