namespace App.Share.Providers
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }

        DateTime UtcNow { get; }

        string GetDateRangeString(DateTime startDate, DateTime endDate);
    }
}
