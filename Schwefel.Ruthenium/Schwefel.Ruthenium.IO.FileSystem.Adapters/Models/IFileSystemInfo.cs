namespace Schwefel.Ruthenium.IO.FileSystem.Adapters.Models
{
    public interface IFileSystemInfo
    {
        bool Exists { get; }
        string Name { get; }
        string Path { get; }
        string FullPath { get; }
        IDirectory ParentDirectory { get; }

        bool Create();
        bool Delete();
        string Rename(string newName);
        string Move(string newPath);
    }
}
