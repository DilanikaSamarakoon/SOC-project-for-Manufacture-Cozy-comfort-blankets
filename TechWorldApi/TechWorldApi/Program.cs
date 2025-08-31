using Microsoft.EntityFrameworkCore; // Add this using statement
using TechWorldApi.Data; // Add this using statement

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext with SQL Server
builder.Services.AddDbContext<TechWorldDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TechWorldDbConnection")));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enables the Swagger JSON endpoint
    app.UseSwaggerUI(); // Enables the Swagger UI (interactive documentation)
}

app.UseHttpsRedirection(); // Redirects HTTP requests to HTTPS

app.UseAuthorization(); // Enables authorization middleware

app.MapControllers(); // Maps controller actions to routes

app.Run(); // Runs the application