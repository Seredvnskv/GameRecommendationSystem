using Backend.Data;
using Backend.Endpoints;
using Backend.Factory;
using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<GameContext>();
builder.Services.AddSingleton<GameFeaturesCache>();
builder.Services.AddScoped<VectorFactory>();
builder.Services.AddScoped<RecommendationService>();
builder.Services.AddScoped<GameDataLoader>();

var app = builder.Build();

app.MigrateDb();
await app.SeedData();
app.MapGamesEndpoints();
app.Run();
