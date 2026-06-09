using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.Data;
using Microsoft.EntityFrameworkCore;

namespace Api_Venda_Ingressos.BoundedContext.Event.Infrastructure.Repository
{
    public class ChairRepository : IChairRepository
    {
        private readonly Context _context;

        public ChairRepository(Context context) {
            _context = context;
        }

        public async Task<Chair?> GetByIdAsync(Guid id)
        {
            return await _context.Chairs.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Chair>> GetAllAsync()
        {
            return await _context.Chairs.ToListAsync();
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
