using System.ComponentModel;
using System.Reflection;

namespace App.Share.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// A utility extension method for attempting to retrieve the
        /// DescriptionAttribute Name property based on the supplied Enum value.
        /// </summary>
        /// <param name="enumValue">The Enum value to operate against.</param>
        /// <returns>A string denoting the DisplayAttribute Name property against the Enum value, if available (or an empty string).</returns>
        public static string GetDescription(this Enum enumValue)
        {
            DescriptionAttribute descriptionAttribute = enumValue.GetAttribute<DescriptionAttribute>();

            return descriptionAttribute == null ? string.Empty : descriptionAttribute.Description ?? string.Empty;
        }
        /// <summary>
        /// A generic extension method that aids in reflecting
        /// and retrieving any attribute that is applied to an `Enum`.
        /// Possibility for extending the usefulness of this later.
        /// </summary>
        /// <typeparam name="TAttribute">The attribute type to return and operate against.</typeparam>
        /// <param name="enumValue">The enum to discover the attribute on.</param>
        /// <returns>The given attribute associated with the Enum.</returns>
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
            where TAttribute : Attribute
            => enumValue.GetType().GetMember(enumValue.ToString()).First().GetCustomAttribute<TAttribute>();
    }
}
