using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicSegregator
{
    internal class Context
    {
        private static readonly string DefaultDestinationFolderName = "music_segregated";
        private Context() { }

        public readonly string NoArtistFolderName = "!NO_ARTIST";


        public string SourcePath { get; private set; }
        public string DestinationPath { get; private set; }
        public bool DeleteSourceFile { get;set; }
        public string FilenameSchema { get; set; }
        public bool RenameFile => !string.IsNullOrEmpty(FilenameSchema);
        public bool CreateLogFiles { get; private set; }
        public bool SearchSubdirectories { get; private set; }

        public static Context From(IOptions options)
        {
            var context = new Context();

            if (string.IsNullOrWhiteSpace(options.SourcePath))
            {
                context.SourcePath = Environment.CurrentDirectory;
            } 
            else
            {
                context.SourcePath = options.SourcePath.Trim();
            }

            if (string.IsNullOrWhiteSpace(options.DestinationPath))
            {
                context.DestinationPath = Path.Combine(context.SourcePath, DefaultDestinationFolderName);
            }
            else
            {
                context.DestinationPath = options.DestinationPath.Trim();
            }

            context.DeleteSourceFile = options.DeleteSourceFile;
            context.FilenameSchema = options.FilenameSchema;
            context.CreateLogFiles = options.CreateLogFiles;
            context.SearchSubdirectories = options.SearchSubdirectories;

            return context;
        }

    }
}
