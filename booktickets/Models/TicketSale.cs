using System;

namespace booktickets.Models
{

    public class TicketSale
    {
        public long Id { get; set; }
        public string? CustomerId { get; set; }
        public long DrawId { get; set; }
        public int NumberOfTickets { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
