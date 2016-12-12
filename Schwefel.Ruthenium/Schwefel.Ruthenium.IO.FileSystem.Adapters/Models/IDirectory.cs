using System.Collections;
using System.Collections.Generic;

namespace Schwefel.Ruthenium.IO.FileSystem.Adapters.Models
{
    public interface IDirectory : IFileSystemInfo
    {
        IEnumerable<IFile> GetFiles();
        IEnumerable<IDirectory> GetSubDirectories();
        bool Delete(bool recursive);

    }
}