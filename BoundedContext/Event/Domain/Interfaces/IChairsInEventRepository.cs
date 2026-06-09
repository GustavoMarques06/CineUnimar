using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;

namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces
{
    public interface IChairsInEventRepository
    {
        Task<ChairsInEvent?> GetByIdAsync(Guid id);
        Task<IEnumerable<ChairsInEvent>> GetAllAsync();
        Task SaveAsync(ChairsInEvent chairsInEvent);
        Task UpdateAsync(ChairsInEvent chairsInEvent);
        Task DeleteAsync(ChairsInEvent chairsInEvent);
    }
}
