using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.UseCases
{
    public class GetTicketsByUserIdUseCase
    {
        private readonly ITicketRepository _ticketRepository;

        public GetTicketsByUserIdUseCase(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<Ticket>> RunAsync(Guid userId)
        {
            return await _ticketRepository.GetByUserIdAsync(userId);
        }
    }
}
