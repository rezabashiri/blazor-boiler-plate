using App.Share.Startup;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeders;
public class AppSeeder : IDatabaseSeeder
{
    private readonly AppDbContext _dbContext;


    public AppSeeder(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        _dbContext.Database.Migrate();
    }

    public async Task Seed(int count)
    {
       
    }
}

