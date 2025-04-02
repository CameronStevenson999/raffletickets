using Microsoft.AspNetCore.Mvc;
using booktickets.Models;

[ApiController]
[Route("api/lottery")]
public class LotteryPurchaseController : ControllerBase
{
    private readonly ILotteryPurchaseService _lotteryPurchaseService;

    public LotteryPurchaseController(ILotteryPurchaseService lotteryPurchaseService)
    {
        _lotteryPurchaseService = lotteryPurchaseService;
    }

    [HttpPost("purchase")]
    public async Task<IActionResult> PurchaseTicket([FromBody] LotteryPurchaseRequest request)
    {
        try
        {
            var response = await _lotteryPurchaseService.PurchaseTicketAsync(request);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}