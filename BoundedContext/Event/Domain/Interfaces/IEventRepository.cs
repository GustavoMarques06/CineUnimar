
namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

public interface IEventRepository
{
    Task<Entities.Events?> GetByIdAsync(Guid id);
    Task<IEnumerable<Entities.Events>> GetAllAsync();
    Task SaveAsync(Entities.Events events);
    Task UpdateAsync(Entities.Events events);
    Task DeleteAsync(Entities.Events events);
}