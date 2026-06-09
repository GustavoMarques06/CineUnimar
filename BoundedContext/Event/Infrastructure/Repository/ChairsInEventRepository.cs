using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.Data;

namespace Api_Venda_Ingressos.BoundedContext.Event.Infrastructure.Repository
{
    public class ChairsInEventRepository : IChairsInEventRepository
    {
        private readonly Context _context;

        public ChairsInEventRepository(Context context)
        {
            _context = context;
        }

        public async Task<ChairsInEvent?> GetByIdAsync(Guid id)
        {
            return await _context.ChairsInEvent.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<ChairsInEvent>> GetAllAsync()
        {
            return await _context.ChairsInEvent.ToListAsync();
        }

        public async Task SaveAsync(Chair chair)
        {
            await _context.Chairs.AddAsync(chair);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Chair chair)
        {
            _context.Chairs.Update(chair);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Chair chair)
        {
            chair.SoftDelete();
            _context.Chairs.Update(chair);
            await _context.SaveChangesAsync();
        }
    }
}
