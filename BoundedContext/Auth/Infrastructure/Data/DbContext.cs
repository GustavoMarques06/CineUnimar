using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api_Venda_Ingressos.BoundedContext.Auth.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<User>(builder =>
            {
                builder.HasKey(u => u.Id);

                builder.OwnsOne(u => u.Email, e => { e.Property(p => p.Value).HasColumnName("Email"); });
                builder.OwnsOne(u => u.FirstName, f => { f.Property(p => p.Value).HasColumnName("FirstName"); });
                builder.OwnsOne(u => u.LastName, l => { l.Property(p => p.Value).HasColumnName("LastName"); });
                builder.OwnsOne(u => u.CPF, c => { c.Property(p => p.Value).HasColumnName("CPF"); });
                builder.OwnsOne(u => u.DateOfBirth, d => { d.Property(p => p.Value).HasColumnName("DateOfBirth"); });
                builder.OwnsOne(u => u.PasswordHash, p => { p.Property(p => p.Value).HasColumnName("PasswordHash"); });
            });
        }
    }
}
