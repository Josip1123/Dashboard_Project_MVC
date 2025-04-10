
using Microsoft.AspNetCore.Mvc;


namespace Dashboard_MVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}