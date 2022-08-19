using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Claim_Identity_Priciple_Basic.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
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
        public async Task< IActionResult> Login()
        {
            var userClaim = new List<Claim> {
                new Claim(ClaimTypes.Name, "kamrul"),
                new Claim(ClaimTypes.Email, "test@gmail.com"),
                new Claim(ClaimTypes.Locality, "bangali")
            };

            var licenseClaim = new List<Claim> {
                new Claim(ClaimTypes.Name, "Kamrul"),
                new Claim(ClaimTypes.NameIdentifier, "12345")
            };

            var userIdentity = new ClaimsIdentity(userClaim, "User Identity");
            var licenceIdentity = new ClaimsIdentity(licenseClaim, "Licence Identity");

            var claimPriciple = new ClaimsPrincipal(new[] { userIdentity, licenceIdentity });

            await HttpContext.SignInAsync(claimPriciple);
            return RedirectToAction("index");
        }

    }
}
