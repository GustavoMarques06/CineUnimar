using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Interfaces;
using Api_Venda_Ingressos.Data;
using Microsoft.EntityFrameworkCore;

namespace Api_Venda_Ingressos.BoundedContext.Auth.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
           return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Value == email);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }    

        public async Task SaveAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            user.SoftDelete();
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
