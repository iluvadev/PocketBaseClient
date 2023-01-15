// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

namespace PocketBaseClient.CodeGenerator.Generation
{
    /// <summary>
    /// Representation of a generated file with its content
    /// </summary>
    internal class GeneratedCodeFile
    {
        /// <summary>
        /// File name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="content"></param>
        public GeneratedCodeFile(string fileName, string content)
        {
            FileName = fileName;
            Content = content;
        }
    }
}
