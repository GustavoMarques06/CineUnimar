using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.UseCases
{
    public class GetTicketByIdUseCase
    {
        private readonly ITicketRepository _ticketRepository;

        public GetTicketByIdUseCase(
            ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Ticket?> RunAsync(Guid id)
        {
            return await _ticketRepository.GetByIdAsync(id);
        }
    }
}
