using Backend.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<GameContext>();
builder.Services.AddScoped<GameDataLoader>();

var app = builder.Build();

app.MigrateDb();
app.SeedData();

app.MapGet("/", () => "Hello World!");

app.Run();
