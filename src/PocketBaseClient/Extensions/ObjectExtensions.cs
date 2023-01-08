// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

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
