using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Api.Modules.Authentication;
using Api.Modules.Clients;
using System.Text;
using Api.Modules.Repositories;
using Api.Modules.Clients.Commands.CreateClient;
using Api.Modules.Clients.Interfaces;
using Api.Modules.Clients.Queries.GetPagedClients;


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

AuthenticationService service = new(usersMock, authSettings);
service.CreateUser(new UserDto("Davi", "davi@gmail.com", "senha123"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("localhost", builder =>
    {
        builder.WithOrigins("http://127.0.0.1:5500")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<ClientRepository>();
//builder.Services.AddScoped<IClientHandler<IClientOutput, IClientInput>, GetPagedClientsHandler>();
//builder.Services.AddScoped<IClientHandler<IClientOutput, IClientInput>, CreateClientHandler>();


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
