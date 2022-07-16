using Identity.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("yes");
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string email, string password)
        {

            var user = await _userManager.CreateAsync(new User()
            {
                UserName = email,
                Email = email,
            }, password);

            return Created("", user);
        }
    }
}
