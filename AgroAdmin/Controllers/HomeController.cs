using Microsoft.AspNetCore.Mvc;

namespace AgroAdmin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
