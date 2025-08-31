using GadgetHubApi.Models;
using System.Net.Http.Headers;
using GadgetHubApi.Services;
using System; // Required for Uri

var builder = WebApplication.CreateBuilder(args);

// Configure HttpClient for each distributor API
builder.Services.AddHttpClient("TechWorldApi", client =>
{
    // Corrected: Access configuration using the key "DistributorApiUrls:TechWorld"
    client.BaseAddress = new Uri(builder.Configuration["DistributorApiUrls:TechWorld"]!); // Use null-forgiving operator
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddHttpClient("ElectroComApi", client =>
{
    // Corrected: Access configuration using the key "DistributorApiUrls:ElectroCom"
    client.BaseAddress = new Uri(builder.Configuration["DistributorApiUrls:ElectroCom"]!); // Use null-forgiving operator
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddHttpClient("GadgetCentralApi", client =>
{
    // Corrected: Access configuration using the key "DistributorApiUrls:GadgetCentral"
    client.BaseAddress = new Uri(builder.Configuration["DistributorApiUrls:GadgetCentral"]!); // Use null-forgiving operator
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

// Register the QuotationService
builder.Services.AddScoped<IQuotationService, QuotationService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
