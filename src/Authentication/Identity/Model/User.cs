using Microsoft.AspNetCore.Identity;

namespace Identity.Model
{
    public class User: IdentityUser
    {
        public string Name { get; set; }
        public string FamilyName { get; set; }
    }
}