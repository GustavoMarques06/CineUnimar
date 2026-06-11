using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task SaveAsync(ChairsInEvent chairInEvent)
        {
            await _context.ChairsInEvent.AddAsync(chairInEvent);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ChairsInEvent chairInEvent)
        {
            _context.ChairsInEvent.Update(chairInEvent);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ChairsInEvent chairInEvent)
        {
            chairInEvent.SoftDelete();
            _context.ChairsInEvent.Update(chairInEvent);
            await _context.SaveChangesAsync();
        }
    }
}
