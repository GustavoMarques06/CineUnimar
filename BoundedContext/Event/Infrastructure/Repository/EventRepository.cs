using Microsoft.EntityFrameworkCore;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.Data;

namespace Api_Venda_Ingressos.BoundedContext.Event.Infrastructure.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly Context _context;

        public EventRepository(Context context)
        {
            _context = context;
        }

        public async Task<Events?> GetByIdAsync(Guid id)
        {
            return await _context.Events.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Events>> GetAllAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task SaveAsync(Events events)
        {
            await _context.Events.AddAsync(events);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Events events)
        {
            _context.Events.Update(events);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Events events)
        {
            events.SoftDelete();
            _context.Events.Update(events);
            await _context.SaveChangesAsync();
        }
    }
}
