using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSegregator
{
    internal interface IOptions
    {
        string SourcePath { get; }
        string DestinationPath { get; }
        bool DeleteSourceFile { get; }
        string FilenameSchema { get; } //todo
        bool CreateLogFiles { get; } //todo
        bool SearchSubdirectories { get; }
    }
}
