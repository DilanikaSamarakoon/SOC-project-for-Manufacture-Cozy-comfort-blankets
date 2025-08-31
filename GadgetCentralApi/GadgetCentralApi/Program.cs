using Microsoft.EntityFrameworkCore;
using GadgetCentralApi.Data; // Changed for Gadget Central

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext with SQL Server for Gadget Central
builder.Services.AddDbContext<GadgetCentralDbContext>(options => // Changed for Gadget Central
    options.UseSqlServer(builder.Configuration.GetConnectionString("GadgetCentralDbConnection"))); // Changed for Gadget Central

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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