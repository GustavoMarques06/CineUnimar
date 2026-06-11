using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Enums;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Sell.Application.UseCases
{
    public class ProcessPaymentUseCase
    {
        private readonly ITicketRepository _ticketRepository;

        public ProcessPaymentUseCase(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        /// <summary>
        /// Aprova o pagamento de um ticket que está com status Pending.
        /// Regra: só é possível aprovar um pagamento que ainda não foi processado.
        /// </summary>
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

        /// <summary>
        /// Rejeita o pagamento de um ticket que está com status Pending.
        /// Regra: só é possível rejeitar um pagamento pendente.
        /// </summary>
        public async Task RejectAsync(Guid ticketId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);

            if (ticket is null)
                throw new Exception("Ingresso não encontrado.");

            if (ticket.Status != PaymentStatus.Pending)
                throw new Exception($"Não é possível rejeitar um pagamento com status '{ticket.Status}'.");

            ticket.RejectPayment();

            await _ticketRepository.UpdateAsync(ticket);
        }
    }
}
