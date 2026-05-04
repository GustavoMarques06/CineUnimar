using Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Sell.Application.DTOs.Response;
using Api_Venda_Ingressos.BoundedContext.Sell.Application.Services;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Api_Venda_Ingressos.BoundedContext.Sell.API.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _service;

        public TicketController(TicketService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketRequest request)
        {
            var ticket = await _service.CreateAsync(
                new Price(request.Price),
                new Location(request.Location),
                new Date(request.Date),
                new Quantity(request.QuantityAvailable)
            );

            return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tickets = await _service.GetAllAsync();

            var response = tickets.Select(t => new TicketResponse
            {
                Id = t.Id,
                Price = t.Price.Value,
                Location = t.Location.Value,
                Date = t.Data.Value,
                QuantityBought = t.Quantity_bought.Value,
                QuantityAvailable = t.Quantity_available.Value
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var ticket = await _service.GetByIdAsync(id);

            if (ticket == null)
                return NotFound();

            var response = new TicketResponse
            {
                Id = ticket.Id,
                Price = ticket.Price.Value,
                Location = ticket.Location.Value,
                Date = ticket.Data.Value,
                QuantityBought = ticket.Quantity_bought.Value,
                QuantityAvailable = ticket.Quantity_available.Value
            };

            return Ok(response);
        }

        [HttpPost("{id}/sell")]
        public async Task<IActionResult> Sell(Guid id, [FromQuery] int quantity)
        {
            try
            {
                await _service.SellAsync(id, quantity);
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
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}