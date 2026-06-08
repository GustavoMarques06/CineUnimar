using Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;  

namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Request
{
    public class SellTicketRequest
    {
        public Guid TicketId { get; set; }
        public Quantity quantity_bought { get; set; }
    }
}
