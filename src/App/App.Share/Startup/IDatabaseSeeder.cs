namespace App.Share.Startup
{
    public interface IDatabaseSeeder 

    {
    public Task Seed (int count);
    }
}
