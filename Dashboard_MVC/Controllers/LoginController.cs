using System.Security.Claims;
using Business.Models;
using Business.Services;
using Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Dashboard_MVC.Controllers;


public class LoginController(UserAuthService authService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) : Controller
{
    [HttpGet]
    public IActionResult UserLogin()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> UserLogin(UserSignInForm form)
    {
        if (!ModelState.IsValid)
        {
            return View(form);
        }

        var res = await signInManager.PasswordSignInAsync(form.Email, form.Password, false, false);

        if (res.Succeeded)
        {
            return RedirectToAction("Projects", "Dashboard");
        }
        
        ViewData["ErrorMessage"] = "Wrong email or password. Try again.";
        return View(form);
    }
    
    [HttpGet ]
    public IActionResult MemberLogin()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> MemberLogin(AdminSignInForm form)
    {
        if (!ModelState.IsValid)
        {
            return View(form);
        }
        
        
        var res = await signInManager.PasswordSignInAsync(form.Email, form.Password, false, false);
        if (res.Succeeded)
        {
            var user = await userManager.FindByEmailAsync(form.Email);
            if (user != null)
            {
                var isAdmin = await userManager.IsInRoleAsync(user, "Admin");
                if (isAdmin) 
                {
                    return RedirectToAction("Members", "Dashboard"); 
                }
                
                await signInManager.SignOutAsync();
                ViewData["ErrorMessage"] = "Access Denied: Not Admin";
                return View(form);
            }
        }

        return View(form);
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
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("UserLogin", "Login");
    }
    
    [HttpPost]
    public async Task<IActionResult> MemberLogout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("MemberLogin", "Login");
    }

    public  IActionResult Denied()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ExternalSignIn(string provider, string returnUrl = null!)
    {
        if (string.IsNullOrEmpty(provider))
        {
            ModelState.AddModelError("", "Invalid Provider");
            return View();
        }

        string redirectUrl = Url.Action("ExternalSignInCallback", "Login", new { returnUrl })!;
        var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Challenge(properties, provider);
    }

    public async Task<IActionResult> ExternalSignInCallback(string returnUrl = null!, string remoteError = null!)
    {
        returnUrl ??= Url.Content("~/");

        if (!string.IsNullOrEmpty(remoteError))
        {
            ModelState.AddModelError("",$"Error from extr");
            return View("UserLogin");
        }

        var externalLoginInfo = await signInManager.GetExternalLoginInfoAsync();
        if (externalLoginInfo == null)
        {
            return RedirectToAction("UserLogin");
        }

        var signInResult = await signInManager.ExternalLoginSignInAsync(externalLoginInfo.LoginProvider,
            externalLoginInfo.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        
        if (signInResult.Succeeded)
        {
            return RedirectToAction("Projects", "Dashboard");
        }
        else
        {
            string firstName = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.GivenName)!;
            string lastName = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Surname)!;
            string email = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email)!;
            string username = $"ext_{externalLoginInfo.LoginProvider.ToLower()}_{email}";

            var user = new ApplicationUser
            {
                UserName = username,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            var identityResult = await userManager.CreateAsync(user);
            if (identityResult.Succeeded)
            {
                await userManager.AddLoginAsync(user, externalLoginInfo);
                await signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Projects", "Dashboard");
            }

            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("UserLogin");
        }
    }
    
}