using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Enums;
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.ValueObjects;

namespace Api_Venda_Ingressos.Data.Mock
{
    public static class AdminMock
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var repo = services.GetRequiredService<IUserRepository>();
            var hasher = services.GetRequiredService<IPasswordHasher>();

            var adminEmail = "admin@sistema.com";
            var existing = await repo.GetByEmailAsync(adminEmail);

            if (existing is not null) return;

            var admin = new User(
                new Name("Admin"),
                new Name("Sistema"),
                new Email(adminEmail),
                DateOfBirth.Create(new DateTime(1990, 1, 1)),
                new CPF("000.000.000-00"),
                new Password(hasher.Hash("Admin@123")),
                UserRole.Admin
            );

            await repo.SaveAsync(admin);
        }
    }
}