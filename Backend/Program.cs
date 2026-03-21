using Backend.Data;
using Backend.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<GameContext>();
builder.Services.AddScoped<GameDataLoader>();

var app = builder.Build();

app.MigrateDb();
app.SeedData();

app.MapGamesEndpoints();

app.Run();
