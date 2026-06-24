using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.TheaterUseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Venda_Ingressos.BoundedContext.Event.API.Controller
{
    [Route("api/Theater")]
    [ApiController]
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
        public async Task<IActionResult> List([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            try
            {
                pageSize = Math.Clamp(pageSize, 1, 100);
                page = Math.Max(1, page);
                var all = (await _listTheatersUseCase.RunAsync()).ToList();
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
                var response = await _getTheaterByIdUseCase.RunAsync(id);
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
        public async Task<IActionResult> Create([FromBody] CreateTheaterRequest theater)
        {
            try
            {
                var result = await _createTheaterUseCase.RunAsync(theater);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] UpdateTheaterRequest theater)
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


        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
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
