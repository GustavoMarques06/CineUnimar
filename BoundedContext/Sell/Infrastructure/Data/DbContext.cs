using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api_Venda_Ingressos.BoundedContext.Auth.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>(builder =>
        {
            builder.ToTable("tickets");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(u => u.RemovedAt).HasColumnName("removed_at");

            builder.OwnsOne(u => u.Quantity_bought, d => d.Property(p => p.Value).HasColumnName("quantity_bought").IsRequired());
            builder.OwnsOne(u => u.Quantity_avaliable, d => d.Property(p => p.Value).HasColumnName("quantity_avaliable").IsRequired());
            builder.OwnsOne(u => u.Data, d => d.Property(p => p.Value).HasColumnName("data").IsRequired());
            builder.OwnsOne(u => u.Price, d => d.Property(p => p.Value).HasColumnName("price").IsRequired());
            builder.OwnsOne(u => u.Location, d => d.Property(p => p.Value).HasColumnName("location").IsRequired());

        });
    }
    
}