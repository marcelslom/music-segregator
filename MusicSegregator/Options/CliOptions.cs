using CommandLine;
using MusicSegregator.Interfaces;

namespace MusicSegregator.Options
{
    internal class CliOptions : IOptions
    {
        [Value(0, MetaName = "source", Required = false, HelpText = "Path of source folder")]
        public string SourcePath { get; set; }

        [Option('d', "destination", Required = false, HelpText = "Set output to verbose messages.")]
        public string DestinationPath { get; set; }

        [Option('m', "move-files", Required = false, Default = false, HelpText = "Set output to verbose messages.")]
        public bool DeleteSourceFile { get; set; }

        [Option('f', "filename-format", Required = false, HelpText = "Set output to verbose messages.")]
        public string FilenameFormat { get; set; }

        [Option('l', "log", Required = false, Default = false, HelpText = "Set output to verbose messages.")]
        public bool CreateLogFiles { get; set; }

        [Option('b', "search-subdirs", Required = false, Default = false, HelpText = "Set output to verbose messages.")]
        public bool SearchSubdirectories { get; set; }
    }
}
