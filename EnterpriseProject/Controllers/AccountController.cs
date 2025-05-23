using System.Data;
using System.Diagnostics;
using EnterpriseProject.Entities;
using EnterpriseProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseProject.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;

        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login(string returnURL = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnURL };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password,
                            isPersistent: model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Get the user
                    var user = await _userManager.FindByNameAsync(model.Username);

                    // Get the user's roles
                    var roles = await _userManager.GetRolesAsync(user);

                    // Check the roles and redirect accordingly
                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("List", "Admin");
                    }
                    else if (roles.Contains("Practitioner"))
                    {
                        return RedirectToAction("List", "Practitioner");
                    }
                    else if (roles.Contains("Billing"))
                    {
                        return RedirectToAction("List", "Billing");
                    }
                    else if (roles.Contains("Client"))
                    {
                        return RedirectToAction("List", "Client");
                    }
                    else
                    {
                        // Default fallback (in case no role is matched)
                        return RedirectToAction("Index", "Home");
                    }
                }
            }

            ModelState.AddModelError("", "Invalid username/password.");
            return View(model);
        }
    }
}
