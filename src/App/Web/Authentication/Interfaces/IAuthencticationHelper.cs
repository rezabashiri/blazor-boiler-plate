using System.Security.Claims;
using IdentityModel.Client;

namespace Web.Authentication.Interfaces;

public interface IAuthencticationHelper
{
    Task<TokenResponse> GetTokenAsync(PasswordTokenRequest request);
    Task<bool> SignOutAsync();
    ClaimsPrincipal ParseToken(string token);

}