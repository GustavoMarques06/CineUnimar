using Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities
{
    public class Ticket : EntityBase
    {
        public Price Price { get; private set; }

        public Location Location { get; private set; }

        public Date Data {get; private set; }

        public Quantity Quantity_bought {get; private set; }

        public Quantity Quantity_avaliable {get; private set; }

        public Ticket(Price price, Location location, Date data, Quantity quantity_bought, Quantity quantity_avaliable)
        {
            Price = price;
            Location = location;
            Data = data;
            Quantity_bought = quantity_bought;
            Quantity_avaliable = quantity_avaliable;
        }

        public void UpdateTicket(Price price, Location location, Date data, Quantity quantity_bought, Quantity quantity_avaliable)
        {
            this.Price = price;
            this.Location = location;
            this.Data = data;
            this.Quantity_bought = quantity_bought;
            this.Quantity_avaliable = quantity_avaliable;
        }
        
        public void Sell(Quantity quantity_bought, Quantity quantity_avaliable)
        {
            
        }
    }
}