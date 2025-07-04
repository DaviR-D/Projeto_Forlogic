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

app.MapPost("/register/{id}", (Registration registration, string id) =>
{
    registrations.Add(id, registration);

});

app.MapGet("/register", () => { return registrations; });

app.MapPut("/register/{id}", (Registration registration, string id) =>
{
    registrations[id] = registration;

});

app.MapDelete("/register/{id}", (string id) => { registrations.Remove(id); });

app.Run();

public record Registration(
    string Id,
    string Name,
    string Email,
    string Status,
    bool Pending,
    string Date,
    int Age,
    string Address,
    string Other,
    string Interests,
    string Feelings,
    string Values
);
