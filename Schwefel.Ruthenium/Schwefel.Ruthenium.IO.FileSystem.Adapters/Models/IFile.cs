using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schwefel.Ruthenium.IO.FileSystem.Adapters.Models
{
    public interface IFile
    {
        bool Exists { get;  }

        string FullPath { get; }

        string FileName { get; }

        bool Create();

        bool Delete();

        string Rename(string newName);

        string Copy(string newPath);

        string Move(string newPath);
    }
}
