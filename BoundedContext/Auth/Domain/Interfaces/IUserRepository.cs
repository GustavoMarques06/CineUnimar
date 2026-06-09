using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Entities;

namespace Api_Venda_Ingressos.BoundedContext.Auth.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByCpfAsync(string cpf);
        Task<IEnumerable<User>> GetAllAsync();
        Task SaveAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}