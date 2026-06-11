namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces
{
    public interface IRoomRepository
    {
        Task<Entities.Room?> GetByIdAsync(Guid id);
        Task<IEnumerable<Entities.Room>> GetAllAsync();
        Task SaveAsync(Entities.Room room);
        Task UpdateAsync(Entities.Room room);
        Task DeleteAsync(Entities.Room room);
    }
}
