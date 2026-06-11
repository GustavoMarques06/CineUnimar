namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces
{
    public interface IRoomEventRepository
    {
        Task<Entities.RoomEvent?> GetByIdAsync(Guid id);
        Task<IEnumerable<Entities.RoomEvent>> GetAllAsync();
        Task SaveAsync(Entities.RoomEvent roomEvent);
        Task UpdateAsync(Entities.RoomEvent roomEvent);
        Task DeleteAsync(Entities.RoomEvent roomEvent);
    }
}
