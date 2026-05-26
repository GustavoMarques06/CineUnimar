namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces
{
    public interface IChairsInEventRepository
    {
        Task<Entities.ChairsInEvent?> GetByIdAsync(Guid id);
        Task<IEnumerable<Entities.ChairsInEvent>> GetAllAsync();
        Task SaveAsync(Entities.ChairsInEvent chairsInEvent);
        Task UpdateAsync(Entities.ChairsInEvent chairsInEvent);
        Task DeleteAsync(Entities.ChairsInEvent chairsInEvent);
    }
}
