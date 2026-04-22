using Api_Venda_Ingressos.BoundedContext.Auth.Application.UseCases;
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace Api_Venda_Ingressos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly CreateUserUseCase _createUserUseCase;
         public UserController(CreateUserUseCase createUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserRequestDTO dto)
        {
            try
            {
                _createUserUseCase.Run(dto);
                return Created("", new { message = "Usuário cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
