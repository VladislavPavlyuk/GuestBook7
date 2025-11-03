using Microsoft.EntityFrameworkCore;
using GBook.Models;
using GBook.Repository;

var builder = WebApplication.CreateBuilder(args);

// All sessions work on top of IDistributedCache object, and
// ASP.NET Core provides built-in implementation of IDistributedCache
builder.Services.AddDistributedMemoryCache(); // add IDistributedMemoryCache
builder.Services.AddSession(); // Add session services

// Get connection string from configuration file
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// add ApplicationContext as a service to the application
builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));

// Add Razor Pages services
builder.Services.AddRazorPages();
builder.Services.AddScoped<IRepository, GBookRepository>();

var app = builder.Build();
app.UseSession(); // Add middleware component for working with sessions
app.UseStaticFiles(); // handles requests to files in wwwroot folder

app.MapRazorPages();

app.Run();
