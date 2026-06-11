using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairInEventUseCases;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairUseCases;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.TheaterUseCases;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public async Task<IActionResult> List()
        {
            try
            {
                var response = await _listChairsInEventUseCase.RunAsync();
                return Ok(response);
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
                var response = await _getChairsInEventByIdUseCase.RunAsync(id);
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
                await _createChairsInEventUseCase.RunAsync(chairInEvent);
                return Ok("Cadeira vinculada ao evento cadastrada com sucesso!");
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
