using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.Services
{
    public class TicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Ticket> CreateAsync(
            Price price,
            Location location,
            Date data,
            Quantity quantityAvailable,
            CancellationToken ct = default)
        {
            var ticket = new Ticket(
                price,
                location,
                data,
                new Quantity(0), // inicialmente ninguém comprou
                quantityAvailable
            );

            await _ticketRepository.SaveAsync(ticket, ct);

            return ticket;
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync(CancellationToken ct = default)
        {
            return await _ticketRepository.GetAllAsync(ct);
        }

        public async Task<Ticket?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _ticketRepository.GetByIdAsync(id, ct);
        }

        public async Task SellAsync(Guid ticketId, int quantity, CancellationToken ct = default)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId, ct);

            if (ticket == null)
                throw new Exception("Ticket não encontrado");

            // regra de negócio
            if (ticket.Quantity_available.Value < quantity)
                throw new Exception("Ingressos insuficientes");

            var newBought = new Quantity(ticket.Quantity_bought.Value + quantity);
            var newAvailable = new Quantity(ticket.Quantity_available.Value - quantity);

            ticket.UpdateTicket(
                ticket.Price,
                ticket.Location,
                ticket.Data,
                newBought,
                newAvailable
            );

            await _ticketRepository.UpdateAsync(ticket, ct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id, ct);

            if (ticket == null)
                throw new Exception("Ticket não encontrado");

            await _ticketRepository.DeleteAsync(ticket, ct);
        }
    }
}
