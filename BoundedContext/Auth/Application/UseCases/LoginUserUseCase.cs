using Api_Venda_Ingressos.BoundedContext.Auth.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Auth.Application.DTOs.Response;
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Interfaces;


namespace Api_Venda_Ingressos.BoundedContext.Auth.Application.UseCases
{
    public class LoginUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public LoginUserUseCase(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> RunAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user is null)
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            if (user.IsDeleted)                                          
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            bool senhaValida = _passwordHasher.Verify(request.Password, user.PasswordHash.Value);
            if (!senhaValida)
                throw new UnauthorizedAccessException("Credenciais inválidas.");

            var (token, expiresAt) = _tokenService.GenerateToken(user);

            return new LoginResponse(
                accessToken: token,
                expiresAt: expiresAt,
                userName: $"{user.FirstName.Value} {user.LastName.Value}",
                role: user.Role.ToString()
            );
        }
    }
}
