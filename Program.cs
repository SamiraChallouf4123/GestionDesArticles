using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GestionDesArticles.Models;
using GestionDesArticles.Models.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configurer la base de donn�es
builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductDBConnection")));

// Configurer Identity + r�gles mot de passe
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Configuration des PasswordOptions
    options.Password.RequireDigit = false;            // Pas oblig� d�avoir un chiffre
    options.Password.RequiredLength = 6;              // Longueur minimale
    options.Password.RequireLowercase = true;         // Minuscule obligatoire
    options.Password.RequireUppercase = false;        // Majuscule non obligatoire
    options.Password.RequireNonAlphanumeric = false;  // Pas de caract�re sp�cial obligatoire
    options.Password.RequiredUniqueChars = 1;         // Nombre de caract�res uniques
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// Injection des repositories
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

// Ajouter les services MVC
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Important : avant Authorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
