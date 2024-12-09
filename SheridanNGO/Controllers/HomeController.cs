using Microsoft.AspNetCore.Mvc;
using SheridanNGO.Models;
using System.Diagnostics;

namespace SheridanNGO.Controllers
{
    public class HomeController : Controller
{
        private DonationDbContext _context;

    public IActionResult Index()
    {
           
        return View();
    }

        // Action for Sign In
        public IActionResult SignIn()
        {
          //  User admin = new User("admin", "admin@sheridan.com", "admin", "234234", "Sheridan College", UserType.Admin);
            //_context.AddUser(admin);
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

            public IActionResult MainPage()
        {
            return View(); // This will return the Newsletter.cshtml view
        }

}
}