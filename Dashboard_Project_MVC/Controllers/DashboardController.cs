using Microsoft.AspNetCore.Mvc;


namespace Dashboard_Project_MVC.Controllers;

public class DashboardController : Controller
{
    public IActionResult Dashboard()
    {
        return View();
    }
}