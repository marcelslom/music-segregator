using CommandLine;
using CommandLine.Text;
using MusicSegregator.Interfaces;

namespace MusicSegregator.Options
{
    internal class CliOptions : IOptions
    {
        [Value(0, MetaName = "source", Required = false, Default = null, HelpText = "The path to the source folder containing mp3 files. If not provided, the current directory will be used by default.")]
        public string SourcePath { get; set; }

        [Option('d', "destination", Required = false, Default = null, HelpText = "The destination folder where segregated music files will be stored. If not provided, the default destination will be set to '{current_directory}/music_segregated'.")]
        public string DestinationPath { get; set; }

        [Option('m', "move-files", Required = false, Default = false, HelpText = "Option that specifies whether files should be moved to the destination folder. By default, files will be copied.")]
        public bool DeleteSourceFile { get; set; }

        [Option('f', "filename-format", Required = false, Default = null, HelpText = "The format for renaming processed files. If provided, this format will be used for renaming. If not provided, the filename will remain unchanged. DO NOT provide file extension in this parameter, as program will use the original one.")]
        public string FilenameFormat { get; set; }

        [Option('l', "log", Required = false, Default = false, HelpText = "Option that enables writing informational logs to a file.")]
        public bool CreateLogFiles { get; set; }

        [Option('r', "recursive", Required = false, Default = false, HelpText = "Option that specifies whether the program should search for files in subdirectories. Default behavior is to search in the top directory only.")]
        public bool SearchSubdirectories { get; set; }

        [Usage(ApplicationAlias ="musicseg")]
        public static List<Example> Examples { get; } = 
            [
            new Example("Simple usage (process current folder)", new CliOptions()),
            new Example("Simple usage with custom destination folder", new CliOptions {DestinationPath = @"C:\Music\Sorted"}),
            new Example("Rename files during copying", new CliOptions {FilenameFormat = "{Track} - {Title}"}),
            new Example(@"Move sorted files from 'Unsorted' and its subfolders to ""Sorted"" folder and create log file in ""C:\Music\Unsorted\logs""", new CliOptions 
            {
                SourcePath = @"C:\Music\Unsorted", 
                DestinationPath = @"C:\Music\Sorted", 
                FilenameFormat = "{Track} - {Title}", 
                DeleteSourceFile = true,
                CreateLogFiles = true, 
                SearchSubdirectories = true
            }),
            ];
    }
}
