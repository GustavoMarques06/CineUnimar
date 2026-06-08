using Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Response;
using Api_Venda_Ingressos.BoundedContext.Sell.Application.Services;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;


namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.UseCases
{
    public class CreateTicketUseCase
    {
        private readonly ITicketRepository _ticketRepository;

        public CreateTicketUseCase(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<Ticket> RunAsync(CreateTicketRequest request)
        {
            var ticket = new Ticket(
                new Price(request.Price),
                new Location(request.Location),
                new Date(request.Date),
                new Quantity(0),
                new Quantity(request.QuantityAvailable)
                );

            await _ticketRepository.SaveAsync(ticket);

            return ticket;
        }
    }
}
