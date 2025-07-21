using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Api.Modules.Clients.Domain;
using Api.Modules.Clients.Infrastructure.Repositories;
using Api.Modules.Authentication.Application;
using Api.Modules.Authentication.Domain;
using Api.Modules.Authentication.Presentation.UserDTOs;
using Api.Modules.Authentication.Presentation;
using Api.Modules.Authentication.Infrastructure.Repositories;
using Api.Shared.Configurations;
using Api.Shared.Interfaces;
using Api.Modules.Clients.Application.Queries.GetPagedClients;
using Api.Modules.Clients.Application.Commands.CreateClient;
using Api.Modules.Clients.Application.Queries.GetClientsStats;
using Api.Modules.Clients.Application.Queries.GetSingleClient;
using Api.Modules.Clients.Application.Queries.GetSortedClients;
using Api.Modules.Clients.Application.Queries.SearchClients;
using Api.Modules.Clients.Application.Queries.VerifyAvailableEmail;
using Api.Modules.Clients.Application.Commands.DeleteClient;
using Api.Modules.Clients.Application.Commands.UpdateClient;
using Api.Modules.Authentication.Application.Commands.Authenticate;
using Api.Modules.Authentication.Application.Commands.CreateUser;

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

builder.Services.AddSingleton(registrationsMock);
builder.Services.AddSingleton(usersMock);
builder.Services.AddSingleton(authSettings);
builder.Services.AddScoped<UserRepository>();

UserRepository repository = new(usersMock);
AuthenticationController controller = new(repository, authSettings);
controller.Create(new UserDto("Davi", "davi@gmail.com", "senha123"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("localhost", builder =>
    {
        builder.WithOrigins("http://127.0.0.1:5500")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddScoped<ClientRepository>();
builder.Services.AddScoped<IRequestHandler<IRequestOutput, IRequestInput>, GetPagedClientsHandler>();
builder.Services.AddScoped<IRequestHandler<IRequestOutput, IRequestInput>, GetClientsStatsHandler>();
builder.Services.AddScoped<IRequestHandler<IRequestOutput, IRequestInput>, GetSingleClientHandler>();
builder.Services.AddScoped<IRequestHandler<IRequestOutput, IRequestInput>, GetSortedClientsHandler>();
builder.Services.AddScoped<IRequestHandler<IRequestOutput, IRequestInput>, SearchClientsHandler>();
builder.Services.AddScoped<IRequestHandler<IRequestOutput, IRequestInput>, VerifyAvailableEmailHandler>();
builder.Services.AddScoped<IRequestHandler<IRequestOutput, IRequestInput>, CreateClientHandler>();
builder.Services.AddScoped<IRequestHandler<IRequestOutput, IRequestInput>, DeleteClientHandler>();
builder.Services.AddScoped<IRequestHandler<IRequestOutput, IRequestInput>, UpdateClientHandler>();
builder.Services.AddScoped<IRequestHandler<IRequestOutput, IRequestInput>, AuthenticateHandler>();
builder.Services.AddScoped<IRequestHandler<IRequestOutput, IRequestInput>, CreateUserHandler>();


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
