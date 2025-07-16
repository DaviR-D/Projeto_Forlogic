using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Api.Modules.Authentication;
using Api.Modules.Registrations;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

var key = AuthenticationSettings.PrivateKey;

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

List<RegistrationDto> registrationsMock = [];
List<User> usersMock = [];


builder.Services.AddTransient<AuthenticationService>();

builder.Services.AddSingleton(registrationsMock);
builder.Services.AddSingleton(usersMock);

AuthenticationService service = new(usersMock);
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

builder.Services.AddScoped<IRegistrationService, RegistrationService>();

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
