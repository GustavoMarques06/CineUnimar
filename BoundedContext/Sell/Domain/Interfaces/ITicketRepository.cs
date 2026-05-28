using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket?> GetByIdAsync(Guid id);
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task SaveAsync(Ticket ticket);

        Task UpdateAsync(Ticket ticket);
        Task DeleteAsync(Ticket ticket);
    }
}
