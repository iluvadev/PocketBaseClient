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
        protected internal List<string> RemovedFileNames { get; } = new List<string>();
        protected internal List<string> AddedFileNames { get; } = new List<string>();

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

        /// <inheritdoc />
        public override T? Remove(T? element)
        {
            var removedElement = base.Remove(element);
            if (removedElement?.IsFromServer ?? false && !string.IsNullOrEmpty(removedElement?.FileName) && !RemovedFileNames.Contains(removedElement.FileName))
                RemovedFileNames.Add(removedElement.FileName!);

            return removedElement;
        }

        /// <inheritdoc />
        public override T? Add(T? element)
        {
            var addedElement = base.Add(element);
            if (!(addedElement?.IsFromServer ?? false) && !string.IsNullOrEmpty(addedElement.FileName) && !AddedFileNames.Contains(addedElement.FileName))
                AddedFileNames.Add(addedElement.FileName!);

            return (addedElement);
        }

        internal void ClearLocalModifications()
        {
            RemovedFileNames.Clear();
            AddedFileNames.Clear();
        }

        // IEPA!!
        // Com fer que un Remove directe del File es vegi reflexat a la llista?

    }
}
