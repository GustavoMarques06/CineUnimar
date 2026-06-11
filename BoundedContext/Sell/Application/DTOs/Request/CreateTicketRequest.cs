using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Enums;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Request
{
    public class CreateTicketRequest
    {
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public Guid ChairInEventId { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

    }
}
