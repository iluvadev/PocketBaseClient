using System.Text.Json;

namespace PocketBaseClient
{
    /// <summary>
    /// Extensions for Objects
    /// </summary>
    public static class ObjectExtensions
    {

        /// <summary>
        /// Converts the object to json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        /// <summary>
        /// Converts the object to json, indenting the result
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJsonIndented(this object obj)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(obj, options);
        }
    }
}
