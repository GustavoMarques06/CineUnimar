using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.Room;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Venda_Ingressos.BoundedContext.Event.API.Controller
{
    [Route("api/Room")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ListRoomsUseCase _listRoomsUseCase;
        private readonly CreateRoomUseCase _createRoomUseCase;
        private readonly UpdateRoomUseCase _updateRoomUseCase;
        private readonly DeleteRoomUseCase _deleteRoomUseCase;
        private readonly GetRoomByIdUseCase _getRoomByIdUseCase;

        public RoomController(
            ListRoomsUseCase listRoomsUseCase,
            CreateRoomUseCase createRoomUseCase,
            UpdateRoomUseCase updateRoomUseCase,
            DeleteRoomUseCase deleteRoomUseCase,
            GetRoomByIdUseCase getRoomByIdUseCase
            )
        {
            _listRoomsUseCase = listRoomsUseCase;
            _createRoomUseCase = createRoomUseCase;
            _updateRoomUseCase = updateRoomUseCase;
            _deleteRoomUseCase = deleteRoomUseCase;
            _getRoomByIdUseCase = getRoomByIdUseCase;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            try
            {
                var response = await _listRoomsUseCase.RunAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                var response = await _getRoomByIdUseCase.RunAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateRoomRequest room)
        {
            try
            {
                await _createRoomUseCase.RunAsync(room);
                return Ok("Sala cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] UpdateRoomRequest room)
        {
            try
            {
                await _updateRoomUseCase.RunAsync(room);
                return Ok("Sala atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await _deleteRoomUseCase.RunAsync(id);
                return Ok("Sala deletada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
