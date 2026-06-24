using Api_Venda_Ingressos.BoundedContext.Auth.Application.Services;
using Api_Venda_Ingressos.BoundedContext.Auth.Application.UseCases;
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Interfaces;
using Api_Venda_Ingressos.Data;
using Api_Venda_Ingressos.BoundedContext.Auth.Infrastructure.Repository;
using Api_Venda_Ingressos.Data.Mock;

using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Sell.Infrastructure.Repository;
using Api_Venda_Ingressos.BoundedContext.Sell.Application.UseCases;

using Api_Venda_Ingressos.BoundedContext.Event.Infrastructure.Repository;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairUseCases;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.TheaterUseCases;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairInEventUseCases;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.Room;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.RoomEvent;
using Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.EventUseCases;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// 1. BANCO DE DADOS
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS — AllowCredentials requer origens explícitas (não wildcard)
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()
    ?? ["http://localhost:5173"];
builder.Services.AddCors(options =>
    options.AddPolicy("Frontend", policy =>
        policy.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()));

// RATE LIMITING — 5 tentativas por minuto no login
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("login", o =>
    {
        o.PermitLimit = 5;
        o.Window = TimeSpan.FromMinutes(1);
        o.QueueLimit = 0;
        o.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

// 2. INJEÇÃO DE DEPENDÊNCIA
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger com suporte a JWT Bearer
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Venda de Ingressos API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira apenas o token JWT (sem o prefixo 'Bearer ')"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer", document),
            new List<string>()
        }
    });
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<RegisterUserUseCase>();
builder.Services.AddScoped<RegisterAdminUseCase>();
builder.Services.AddScoped<LoginUserUseCase>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();

//UseCases de Ticket
builder.Services.AddScoped<GetAllTicketUseCase>();
builder.Services.AddScoped<GetTicketByIdUseCase>();
builder.Services.AddScoped<GetTicketsByUserIdUseCase>();
builder.Services.AddScoped<SellTicketUseCase>();
builder.Services.AddScoped<DeleteTicketUseCase>();
builder.Services.AddScoped<ProcessPaymentUseCase>();

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
builder.Services.AddScoped<ListEventsUseCase>();
builder.Services.AddScoped<GetEventByIdUseCase>();
builder.Services.AddScoped<UpdateEventUseCase>();
builder.Services.AddScoped<DeleteEventUseCase>();
builder.Services.AddScoped<IEventRepository, EventRepository>();

// 3. JWT AUTHENTICATION
var jwt = builder.Configuration.GetSection("Jwt");
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET")
    ?? jwt["Secret"];
if (string.IsNullOrWhiteSpace(jwtSecret) || jwtSecret.Length < 32)
    throw new InvalidOperationException(
        "JWT_SECRET não configurado ou muito curto (mínimo 32 caracteres). Defina a variável de ambiente JWT_SECRET.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSecret)),
            ClockSkew = TimeSpan.Zero
        };

        // Lê o token do cookie httpOnly se não houver Authorization header
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = ctx =>
            {
                if (string.IsNullOrEmpty(ctx.Token) &&
                    ctx.Request.Cookies.TryGetValue("access_token", out var cookieToken))
                    ctx.Token = cookieToken;
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// 4. PIPELINE DE EXECUÇÃO

// Tratamento global de erros — nunca expõe ex.Message ao cliente em produção
app.UseExceptionHandler(errApp => errApp.Run(async ctx =>
{
    ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
    ctx.Response.ContentType = "application/json";
    await ctx.Response.WriteAsJsonAsync(new { error = "Erro interno do servidor." });
}));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Venda de Ingressos API v1");
        options.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseCors("Frontend");

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    await AdminMock.SeedAsync(scope.ServiceProvider);
}

app.Run();
