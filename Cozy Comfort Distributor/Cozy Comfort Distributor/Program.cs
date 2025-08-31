using Cozy_Comfort_Distributor.Data;    // <--- CRITICAL
using Cozy_Comfort_Distributor.Mappings; // <--- CRITICAL
using Cozy_Comfort_Distributor.Services; // <--- CRITICAL
using Microsoft.EntityFrameworkCore;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 1. Configure your DbContext
builder.Services.AddDbContext<DistributorDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// 3. Register Service Layer
builder.Services.AddScoped<IDistributorService, DistributorService>();
builder.Services.AddScoped<IDistributorOrderService, DistributorOrderService>();

// 4. Add Web API services
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