using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Enums;

namespace Api_Venda_Ingressos.BoundedContext.Auth.Application.DTOs.Request
{
    public class RegisterUserRequestDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CPF { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public UserRole Role { get; set; } = UserRole.User;

        public RegisterUserRequestDTO(string firstName, string lastName, string email, string password, string cpf, DateOnly dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            CPF = cpf;
            DateOfBirth = dateOfBirth;
        }
    }
}
