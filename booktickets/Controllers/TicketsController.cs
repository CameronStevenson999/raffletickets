using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using booktickets.Models;

namespace booktickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly TicketContext _context;

        public TicketsController(TicketContext context)
        {
            _context = context;
        }

        // GET: api/tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketSale>>> GetTicket()
        {
            return await _context.TicketSales.ToListAsync();
        }

        // GET: api/tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketSale>> GetTicket(long id)
        {
            var ticket = await _context.TicketSales.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return ticket;
        }

        // PUT: api/tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(long id, TicketSale ticket)
        {
            if (id != ticket.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TicketSale>> PostTicket(TicketSale ticket)
        {
            _context.TicketSales.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
        }

        // DELETE: api/tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(long id)
        {
            var ticket = await _context.TicketSales.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.TicketSales.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(long id)
        {
            return _context.TicketSales.Any(e => e.Id == id);
        }
    }
}
