using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.Data;
using Microsoft.EntityFrameworkCore;

namespace Api_Venda_Ingressos.BoundedContext.Event.Infrastructure.Repository
{
    public class TheaterRepository : ITheaterRepository
    {
        private readonly Context _context;

        public TheaterRepository(Context context)
        {
            _context = context;
        }

        public async Task<Theater?> GetByIdAsync(Guid id)
        {
           return await _context.Theaters.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<Theater>> GetAllAsync()
        {
            return await _context.Theaters.ToListAsync();
        }    

        public async Task SaveAsync(Theater theater)
        {
            await _context.Theaters.AddAsync(theater);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Theater theater)
        {
            _context.Theaters.Update(theater);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Theater theater)
        {
            theater.SoftDelete();
            _context.Theaters.Update(theater);
            await _context.SaveChangesAsync();
        }
    }
}
