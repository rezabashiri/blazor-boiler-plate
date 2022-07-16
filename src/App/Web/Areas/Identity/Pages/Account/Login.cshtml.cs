using System.ComponentModel.DataAnnotations;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Authentication.Interfaces;

namespace Web.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        [Required]
        [EmailAddress]
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        [Required]
        public string Password { get; set; }


        private readonly IAuthencticationHelper _authencticationHelper;
        private readonly PasswordTokenRequest _passwordTokenRequest;
        private readonly IConfiguration _configuration;

        public LoginModel(IAuthencticationHelper authencticationHelper, PasswordTokenRequest passwordTokenRequest, IConfiguration configuration)
        {
            _authencticationHelper = authencticationHelper;
            _passwordTokenRequest = passwordTokenRequest;
            _configuration = configuration;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl)
        {
            _passwordTokenRequest.UserName = Email;
            _passwordTokenRequest.Password = Password;

            var token = await _authencticationHelper.GetTokenAsync(_passwordTokenRequest);

            if (token.IsError)
                return Challenge(CookieAuthenticationDefaults.AuthenticationScheme);

            var claimsPrincipal = _authencticationHelper.ParseToken(token.AccessToken);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return Redirect(!string.IsNullOrEmpty(returnUrl) ?  returnUrl : "/");
        }
    }
}
