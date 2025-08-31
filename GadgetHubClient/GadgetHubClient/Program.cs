using System.Net.Http.Headers;
using System; // Required for Uri

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configure HttpClient for GadgetHub API
builder.Services.AddHttpClient("GadgetHubApi", client =>
{
    // Fixed: Access configuration using the key "GadgetHubApiUrl"
    client.BaseAddress = new Uri(builder.Configuration["GadgetHubApiUrl"]!); // Use null-forgiving operator
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
