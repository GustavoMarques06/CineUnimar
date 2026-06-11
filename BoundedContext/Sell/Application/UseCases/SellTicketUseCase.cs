using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairInEventUseCases;
//using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.EventUseCases;
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

        private readonly CreateTicketUseCase _createTicketUseCase;

        //private readonly GetEventUseCase _getEventUseCase;

        private readonly GetChairsInEventByIdUseCase _getChairUseCase;


        public SellTicketUseCase(
            ITicketRepository ticketRepository, CreateTicketUseCase createTicketUseCase, GetChairsInEventByIdUseCase getChairUseCase)
        {
            _ticketRepository = ticketRepository;
            _createTicketUseCase = createTicketUseCase;
            _getChairUseCase = getChairUseCase;

        }

        public async Task RunAsync(
    SellTicketRequest request)
        {
            

            //var evento = await _getEventUseCase.GetByIdAsync(ticket.EventId);

            /*if (evento is null)
                throw new Exception(
                    "Evento não encontrado");*/

            /*if (evento.Date < DateTime.UtcNow)
                throw new Exception(
                    "Evento já ocorreu");*/


            var chair = await _getChairUseCase.RunAsync(request.ChairInEventId);
            

            if (chair is null)
                throw new Exception(
                    "Cadeira não encontrada");
            
            if(chair.Status == Event.Domain.Enums.ChairStatus.Occupied)
                throw new Exception(
                    "Cadeira já está ocupada");

            chair.OccupyChair();


            

            var ticket =
       new Ticket(
           request.EventId,
           request.ChairInEventId,
           request.UserId,
           new Price(request.Price));

            ticket.ApprovePayment();

            await _ticketRepository.SaveAsync(ticket);
        }
    }
}
