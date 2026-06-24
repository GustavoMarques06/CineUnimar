using Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Enums;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities
{
    public class Ticket : EntityBase
    {
        public Guid EventId { get; private set; }
        public Guid ChairInEventId { get; private set; }
        public Guid UserId { get; private set; }
        public Price Price { get; private set; } = null!;
        public Date PurchaseDate { get; private set; } = null!;
        public PaymentStatus Status { get; private set; }

        private Ticket()
        {
        }

        public Ticket(
            Guid eventId,
            Guid chairInEventId,
            Guid userId,
            Price price)
        {
            EventId = eventId;
            ChairInEventId = chairInEventId;
            UserId = userId;
            Price = price;
            PurchaseDate = new Date(DateTime.UtcNow);
            Status = PaymentStatus.Pending;
        }

        public void UpdateTicket(
            Guid eventId,
            Guid chairInEventId,
            Guid userId,
            Price price)
        {
            UserId = userId;
            EventId = eventId;
            ChairInEventId = chairInEventId;
            Price = price;
        }

        public void ApprovePayment()
        {
            Status = PaymentStatus.Approved;
        }

        public void RejectPayment()
        {
            Status = PaymentStatus.Rejected;
        }
    }
}
