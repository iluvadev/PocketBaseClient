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
    /// <summary>
    /// Definition for field types of Lists of files
    /// </summary>
    public interface IFieldFileList<T> : IFieldBasicList<T>
        where T : FieldFileBase, new()
    {

        /// <summary>
        /// Adds a file from a Local file
        /// </summary>
        /// <param name="localPathFile">The entire path of the local file</param>
        /// <returns></returns>
        T? AddFromLocalFile(string localPathFile);


        /// <summary>
        /// Says if the fileName is contained in the list
        /// </summary>
        /// <param name="fileName">The fileName to check if is contained</param>
        /// <returns></returns>
        bool Contains(string fileName);


        /// <summary>
        /// Removes the file with fileName from the list
        /// </summary>
        /// <param name="fileName">The fileName of the element to be removed</param>
        /// <returns></returns>
        T? Remove(string fileName);

        /// <summary>
        /// Removes all elements with fileNames from the list
        /// </summary>
        /// <param name="fileNames">The filenames of the elements to be removed</param>
        void RemoveRange(IEnumerable<string> fileNames);
    }
}
