using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.UseCases
{
    public class ProcessPaymentUseCase
    {
        private readonly ITicketRepository _ticketRepository;

        public ProcessPaymentUseCase(
            ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task RunAsync(Guid ticketId)
        {
            var ticket =
                await _ticketRepository.GetByIdAsync(ticketId);

            if (ticket is null)
                throw new Exception("Ticket não encontrado");

            ticket.ApprovePayment();

            await _ticketRepository.UpdateAsync(ticket);
        }
    }
}
