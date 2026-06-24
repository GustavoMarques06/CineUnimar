using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairInEventUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Venda_Ingressos.BoundedContext.Event.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChairsInEventController : ControllerBase
    {
        private readonly ListChairsInEventUseCase _listChairsInEventUseCase;
        private readonly CreateChairsInEventUseCase _createChairsInEventUseCase;
        private readonly UpdateChairsInEventUseCase _updateChairsInEventUseCase;
        private readonly DeleteChairsInEventUseCase _deleteChairsInEventUseCase;
        private readonly GetChairsInEventByIdUseCase _getChairsInEventByIdUseCase;

        public ChairsInEventController(ListChairsInEventUseCase listChairsInEventUseCase, CreateChairsInEventUseCase createChairsInEventUseCase, UpdateChairsInEventUseCase updateChairsInEventUseCase, DeleteChairsInEventUseCase deleteChairsInEventUseCase, GetChairsInEventByIdUseCase getChairsInEventByIdUseCase)
        {
            _listChairsInEventUseCase = listChairsInEventUseCase;
            _createChairsInEventUseCase = createChairsInEventUseCase;
            _updateChairsInEventUseCase = updateChairsInEventUseCase;
            _deleteChairsInEventUseCase = deleteChairsInEventUseCase;
            _getChairsInEventByIdUseCase = getChairsInEventByIdUseCase;
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult> List([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            try
            {
                pageSize = Math.Clamp(pageSize, 1, 100);
                page = Math.Max(1, page);
                var all = (await _listChairsInEventUseCase.RunAsync()).ToList();
                var items = all.Skip((page - 1) * pageSize).Take(pageSize);
                return Ok(new { total = all.Count, page, pageSize, items });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("get/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var response = await _getChairsInEventByIdUseCase.RunAsync(id);
                if (response is null) return NotFound();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateChairInEventRequest chairInEvent)
        {
            try
            {
                var result = await _createChairsInEventUseCase.RunAsync(chairInEvent);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] UpdateChairInEventRequest chairInEvent)
        {
            try
            {
                await _updateChairsInEventUseCase.RunAsync(chairInEvent);
                return Ok("Cadeira vinculada ao evento atualizada com sucesso!");
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
                await _deleteChairsInEventUseCase.RunAsync(id);
                return Ok("Cadeira deletada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
