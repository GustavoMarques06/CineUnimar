using Api_Venda_Ingressos.BoundedContext.Auth.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Auth.Domain.DTOs.Request
{
    public class CreateUserRequestDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CPF { get; set; }
        public DateOnly DateOfBirth { get; set; }

    }
}
