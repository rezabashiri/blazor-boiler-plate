using Identity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;

namespace Identity.Seeders;
public class IdentitySeeder : IDatabaseSeeder
{
    private readonly AuthenticationDbContext _dbContext;
    private readonly IOpenIddictApplicationManager _applicationManager;
    private readonly IOpenIddictScopeManager _scopeManager;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentitySeeder(AuthenticationDbContext dbContext, IOpenIddictApplicationManager applicationManager, IOpenIddictScopeManager scopeManager, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _dbContext = dbContext;
        _applicationManager = applicationManager;
        _scopeManager = scopeManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext.Database.EnsureCreated();
    }

    public async Task Seed(int count)
    {
        // In multiple app senarios have to configure all apps
        if (await _applicationManager.FindByClientIdAsync("BlazorBoilerPlate") is null)
            await _applicationManager.CreateAsync(new OpenIddictApplicationDescriptor()
            {
                ClientId = "BlazorBoilerPlate",
                DisplayName = "Accent company",
                ClientSecret = "e4281a1da09f7332e6300cc24a593749",
                ConsentType = OpenIddictConstants.ConsentTypes.Implicit,
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Token,
                    OpenIddictConstants.Permissions.Endpoints.Logout,

                    OpenIddictConstants.Permissions.GrantTypes.Password,
                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken,

                    OpenIddictConstants.Permissions.ResponseTypes.Code,

                    OpenIddictConstants.Permissions.Scopes.Email,
                    OpenIddictConstants.Permissions.Scopes.Profile,
                    OpenIddictConstants.Permissions.Scopes.Roles,
                 }
             });
        if (await _roleManager.RoleExistsAsync("admin") is false) {
            await _roleManager.CreateAsync(new IdentityRole("admin"));
        }

        var adminUsers = new List<(User user, string password)>() {
            (new User() { Email = "rzbashiri@gmail.com", UserName = "rzbashiri@gmail.com", Name = "Reza", FamilyName = "Bashiri" },
                "Reza123$%^"),
        };
 
        foreach (var userPassword in adminUsers) {
            if (await _userManager.Users.AnyAsync(user => user.UserName == userPassword.user.UserName) is false) {

                if (!(await _userManager.CreateAsync(
                        userPassword.user,
                        userPassword.password)).Succeeded)
                    throw new ApplicationException("admin user wasn't added to database");
            }
            var adminUser = await _userManager.FindByNameAsync(userPassword.user.UserName);
            if (await _userManager.IsInRoleAsync(adminUser, "admin") is false) {
                await _userManager.AddToRoleAsync(adminUser, "admin");
            }
        }
    }
}