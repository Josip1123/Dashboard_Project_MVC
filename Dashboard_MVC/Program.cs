using System.Security.Claims;
using Business.Services;
using Data.Context;
using Data.Entities;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
    {
        option.Password.RequiredLength = 8;
        option.User.RequireUniqueEmail = true;
        option.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(option =>
{
    option.AccessDeniedPath = "/Login/Denied";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(15);
    option.SlidingExpiration = true;
    option.Cookie.SameSite = SameSiteMode.None;
    option.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    
    option.Events = new CookieAuthenticationEvents
    {
        OnRedirectToLogin = context =>
        {
            context.Response.Redirect("/Login/Denied");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ProjectRepository>();
builder.Services.AddScoped<MemberRepository>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<MemberService>();
builder.Services.AddScoped<UserAuthService>();

builder.Services.AddAuthentication(option =>
{
    option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie().AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    //options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "Admin", "Standard" };
    foreach (var roleName in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
   
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var adminEmail = "admin@admin.com"; 
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser { UserName = adminEmail, 
            Email = adminEmail, 
            EmailConfirmed = true,
            FirstName = "Admin",
            LastName = "Admin"
        };
        var result = await userManager.CreateAsync(adminUser, "AdminPassword123!");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
            if (!string.IsNullOrEmpty(adminUser.FirstName))
            {
                await userManager.AddClaimAsync(adminUser, new Claim("FirstName", adminUser.FirstName));
            }
        }
    }
}


if (!app.Environment.IsDevelopment())
{
    //app.UseHsts();
}



app.UseStaticFiles();
//app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCookiePolicy();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();