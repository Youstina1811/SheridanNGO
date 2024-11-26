using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SheridanNGO.Models;
using Microsoft.AspNetCore.Identity;

namespace SheridanNGO.Controllers
{

    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;

        public AccountController(Microsoft.AspNetCore.Identity.UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }/*

        [HttpPost]
        public async Task<IActionResult> Login(User model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }*/

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email };
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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}