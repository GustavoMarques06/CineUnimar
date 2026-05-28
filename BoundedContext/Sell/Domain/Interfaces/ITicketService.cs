using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.Interfaces
{
    public interface ITicketService
    {
        Task<Ticket> SaveAsync(Price price, Location location, Date data, Quantity quantityAvailable);

        Task<IEnumerable<Ticket>> GetAllAsync();

        Task<Ticket?> GetByIdAsync(Guid id);

        Task SellAsync(Guid ticketId, int quantity);

        Task DeleteAsync(Guid id);
    }
}

