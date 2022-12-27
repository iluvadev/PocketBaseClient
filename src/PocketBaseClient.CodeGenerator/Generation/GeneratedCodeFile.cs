using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBaseClient.CodeGenerator.Generation
{
    internal class GeneratedCodeFile
    {
        public string FileName { get; set; }
        public string Content { get; set; }

        public GeneratedCodeFile(string fileName, string content)
        {
            FileName = fileName;
            Content = content;
        }
    }
}
