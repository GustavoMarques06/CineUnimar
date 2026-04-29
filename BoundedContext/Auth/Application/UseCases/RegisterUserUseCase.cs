using Api_Venda_Ingressos.BoundedContext.Auth.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Enums;
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Auth.Application.UseCases
{
    public class RegisterUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _hasher;

        public RegisterUserUseCase(IUserRepository userRepository, IPasswordHasher hasher)
        {
            _userRepository = userRepository;
            _hasher = hasher;
        }

        public async Task RunAsync(RegisterUserRequestDTO dto, CancellationToken ct = default)
        {
            var email = new Email(dto.Email);
            var cpf = new CPF(dto.CPF);
            var dateOfBirth = DateOfBirth.Create(dto.DateOfBirth.ToDateTime(TimeOnly.MinValue));

            var existingUser = await _userRepository.GetByEmailAsync(email.Value, ct);
            if (existingUser is not null)
                throw new Exception("Email já cadastrado no sistema.");

            var passwordHash = _hasher.Hash(dto.Password);

            var newUser = new User(
                new Name(dto.FirstName),
                new Name(dto.LastName),
                email,
                dateOfBirth,
                cpf,
                new Password(passwordHash),
                UserRole.User
            );

            await _userRepository.SaveAsync(newUser, ct);
        }
    }
}