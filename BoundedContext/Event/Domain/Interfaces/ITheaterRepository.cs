namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces
{
    public interface ITheaterRepository
    {
        Task<Entities.Theater?> GetByIdAsync(Guid id);
        Task<IEnumerable<Entities.Theater>> GetAllAsync();
        Task SaveAsync(Entities.Theater theater);
        Task UpdateAsync(Entities.Theater theater);
        Task DeleteAsync(Entities.Theater theater);
    }
}
