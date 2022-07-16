using System.Security.Claims;
using Identity.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OpenIddict.Abstractions;

namespace Identity.Managers
{
    public class CustomSignInManager:SignInManager<User>
    {
        public CustomSignInManager(UserManager<User> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<User> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<User>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<User> userConfirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, userConfirmation) 
        {
        }


        override public async Task<ClaimsPrincipal> CreateUserPrincipalAsync(User user)
        {
            var principal = await base.CreateUserPrincipalAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;
            identity.AddClaim(new Claim(OpenIddictConstants.Claims.Name, user.Name));
            identity.AddClaim(new Claim(OpenIddictConstants.Claims.FamilyName, user.FamilyName));

            return principal;
        }
    }
}