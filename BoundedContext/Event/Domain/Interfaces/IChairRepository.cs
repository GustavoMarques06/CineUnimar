namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces
{
    public interface IChairRepository
    {
        Task<Entities.Chair?> GetByIdAsync(Guid id);
        Task<IEnumerable<Entities.Chair>> GetAllAsync();
        Task SaveAsync(Entities.Chair chair);
        Task UpdateAsync(Entities.Chair chair);
        Task DeleteAsync(Entities.Chair chair);
    }
}
