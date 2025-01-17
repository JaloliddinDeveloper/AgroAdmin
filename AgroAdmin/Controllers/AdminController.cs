using Microsoft.AspNetCore.Mvc;

namespace AgroAdmin.Controllers
{
    public class AdminController: Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult Yangilik()
        {
            return View();
        }

        public IActionResult Photo()
        {
            return View();
        }

        public IActionResult ProOne()
        {
            return View();
        }

        public IActionResult ProTwo()
        {
            return View();
        }
    }
}
