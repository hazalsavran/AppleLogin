using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace SignInWithApple.Controller
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> TestChallenge()
        {
            var result = await HttpContext.AuthenticateAsync();

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            return Challenge("apple");
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync("cookie");
            return RedirectToAction("Index");
        }
    }
}
