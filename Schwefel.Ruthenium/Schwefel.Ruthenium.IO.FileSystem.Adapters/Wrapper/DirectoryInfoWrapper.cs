using System;
using System.Collections.Generic;
using System.IO;

namespace Schwefel.Ruthenium.IO.FileSystem.Adapters.Models
{
    public class DirectoryInfoWrapper : IDirectory
    {
        private readonly DirectoryInfo _directoryInfo;

        public DirectoryInfoWrapper(string path)
        {
            _directoryInfo = new DirectoryInfo(path);
        }

        public DirectoryInfoWrapper(DirectoryInfo directoryInfo)
        {
            _directoryInfo = directoryInfo;
        }

        /// <inheritdoc />
        public bool Exists => _directoryInfo.Exists;

        /// <inheritdoc />
        public string Name => _directoryInfo.Name;

        /// <inheritdoc />
        public string Path => _directoryInfo.Parent.FullName;

        /// <inheritdoc />
        public string FullPath => _directoryInfo.FullName;

        /// <inheritdoc />
        public IDirectory ParentDirectory => new DirectoryInfoWrapper(_directoryInfo.Parent);

        /// <inheritdoc />
        public bool Create()
        {
            try
            {
                if(!_directoryInfo.Exists)
                    _directoryInfo.Create();

                return true;
            }
            catch (Exception)
            {
                //TODO: Log Error
                return false;
            }
        }

        /// <inheritdoc />
        public bool Delete()
        {
            try
            {
                DeleteInternal(false);

                return true;
            }
            catch(Exception)
            {
                //TODO: Log Error
                return false;
            }
        }

        public bool Delete(bool recursive)
        {
            try
            {
                DeleteInternal(recursive);

                return true;
            }
            catch(Exception)
            {
                //TODO: Log Error
                return false;
            }
        }

        private void DeleteInternal(bool recursive)
        {
            if(_directoryInfo.Exists)
                _directoryInfo.Delete(recursive);
        }

        /// <inheritdoc />
        public string Rename(string newName)
        {
            _directoryInfo.MoveTo(System.IO.Path.Combine(Path, newName));

            return FullPath;
        }

        /// <inheritdoc />
        public string Move(string newPath)
        {
            _directoryInfo.MoveTo(newPath);

            return FullPath;
        }

        /// <inheritdoc />
        public IEnumerable<IFile> GetFiles()
        {
            foreach (var fileInfo in _directoryInfo.GetFiles())
            {
                yield return new FileInfoWrapper(fileInfo);
            }
        }

        /// <inheritdoc />
        public IEnumerable<IDirectory> GetSubDirectories()
        {
            foreach (var directoryInfo in _directoryInfo.GetDirectories())
            {
                yield return new DirectoryInfoWrapper(directoryInfo);
            }
        }
    }
}
