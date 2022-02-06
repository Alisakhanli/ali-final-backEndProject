using AliProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AliProject.Controllers
{
    public class UserController : Controller
    {
        public UserManager<User> userManager { get; set; }
        public SignInManager<User> signInManager;

        public UserController(UserManager<User> _userManager, SignInManager<User> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public IActionResult Account()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Account");
            }

            var user = await userManager.FindByNameAsync(model.LoginModel.Username);
            if(user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var signInResult = await signInManager.PasswordSignInAsync(user, model.LoginModel.Password, true, true);

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Credentials!");
                return View();
            }

            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Account");
            }

            User user = new User
            {
                FullName = "",
                UserName = model.RegisterModel.Username,
                Email = model.RegisterModel.Email
            };

            var identityResult = await userManager.CreateAsync(user, model.RegisterModel.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View("Account", model);
            }

            await signInManager.SignInAsync(user, true);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
