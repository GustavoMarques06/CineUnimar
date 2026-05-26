namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

public interface IEventRepository
{
    Task<Entities.Event?> GetByIdAsync(Guid id);
    Task<IEnumerable<Entities.Event>> GetAllAsync();
    Task SaveAsync(Entities.Event user);
    Task UpdateAsync(Entities.Event user);
    Task DeleteAsync(Entities.Event user);
}