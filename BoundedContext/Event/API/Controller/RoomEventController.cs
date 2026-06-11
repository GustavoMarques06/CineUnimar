using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.Room;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.RoomEvent;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Venda_Ingressos.BoundedContext.Event.API.Controller
{
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
        public async Task<IActionResult> List()
        {
            try
            {
                var response = await _listRoomsEventUseCase.RunAsync();
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
                var response = await _getRoomEventByIdUseCase.RunAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromRoute] CreateRoomEventRequest room)
        {
            try
            {
                await _createRoomEventUseCase.RunAsync(room);
                return Ok("Sala de evento cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] UpdateRoomEventRequest room)
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

        [HttpGet("delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
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
