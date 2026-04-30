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
        }
    }
}
