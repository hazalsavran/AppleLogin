using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SignInWithApple.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
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
    }
}
