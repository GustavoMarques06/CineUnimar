using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairInEventUseCases;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Enums;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;
using Api_Venda_Ingressos.Data;
using Microsoft.EntityFrameworkCore;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.UseCases
{
    public class SellTicketUseCase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IChairsInEventRepository _chairsInEventRepository;
        private readonly GetChairsInEventByIdUseCase _getChairUseCase;
        private readonly Context _context;

        public SellTicketUseCase(
            ITicketRepository ticketRepository,
            IEventRepository eventRepository,
            IChairsInEventRepository chairsInEventRepository,
            GetChairsInEventByIdUseCase getChairUseCase,
            Context context)
        {
            _ticketRepository = ticketRepository;
            _eventRepository = eventRepository;
            _chairsInEventRepository = chairsInEventRepository;
            _getChairUseCase = getChairUseCase;
            _context = context;
        }

        public async Task<Ticket> RunAsync(SellTicketRequest request, Guid userId)
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

            // Transação garante que cadeira e ticket são persistidos atomicamente.
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                chair.OccupyChair();
                await _chairsInEventRepository.UpdateAsync(chair);

                var ticket = new Ticket(
                    request.EventId,
                    request.ChairInEventId,
                    userId,
                    new Price(evento.Price));

                await _ticketRepository.SaveAsync(ticket);
                await transaction.CommitAsync();
                return ticket;
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync();
                throw new Exception("Esta cadeira foi reservada por outro usuário simultaneamente. Escolha outra ou tente novamente.");
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
