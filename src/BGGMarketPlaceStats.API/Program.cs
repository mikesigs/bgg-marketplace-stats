using BGGMarketPlaceStats.Core;
using BGGMarketPlaceStats.Core.Interfaces;
using BGGMarketPlaceStats.Core.Model;
using BGGMarketPlaceStats.Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddCoreServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();

app.MapGet("/stats/{gameId}/{currency}", async (int gameId, string currency, [FromServices] IBggService bggService, [FromServices] IMarketplaceStatisticsService statisticsService) =>
{
    var game = await bggService.GetGame(gameId);
    var targetCurrency = Currency.FromName(currency);
    var marketplaceStatistics = await statisticsService.GetMarketplaceStatistics(game, targetCurrency);
    //return Results.Ok(marketplaceStatistics.Select(s => new GetGameStatsResponse(game.Id, game.Name, s.Condition, s.NumberOfListings, s.MinPrice.Value, s.MaxPrice.Value, s.AveragePrice.Value, s.MedianPrice.Value, s.ModePrice)));
    return Results.Ok(new
    {
        GameId = game.Id,
        GameName = game.Name,
        Statistics = marketplaceStatistics.Select(s => new
        {
            Condition = s.Condition,
            NumberOfListings = s.NumberOfListings,
            MinPrice = Math.Round(s.MinPrice.Value, 0),
            MaxPrice = Math.Round(s.MaxPrice.Value, 0),
            AveragePrice = Math.Round(s.AveragePrice.Value, 0),
            MedianPrice = Math.Round(s.MedianPrice.Value, 0),
            ModePrice = s.ModePrice
        })
    });
})
.WithName("GetGameStats");

app.Run();
