using App.Share.Extensions;

namespace App.Share.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        // TODO: Haven't finalised if these need to be injectable/customisable - making constants as a first step
        public const string StockDateFormat = "d{0} MMM yyyy";

        public const string StockDayMonthFormat = "d{0} MMM";

        public const string PrintoutDateFormat = "dddd d MMMM yyyy";

        public const string StockTimeZone = "Europe/London";
        public const string DateTimeDisplayFormat = "dd/MM/yyyy hh:mm";
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;

        /// <summary>
        /// Utility date method that consumes two dates and gets out a 'range' string.
        /// </summary>
        /// <param name="startDate">The start date (times are stripped out for comparison reasons).</param>
        /// <param name="endDate">The end date (times are stripped out for comparison reasons).</param>
        /// <returns>A range string based on the supplied dates (separated by a '-').</returns>
        public string GetDateRangeString(DateTime startDate, DateTime endDate)
        {
            DateTime simplifiedStartDate = GetSimplifiedDateFromBaseDate(startDate),
                simplifiedEndDate = GetSimplifiedDateFromBaseDate(endDate);

            // Default logic is to simply return a full version of the range (best to display something - does match the final else if technically but separating in case logic diverges later)
            string convertedDateRangeString = $"{ simplifiedStartDate.ToStringWithSuffix(StockDateFormat) } - { simplifiedEndDate.ToStringWithSuffix(StockDateFormat) }";

            if (simplifiedStartDate == simplifiedEndDate) {
                convertedDateRangeString = $"{ simplifiedStartDate.ToStringWithSuffix(StockDateFormat) }";
            }
            else if (simplifiedStartDate.Day != simplifiedEndDate.Day
                && simplifiedStartDate.Month == simplifiedEndDate.Month
                && simplifiedStartDate.Year == simplifiedEndDate.Year) {
                convertedDateRangeString = $"{ simplifiedStartDate.Day }{ simplifiedStartDate.GetSuffixForDateDay() } - { simplifiedEndDate.ToStringWithSuffix(StockDateFormat) }";
            }
            else if (simplifiedStartDate.Month != simplifiedEndDate.Month
                && simplifiedStartDate.Year == simplifiedEndDate.Year) {
                convertedDateRangeString = $"{ simplifiedStartDate.ToStringWithSuffix(StockDayMonthFormat) } - { simplifiedEndDate.ToStringWithSuffix(StockDateFormat) }";
            }
            else if (simplifiedStartDate.Year != simplifiedEndDate.Year) {
                convertedDateRangeString = $"{ simplifiedStartDate.ToStringWithSuffix(StockDateFormat) } - { simplifiedEndDate.ToStringWithSuffix(StockDateFormat) }";
            }

            return convertedDateRangeString;
        }

        /// <summary>
        /// Helper method that strips time components from a supplied DateTime for easier comparisons.
        /// </summary>
        /// <param name="baseDate">The date to generate a new date from (with just the day/month/year components copied over).</param>
        /// <returns>A date containing just the day/month/year from the supplied date.</returns>
        private static DateTime GetSimplifiedDateFromBaseDate(DateTime baseDate) => new DateTime(baseDate.Year, baseDate.Month, baseDate.Day);
    }
}
