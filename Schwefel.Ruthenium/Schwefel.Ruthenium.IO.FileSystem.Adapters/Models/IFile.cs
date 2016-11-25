namespace Schwefel.Ruthenium.IO.FileSystem.Adapters.Models
{
    public interface IFile : IFileSystemInfo
    {
        string Copy(string newPath);

    }
}
