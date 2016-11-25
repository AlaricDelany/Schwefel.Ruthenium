
using Schwefel.Ruthenium.IO.FileSystem.Adapters.Models;

namespace Schwefel.Ruthenium.IO.FileSystem.Adapters
{
    public interface IFileAdapter
    {
        IFile GetFile(string path);
    }
}
