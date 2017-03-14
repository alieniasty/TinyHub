using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TinyHub.Models;
using TinyHub.ViewModels;

namespace TinyHub.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<TinyHubUser> _signInManager;
        private UserManager<TinyHubUser> _userManager;

        public AuthController(SignInManager<TinyHubUser> signInManager, UserManager<TinyHubUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var singInResult = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);
                if (singInResult.Succeeded)
                {
                    return RedirectToAction("MyProjects", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "We're sorry, username or password is incorrect");
                }
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost] 
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (await _userManager.FindByEmailAsync(vm.Email) == null)
                {
                    var user = new TinyHubUser()
                    {
                        UserName = vm.Username,
                        Email = vm.Email
                    };

                    var result = await _userManager.CreateAsync(user, vm.Password);

                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("", "We're sorry, username, password or email is incorrect");
                    }
                }
            }

            return RedirectToAction("Login", "Auth");
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
