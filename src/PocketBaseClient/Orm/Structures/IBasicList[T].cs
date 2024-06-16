// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase


namespace PocketBaseClient.Orm.Structures
{
    public interface IBasicList<T> : IBasicList, IEnumerable<T>, IAsyncEnumerable<T>
    {

        /// <summary>
        /// Adds an element to the list
        /// </summary>
        /// <param name="element">The element to be added</param>
        /// <returns></returns>
        T? Add(T? element);

        /// <summary>
        /// Adds all elements to the list
        /// </summary>
        /// <param name="elements">The elements to be added</param>
        void AddRange(IEnumerable<T?> elements)
        {
            foreach (var element in elements)
                Add(element);
        }
 
        /// <summary>
        /// Removes the element from the list
        /// </summary>
        /// <param name="element">The element to be removed</param>
        /// <returns></returns>
        T? Remove(T? element);

        /// <summary>
        /// Removes all elements from the list
        /// </summary>
        /// <param name="elements">The elements to be removed</param>
        void RemoveRange(IEnumerable<T?> elements)
        {
            foreach (var element in elements)
                Remove(element);
        }

    }
}
