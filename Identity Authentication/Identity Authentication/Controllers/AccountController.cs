using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity_Authentication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        

        [HttpGet]
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Secrect()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Test()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _userManager.FindByNameAsync(username).Result;
            if(user != null)
            {
                var res = _signInManager.PasswordSignInAsync(user, password, false, false).Result;
                if (res.Succeeded)
                {
                    return RedirectToAction("Test");
                }

            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            // you have have to provide email. Otherwise I found signin is failed.
            var user = new IdentityUser
            {
                UserName = username,
                Email = ""
            };

            var res =  _userManager.CreateAsync(user, password).Result;
            
            return View();
        }


        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
