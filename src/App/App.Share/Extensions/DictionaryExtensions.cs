namespace App.Share.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Gets the first discovered value, by key, as
        /// the desired type (handling nullable types also).
        /// </summary>
        /// <typeparam name="T">The type of the value desired.</typeparam>
        /// <param name="dictionaryData">The dictionary source data.</param>
        /// <param name="key">The dictionary key to source the value using.</param>
        /// <returns>The first value discovered as type T.</returns>
        public static T GetFirstValueByKey<T>(this Dictionary<string, List<string>> dictionaryData, string key)
        {
            if (key == null
                || dictionaryData == null
                || dictionaryData?.Count == 0) {
                return default;
            }

            dictionaryData.TryGetValue(key, out List<string> matchingValues);

            if (matchingValues == null
                || matchingValues?.Count == 0) {
                return default;
            }

            return TryGetValueAsType<T>(matchingValues?.FirstOrDefault());
        }

        /// <summary>
        /// Attempts to parse a value to type T (handling nullable also).
        /// </summary>
        /// <typeparam name="T">The type to parse to.</typeparam>
        /// <param name="valueToParse">The value to parse.</param>
        /// <returns>The value parsed or a default value.</returns>
        private static T TryGetValueAsType<T>(string valueToParse)
        {
            T parsedValue = default;

            try {
                Type underlyingType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
                parsedValue = (T)Convert.ChangeType(valueToParse, underlyingType);
            }
            catch {
                // The type cannot be parse so the default for type T is the only reasonable thing to provide
            }

            return parsedValue;
        }
    }
}