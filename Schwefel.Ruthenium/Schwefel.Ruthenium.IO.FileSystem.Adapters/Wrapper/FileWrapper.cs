
using System;
using Schwefel.Ruthenium.IO.FileSystem.Adapters.Models;

namespace Schwefel.Ruthenium.IO.FileSystem.Adapters.Wrapper
{
    public class FileWrapper : IFileAdapter
    {
        /// <inheritdoc />
        public IFile GetFile(string path)
        {
            IFile lResult = new FileInfoWrapper(path);

            return lResult;
        }
    }
}
