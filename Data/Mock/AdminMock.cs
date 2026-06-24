using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Enums;
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.ValueObjects;
using Microsoft.Extensions.Configuration;

namespace Api_Venda_Ingressos.Data.Mock
{
    public static class AdminMock
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var repo = services.GetRequiredService<IUserRepository>();
            var hasher = services.GetRequiredService<IPasswordHasher>();
            var config = services.GetRequiredService<IConfiguration>();

            var adminEmail = config["Admin:Email"] ?? "admin@sistema.com";
            var existing = await repo.GetByEmailAsync(adminEmail);

            if (existing is not null) return;

            var adminPassword = config["Admin:Password"] ?? "Admin@123";
            var adminCpf = config["Admin:Cpf"] ?? "52998224725";

            var admin = new User(
                new Name("Admin"),
                new Name("Sistema"),
                new Email(adminEmail),
                DateOfBirth.Create(new DateTime(1990, 1, 1)),
                new CPF(adminCpf),
                new Password(hasher.Hash(adminPassword)),
                UserRole.Admin
            );

            await repo.SaveAsync(admin);
        }
    }
}