
using System;
using Schwefel.Ruthenium.IO.FileSystem.Adapters.Models;

namespace Schwefel.Ruthenium.IO.FileSystem.Adapters.Wrapper
{
    public class FileWrapper : IFileAdapter
    {
        /// <inheritdoc />
        public IFile Create(string path)
        {
            IFile lResult = new FileInfoWrapper(path);

            bool lCreateResult = lResult.Create();

            if(lCreateResult == false)
                throw new InvalidOperationException($"Creating of the File: \"{path}\" failed.");

            return lResult;
        }
    }
}
