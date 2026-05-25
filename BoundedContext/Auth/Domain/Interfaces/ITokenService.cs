using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Entities;

namespace Api_Venda_Ingressos.BoundedContext.Auth.Domain.Interfaces
{
    public interface ITokenService
    {
        (string token, DateTime expiresAt) GenerateToken(User user);
    }
}
