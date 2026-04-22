using Api_Venda_Ingressos.BoundedContext.Auth.Domain.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Enums;
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Auth.Application.UseCases
{
    public class CreateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _hasher;

        public CreateUserUseCase(IUserRepository userRepository, IPasswordHasher hasher)
        {
            _userRepository = userRepository;
            _hasher = hasher;
        }

        public void Run(CreateUserRequestDTO dto)
        {
            try
            {
                var email = new Email(dto.Email);
                var cpf = new CPF(dto.CPF);
                var dateOfBirth = DateOfBirth.Create(DateTime.Parse(dto.DateOfBirth.ToString()));
                if (_userRepository.GetByEmail(email.Value) != null)
                {
                    throw new Exception("Email já cadastrado no sistema.");
                }

                // 3. Criptografar a senha
                string passwordHash = _hasher.Hash(dto.Password);

                // 4. Instanciar a Entidade User
                User newUser = new User(
                    new Name(dto.FirstName),
                    new Name(dto.LastName),
                    email,
                    dateOfBirth,
                    cpf,
                    new Password(passwordHash),
                    UserRole.User 
                );

                _userRepository.Save(newUser);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}