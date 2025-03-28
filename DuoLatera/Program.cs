using DuoLatera;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DuoLatera.Repository.IRepository;
using DuoLatera.Repository;
using DuoLatera.Managers;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
//set up the database here
builder.Services.AddDbContext<ApplicationDbContext>
    (options => options.UseSqlServer
    (builder.Configuration.GetConnectionString
    ("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ILoginManager, LoginManager>();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
