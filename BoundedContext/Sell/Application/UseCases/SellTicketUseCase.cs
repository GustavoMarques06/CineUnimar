using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairInEventUseCases;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.EventUseCases;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Enums;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Enums;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.UseCases
{
    public class SellTicketUseCase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IChairsInEventRepository _chairsInEventRepository;
        private readonly GetChairsInEventByIdUseCase _getChairUseCase;

        public SellTicketUseCase(
            ITicketRepository ticketRepository,
            IEventRepository eventRepository,
            IChairsInEventRepository chairsInEventRepository,
            GetChairsInEventByIdUseCase getChairUseCase)
        {
            _ticketRepository = ticketRepository;
            _eventRepository = eventRepository;
            _chairsInEventRepository = chairsInEventRepository;
            _getChairUseCase = getChairUseCase;
        }

        public async Task<Ticket> RunAsync(SellTicketRequest request)
        {
            
            var evento = await _eventRepository.GetByIdAsync(request.EventId);

            if (evento is null)
                throw new Exception("Evento não encontrado.");

            if (evento.Date < DateTime.UtcNow)
                throw new Exception("Não é possível comprar ingressos para um evento que já ocorreu.");

            if (evento.Status == EventStatus.Ended || evento.Status == EventStatus.Cancelled)
                throw new Exception($"Não é possível comprar ingressos para um evento com status '{evento.Status}'.");

            
            var chair = await _getChairUseCase.RunAsync(request.ChairInEventId);

            if (chair is null)
                throw new Exception("Cadeira não encontrada.");

            if (chair.Status == ChairStatus.Occupied)
                throw new Exception("Esta cadeira já está ocupada. Escolha outra disponível.");

            
            chair.OccupyChair();
            await _chairsInEventRepository.UpdateAsync(chair);

            
            var ticket = new Ticket(
                request.EventId,
                request.ChairInEventId,
                request.UserId,
                new Price(request.Price));

            // NÃO chamamos ticket.ApprovePayment() aqui.
            // O status fica como Pending até o pagamento ser processado.

            await _ticketRepository.SaveAsync(ticket);

            return ticket;
        }
    }
}
