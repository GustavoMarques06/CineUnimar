using Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Response;
using Api_Venda_Ingressos.BoundedContext.Sell.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Api_Venda_Ingressos.BoundedContext.Sell.API.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly CreateTicketUseCase _createTicketUseCase;
        private readonly GetAllTicketUseCase _getAllTicketUseCase;
        private readonly GetTicketByIdUseCase _getTicketByIdUseCase;
        private readonly SellTicketUseCase _sellTicketUseCase;
        private readonly DeleteTicketUseCase _deleteTicketUseCase;
        private readonly ProcessPaymentUseCase _processPaymentUseCase;

        public TicketController(
            CreateTicketUseCase createTicketUseCase,
            GetAllTicketUseCase getAllTicketsUseCase,
            GetTicketByIdUseCase getTicketByIdUseCase,
            SellTicketUseCase sellTicketUseCase,
            DeleteTicketUseCase deleteTicketUseCase,
            ProcessPaymentUseCase processPaymentUseCase)
        {
            _createTicketUseCase = createTicketUseCase;
            _getAllTicketUseCase = getAllTicketsUseCase;
            _getTicketByIdUseCase = getTicketByIdUseCase;
            _sellTicketUseCase = sellTicketUseCase;
            _deleteTicketUseCase = deleteTicketUseCase;
            _processPaymentUseCase = processPaymentUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketRequest request)
        {
            var ticket = await _createTicketUseCase.RunAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tickets = await _getAllTicketUseCase.RunAsync();

            var response = tickets.Select(t => new TicketResponse
            {
                Id = t.Id,
                EventId = t.EventId,
                ChairInEventId = t.ChairInEventId,
                UserId = t.UserId,
                Price = t.Price.value,
                Purchase_Data = t.Purchase_Data.value,
                PaymentStatus = t.Status.ToString()
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var ticket = await _getTicketByIdUseCase.RunAsync(id);

            if (ticket is null)
                return NotFound();

            var response = new TicketResponse
            {
                Id = ticket.Id,
                EventId = ticket.EventId,
                ChairInEventId = ticket.ChairInEventId,
                UserId = ticket.UserId,
                Price = ticket.Price.value,
                Purchase_Data = ticket.Purchase_Data.value,
                PaymentStatus = ticket.Status.ToString()
            };

            return Ok(response);
        }

        /// <summary>
        /// Compra um ingresso.
        /// Regras: evento não pode ter passado, cadeira não pode estar ocupada.
        /// Retorna o ticket com status Pending (aguardando aprovação do pagamento).
        /// </summary>
        [HttpPost("sell")]
        public async Task<IActionResult> Sell([FromBody] SellTicketRequest request)
        {
            try
            {
                var ticket = await _sellTicketUseCase.RunAsync(request);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = ticket.Id },
                    new
                    {
                        ticket.Id,
                        Mensagem = "Ingresso reservado com sucesso. Aguardando confirmação do pagamento.",
                        Status = ticket.Status.ToString()
                    });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Erro = ex.Message });
            }
        }

        /// <summary>
        /// Aprova o pagamento de um ingresso com status Pending.
        /// </summary>
        [HttpPost("{id}/payment/approve")]
        public async Task<IActionResult> ApprovePayment(Guid id)
        {
            try
            {
                await _processPaymentUseCase.ApproveAsync(id);
                return Ok(new { Mensagem = "Pagamento aprovado com sucesso. Ingresso confirmado." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Erro = ex.Message });
            }
        }

        /// <summary>
        /// Rejeita o pagamento de um ingresso com status Pending.
        /// </summary>
        [HttpPost("{id}/payment/reject")]
        public async Task<IActionResult> RejectPayment(Guid id)
        {
            try
            {
                await _processPaymentUseCase.RejectAsync(id);
                return Ok(new { Mensagem = "Pagamento rejeitado." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Erro = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _deleteTicketUseCase.RunAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { Erro = ex.Message });
            }
        }
    }
}
