namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Request
{
    public class UpdateTicketRequest
    {
        public Guid TicketId { get; set; }
        public double Price { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public int QuantityAvailable { get; set; }
    }
}
