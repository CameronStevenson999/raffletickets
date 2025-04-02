using Microsoft.EntityFrameworkCore;

namespace booktickets.Models;

public class LotteryDbContext : DbContext
{
    public LotteryDbContext(DbContextOptions<LotteryDbContext> options)
        : base(options)
    {
    }

    public DbSet<PurchaseRecord> Purchases { get; set; } = null!;
}
