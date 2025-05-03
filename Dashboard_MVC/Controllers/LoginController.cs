using Business.Models;
using Business.Services;
using Dashboard_MVC.Views.Models;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard_MVC.Controllers;


public class LoginController(UserAuthService authService, SignInManager<ApplicationUser> signInManager) : Controller
{
    
    public async Task<IActionResult> UserLogin(UserSignInForm form)
    {
        if (!ModelState.IsValid)
            return View(form);

        var res = await signInManager.PasswordSignInAsync(form.Email, form.Password, false, false);

        if (res.Succeeded)
        {
            return RedirectToAction("Projects", "Dashboard");
        }
        
        return View(form);
    }
    
    public IActionResult MemberLogin()
    {
        return View();
    }
    
    public IActionResult Signup()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Signup(RegistrationForm form)
    {
        if (!ModelState.IsValid)
        {
            return View(form);
        }

        var res = await authService.CreateAsync(form);
        if (res)
        {
            return RedirectToAction("UserLogin");
        }
        
        return View();
    }
}