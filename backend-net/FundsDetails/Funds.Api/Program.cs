
using Funds.Api;
using Funds.Api.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<FundsDb>();

builder.Services.AddHttpClient<ExternalApi>();

builder.Services.AddScoped<FundsImportService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/api/data", async (FundsDb repo) =>
{
    var funds = await repo.GetAllAsync();
    return Results.Ok(funds);
});


app.MapPut("/api/data/{isin}", async (string isin, FundDto fund, FundsDb repo) =>
{

    fund.Isin = isin;
    Console.WriteLine(isin);
    var rows = await repo.UpdateAsync(fund);
    if (rows == 0)
        return Results.NotFound($"Fund with ISIN {isin} not found.");

    return Results.Ok(fund);
});

app.MapPost("/admin/import-funds", async ([FromServices] FundsImportService importService) =>
{
    var rows = await importService.ImportAsync();
    return Results.Ok(new { importedOrUpdated = rows });
});


app.Run();
