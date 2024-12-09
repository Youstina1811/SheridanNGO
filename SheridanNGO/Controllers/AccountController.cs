using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SheridanNGO.Models;
using Microsoft.AspNetCore.Identity;
using AspNetCoreGeneratedDocument;

namespace SheridanNGO.Controllers
{

    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
        private DonationDbContext _donationDbContext;
       
       // User admin = new User("admin","admin@sheridan.com","admin","23423423","Sheridan College");
       
       
/*        public IActionResult Login()
        {
          //  _donationDbContext.Add(admin);
            return View();
        }*/

        [HttpPost]
        public async Task<IActionResult> Login(User model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,true, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }


        //test sign in page backend.

//        [HttpPost]
/*public async Task<IActionResult> Login(string username, string password, bool rememberMe = false)
{
    if (ModelState.IsValid)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user != null)
        {
            var result = await _signInManager.PasswordSignInAsync(user, password, rememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "User not found.");
        }
    }
    return View();
}*/


       

        /*[HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { Name = model.Name, Email = model.Email,Password = model.Password, Phone="123123",Address = "blah blah" };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
*/


[HttpPost]
public async Task<IActionResult> Login(LoginViewModel model)
{
    if (ModelState.IsValid)
    {
        // Use SignInManager to authenticate the user
        var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
        
        if (result.Succeeded)
        {
            // Redirect to the main page after successful login
            return RedirectToAction("Index", "Home");
        }
        else
        {
            // If the login failed, add an error to ModelState
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }
    }
    
    // If we reach this point, it means there was an error and the user will be returned to the login page
    return View(model);
}

        /*  [HttpPost]
          public async Task<IActionResult> Register(User model)
          {
              model.Phone = "9999999999";
              model.Address = "Sheridan";
              List<Donation> donations = new List<Donation>();
              model.Donations = donations;
              if (ModelState.IsValid)
              {
                  // Hash the password
                  var hasher = new PasswordHasher<User>();
                  var passwordHash = hasher.HashPassword(null, model.Password);

                  // Create the user instance
                  var user = new User
                  {
                      Name = model.Name,
                      Email = model.Email,
                      Password = passwordHash,
                      Phone = model.Phone,
                      Address = model.Address
                  };

                  _donationDbContext.Users.Add(user);
                  await _donationDbContext.SaveChangesAsync();

                  // Handle login or redirect
                  return RedirectToAction("Index", "Home");
              }

              // Return view with validation errors
              return View(model);
          }
        */
        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            // Custom validation
            if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
            {
                ModelState.AddModelError("", "Name, Email, and Password are required fields.");
                return View(model);
            }

            // Set defaults for optional fields
            model.Phone ??= "9999999999";
            model.Address ??= "Sheridan";
            model.Donations ??= new List<Donation>();

            // Hash the password
            var hasher = new PasswordHasher<User>();
            var passwordHash = hasher.HashPassword(null, model.Password);

            // Create the user instance
            var user = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = passwordHash,
                Phone = model.Phone,
                Address = model.Address
            };

          //  _donationDbContext.Users.Add(user);
           // await _donationDbContext.SaveChangesAsync();

            // Redirect on success
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}