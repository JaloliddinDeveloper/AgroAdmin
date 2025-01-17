// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc;

namespace AgroAdmin.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() =>
             View();

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            if (username == "1" && password == "1")
            {
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.ErrorMessage = "Invalid username or password.";
            return View();
        }
    }
}
