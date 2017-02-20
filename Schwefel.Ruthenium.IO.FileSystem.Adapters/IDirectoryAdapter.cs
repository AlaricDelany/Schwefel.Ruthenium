using Schwefel.Ruthenium.IO.FileSystem.Adapters.Models;

namespace Schwefel.Ruthenium.IO.FileSystem.Adapters
{
    public interface IDirectoryAdapter
    {
        IDirectory GetDirectory(string path);
    }
}
