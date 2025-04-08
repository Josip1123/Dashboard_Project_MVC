using Microsoft.AspNetCore.Mvc;

namespace Dashboard_Project_MVC.Controllers;


public class LoginController : Controller
{
    public IActionResult UserLogin()
    {
        return View();
    }
    
    public IActionResult MemberLogin()
    {
        return View();
    }
    
    public IActionResult Signup()
    {
        return View();
    }
}