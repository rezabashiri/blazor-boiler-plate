using Identity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity;
public class AuthenticationDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options):base(options)
    {
        
    }
}

