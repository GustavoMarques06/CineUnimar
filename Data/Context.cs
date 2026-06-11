
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api_Venda_Ingressos.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Theater> Theaters { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomEvent> RoomsEvent { get; set; }

    public DbSet<Chair> Chairs { get; set; }

    public DbSet<ChairsInEvent> ChairsInEvent { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("users");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.Role).HasColumnName("role").HasConversion<string>().HasMaxLength(20).IsRequired();
            builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(u => u.RemovedAt).HasColumnName("removed_at");

            builder.OwnsOne(u => u.Email, e => {
                e.Property(p => p.Value).HasColumnName("email").HasMaxLength(256).IsRequired();
                e.HasIndex(p => p.Value).IsUnique();
            });

            builder.OwnsOne(u => u.FirstName, f => f.Property(p => p.Value).HasColumnName("first_name").HasMaxLength(100).IsRequired());

            builder.OwnsOne(u => u.LastName, l => l.Property(p => p.Value).HasColumnName("last_name").HasMaxLength(100).IsRequired());

            builder.OwnsOne(u => u.CPF, c => {
                c.Property(p => p.Value).HasColumnName("cpf").HasMaxLength(11).IsRequired();
                c.HasIndex(p => p.Value).IsUnique();
            });

            builder.OwnsOne(u => u.DateOfBirth, d => d.Property(p => p.Value).HasColumnName("date_of_birth").IsRequired());

            builder.OwnsOne(u => u.PasswordHash, p => p.Property(p => p.Value).HasColumnName("password_hash").IsRequired());
        });

        modelBuilder.Entity<Event>(builder =>
        {
            builder.ToTable("events");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.Status).HasColumnName("status").HasConversion<string>().HasMaxLength(20).IsRequired();
            builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(u => u.RemovedAt).HasColumnName("removed_at");

            builder.Property(u => u.Date).HasColumnName("Data");

            builder.OwnsOne(u => u.Name, f => f.Property(p => p.Value).HasColumnName("name").HasMaxLength(100).IsRequired());

            builder.OwnsOne(u => u.Description, d => d.Property(p => p.Value).HasColumnName("descrição").IsRequired());

            builder.OwnsOne(u => u.Duration, d => d.Property(p => p.Value).HasColumnName("duração"));

        });

        modelBuilder.Entity<Theater>(builder =>
        {
            builder.ToTable("theaters");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(u => u.RemovedAt).HasColumnName("removed_at");

            builder.OwnsOne(u => u.Name, f => f.Property(p => p.Value).HasColumnName("name").HasMaxLength(100).IsRequired());


        });

        modelBuilder.Entity<Room>(builder =>
        {
            builder.ToTable("rooms");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(u => u.RemovedAt).HasColumnName("removed_at");

            builder.OwnsOne(u => u.Name, f => f.Property(p => p.Value).HasColumnName("name").HasMaxLength(100).IsRequired());


        });

        modelBuilder.Entity<Chair>(builder =>
        {
            builder.ToTable("chairs");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(u => u.RemovedAt).HasColumnName("removed_at");

            builder.OwnsOne(u => u.ChairPosition, f => f.Property(p => p.Value).HasColumnName("chair_position").HasMaxLength(100).IsRequired());
        
        });

        modelBuilder.Entity<ChairsInEvent>(builder =>
        {
            builder.ToTable("chairs_in_event");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(u => u.RemovedAt).HasColumnName("removed_at");

        });
    }
}