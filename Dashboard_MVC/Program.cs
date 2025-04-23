using Business.Services;
using Data.Context;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ProjectRepository>();
builder.Services.AddScoped<ProjectService>();


builder.Services.Configure<CookiePolicyOptions>(options =>
{
   
    options.CheckConsentNeeded = context => true;


    options.MinimumSameSitePolicy = SameSiteMode.Lax;




});


builder.Services.AddHttpContextAccessor();




var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    //app.UseHsts();
}

app.UseStaticFiles();
//app.UseHttpsRedirection();
app.UseRouting();

app.UseCookiePolicy();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();