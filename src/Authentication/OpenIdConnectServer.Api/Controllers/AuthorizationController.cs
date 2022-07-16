using System.Security.Claims;
using Identity.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IOpenIddictApplicationManager _applicationManager;
        private readonly IOpenIddictAuthorizationManager _authorizationManager;
        private readonly IOpenIddictScopeManager _scopeManager;
        private readonly IOpenIddictTokenManager _tokenManager;

        public AuthorizationController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IOpenIddictApplicationManager applicationManager,
            IOpenIddictAuthorizationManager authorizationManager,
            IOpenIddictScopeManager scopeManager,
            IOpenIddictTokenManager tokenManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationManager = applicationManager;
            _authorizationManager = authorizationManager;
            _scopeManager = scopeManager;
            _tokenManager = tokenManager;
        }


        [HttpPost("~/token"), Produces("application/json")]
        public async Task<IActionResult> Exchange()
        {
            var request = HttpContext.GetOpenIddictServerRequest() ??
                throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

            if (request.IsPasswordGrantType()) {
                var user = await _userManager.FindByNameAsync(request.Username);

                if (user is null)
                    return CreateForbiddenResult(OpenIddictConstants.Errors.InvalidClient, "The user cant be find.");

                if (await _applicationManager.FindByClientIdAsync(request.ClientId) is null)
                    return CreateForbiddenResult(OpenIddictConstants.Errors.InvalidClient, "The application cant be find.");

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);

                if (!result.Succeeded)
                    return CreateForbiddenResult(OpenIddictConstants.Errors.InvalidClient, "The username / password couple is invalid.");


                return await SignIn(user);

            }
            else if (request.IsRefreshTokenGrantType()) {
                // Retrieve the claims principal stored in the authorization code/device code/refresh token.
                var principal = (await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)).Principal;

                // Retrieve the user profile corresponding to the authorization code/refresh token.
                // Note: if you want to automatically invalidate the authorization code/refresh token
                // when the user password/roles change, use the following line instead:
                // var user = _signInManager.ValidateSecurityStampAsync(info.Principal);
                var user = await _userManager.GetUserAsync(principal);
                if (user == null) {
                    return CreateForbiddenResult(OpenIddictConstants.Errors.InvalidGrant, "The token is no longer valid.");
                }

                // Ensure the user is still allowed to sign in.
                if (!await _signInManager.CanSignInAsync(user)) {
                    return CreateForbiddenResult(OpenIddictConstants.Errors.InvalidGrant, "The user is no longer allowed to sign in.");
                }

                SetDistinations(principal);

                // Returning a SignInResult will ask OpenIddict to issue the appropriate access/identity tokens.
                return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            throw new InvalidOperationException("The specified grant type is not supported.");
        }
        private ForbidResult CreateForbiddenResult(string error, string errorType)
        {
            var properties = new AuthenticationProperties(new Dictionary<string, string>
            {
                [OpenIddictServerAspNetCoreConstants.Properties.Error] = errorType,
                [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] = error
            });
            return Forbid(properties, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }


        [HttpPost("~/logout")]
        [HttpGet("~/logout")]
        [Authorize]
        public async Task<IActionResult> LogoutPost()
        {
            var request = HttpContext.GetOpenIddictServerRequest();

            // Ask ASP.NET Core Identity to delete the local and external cookies created
            // when the user agent is redirected from the external identity provider
            // after a successful authentication flow (e.g Google or Facebook).
            await _signInManager.SignOutAsync();
            await foreach (var token in _tokenManager.FindBySubjectAsync(User.Claims
                               .FirstOrDefault(claim => claim.Type == "sub").Value)) {
                await _tokenManager.TryRevokeAsync(token);
            }

            // Returning a SignOutResult will ask OpenIddict to redirect the user agent
            // to the post_logout_redirect_uri specified by the client application or to
            // the RedirectUri specified in the authentication properties if none was set.
            return Accepted();
        }

        private async Task<IActionResult> SignIn(User user)
        {
            // Create a new ClaimsPrincipal containing the claims that
            // will be used to create an id_token, a token or a code.
            var principal = await _signInManager.CreateUserPrincipalAsync(user);
            SetDistinations(principal);


            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }
        private static void SetDistinations(ClaimsPrincipal principal)
        {
            foreach (var claim in principal.Claims)
                claim.SetDestinations(claim.Type switch
                {
                    // If the "profile" scope was granted, allow the "name" claim to be
                    // added to the access and identity tokens derived from the principal.
                    OpenIddictConstants.Claims.Subject or
                        OpenIddictConstants.Claims.Name or
                        OpenIddictConstants.Claims.Gender or
                        OpenIddictConstants.Claims.GivenName or
                        OpenIddictConstants.Claims.MiddleName or
                        OpenIddictConstants.Claims.FamilyName or
                        OpenIddictConstants.Claims.Nickname or
                        OpenIddictConstants.Claims.PreferredUsername or
                        OpenIddictConstants.Claims.Birthdate or
                        OpenIddictConstants.Claims.Profile or
                        OpenIddictConstants.Claims.Picture or
                        OpenIddictConstants.Claims.Website or
                        OpenIddictConstants.Claims.Locale or
                        OpenIddictConstants.Claims.Zoneinfo or
                        OpenIddictConstants.Claims.Name when principal.HasScope(OpenIddictConstants.Scopes.Profile)
                        => new[]
                        {
                            OpenIddictConstants.Destinations.AccessToken,
                            OpenIddictConstants.Destinations.IdentityToken
                        },

                    OpenIddictConstants.Claims.Email when principal.HasScope(OpenIddictConstants.Scopes.Email) =>
                        new[]
                        {
                            OpenIddictConstants.Destinations.AccessToken,
                            OpenIddictConstants.Destinations.IdentityToken
                        },

                    OpenIddictConstants.Claims.PhoneNumber when principal.HasScope(OpenIddictConstants.Scopes.Phone)
                        => new[]
                        {
                            OpenIddictConstants.Destinations.IdentityToken
                        },

                    // Never add the "secret_value" claim to access or identity tokens.
                    // In this case, it will only be added to authorization codes,
                    // refresh tokens and user/device codes, that are always encrypted.
                    "secret_value" => Array.Empty<string>(),

                    // Otherwise, add the claim to the access tokens only.
                    _ => new[]
                    {
                        OpenIddictConstants.Destinations.AccessToken
                    }
                });

            principal.SetScopes(new[]
            {
                OpenIddictConstants.Scopes.OpenId,
                OpenIddictConstants.Scopes.Email,
                OpenIddictConstants.Scopes.Profile,
                OpenIddictConstants.Scopes.Roles,
                OpenIddictConstants.Scopes.OfflineAccess
            });
        }

    }
}
