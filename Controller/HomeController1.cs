using Microsoft.AspNetCore.Mvc;

namespace SignInWithApple.Controller
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
