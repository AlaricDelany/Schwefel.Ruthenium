using System;
using System.IO;

namespace Schwefel.Ruthenium.IO.FileSystem.Adapters.Models
{
    public class FileInfoWrapper : IFile
    {
        private readonly FileInfo _FileInfo;

        public FileInfoWrapper(string fullPath)
        {
            _FileInfo = new FileInfo(fullPath);
        }

        public FileInfoWrapper(FileInfo fileInfo)
        {
            _FileInfo = fileInfo;
        }

        /// <inheritdoc />
        public bool Exists => _FileInfo.Exists;

        /// <inheritdoc />
        public string Name => _FileInfo.Name;

        /// <inheritdoc />
        public string Path => _FileInfo.Directory.Name;

        /// <inheritdoc />
        public string FullPath => _FileInfo.FullName;

        /// <inheritdoc />
        public IDirectory ParentDirectory => new DirectoryInfoWrapper(_FileInfo.Directory);

        /// <inheritdoc />
        public string FileName => _FileInfo.Name;

        /// <inheritdoc />
        public bool Create()
        {
            try
            {
                if (!_FileInfo.Exists)
                    _FileInfo.Create();

                return true;
            }
            catch (Exception)
            {
                //TODO: Log Exception
                return false;
            }
        }

        /// <inheritdoc />
        public bool Delete()
        {
            try
            {
                if(_FileInfo.Exists)
                    _FileInfo.Delete();

                return true;
            }
            catch (Exception)
            {
                //TODO: Log Exception
                return false;
            }
        }

        /// <inheritdoc />
        public string Rename(string newName)
        {
            _FileInfo.CopyTo(System.IO.Path.Combine(_FileInfo.Directory.FullName, newName));

            return _FileInfo.FullName;
        }

        /// <inheritdoc />
        public string Copy(string newPath)
        {
            _FileInfo.CopyTo(newPath);

            return _FileInfo.FullName;
        }

        /// <inheritdoc />
        public string Move(string newPath)
        {
            _FileInfo.MoveTo(newPath);

            return _FileInfo.FullName;
        }
    }
}
