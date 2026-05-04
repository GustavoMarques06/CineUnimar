using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IEnumerable<Ticket>> GetAllAsync(CancellationToken ct = default);
        Task SaveAsync(Ticket ticket, CancellationToken ct = default);
        Task UpdateAsync(Ticket ticket, CancellationToken ct = default);
        Task DeleteAsync(Ticket ticket, CancellationToken ct = default);
    }
}
