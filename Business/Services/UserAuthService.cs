using System.Security.Claims;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace Business.Services;

public class UserAuthService(UserManager<ApplicationUser> userManager)
{
    
    public async Task<bool> CreateAsync(RegistrationForm form)
    {
        
        if (await userManager.Users.AnyAsync(u => u.Email == form.Email ))
            return false;

        var applicationUser = new ApplicationUser
        {
            UserName = form.Email,
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email,
        };

        var res = await userManager.CreateAsync(applicationUser, form.Password);
        await userManager.AddClaimAsync(applicationUser, new Claim("FirstName", form.FirstName));

        if (res.Succeeded)
        {
            return true;
        }

        return false;
    }
}