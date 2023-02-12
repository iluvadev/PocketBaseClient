using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBaseClient.Orm.Structures
{
    public class Thumbnail
    {
        protected FieldFileBase FieldFile { get; }
        protected string ThumbDimensions { get; }

        public Thumbnail(FieldFileBase fieldFile, string thumbDimensions)
        {
            FieldFile = fieldFile;
            ThumbDimensions = thumbDimensions;
        }

        /// <summary>
        /// Gets the Stream of the Thumbnail (async)
        /// </summary>
        /// <returns></returns>
        public async Task<Stream> GetStreamAsync()
            => await FieldFile.GetStreamAsync(ThumbDimensions);

        /// <summary>
        /// Saves the remote Thumbnail to local file (async)
        /// </summary>
        /// <param name="localPathFile"></param>
        /// <returns></returns>
        public async Task SaveToLocalFileAsync(string localPathFile)
            => await FieldFile.SaveToLocalFileAsync(localPathFile, null);
    }
}
