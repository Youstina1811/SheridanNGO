using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SheridanNGO.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            var volunteers = _context.Volunteers.OrderByDescending(v => v.Days).ToList();

            var model = new
            {
                Donors = donors,
                NGOs = ngos,
                Admins = admins,
                Volunteers = volunteers
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            // Only add the test campaign once (for testing or seeding, not every request)
           /* if (!_context.Campaigns.Any())  // This ensures we don't add duplicates every time
            {
                Campaign test = new Campaign
                {
                    Title = "Hello",
                    NGOID = 1,
                    CurrentAmount = 100,
                    Description = "Blah Blah",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(1),
                    CauseCategory = "Education"
                };
                _context.Campaigns.Add(test);
                await _context.SaveChangesAsync(); // Don't forget to save the changes to persist the campaign
            }*/

            // Get the email from the logged-in user
            var email = User.Identity.Name;

            if (string.IsNullOrEmpty(email)) // If the user is not logged in
            {
                return RedirectToAction("Login", "Account");
            }

            // Retrieve user from the database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null) // If the user doesn't exist in the database
            {
                return RedirectToAction("Login", "Account");
            }

            // Fetch campaigns from the database (async)
            var campaigns = await _context.Campaigns.ToListAsync() ?? new List<Campaign>();

            // Pass user data and campaigns to the view
            ViewBag.User = user;
            return View(campaigns);
        }
        private async Task SignInUser(User user)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Email),
        new Claim("UserId", user.UserID.ToString()),
        new Claim("UserType", user.UserType)
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
            {
                IsPersistent = true, // Keep the user logged in
                ExpiresUtc = DateTime.UtcNow.AddHours(2) // Set expiration time
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          new ClaimsPrincipal(claimsIdentity),
                                          authProperties);
        }

        [HttpGet]
        public async Task<IActionResult> ManageProfile()
        {
            // Get the currently logged-in user's email
            var email = User.Identity.Name;

            // Find the user from the database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(user); // Pass the user data to the view for editing
        }

        [HttpPost]
        public async Task<IActionResult> ManageProfile(User model)
        {
            if (ModelState.IsValid)
            {
                // Find the user in the database
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == model.UserID);

                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                // Update user properties
                user.Name = model.Name;
                user.Email = model.Email;
                user.Phone = model.Phone;
                user.Address = model.Address;

                // Save changes to the database
                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Dashboard", "Account");
            }

            return View(model);
        }

      /*  [HttpGet]
        public async Task<IActionResult> Receipts()
        {
            // Get the currently logged-in user's email
            var email = User.Identity.Name;

            // Find the user from the database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Get the donations made by the user
            var donations = await _context.Donations
                .Where(d => d.DonationID == user.UserID)
                .ToListAsync();

            // Get receipts for the donations
            var receipts = await _context.Receipts
                .Where(r => donations.Select(d => d.DonationID).Contains(r.DonationID))
                .ToListAsync();

            return View(receipts); // Pass the receipts to the view
        }
*/
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            // Get the campaign by its ID
            var campaign = await _context.Campaigns
                .Include(c => c.NGO) // Include NGO details if needed
                .FirstOrDefaultAsync(c => c.CampaignID == id);

            if (campaign == null)
            {
                return RedirectToAction("Dashboard", "Account");
            }

            return View(campaign); // Pass the campaign details to the view
        }

        [HttpGet]
        public async Task<IActionResult> Receipts()
        {
            var email = User.Identity.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var receipts = await _context.Receipts
                                        .Where(r => r.Donation.DonationID == user.UserID)
                                        .ToListAsync();

            return View(receipts);
        }

        // Logout method
        [HttpPost]
        public IActionResult Logout()
        {
          //  _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // Explore Campaign method
        public IActionResult ExploreCampaign(int id)
        {
            var campaign = _context.Campaigns.Include(c => c.NGO).FirstOrDefault(c => c.CampaignID == id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign); // Display the campaign details view
        }


        [HttpGet]
        public async Task<IActionResult> DownloadReceipt(int id)
        {
            var receipt = await _context.Receipts
                                         .Include(r => r.Donation)
                                         .FirstOrDefaultAsync(r => r.ReceiptID == id);

            if (receipt == null)
            {
                return NotFound();
            }

            // Generate and return a downloadable file (e.g., PDF, or CSV)
            // This is a placeholder and can be adapted based on the desired file format.
            var fileContent = $"Receipt ID: {receipt.ReceiptID}\nAmount: {receipt.AmountDonated:C}\nDate: {receipt.ReceiptDate.ToShortDateString()}";
            var fileName = $"Receipt_{receipt.ReceiptID}.txt";
            return File(System.Text.Encoding.UTF8.GetBytes(fileContent), "text/plain", fileName);
        }


        [HttpPost]
        public async Task<IActionResult> Login(User model)
        {
            // Manual validation
            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
            {
                ModelState.AddModelError(string.Empty, "Email and Password are required.");
                return View(model);
            }

            // Fetch user from database
            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);

            if (user != null)
            {
                // Check if the user is the hardcoded admin
                if (user.Email == "admin@sheridan.com" && user.Password == "23423423")
                {
                    // Redirect to Admin dashboard
                    await SignInUser(user);
                    return RedirectToAction("Admin", "Account");
                }

                // Verify hashed password
                var hasher = new PasswordHasher<User>();
                var result = hasher.VerifyHashedPassword(user, user.Password, model.Password);

                if (result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success)
                {
                    // Sign in the user
                    await SignInUser(user);

                    // Redirect based on UserType
                    return user.UserType.ToLower() == "admin"
                        ? RedirectToAction("Admin", "Account")
                        : RedirectToAction("Dashboard", "Account");
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

