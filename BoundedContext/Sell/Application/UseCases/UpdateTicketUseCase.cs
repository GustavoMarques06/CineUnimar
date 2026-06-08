using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;
using Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Request;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.UseCases
{
    public class UpdateTicketUseCase
    {

        private readonly ITicketRepository _ticketRepository;

        public UpdateTicketUseCase(
            ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task RunAsync(
            UpdateTicketRequest request)
        {
            var ticket =
                await _ticketRepository.GetByIdAsync(
                    request.TicketId);

            if (ticket is null)
                throw new Exception("Ticket não encontrado");

            ticket.UpdateTicket(
                new Price(request.Price),
                new Location(request.Location),
                new Date(request.Date),
                ticket.Quantity_bought,
                new Quantity(request.QuantityAvailable)
            );

            await _ticketRepository.UpdateAsync(ticket);
        }
    }
}
