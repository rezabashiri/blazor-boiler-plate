namespace App.Share.Extensions
{
    /// <summary>
    /// Houses relevant FaceToFace (maybe useful across wider projects, going forward)
    /// DateTime-based extension methods.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Provides a DateTime string, based on the format supplied with an
        /// appropriate suffix for the day in scope and prefix for the day of the week.
        /// </summary>
        /// <param name="baseDateTime">The base DateTime to operate against.</param>
        /// <param name="format">The format (including replacementIdentifier) for the output date.</param>
        /// <param name="replacementIdentifier">The placeholder to be replaced with the suffix for the day.</param>
        /// <returns>A string representing the current DateTime based on the supplied format with the correct suffix, with the day of the week as a prefix.</returns>
        public static string ToStringWithSuffixPlusDay(this DateTime baseDateTime, string format, string replacementIdentifier = "{0}") =>
            $"{ baseDateTime.DayOfWeek } { baseDateTime.ToStringWithSuffix(format, replacementIdentifier) }";

        /// <summary>
        /// Provides a DateTime string, based on the format supplied with an
        /// appropriate suffix for the day in scope.
        /// </summary>
        /// <param name="baseDateTime">The base DateTime to operate against.</param>
        /// <param name="format">The format (including replacementIdentifier) for the output date.</param>
        /// <param name="replacementIdentifier">The placeholder to be replaced with the suffix for the day.</param>
        /// <returns>A string representing the current DateTime based on the supplied format with the correct suffix.</returns>
        public static string ToStringWithSuffix(this DateTime baseDateTime, string format, string replacementIdentifier = "{0}") =>
            baseDateTime.ToString(format).Replace(replacementIdentifier, GetSuffixForDay(baseDateTime.Day));

        /// <summary>
        /// Provides just the suffix string for the 'day' associated with the provided DateTime.
        /// </summary>
        /// <param name="baseDateTime">The base DateTime to operate against.</param>
        /// <returns>Just the string suffix for the 'day' (based on the DateTime provided) - e.g. 'st', 'nd', 'rd', 'th'.</returns>
        public static string GetSuffixForDateDay(this DateTime baseDateTime) => GetSuffixForDay(baseDateTime.Day);

        /// <summary>
        /// Provides a Unix Timestamp in seconds from the provided DateTime object.
        /// </summary>
        /// <param name="dateTime">The starting DateTime to operate against.</param>
        /// <returns>The long (representing seconds) for the particular instant in time (Unix).</returns>
        public static long DateTimeToUnixTimestampInSeconds(this DateTime dateTime) => new DateTimeOffset(dateTime).ToUnixTimeSeconds();

        /// <summary>
        /// Provides a UTC DateTime from the specified UNIX TimeStamp.
        /// </summary>
        /// <param name="unixTimeStamp">The UNIX TimeStamp to generate a UTC DateTime from.</param>
        /// <returns>A UTC DateTime based on the UNIX TimeStamp provided.</returns>
        public static DateTime UnixTimeStampToUtcDateTime(this int unixTimeStamp) => DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).UtcDateTime;

        /// <summary>
        /// Provides a UTC DateTime from the specified UNIX TimeStamp.
        /// </summary>
        /// <param name="unixTimeStamp">The UNIX TimeStamp to generate a UTC DateTime from.</param>
        /// <returns>A UTC DateTime based on the UNIX TimeStamp provided.</returns>
        public static DateTime UnixTimeStampToUtcDateTime(this long unixTimeStamp) => DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).UtcDateTime;

        /// <summary>
        /// Provides a UTC DateTime from the specified UNIX TimeStamp.
        /// </summary>
        /// <param name="unixTimeStamp">The UNIX TimeStamp to generate a UTC DateTime from.</param>
        /// <returns>A UTC DateTime based on the UNIX TimeStamp provided.</returns>
        public static DateTime UnixTimeStampToDateTime(this long unixTimeStamp) => DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp).DateTime;



        /// <summary>
        /// Returns an appropriate suffix based on the day (from a date object) supplied.
        /// Idea taken from: https://gist.github.com/woodss/006f28f01dcf371c2bd1.
        /// </summary>
        /// <param name="day">The day (from a datetime) in question. Will also work outside the scope of DateTime (e.g. for 62 you'd get 'nd' (62nd))</param>
        /// <returns>Just the string suffix for the 'day' (based on the DateTime provided) - e.g. 'st', 'nd', 'rd', 'th'.</returns>
        private static string GetSuffixForDay(int day)
        {
            string suffix = string.Empty;

            // Exception for the 11th, 12th, & 13th (which would otherwise end up as 11st, 12nd, 13rd) - ripped from the original example and works well enough
            if (day % 100 >= 11 && day % 100 <= 13) {
                suffix = "th";
            }
            else {
                switch (day % 10) {
                    case 1:
                        suffix = "st";
                        break;

                    case 2:
                        suffix = "nd";
                        break;

                    case 3:
                        suffix = "rd";
                        break;

                    default:
                        suffix = "th";
                        break;
                }
            }

            return suffix;
        }
    }
}
