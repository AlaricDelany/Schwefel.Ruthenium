using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Schwefel.Ruthenium.IO.FileSystem.Adapters.Models
{
    public class FileInfoWrapper : IFile
    {
        private readonly FileInfo _FileInfo;

        public FileInfoWrapper(string fullPath)
        {
            _FileInfo = new FileInfo(fullPath);
        }

        /// <inheritdoc />
        public bool DoesExists()
        {
            return _FileInfo.Exists;
        }

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
            _FileInfo.CopyTo(Path.Combine(_FileInfo.Directory.FullName, newName));

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
