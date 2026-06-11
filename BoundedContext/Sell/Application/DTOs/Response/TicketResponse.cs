using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Enums;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Response
{
    public class TicketResponse
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public Guid ChairInEventId { get; set; }
        public double Price { get; set; }
        public DateTime Purchase_Data { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
    }
}
