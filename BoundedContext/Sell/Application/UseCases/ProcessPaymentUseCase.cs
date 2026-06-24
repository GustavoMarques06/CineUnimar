using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Enums;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.UseCases
{
    public class ProcessPaymentUseCase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IChairsInEventRepository _chairsInEventRepository;

        public ProcessPaymentUseCase(
            ITicketRepository ticketRepository,
            IChairsInEventRepository chairsInEventRepository)
        {
            _ticketRepository = ticketRepository;
            _chairsInEventRepository = chairsInEventRepository;
        }

        public async Task ApproveAsync(Guid ticketId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);

            if (ticket is null)
                throw new Exception("Ingresso não encontrado.");

            if (ticket.Status == PaymentStatus.Approved)
                throw new Exception("O pagamento deste ingresso já foi aprovado.");

            if (ticket.Status == PaymentStatus.Rejected)
                throw new Exception("Não é possível aprovar um pagamento que foi rejeitado.");

            ticket.ApprovePayment();

            await _ticketRepository.UpdateAsync(ticket);
        }

        public async Task RejectAsync(Guid ticketId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);

            if (ticket is null)
                throw new Exception("Ingresso não encontrado.");

            if (ticket.Status != PaymentStatus.Pending)
                throw new Exception($"Não é possível rejeitar um pagamento com status '{ticket.Status}'.");

            ticket.RejectPayment();
            await _ticketRepository.UpdateAsync(ticket);

            var chair = await _chairsInEventRepository.GetByIdAsync(ticket.ChairInEventId);
            if (chair is not null && chair.Status == Api_Venda_Ingressos.BoundedContext.Event.Domain.Enums.ChairStatus.Occupied)
            {
                chair.VacateChair();
                await _chairsInEventRepository.UpdateAsync(chair);
            }
        }
    }
}
