using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pocketbase_csharp_sdk.Models.Files
{

    /// <summary>
    /// simple class for uploading files to PocketBase, accepting a path to a file
    /// </summary>
    public class FilepathFile : BaseFile, IFile
    {
        public string? FilePath { get; set; }
        private Stream? _stream;
        public Stream? GetStream()
        {
            if(_stream is not null)
            {
                _stream.Position = 0;
                return _stream;
            }
            if (string.IsNullOrWhiteSpace(FilePath))
            {
                return null;
            }

            try
            {
                return File.OpenRead(FilePath);
            }
            catch
            {
                return null;
            }
        }

        public FilepathFile()
        {
            
        }

        public FilepathFile(string? filePath)
        {
            this.FilePath = filePath;
        }
        public FilepathFile(string? fileName,Stream stream)
        {
            this.FilePath = fileName;
            this.FileName = fileName;
            _stream = stream;
        }
    }
}
