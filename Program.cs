using Api_Venda_Ingressos.BoundedContext.Auth.Application.Services;
using Api_Venda_Ingressos.BoundedContext.Auth.Application.UseCases;
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Interfaces;
using Api_Venda_Ingressos.Data;
using Api_Venda_Ingressos.BoundedContext.Auth.Infrastructure.Repository;
using Api_Venda_Ingressos.Data.Mock;


using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Sell.Infrastructure.Data;
using Api_Venda_Ingressos.BoundedContext.Sell.Infrastructure.Repository;
using Api_Venda_Ingressos.BoundedContext.Sell.Application.UseCases;

using Api_Venda_Ingressos.BoundedContext.Event.Infrastructure.Repository;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairUseCases;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.TheaterUseCases;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairInEventUseCases;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.Room;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.RoomEvent;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.EventUseCases;

var builder = WebApplication.CreateBuilder(args);

// 1. BANCO DE DADOS
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDbContext<AppSellDbContext>(options => options.UseNpgsql(connectionString));



// 2. INJEÇÃO DE DEPENDÊNCIA
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<RegisterUserUseCase>();
builder.Services.AddScoped<RegisterAdminUseCase>();
builder.Services.AddScoped<LoginUserUseCase>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

//UseCases de Ticket
builder.Services.AddScoped<CreateTicketUseCase>();
builder.Services.AddScoped<GetAllTicketUseCase>();
builder.Services.AddScoped<GetTicketByIdUseCase>();
builder.Services.AddScoped<SellTicketUseCase>();
builder.Services.AddScoped<DeleteTicketUseCase>();

builder.Services.AddScoped<ITicketRepository, TicketRepository>();

//UseCases de Chair
builder.Services.AddScoped<CreateChairUseCase>();
builder.Services.AddScoped<ListChairUseCase>();
builder.Services.AddScoped<GetChairByIdUseCase>();
builder.Services.AddScoped<UpdateChairUseCase>();
builder.Services.AddScoped<DeleteChairUseCase>();
builder.Services.AddScoped<IChairRepository, ChairRepository>();

//UseCases de Theater
builder.Services.AddScoped<CreateTheaterUseCase>();
builder.Services.AddScoped<ListTheatersUseCase>();
builder.Services.AddScoped<GetTheaterByIdUseCase>();
builder.Services.AddScoped<UpdateTheaterUseCase>();
builder.Services.AddScoped<DeleteTheaterUseCase>();
builder.Services.AddScoped<ITheaterRepository, TheaterRepository>();

//UseCases de ChairInEvent
builder.Services.AddScoped<CreateChairsInEventUseCase>();
builder.Services.AddScoped<ListChairsInEventUseCase>();
builder.Services.AddScoped<GetChairsInEventByIdUseCase>();
builder.Services.AddScoped<UpdateChairsInEventUseCase>();
builder.Services.AddScoped<DeleteChairsInEventUseCase>();
builder.Services.AddScoped<IChairsInEventRepository, ChairsInEventRepository>();

//UseCases de Room
builder.Services.AddScoped<CreateRoomUseCase>();
builder.Services.AddScoped<ListRoomsUseCase>();
builder.Services.AddScoped<GetRoomByIdUseCase>();
builder.Services.AddScoped<UpdateRoomUseCase>();
builder.Services.AddScoped<DeleteRoomUseCase>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

//UseCases de RoomEvent
builder.Services.AddScoped<CreateRoomEventUseCase>();
builder.Services.AddScoped<ListRoomsEventUseCase>();
builder.Services.AddScoped<GetRoomEventByIdUseCase>();
builder.Services.AddScoped<UpdateRoomEventUseCase>();
builder.Services.AddScoped<DeleteRoomEventUseCase>();
builder.Services.AddScoped<IRoomEventRepository, RoomEventRepository>();

builder.Services.AddScoped<CreateEventUseCase>();
builder.Services.AddScoped<IEventRepository, EventRepository>();

// 3. JWT AUTHENTICATION
var jwt = builder.Configuration.GetSection("Jwt");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                                           Encoding.UTF8.GetBytes(jwt["Secret"]!)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// 4. PIPELINE DE EXECUÇÃO
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Venda de Ingressos API");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    await AdminMock.SeedAsync(scope.ServiceProvider); //Adm mockado para ter acesso total
}
app.Run();