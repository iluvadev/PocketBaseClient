using System.Text.Json;

namespace PocketBaseClient
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object obj)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(obj, options);
        }
    }
}
