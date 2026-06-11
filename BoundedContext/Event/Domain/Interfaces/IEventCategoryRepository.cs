namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces
{
    public interface IEventCategoryRepository
    {
        Task<Entities.EventCategory?> GetByIdAsync(Guid id);
        Task<IEnumerable<Entities.EventCategory>> GetAllAsync();
        Task SaveAsync(Entities.EventCategory eventCategory);
        Task UpdateAsync(Entities.EventCategory eventCategory);
        Task DeleteAsync(Entities.EventCategory eventCategory);
    }
}
