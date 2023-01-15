// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace PocketBaseClient.CodeGenerator
{
    /// <summary>
    /// Extensions for String
    /// </summary>
    internal static class StringExtensions
    {
        private static PluralizationService? _PluralizationService = null;
        private static PluralizationService PluralizationService => _PluralizationService ??= PluralizationService.CreateService(new CultureInfo("en"));

        /// <summary>
        /// Flag to define the Singularize and Pluralize behaviour
        /// </summary>
        public static bool SingularizeAndPluralize { get; set; } = false;
        
        /// <summary>
        /// Converts the string to PascalCase to be used as Namespace
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToPascalCaseForNamespace(this string s)
        {
            var splitted = s.Split('.');
            return string.Join(".", splitted.Select(n => n.ToPascalCase()));
        }

        /// <summary>
        /// Converts the string to Namespace, with '.' as separators and maintaining capitalization
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToNamespace(this string s)
        {
            var nonWordChars = new Regex(@"[^a-zA-Z0-9]+");
            var tokens = nonWordChars.Split(s.Trim());
            return string.Join(".", tokens);
        }

        /// <summary>
        /// Convers the string to PascalCase
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToPascalCase(this string s)
        {
            var result = new StringBuilder();
            var nonWordChars = new Regex(@"[^a-zA-Z0-9]+");
            var tokens = nonWordChars.Split(s);
            foreach (var token in tokens)
            {
                result.Append(PascalCaseSingleWord(token));
            }

            return result.ToString();
        }

        static string PascalCaseSingleWord(string s)
        {
            var match = Regex.Match(s, @"^(?<word>\d+|^[a-z]+|[A-Z]+|[A-Z][a-z]+|\d[a-z]+)+$");
            var groups = match.Groups["word"];

            var textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
            var result = new StringBuilder();
            foreach (var capture in groups.Captures.Cast<Capture>())
            {
                result.Append(textInfo.ToTitleCase(capture.Value.ToLower()));
            }
            return result.ToString();
        }

        /// <summary>
        /// Converts the sting to camelCase
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string s)
        {
            // If there are 0 or 1 characters, just return the string.
            //if (the_string == null || the_string.Length < 2)
            if (s.Length < 2)
                return s;

            string result = s.ToPascalCase();
            return result[..1].ToLower() + result[1..];
        }

        /// <summary>
        /// Capitalize the first character and add a space before 
        /// each capitalized letter (except the first character).
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToProperCase(this string s)
        {
            // If there are 0 or 1 characters, just return the string.
            //if (the_string == null) return the_string;
            if (s.Length < 2) return s.ToUpper();

            // Start with the first character.
            string result = s[..1].ToUpper();

            // Add the remaining characters.
            for (int i = 1; i < s.Length; i++)
            {
                if (char.IsUpper(s[i])) result += " ";
                result += s[i];
            }

            return result;
        }

        /// <summary>
        /// Converts the string to Singular (in english), if <see cref="SingularizeAndPluralize"/> is true
        /// </summary>
        /// <remarks>The behaviour can be modified changing <see cref="SingularizeAndPluralize"/></remarks>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Singularize(this string s)
            => SingularizeAndPluralize ? PluralizationService.Singularize(s) : s;

        /// <summary>
        /// Converts the string to Plural (in english), if <see cref="SingularizeAndPluralize"/> is true
        /// </summary>
        /// <remarks>The behaviour can be modified changing <see cref="SingularizeAndPluralize"/></remarks>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Pluralize(this string s)
            => SingularizeAndPluralize ? PluralizationService.Pluralize(s) : s;
    }
}
