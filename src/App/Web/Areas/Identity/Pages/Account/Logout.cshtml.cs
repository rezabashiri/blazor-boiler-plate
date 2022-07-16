using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Authentication.Interfaces;

namespace Web.Areas.Identity.Pages.Account
{
    [Authorize]
    public class LogoutModel : PageModel
    {
        private readonly ILogger<LogoutModel> _logger;
        private readonly IAuthencticationHelper _authencticationHelper;


        public LogoutModel(ILogger<LogoutModel> logger, IAuthencticationHelper authencticationHelper)
        {
            _logger = logger;
            _authencticationHelper = authencticationHelper;
        }

        public async Task<IActionResult> OnGet()
        {
            _logger.LogInformation("User logged out.");

            await _authencticationHelper.SignOutAsync();

            return SignOut(new AuthenticationProperties() { RedirectUri = "/" }, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> OnPost()
        {
            return await OnGet();
        }

    }
}