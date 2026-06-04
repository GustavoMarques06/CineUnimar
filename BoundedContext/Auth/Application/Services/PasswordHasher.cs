using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Auth.Application.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
