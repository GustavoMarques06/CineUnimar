using Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities
{
    public class Ticket : EntityBase
    {
        public Price Price { get; private set; }

        public Location Location { get; private set; }

        public Date Data { get; private set; }

        public Quantity Quantity_bought { get; private set; }

        public Quantity Quantity_available { get; private set; }

        // Construtor vazio para o EF Core
        private Ticket()
        {
        }

        public Ticket(
            Price price,
            Location location,
            Date data,
            Quantity quantity_bought,
            Quantity quantity_available)
        {
            Price = price;
            Location = location;
            Data = data;
            Quantity_bought = quantity_bought;
            Quantity_available = quantity_available;
        }

        public void UpdateTicket(
            Price price,
            Location location,
            Date data,
            Quantity quantity_bought,
            Quantity quantity_available)
        {
            Price = price;
            Location = location;
            Data = data;
            Quantity_bought = quantity_bought;
            Quantity_available = quantity_available;
        }

        public void Sell(Quantity quantity)
        {
            if (Quantity_available.value < quantity.value)
                throw new Exception("Ingressos insuficientes");

            Quantity_bought =
                new Quantity(Quantity_bought.value + quantity.value);

            Quantity_available =
                new Quantity(Quantity_available.value - quantity.value);
        }
    }
}

