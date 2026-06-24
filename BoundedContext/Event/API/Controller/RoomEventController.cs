using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.RoomEvent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Venda_Ingressos.BoundedContext.Event.API.Controller
{
    [Route("api/RoomEvent")]
    [ApiController]
    public class RoomEventController : ControllerBase
    {
        private readonly ListRoomsEventUseCase _listRoomsEventUseCase;
        private readonly CreateRoomEventUseCase _createRoomEventUseCase;
        private readonly UpdateRoomEventUseCase _updateRoomEventUseCase;
        private readonly DeleteRoomEventUseCase _deleteRoomEventUseCase;
        private readonly GetRoomEventByIdUseCase _getRoomEventByIdUseCase;

        public RoomEventController(
            ListRoomsEventUseCase listRoomsEventUseCase,
            CreateRoomEventUseCase createRoomEventUseCase,
            UpdateRoomEventUseCase updateRoomEventUseCase,
            DeleteRoomEventUseCase deleteRoomEventUseCase,
            GetRoomEventByIdUseCase getRoomEventByIdUseCase
            )
        {
            _listRoomsEventUseCase = listRoomsEventUseCase;
            _createRoomEventUseCase = createRoomEventUseCase;
            _updateRoomEventUseCase = updateRoomEventUseCase;
            _deleteRoomEventUseCase = deleteRoomEventUseCase;
            _getRoomEventByIdUseCase = getRoomEventByIdUseCase;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            try
            {
                pageSize = Math.Clamp(pageSize, 1, 100);
                page = Math.Max(1, page);
                var all = (await _listRoomsEventUseCase.RunAsync()).ToList();
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
                var response = await _getRoomEventByIdUseCase.RunAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateRoomEventRequest room)
        {
            try
            {
                var result = await _createRoomEventUseCase.RunAsync(room);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] UpdateRoomEventRequest room)
        {
            try
            {
                await _updateRoomEventUseCase.RunAsync(room);
                return Ok("Sala de evento atualizada com sucesso!");
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
                await _deleteRoomEventUseCase.RunAsync(id);
                return Ok("Sala de evento deletada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
