using Gestao_Clientes.DAL;
using Gestao_Clientes.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


var builder = WebApplication.CreateBuilder(args);

var connectionString =
builder.Configuration.GetConnectionString("CA_RS11_P2_2_AlexandraMendes_DBCS");

builder.Services.AddDbContext<CA_RS11_P2_2_AlexandraMendes_DBContext>(options =>
options.UseSqlServer(connectionString));

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Initialize the database with seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CA_RS11_P2_2_AlexandraMendes_DBContext>();
    var seedData = new SeedData();
    seedData.SeedDataInDatabase(context);
}

app.Run();

