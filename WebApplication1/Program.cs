using Common.Interfaces;
using Common.Models;
using DataAccess.Context;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<CustomUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<LibraryDbContext>();
builder.Services.AddControllersWithViews();

//Scoped => it creates one instance per request
//           e.g. user => /Books/Create => 1) checking whether book exists 2) to add the book
//Transient => it creates one instance per call
//Singleton => it creates one instance for all users

string dataSource = builder.Configuration["dataSource"].ToString();
string jsonPath = builder.Configuration["jsonPath"].ToString();
if (dataSource == "db")
{
    builder.Services.AddScoped<IBooksRepository, BooksRepository>();
}
else
{
    builder.Services.AddScoped<IBooksRepository, BooksJsonRepository>
        (x=> new BooksJsonRepository(jsonPath));
}

    
builder.Services.AddScoped<CategoriesRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
