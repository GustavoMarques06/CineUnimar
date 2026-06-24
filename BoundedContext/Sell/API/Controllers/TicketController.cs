using Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Response;
using Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Sell.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api_Venda_Ingressos.BoundedContext.Sell.API.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    [Authorize]
    public class TicketController : ControllerBase
    {
        private readonly GetAllTicketUseCase _getAllTicketUseCase;
        private readonly GetTicketByIdUseCase _getTicketByIdUseCase;
        private readonly GetTicketsByUserIdUseCase _getTicketsByUserIdUseCase;
        private readonly SellTicketUseCase _sellTicketUseCase;
        private readonly DeleteTicketUseCase _deleteTicketUseCase;
        private readonly ProcessPaymentUseCase _processPaymentUseCase;

        public TicketController(
            GetAllTicketUseCase getAllTicketsUseCase,
            GetTicketByIdUseCase getTicketByIdUseCase,
            GetTicketsByUserIdUseCase getTicketsByUserIdUseCase,
            SellTicketUseCase sellTicketUseCase,
            DeleteTicketUseCase deleteTicketUseCase,
            ProcessPaymentUseCase processPaymentUseCase)
        {
            _getAllTicketUseCase = getAllTicketsUseCase;
            _getTicketByIdUseCase = getTicketByIdUseCase;
            _getTicketsByUserIdUseCase = getTicketsByUserIdUseCase;
            _sellTicketUseCase = sellTicketUseCase;
            _deleteTicketUseCase = deleteTicketUseCase;
            _processPaymentUseCase = processPaymentUseCase;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            pageSize = Math.Clamp(pageSize, 1, 100);
            page = Math.Max(1, page);

            var all = (await _getAllTicketUseCase.RunAsync()).ToList();
            var items = all.Skip((page - 1) * pageSize).Take(pageSize).Select(t => new TicketResponse
            {
                Id = t.Id,
                EventId = t.EventId,
                ChairInEventId = t.ChairInEventId,
                UserId = t.UserId,
                Price = t.Price.value,
                PurchaseDate = t.PurchaseDate.value,
                PaymentStatus = t.Status.ToString()
            });

            return Ok(new { total = all.Count, page, pageSize, items });
        }

        [HttpGet("mine")]
        public async Task<IActionResult> GetMine()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var tickets = await _getTicketsByUserIdUseCase.RunAsync(userId);

            var response = tickets.Select(t => new TicketResponse
            {
                Id = t.Id,
                EventId = t.EventId,
                ChairInEventId = t.ChairInEventId,
                UserId = t.UserId,
                Price = t.Price.value,
                PurchaseDate = t.PurchaseDate.value,
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

            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            if (!isAdmin && ticket.UserId.ToString() != userIdStr)
                return Forbid();

            var response = new TicketResponse
            {
                Id = ticket.Id,
                EventId = ticket.EventId,
                ChairInEventId = ticket.ChairInEventId,
                UserId = ticket.UserId,
                Price = ticket.Price.value,
                PurchaseDate = ticket.PurchaseDate.value,
                PaymentStatus = ticket.Status.ToString()
            };

            return Ok(response);
        }

        [HttpPost("sell")]
        public async Task<IActionResult> Sell([FromBody] SellTicketRequest request)
        {
            try
            {
                var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!Guid.TryParse(userIdStr, out var userId))
                    return Unauthorized();

                var ticket = await _sellTicketUseCase.RunAsync(request, userId);

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

        [HttpPost("{id}/payment/approve")]
        [Authorize(Roles = "Admin")]
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

        [HttpPost("{id}/payment/reject")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
