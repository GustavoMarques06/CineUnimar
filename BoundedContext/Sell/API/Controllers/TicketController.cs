using Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Response;
using Api_Venda_Ingressos.BoundedContext.Sell.Application.Services;
using Api_Venda_Ingressos.BoundedContext.Sell.Application.UseCases;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;
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

        public TicketController(CreateTicketUseCase createTicketUseCase,
    GetAllTicketUseCase getAllTicketsUseCase,
    GetTicketByIdUseCase getTicketByIdUseCase,
    SellTicketUseCase sellTicketUseCase,
    DeleteTicketUseCase deleteTicketUseCase)
        {
            _createTicketUseCase = createTicketUseCase;
            _getAllTicketUseCase = getAllTicketsUseCase;
            _getTicketByIdUseCase = getTicketByIdUseCase;
            _sellTicketUseCase = sellTicketUseCase;
            _deleteTicketUseCase = deleteTicketUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketRequest request)
        {
            var ticket =
                await _createTicketUseCase.RunAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = ticket.Id },
                ticket);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tickets =
                await _getAllTicketUseCase.RunAsync();

            var response = tickets.Select(t => new TicketResponse
            {
                Id = t.Id,
                Price = t.Price.value,
                Location = t.Location.value,
                Date = t.Data.value,
                QuantityBought = t.Quantity_bought.value,
                QuantityAvailable = t.Quantity_available.value
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var ticket =
                await _getTicketByIdUseCase.RunAsync(id);

            if (ticket is null)
                return NotFound();

            var response = new TicketResponse
            {
                Id = ticket.Id,
                Price = ticket.Price.value,
                Location = ticket.Location.value,
                Date = ticket.Data.value,
                QuantityBought = ticket.Quantity_bought.value,
                QuantityAvailable = ticket.Quantity_available.value
            };

            return Ok(response);
        }

        [HttpPost("{id}/sell")]
        public async Task<IActionResult> Sell(Guid id, [FromQuery] Quantity quantity)
        {
            try
            {
                await _sellTicketUseCase.RunAsync(
                    new SellTicketRequest
                    {
                        TicketId = id,
                        quantity_bought = quantity
                    }
                    );
                return Ok("Compra realizada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                return NotFound(ex.Message);
            }
        }
    }
}