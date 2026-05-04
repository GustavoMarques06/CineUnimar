using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Infrastructure.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Ticket?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Tickets.FirstOrDefaultAsync(u => u.Id == id, ct);
        }
        public async Task<IEnumerable<Ticket>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Tickets.ToListAsync(ct);
        }

        public async Task SaveAsync(Ticket ticket, CancellationToken ct = default)
        {
            await _context.Tickets.AddAsync(ticket, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Ticket ticket, CancellationToken ct = default)
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(Ticket ticket, CancellationToken ct = default)
        {
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync(ct);
        }
    }
}
