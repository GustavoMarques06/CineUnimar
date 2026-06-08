using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Venda_Ingressos.BoundedContext.Event.API.Controller
{
    public class TheaterController : ControllerBase
    {
        private readonly ListTheatersUseCase _listTheatersUseCase;
        private readonly CreateTheaterUseCase _createTheaterUseCase;
        private readonly UpdateTheaterUseCase _updateTheaterUseCase;
        private readonly DeleteTheaterUseCase _deleteTheaterUseCase;
        private readonly GetTheaterByIdUseCase _getTheaterByIdUseCase;

        public TheaterController(
            ListTheatersUseCase listTheatersUseCase,
            CreateTheaterUseCase createTheaterUseCase,
            UpdateTheaterUseCase updateTheaterUseCase,
            DeleteTheaterUseCase deleteTheaterUseCase,
            GetTheaterByIdUseCase getTheaterByIdUseCase
            )
        {
            _listTheatersUseCase = listTheatersUseCase;
            _createTheaterUseCase = createTheaterUseCase;
            _updateTheaterUseCase = updateTheaterUseCase;
            _deleteTheaterUseCase = deleteTheaterUseCase;
            _getTheaterByIdUseCase = getTheaterByIdUseCase;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            try
            {
                var response = await _listTheatersUseCase.RunAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("get")]
        public async Task<IActionResult> GeyById([FromRoute] Guid id)
        {
            try
            {
                var response = await _getTheaterByIdUseCase.RunAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create([FromRoute] CreateTheaterRequest theater)
        {
            try
            {
                await _createTheaterUseCase.RunAsync(theater);
                return Ok("Teatro cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("update")]
        public async Task<IActionResult> Update([FromRoute] UpdateTheaterRequest theater)
        {
            try
            {
                await _updateTheaterUseCase.RunAsync(theater);
                return Ok("Teatro atualizado com sucesso!");
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
                await _deleteTheaterUseCase.RunAsync(id);
                return Ok("Teatro deletado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
