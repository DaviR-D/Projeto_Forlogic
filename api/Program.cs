using Api.Modules.Authentication.Application.Commands.Authenticate;
using Api.Modules.Clients.Application.Queries.VerifyAvailableEmail;
using Api.Modules.Authentication.Application.Commands.CreateUser;
using Api.Modules.Clients.Application.Queries.GetSortedClients;
using Api.Modules.Clients.Application.Queries.GetPagedClients;
using Api.Modules.Clients.Application.Queries.GetClientsStats;
using Api.Modules.Clients.Application.Queries.GetSingleClient;
using Api.Modules.Authentication.Infrastructure.Repositories;
using Api.Modules.Clients.Application.Queries.SearchClients;
using Api.Modules.Clients.Application.Commands.DeleteClient;
using Api.Modules.Clients.Application.Commands.UpdateClient;
using Api.Modules.Clients.Application.Commands.CreateClient;
using Api.Modules.Authentication.Presentation.UserDTOs;
using Api.Modules.Clients.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Api.Modules.Authentication.Application;
using Api.Modules.Authentication.Domain;
using Api.Modules.Clients.Application;
using Microsoft.IdentityModel.Tokens;
using Api.Modules.Clients.Domain;
using Api.Shared.Configurations;
using System.Text;
using Api.Modules.Logs.Application;
using Api.Modules.Logs.Infrastructure.Repositories;
using Api.Modules.Logs.Application.Commands.CreateLog;
using Api.Modules.Logs.Application.Queries.GetLogs;
using Api.Modules.Logs.Domain;

var builder = WebApplication.CreateBuilder(args);

AuthenticationSettings? authSettings = builder.Configuration.GetSection("AuthenticationSettings").Get<AuthenticationSettings>();
string key = authSettings.PrivateKey;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

List<Client> registrationsMock = [];
List<User> usersMock = [];
List<Log> logsMock = [];

builder.Services.AddSingleton(registrationsMock);
builder.Services.AddSingleton(usersMock);
builder.Services.AddSingleton(logsMock);
builder.Services.AddSingleton(authSettings);
builder.Services.AddScoped<AuthenticationHandlerFactory>();
builder.Services.AddScoped<ClientsHandlerFactory>();
builder.Services.AddScoped<LogHandlerFactory>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<LogRepository>();
builder.Services.AddScoped<AuthenticateHandler>();
builder.Services.AddScoped<CreateUserHandler>();
builder.Services.AddScoped<GetPagedClientsHandler>();
builder.Services.AddScoped<GetClientsStatsHandler>();
builder.Services.AddScoped<GetSingleClientHandler>();
builder.Services.AddScoped<GetSortedClientsHandler>();
builder.Services.AddScoped<SearchClientsHandler>();
builder.Services.AddScoped<VerifyAvailableEmailHandler>();
builder.Services.AddScoped<CreateClientHandler>();
builder.Services.AddScoped<DeleteClientHandler>();
builder.Services.AddScoped<UpdateClientHandler>();
builder.Services.AddScoped<CreateLogHandler>();
builder.Services.AddScoped<GetLogsHandler>();

UserRepository repository = new(usersMock);
var handler = new CreateUserHandler(repository);
handler.Handle(new CreateUserCommand(new UserDto("Davi", "davi@gmail.com", "senha123")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("localhost", builder =>
    {
        builder.WithOrigins("http://127.0.0.1:5500")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("localhost");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
