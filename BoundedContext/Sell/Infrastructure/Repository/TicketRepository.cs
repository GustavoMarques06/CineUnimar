using Api_Venda_Ingressos.Data;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Infrastructure.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly Context _context;

        public TicketRepository(Context context)
        {
            _context = context;
        }

        public async Task<Ticket?> GetByIdAsync(Guid id)
        {
            return await _context.Ticket.FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await _context.Ticket.ToListAsync();
        }

        public async Task SaveAsync(Ticket ticket)
        {
            await _context.Ticket.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ticket ticket)
        {
            _context.Ticket.Update(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Ticket ticket)
        {
            ticket.SoftDelete();
            _context.Ticket.Update(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByChairInEventIdAsync(Guid chairInEventId)
        {
            return await _context.Ticket.AnyAsync(t => t.ChairInEventId == chairInEventId);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByEventIdAsync(Guid eventId)
        {
            return await _context.Ticket.Where(t => t.EventId == eventId).ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Ticket.Where(t => t.UserId == userId).ToListAsync();
        }
    }
}
