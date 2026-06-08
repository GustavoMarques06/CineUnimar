using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.UseCases
{
    public class DeleteTicketUseCase
    {
        private readonly ITicketRepository _ticketRepository;

        public DeleteTicketUseCase(
            ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task RunAsync(Guid id)
        {
            var ticket =
                await _ticketRepository.GetByIdAsync(id);

            if (ticket is null)
                throw new Exception("Ticket não encontrado");

            await _ticketRepository.DeleteAsync(ticket);
        }
    }
}
