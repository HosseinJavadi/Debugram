
using System.Reflection;
using System.Text.RegularExpressions;

namespace Debugram.Common.Utilities
{
    public static class Tool
    {
        public static bool Like(this string toSearch, string toFind)
        {
            return new Regex(@"\A" + new Regex(@"\.|\$|\^|\{|\[|\(|\||\)|\*|\+|\?|\\").Replace(toFind, ch => @"\" + ch).Replace('_', '.').Replace("%", ".*") + @"\z", RegexOptions.Singleline).IsMatch(toSearch);
        }
        public static string[] ToLowercaseNamingConvention(this string s)
        {

            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

            return r.Replace(s, "_").Split("_", StringSplitOptions.None);

        }
        public static object CastPropertyValue(this PropertyInfo property, string value)
        {
            if (property == null || String.IsNullOrEmpty(value))
                return null;
            if (property.PropertyType.IsEnum)
            {
                Type enumType = property.PropertyType;
                if (Enum.IsDefined(enumType, value))
                    return Enum.Parse(enumType, value);
            }
            if (property.PropertyType == typeof(bool))
                return value == "1" || value == "true" || value == "on" || value == "checked";
            else if (property.PropertyType == typeof(Uri))
                return new Uri(Convert.ToString(value));
            else if (property.PropertyType == typeof(Guid))
                return Guid.Parse(value);
            else
                return Convert.ChangeType(value, property.PropertyType);
        }
    }
}
