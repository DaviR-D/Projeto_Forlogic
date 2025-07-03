var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("policy", builder =>
    {
        builder.WithOrigins("http://127.0.0.1:5500")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("policy");

app.UseHttpsRedirection();

var registrations = new Dictionary<string, Registration>();

app.MapGet("/register", () => { return registrations; });

app.MapPost("/register/{id}", (Registration registration, string id) =>
{
    if (registrations.TryAdd(id, registration)) { }
    else
    {
        registrations[id] = registration;
    }
});

app.MapDelete("/register/{id}", (string id) => { registrations.Remove(id); });

app.Run();

public record Registration(
    string id,
    string name,
    string email,
    string status,
    bool pending,
    string date,
    int age,
    string address,
    string other,
    string interests,
    string feelings,
    string values
);
