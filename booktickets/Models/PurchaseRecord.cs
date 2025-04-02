using System;

namespace booktickets.Models
{

    public class PurchaseRecord
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public int DrawId { get; set; }
        public int NumberOfTickets { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
