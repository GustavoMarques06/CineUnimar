using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.Data;
using Microsoft.EntityFrameworkCore;

namespace Api_Venda_Ingressos.BoundedContext.Event.Infrastructure.Repository
{
    public class RoomEventRepository : IRoomEventRepository
    {
        private readonly Context _context;

        public RoomEventRepository(Context context)
        {
            _context = context;
        }

        public async Task<RoomEvent?> GetByIdAsync(Guid id)
        {
           return await _context.RoomsEvent.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<RoomEvent>> GetAllAsync()
        {
            return await _context.RoomsEvent.ToListAsync();
        }    

        public async Task SaveAsync(RoomEvent room)
        {
            await _context.RoomsEvent.AddAsync(room);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RoomEvent room)
        {
            _context.RoomsEvent.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RoomEvent room)
        {
            room.SoftDelete();
            _context.RoomsEvent.Update(room);
            await _context.SaveChangesAsync();
        }
    }
}
