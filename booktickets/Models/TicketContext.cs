using Microsoft.EntityFrameworkCore;

namespace booktickets.Models;

public class TicketContext : DbContext
{
    public TicketContext(DbContextOptions<TicketContext> options)
        : base(options)
    {
    }

    public DbSet<TicketSale> TicketSales { get; set; } = null!;
}