using System.Text.Json;

namespace PocketBaseClient
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object obj)
        {
            return JsonSerializer.Serialize(obj);
        }
        public static string ToJsonIndented(this object obj)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(obj, options);
        }
    }
}
