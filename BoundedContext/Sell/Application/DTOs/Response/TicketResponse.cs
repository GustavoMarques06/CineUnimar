namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Response
{
    public class TicketResponse
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public int QuantityBought { get; set; }
        public int QuantityAvailable { get; set; }
    }
}
