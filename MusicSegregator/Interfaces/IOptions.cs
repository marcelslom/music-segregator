
namespace MusicSegregator.Interfaces
{
    internal interface IOptions
    {
        string SourcePath { get; }
        string DestinationPath { get; }
        bool DeleteSourceFile { get; }
        string FilenameFormat { get; }
        bool CreateLogFiles { get; }
        bool SearchSubdirectories { get; }
    }
}
