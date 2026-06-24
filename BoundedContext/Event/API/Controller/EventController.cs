using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.EventUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Venda_Ingressos.BoundedContext.Event.API.Controller;

[ApiController]
[Route("api/Events")]
public class EventController : ControllerBase
{
    private readonly CreateEventUseCase _createEventUseCase;
    private readonly ListEventsUseCase _listEventsUseCase;
    private readonly GetEventByIdUseCase _getEventByIdUseCase;
    private readonly UpdateEventUseCase _updateEventUseCase;
    private readonly DeleteEventUseCase _deleteEventUseCase;

    public EventController(
        CreateEventUseCase createEventUseCase,
        ListEventsUseCase listEventsUseCase,
        GetEventByIdUseCase getEventByIdUseCase,
        UpdateEventUseCase updateEventUseCase,
        DeleteEventUseCase deleteEventUseCase)
    {
        _createEventUseCase = createEventUseCase;
        _listEventsUseCase = listEventsUseCase;
        _getEventByIdUseCase = getEventByIdUseCase;
        _updateEventUseCase = updateEventUseCase;
        _deleteEventUseCase = deleteEventUseCase;
    }

    [HttpGet("list")]
    public async Task<IActionResult> List([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        try
        {
            pageSize = Math.Clamp(pageSize, 1, 100);
            page = Math.Max(1, page);
            var all = (await _listEventsUseCase.RunAsync()).ToList();
            var items = all.Skip((page - 1) * pageSize).Take(pageSize);
            return Ok(new { total = all.Count, page, pageSize, items });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var response = await _getEventByIdUseCase.RunAsync(id);
            if (response is null)
                return NotFound(new { error = "Evento não encontrado." });
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("create")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateEventRequest request)
    {
        try
        {
            var evento = await _createEventUseCase.RunAsync(request);
            return Ok(evento);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("update")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] UpdateEventRequest request)
    {
        try
        {
            await _updateEventUseCase.RunAsync(request);
            return Ok("Evento atualizado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("delete/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _deleteEventUseCase.RunAsync(id);
            return Ok("Evento deletado com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
