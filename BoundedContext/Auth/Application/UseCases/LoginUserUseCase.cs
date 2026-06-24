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

        // Dummy hash usado quando o usuário não existe, para manter tempo de resposta constante.
        private const string _dummyHash = "$2a$12$OVPWh8X8C0bJ3Z5Y2fK1duD7OLROzHTMhJJO0EjGLkXfHoWp5XJRa";

        public async Task<LoginResponse> RunAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            // Sempre executa bcrypt para evitar timing attack (enumeração de e-mails).
            var hashToVerify = (user is null || user.IsDeleted)
                ? _dummyHash
                : user.PasswordHash.Value;

            bool senhaValida = _passwordHasher.Verify(request.Password, hashToVerify);

            if (user is null || user.IsDeleted || !senhaValida)
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
