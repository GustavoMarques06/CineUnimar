using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Entities;

namespace Api_Venda_Ingressos.BoundedContext.Auth.Domain.Interfaces
{
    public interface IUserRepository
    {
        User GetById(Guid id);
        User GetByEmail(string email);
        IEnumerable<User> GetAll();
        void Save(User user);     
        void Update(User user);    
        void Delete(User user);
    }
}
