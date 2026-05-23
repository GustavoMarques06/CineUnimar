namespace Api_Venda_Ingressos.BoundedContext.Auth.Domain.Entities
{
    public class EntityBase
    {
        public Guid Id { get; private set; }
        public DateTime? RemovedAt { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }
}
