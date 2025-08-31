using Microsoft.EntityFrameworkCore;
using ElectroComApi.Data; // Changed for ElectroCom

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext with SQL Server for ElectroCom
builder.Services.AddDbContext<ElectroComDbContext>(options => // Changed for ElectroCom
    options.UseSqlServer(builder.Configuration.GetConnectionString("ElectroComDbConnection"))); // Changed for ElectroCom

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