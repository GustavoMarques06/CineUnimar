
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities;
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
    public DbSet<Events> Events { get; set; }
    public DbSet<Ticket> Ticket { get; set; }

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
            builder.HasQueryFilter(u => u.RemovedAt == null);

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

        modelBuilder.Entity<Events>(builder =>
        {
            builder.ToTable("events");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.Status).HasColumnName("status").HasConversion<string>().HasMaxLength(20).IsRequired();
            builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(u => u.RemovedAt).HasColumnName("removed_at");
            builder.Property(u => u.Date).HasColumnName("date");
            builder.Property(u => u.RoomId).HasColumnName("room_id").IsRequired();
            builder.Property(u => u.CategoryId).HasColumnName("category_id").IsRequired();
            builder.Property(u => u.UserCreatorId).HasColumnName("user_creator_id").IsRequired();
            builder.HasQueryFilter(u => u.RemovedAt == null);

            builder.OwnsOne(u => u.Name, f => f.Property(p => p.Value).HasColumnName("name").HasMaxLength(100).IsRequired());
            builder.OwnsOne(u => u.Description, d => d.Property(p => p.Value).HasColumnName("description"));
            builder.OwnsOne(u => u.Duration, d => d.Property(p => p.Value).HasColumnName("duration"));
        });

        modelBuilder.Entity<Theater>(builder =>
        {
            builder.ToTable("theaters");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.Location).HasColumnName("Location");
            builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(u => u.RemovedAt).HasColumnName("removed_at");
            builder.HasQueryFilter(u => u.RemovedAt == null);

            builder.OwnsOne(u => u.Name, f => f.Property(p => p.Value).HasColumnName("name").HasMaxLength(100).IsRequired());
        });

        modelBuilder.Entity<Room>(builder =>
        {
            builder.ToTable("rooms");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.IdTheater).HasColumnName("id_theater").IsRequired();
            builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(u => u.RemovedAt).HasColumnName("removed_at");
            builder.HasQueryFilter(u => u.RemovedAt == null);

            builder.OwnsOne(u => u.Name, f => f.Property(p => p.Value).HasColumnName("name").HasMaxLength(100).IsRequired());

            builder.HasOne<Theater>()
                .WithMany()
                .HasForeignKey(x => x.IdTheater)
                .HasConstraintName("FK_rooms_theaters")
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Chair>(builder =>
        {
            builder.ToTable("chairs");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.IdRoom).HasColumnName("id_room").IsRequired();
            builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(u => u.RemovedAt).HasColumnName("removed_at");
            builder.HasQueryFilter(u => u.RemovedAt == null);

            builder.OwnsOne(u => u.ChairPosition, f => f.Property(p => p.Value).HasColumnName("chair_position").HasMaxLength(100).IsRequired());

            builder.HasOne<Room>()
                .WithMany()
                .HasForeignKey(x => x.IdRoom)
                .HasConstraintName("FK_rooms_chairs")
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<RoomEvent>(builder =>
        {
            builder.ToTable("rooms_in_event");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.IdRoom).HasColumnName("id_room").IsRequired();
            builder.Property(u => u.IsFull).HasColumnName("IsFull");
            builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(u => u.RemovedAt).HasColumnName("removed_at");
            builder.HasQueryFilter(u => u.RemovedAt == null);

            builder.HasOne<Room>()
                .WithMany()
                .HasForeignKey(x => x.IdRoom)
                .HasConstraintName("FK_rooms_in_event_rooms")
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ChairsInEvent>(builder =>
        {
            builder.ToTable("chairs_in_event");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.IdRoomEvent).HasColumnName("id_room_event").IsRequired();
            builder.Property(u => u.Status).HasColumnName("Status").HasConversion<int>();
            builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(u => u.RemovedAt).HasColumnName("removed_at");
            builder.HasQueryFilter(u => u.RemovedAt == null);

            builder.HasOne<RoomEvent>()
                .WithMany()
                .HasForeignKey(x => x.IdRoomEvent)
                .HasConstraintName("FK_chairs_in_event_rooms_in_event")
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Ticket>(builder =>
        {
            builder.ToTable("tickets");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("id");
            builder.Property(u => u.EventId).HasColumnName("event_id").IsRequired();
            builder.Property(u => u.ChairInEventId).HasColumnName("chair_in_event_id").IsRequired();
            builder.Property(u => u.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(u => u.Status).HasColumnName("status").HasConversion<int>().IsRequired();
            builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
            builder.Property(u => u.RemovedAt).HasColumnName("removed_at");

            builder.OwnsOne(u => u.Purchase_Data, d => d.Property(p => p.value).HasColumnName("purchase_data").IsRequired());
            builder.OwnsOne(u => u.Price, d => d.Property(p => p.value).HasColumnName("price").IsRequired());
        });
    }
}
