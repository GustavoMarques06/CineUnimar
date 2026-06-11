using Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Enums;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities
{
    public class Ticket : EntityBase
    {
        public Guid EventId { get; private set; }

        public Guid ChairInEventId { get; private set; }

        public Guid UserId { get; private set; }


        public Price Price { get; private set; }

        //public Location Location { get; private set; }

        public Date Purchase_Data { get; private set; }

        public PaymentStatus Status { get; private set; }

        public Quantity Quantity_bought { get; private set; }

        public Quantity Quantity_available { get; private set; }

        // Construtor vazio para o EF Core
        private Ticket()
        {
        }

        public Ticket(
             Guid eventId,
            Guid chairInEventId,
            Guid userId,
            Price price
            //Location location,
            //Quantity quantity_bought,//Quantity quantity_available
            )
        {
            EventId = eventId;
            ChairInEventId = chairInEventId;
            UserId = userId;
            Price = price;
            //Location = location;
            Purchase_Data = new Date(DateTime.UtcNow);
            Status = PaymentStatus.Pending;
            //Quantity_bought = quantity_bought;
            //Quantity_available = quantity_available;
        }

        public void UpdateTicket(
            Guid eventId,
            Guid chairInEventId,
            Guid userId,
            Price price
            //Location location,
            //Quantity quantity_bought,  //Quantity quantity_available
           )
        {
            UserId = userId;
            EventId = eventId;
            ChairInEventId = chairInEventId;
            Price = price;
            //Location = location;
            //Quantity_bought = quantity_bought;
            //Quantity_available = quantity_available;
        }

        //Função de Venda de Ingresso.

        /*public void Sell(Quantity quantity)
        {
            if (PaymentStatus != PaymentStatus.Approved)
                throw new Exception(
                    "Pagamento não aprovado");

            if (Quantity_available.value < quantity.value)
                throw new Exception(
                    "Ingressos insuficientes");

            Quantity_bought =
                new Quantity(
                    Quantity_bought.value + quantity.value);

            Quantity_available =
                new Quantity(
                    Quantity_available.value - quantity.value);
        }*/

        //Metodos para aprovar ou rejeitar o pagamento
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

