using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairUseCases;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.TheaterUseCases;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api_Venda_Ingressos.BoundedContext.Event.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChairController : ControllerBase
    {

        private readonly ListChairUseCase _listChairsUseCase;
        private readonly CreateChairUseCase _createChairUseCase;
        private readonly UpdateChairUseCase _updateChairUseCase;
        private readonly DeleteChairUseCase _deleteChairUseCase;
        private readonly GetChairByIdUseCase _getChairByIdUseCase;

        public ChairController(ListChairUseCase listChairsUseCase, CreateChairUseCase createChairUseCase, UpdateChairUseCase updateChairUseCase, DeleteChairUseCase deleteChairUseCase, GetChairByIdUseCase getChairByIdUseCase)
        {
            _listChairsUseCase = listChairsUseCase;
            _createChairUseCase = createChairUseCase;
            _updateChairUseCase = updateChairUseCase;
            _deleteChairUseCase = deleteChairUseCase;
            _getChairByIdUseCase = getChairByIdUseCase;
        }




        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            try
            {
                var response = await _listChairsUseCase.RunAsync();
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
                var response = await _getChairByIdUseCase.RunAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpGet("create")]
        public async Task<IActionResult> Create([FromRoute] CreateChairRequest chair)
        {
            try
            {
                await _createChairUseCase.RunAsync(chair);
                return Ok("Cadeira cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("update")]
        public async Task<IActionResult> Update([FromRoute] UpdateChairRequest chair)
        {
            try
            {
                await _updateChairUseCase.RunAsync(chair);
                return Ok("Cadeira atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpGet("delete")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await _deleteChairUseCase.RunAsync(id);
                return Ok("Cadeira deletada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
