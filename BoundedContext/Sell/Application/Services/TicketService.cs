
using Api_Venda_Ingressos.BoundedContext.Sell.Application.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Ticket> SaveAsync(
            Price price,
            Location location,
            Date data,
            Quantity quantityAvailable)
        {
            var ticket = new Ticket(
                price,
                location,
                data,
                new Quantity(0),
                quantityAvailable
            );

            await _ticketRepository.SaveAsync(ticket);

            return ticket;
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await _ticketRepository.GetAllAsync();
        }

        public async Task<Ticket?> GetByIdAsync(Guid id)
        {
            return await _ticketRepository.GetByIdAsync(id);
        }

        public async Task SellAsync(Guid ticketId, int quantity)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);

            if (ticket == null)
                throw new Exception("Ticket não encontrado");

            if (ticket.Quantity_available.value < quantity)
                throw new Exception("Ingressos insuficientes");

            var newBought =
                new Quantity(ticket.Quantity_bought.value + quantity);

            var newAvailable =
                new Quantity(ticket.Quantity_available.value - quantity);

            ticket.UpdateTicket(
                ticket.Price,
                ticket.Location,
                ticket.Data,
                newBought,
                newAvailable
            );

            await _ticketRepository.UpdateAsync(ticket);
        }

        public async Task DeleteAsync(Guid id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);

            if (ticket == null)
                throw new Exception("Ticket não encontrado");

            await _ticketRepository.DeleteAsync(ticket);
        }
    }
}

