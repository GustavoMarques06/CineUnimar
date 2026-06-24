using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.UseCases
{
    public class DeleteTicketUseCase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IChairsInEventRepository _chairsInEventRepository;

        public DeleteTicketUseCase(
            ITicketRepository ticketRepository,
            IChairsInEventRepository chairsInEventRepository)
        {
            _ticketRepository = ticketRepository;
            _chairsInEventRepository = chairsInEventRepository;
        }

        public async Task RunAsync(Guid id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);

            if (ticket is null)
                throw new Exception("Ticket não encontrado");

            var chair = await _chairsInEventRepository.GetByIdAsync(ticket.ChairInEventId);
            if (chair is not null && chair.Status == Api_Venda_Ingressos.BoundedContext.Event.Domain.Enums.ChairStatus.Occupied)
            {
                chair.VacateChair();
                await _chairsInEventRepository.UpdateAsync(chair);
            }

            await _ticketRepository.DeleteAsync(ticket);
        }
    }
}
