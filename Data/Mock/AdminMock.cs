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

            var adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL")
                ?? config["Admin:Email"];
            if (string.IsNullOrWhiteSpace(adminEmail))
                throw new InvalidOperationException(
                    "Credencial do admin não configurada. Defina a variável de ambiente ADMIN_EMAIL.");

            var existing = await repo.GetByEmailAsync(adminEmail);
            if (existing is not null) return;

            var adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD")
                ?? config["Admin:Password"];
            if (string.IsNullOrWhiteSpace(adminPassword))
                throw new InvalidOperationException(
                    "Credencial do admin não configurada. Defina a variável de ambiente ADMIN_PASSWORD.");

            var adminCpf = Environment.GetEnvironmentVariable("ADMIN_CPF")
                ?? config["Admin:Cpf"];
            if (string.IsNullOrWhiteSpace(adminCpf))
                throw new InvalidOperationException(
                    "Credencial do admin não configurada. Defina a variável de ambiente ADMIN_CPF.");

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