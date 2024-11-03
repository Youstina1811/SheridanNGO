using Microsoft.AspNetCore.Mvc;
using SheridanNGO.Models;
using System.Diagnostics;

namespace SheridanNGO.Controllers
{
    public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

        // Action for Sign In
        public IActionResult SignIn()
        {
            return View(); // This will return the SignIn view
        }
    
        //Action for Sign up
            public IActionResult SignUp()
        {
            return View(); // Return the SignUp view
        }

        //Action for Volunteer
            public IActionResult Volunteer()
        {
            return View(); // Return the Volunteer view
        }

            public IActionResult Contact()
        {
            return View(); // Return the Contact view
        }
            public IActionResult Newsletter()
        {
            return View(); // This will return the Newsletter.cshtml view
        }

}
}