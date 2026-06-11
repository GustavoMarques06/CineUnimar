using Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;  

namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Request
{
    public class SellTicketRequest
    {
        public Guid TicketId { get; set; }

        public Guid UserId { get; set; }

        public Guid EventId { get; set; }

        public Guid ChairInEventId { get; set; }

        public double Price { get; set; }
        
    }
}
