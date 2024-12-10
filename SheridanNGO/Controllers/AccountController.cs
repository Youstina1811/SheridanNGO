using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SheridanNGO.Models;
using Microsoft.AspNetCore.Identity;
//using AspNetCoreGeneratedDocument;

namespace SheridanNGO.Controllers
{

    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
        private readonly DonationDbContext _context;
        public AccountController(DonationDbContext context)
        {
            _context = context;
        }

        //User admin = new User("admin","admin@sheridan.com","admin","23423423","Sheridan College");
        User admin = new User("admin@sheridan.com", "23423423", "admin", "admin", "905 905 9055", "Sheridan College");

        
        public IActionResult Admin()
        {
            var donors = _context.Users.Where(u => u.UserType == "donor").ToList();
            var ngos = _context.Users.Where(u => u.UserType == "ngo").ToList();
            var admins = _context.Users.Where(u => u.UserType == "admin").ToList();

            var model = new
            {
                Donors = donors,
                NGOs = ngos,
                Admins = admins

            };

            return View(model);
        }
        


     /*   [HttpPost]
        public async Task<IActionResult> Login(User model)
        {
            _context.Users.Add(admin);

            // Save the changes to the database
            await _context.SaveChangesAsync();

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
*/

        [HttpPost]
        public async Task<IActionResult> Login(User model)
        {
            // Check for required fields manually
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
            {
                ModelState.AddModelError(string.Empty, "Email and Password are required.");
                return View(model);
            }

            // Find the user in the database
            var user =  _context.Users.FirstOrDefault(u => u.Email == model.Email);

            if (user != null)
            {
                if (user.Email == "admin@sheridan.com" && user.Password == "23423423")
                {
                    return RedirectToAction("Admin", "Account");
                }
                // Verify the password (hashed)
                var hasher = new PasswordHasher<User>();
                var result = hasher.VerifyHashedPassword(user, user.Password, model.Password);

                if (result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success)
                {
                    // Check user type and redirect accordingly
                    if (user.UserType == "admin")
                    {
                        return RedirectToAction("Admin", "Account");
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid email or password.");
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

        /*
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
      */

        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {

            foreach (var entry in ModelState)
            {
                var fieldName = entry.Key;
                var errors = entry.Value.Errors;

                foreach (var error in errors)
                {
                    Console.WriteLine($"Field: {fieldName}, Error: {error.ErrorMessage}");
                }
            }
            if (ModelState.IsValid)
            {
                // Hash the password
                var hasher = new PasswordHasher<User>();
                var passwordHash = hasher.HashPassword(null, model.Password);

                // Create the user instance with all required fields
                var user = new User(
                    model.Email,
                    passwordHash,
                    model.Name,
                    "donor",  // Default UserType, e.g., 'donor'
                    model.Phone ?? "999 999 9999",  // Default phone if empty
                    model.Address ?? "Somewhere"     // Default address if empty
                );

                // Add the user to the context
                _context.Users.Add(user);

                // Save the changes to the database
                await _context.SaveChangesAsync();

                // Handle login or redirect
                return RedirectToAction("Index", "Home");
            }

            // Return the view with validation errors if ModelState is invalid
            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult GetDonors()
        {
            var donors = _context.Users
                .Where(u => u.UserType == "donor")
                .Select(u => new { u.UserID, u.Name, u.Email, u.Phone,u.UserType,u.Address })
                .ToList();

            return Json(donors);
        }


        [HttpGet]
        public IActionResult GetNGOs()
        {
            var ngos = _context.Users
                .Where(u => u.UserType == "NGO")
                .Select(u => new { u.UserID, u.Name, u.Email, u.Phone, u.UserType, u.Address })
                .ToList();

            return Json(ngos);
        }


        [HttpPost]
        public IActionResult MakeAdmin(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == id);

            if (user != null && user.UserType == "donor")
            {
                user.UserType = "admin";
                _context.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                // Retrieve user by ID
                var user = _context.Users.FirstOrDefault(u => u.UserID == id);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Remove user
                _context.Users.Remove(user);
                _context.SaveChanges();

                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult DemoteAdmin(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == id);

            if (user == null)
                return NotFound("User not found.");

            if (user.Email == "admin")
                return BadRequest("The main admin cannot be demoted.");

            if (user.UserType == "admin")
            {
                user.UserType = "donor"; // Change admin to donor
                _context.SaveChanges();
                return Ok("Admin demoted to Donor successfully.");
            }

            return BadRequest("User is not an admin.");
        }

    }
}

