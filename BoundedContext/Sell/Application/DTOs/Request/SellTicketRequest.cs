namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Request
{
    public class SellTicketRequest
    {
        public Guid EventId { get; set; }
        public Guid ChairInEventId { get; set; }
    }
}
