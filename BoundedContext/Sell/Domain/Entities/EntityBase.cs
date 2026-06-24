namespace Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities
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

        public void SoftDelete()
        {
            if (RemovedAt.HasValue)
                throw new InvalidOperationException("Entidade já foi removida.");
            RemovedAt = DateTime.UtcNow;
        }
    }
}
