using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using App.Share.Consts;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using OpenIddict.Abstractions;
using Web.Authentication.Interfaces;

namespace Web.Authentication.Helpers
{
    public class AuthencticationHelper : IAuthencticationHelper
    {
        public static string GetAuthority(IWebHostEnvironment env, IConfiguration configuration)
        {
            return (env.IsProduction() ? "https://" : "http://") + configuration.GetSection("OpenIDConnectSettings:Authority").Value;
        }

        public static string? GetToken(HttpContext httpContext)
        {
            var token = httpContext.Request.Cookies.TryGetValue(AuthenticationConst.CoockieName, out var value);
            return value;
        }

        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _client;

        public AuthencticationHelper(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
            _client = _clientFactory.CreateClient("authenticationServer");
        }

        public async Task<TokenResponse> GetTokenAsync(PasswordTokenRequest request)
        {
            var configuration = await DiscoveryOpeniddictDocument();

            request.Address = configuration.TokenEndpoint;
            var response = await _client.RequestPasswordTokenAsync(request);
            return response;
        }

        public ClaimsPrincipal ParseToken(string token)
        {
            var jwt =new JwtSecurityTokenHandler().ReadJwtToken(token);
            return new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims, CookieAuthenticationDefaults.AuthenticationScheme, OpenIddictConstants.Claims.Name, OpenIddictConstants.Claims.Role));
        }
        public async Task<bool> SignOutAsync()
        {
            var configuration = await DiscoveryOpeniddictDocument();
            var httpContent = new StringContent("", Encoding.UTF8, "application/x-www-form-urlencoded");
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", GetToken(_httpContextAccessor.HttpContext));

            var response = await _client.PostAsync(configuration.EndSessionEndpoint, httpContent);
            if (response.IsSuccessStatusCode) {
                _httpContextAccessor.HttpContext.Request.Headers.Authorization = string.Empty;
                _httpContextAccessor.HttpContext.Response.Cookies.Delete(AuthenticationConst.CoockieName);
            }

            return response.IsSuccessStatusCode;
        }
        private async Task<DiscoveryDocumentResponse> DiscoveryOpeniddictDocument()
        {
            // Retrieve the OpenIddict server configuration document containing the endpoint URLs.
            var configuration = await _client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest()
            {
                Policy =
                {
                    RequireHttps = false
                }
            });
            if (configuration.IsError) {
                throw new Exception($"An error occurred while retrieving the configuration document: {configuration.Error}");
            }

            return configuration;
        }
    }
}
