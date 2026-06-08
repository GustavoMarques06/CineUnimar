using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.UseCases
{
    public class GetAllTicketUseCase
    {
        private readonly ITicketRepository _ticketRepository;

        public GetAllTicketUseCase(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<Ticket>> RunAsync()
        {
            return await _ticketRepository.GetAllAsync();
        }


    }
}
