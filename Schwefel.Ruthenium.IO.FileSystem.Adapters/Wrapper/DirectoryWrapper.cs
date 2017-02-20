using System;
using Schwefel.Ruthenium.IO.FileSystem.Adapters.Models;

namespace Schwefel.Ruthenium.IO.FileSystem.Adapters.Wrapper
{
    public class DirectoryWrapper : IDirectoryAdapter
    {
        /// <inheritdoc />
        public IDirectory GetDirectory(string path)
        {
            IDirectory lResult = new DirectoryInfoWrapper(path);

            return lResult;
        }
    }
}
