using Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.UseCases
{
    public class SellTicketUseCase
    {
        private readonly ITicketRepository _ticketRepository;

        public SellTicketUseCase(
            ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task RunAsync(
    SellTicketRequest request)
        {
            var ticket =
                await _ticketRepository.GetByIdAsync(
                    request.TicketId);

            if (ticket is null)
                throw new Exception(
                    "Ticket não encontrado");

            ticket.Sell(request.quantity_bought);

            await _ticketRepository.UpdateAsync(ticket);
        }
    }
}
