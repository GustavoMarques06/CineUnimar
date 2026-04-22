namespace Api_Venda_Ingressos.BoundedContext.Auth.Domain.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string hash);
    }
}
