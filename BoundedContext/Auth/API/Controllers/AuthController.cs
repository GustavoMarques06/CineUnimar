using Api_Venda_Ingressos.BoundedContext.Auth.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Auth.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_Venda_Ingressos.BoundedContext.Auth.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly RegisterUserUseCase _registerUserUseCase;
        private readonly RegisterAdminUseCase _registerAdminUseCase;
        private readonly LoginUserUseCase _loginUserUseCase;

        public AuthController(
            RegisterUserUseCase registerUserUseCase,
            RegisterAdminUseCase registerAdminUseCase,
            LoginUserUseCase loginUserUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
            _registerAdminUseCase = registerAdminUseCase;
            _loginUserUseCase = loginUserUseCase;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDTO dto)
        {
            try
            {
                await _registerUserUseCase.RunAsync(dto);
                return Created("", new { message = "Usuário cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("register-admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserRequestDTO dto)
        {
            try
            {
                await _registerAdminUseCase.RunAsync(dto);
                return Created("", new { message = "Admin cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _loginUserUseCase.RunAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }
    }
}
