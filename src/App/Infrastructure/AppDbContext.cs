using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext, IAccentDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
     }


    override protected void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties(typeof(Enum))
            .HaveConversion<string>();
    }
}

