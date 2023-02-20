// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBaseClient.Orm.Structures
{
    public class FieldFileList<T> : FieldBasicList<T>, IFieldFileList<T>
        where T : FieldFileBase, new()
    {
        /// <summary>
        /// The list of FileNames in PocketBase
        /// </summary>
        internal List<string> OriginalFileNames { get; } = new List<string>();

        /// <summary>
        /// The list of Removed Filenames
        /// </summary>
        protected internal List<string> RemovedFileNames 
            => OriginalFileNames.Where(n => !this.Any(f => f.FileName == n)).ToList();

        ///// <summary>
        ///// The list of Removed Filenames
        ///// </summary>
        //protected internal List<string> AddedFileNames
        //    => this.Where(f => f.HasChanges && !f.IsEmpty && !string.IsNullOrEmpty(f.FileName) && !OriginalFileNames.Any(n => n == f.FileName))
        //           .Select(f=> f.FileName!).ToList();

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyId"></param>
        /// <param name="maxSize"></param>
        public FieldFileList(ItemBase? owner, string propertyName, string propertyId, int? maxSize = null) : base(owner, propertyName, propertyId, maxSize) { }


        /// <inheritdoc />
        public T? AddFromLocalFile(string localPathFile)
        {
            var file = new T();
            file.LoadFromLocalFile(localPathFile);
            return Add(file);
        }


        /// <inheritdoc />
        public bool Contains(string fileName)
            => this.Any(f => f.FileName?.Equals(fileName) ?? false);


        /// <inheritdoc />
        public T? Remove(string fileName)
            => Remove(this.FirstOrDefault(f => f.FileName?.Equals(fileName) ?? false));

        /// <inheritdoc />
        public void RemoveRange(IEnumerable<string> fileNames)
        {
            foreach (var fileName in fileNames)
                Remove(fileName);
        }
    }
}
