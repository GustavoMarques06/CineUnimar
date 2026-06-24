using Api_Venda_Ingressos.BoundedContext.Auth.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Auth.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

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
        [EnableRateLimiting("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _loginUserUseCase.RunAsync(request);

                Response.Cookies.Append("access_token", response.AccessToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = response.ExpiresAt
                });

                return Ok(new
                {
                    response.UserName,
                    response.Role,
                    response.ExpiresAt
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("access_token");
            return NoContent();
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var email = User.FindFirstValue(ClaimTypes.Email);
            var name = User.FindFirstValue(ClaimTypes.Name);
            var role = User.FindFirstValue("role");

            return Ok(new { userId, email, userName = name, role });
        }
    }
}
