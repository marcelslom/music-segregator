
namespace MusicSegregator.Interfaces
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
