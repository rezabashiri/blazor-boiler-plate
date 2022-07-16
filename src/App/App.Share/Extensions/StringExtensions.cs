namespace App.Share.Extensions
{
    public static class StringExtensions
    {
        public static bool TrueForValueMatch(
            this string providedValue,
            string trueValue,
            StringComparison stringComparisonType = StringComparison.InvariantCultureIgnoreCase)
        {
            if (providedValue == null && trueValue == null) {
                return true;
            }

            if (providedValue == null || trueValue == null) {
                return false;
            }

            return providedValue.Equals(trueValue, stringComparisonType);
        }
    }
}