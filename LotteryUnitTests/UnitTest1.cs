using Microsoft.EntityFrameworkCore;
using Xunit;
using booktickets.Models;

namespace LotteryUnitTests;

public class LotteryPurchaseTests
{
    private readonly LotteryDbContext _context;
    private readonly ILotteryPurchaseService _service;

    public LotteryPurchaseTests()
    {
        var options = new DbContextOptionsBuilder<LotteryDbContext>()
            .UseInMemoryDatabase(databaseName: "LotteryTestDb")
            .Options;
        _context = new LotteryDbContext(options);
        _service = new LotteryPurchaseService(_context);
    }

    [Fact]
    public async Task PurchaseTicket_SuccessfulPurchase()
    {
        var request = new LotteryPurchaseRequest
        {
            CustomerId = Guid.NewGuid(),
            DrawId = 1,
            NumberOfTickets = 2,
            Timestamp = DateTime.UtcNow
        };

        var response = await _service.PurchaseTicketAsync(request);
        Assert.NotNull(response);
        Assert.Equal(20m, response.TotalCost);
    }

    [Fact]
    public async Task PurchaseTicket_Fails_WhenDrawIsSoldOut()
    {
        var request = new LotteryPurchaseRequest
        {
            CustomerId = Guid.NewGuid(),
            DrawId = 1,
            NumberOfTickets = 600,
            Timestamp = DateTime.UtcNow
        };

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _service.PurchaseTicketAsync(request));
    }

    [Fact]
    public async Task PurchaseTicket_Fails_OnDuplicateRequest()
    {
        var request = new LotteryPurchaseRequest
        {
            CustomerId = Guid.NewGuid(),
            DrawId = 1,
            NumberOfTickets = 2,
            Timestamp = DateTime.UtcNow
        };

        await _service.PurchaseTicketAsync(request);

        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await _service.PurchaseTicketAsync(request));
    }
}
