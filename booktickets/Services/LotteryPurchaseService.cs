using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using booktickets.Models;

// Service interface
public interface ILotteryPurchaseService
{
    Task<LotteryPurchaseResponse> PurchaseTicketAsync(LotteryPurchaseRequest request);
}

// Service implementation
public class LotteryPurchaseService : ILotteryPurchaseService
{
    private readonly LotteryDbContext _context;
    private readonly HashSet<string> _requestCache;

    public LotteryPurchaseService(LotteryDbContext context)
    {
        _context = context;
        _requestCache = new HashSet<string>();
    }

    public async Task<LotteryPurchaseResponse> PurchaseTicketAsync(LotteryPurchaseRequest request)
    {
        string requestHash = $"{request.CustomerId}-{request.DrawId}-{request.NumberOfTickets}-{request.Timestamp}";
        if (_requestCache.Contains(requestHash))
        {
            throw new InvalidOperationException("Duplicate request detected");
        }
        _requestCache.Add(requestHash);

        HttpResponseMessage response = await MockThirdPartyLotteryApi.PurchaseTicketsAsync(request.DrawId, request.NumberOfTickets);
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException("Purchase failed at third-party API");
        }

        var partnerResponse = JsonSerializer.Deserialize<LotteryPurchaseResponse>(await response.Content.ReadAsStringAsync());

        var purchaseRecord = new PurchaseRecord
        {
            Id = partnerResponse.PurchaseId,
            CustomerId = request.CustomerId,
            DrawId = request.DrawId,
            NumberOfTickets = request.NumberOfTickets,
            TotalCost = partnerResponse.TotalCost,
            Timestamp = request.Timestamp
        };

        _context.Purchases.Add(purchaseRecord);
        await _context.SaveChangesAsync();

        return partnerResponse;
    }
}


// Mock third-party API
public class MockThirdPartyLotteryApi
{
    private static readonly ConcurrentDictionary<int, int> TicketStock = new();
    private const int MaxTicketsPerDraw = 500;
    
    public static async Task<HttpResponseMessage> PurchaseTicketsAsync(int drawId, int numberOfTickets)
    {
        int remainingTickets = TicketStock.GetOrAdd(drawId, MaxTicketsPerDraw);
        if (remainingTickets < numberOfTickets)
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
            {
                Content = new StringContent("Not enough tickets available")
            };
        }
        TicketStock[drawId] -= numberOfTickets;
        return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            Content = new StringContent(JsonSerializer.Serialize(new LotteryPurchaseResponse
            {
                PurchaseId = Guid.NewGuid(),
                TotalCost = numberOfTickets * 10m  // Assume $10 per ticket
            }))
        };
    }
}
