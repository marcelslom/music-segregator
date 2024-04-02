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
        public static Context Instance { get; set; } = new Context();
        private Context() { }

        public string SourcePath { get; private set; }
        public string DestinationPath { get; private set; }
        public bool DeleteSourceFile { get;set; }
        public string FilenameSchema { get; set; }
        public bool RenameFile => !string.IsNullOrEmpty(FilenameSchema);
        public bool CreateLogFiles { get; private set; }

        public void LoadOptions(IOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.SourcePath))
            {
                SourcePath = Environment.CurrentDirectory;
            } 
            else
            {
                SourcePath = options.SourcePath.Trim();
            }

            if (string.IsNullOrWhiteSpace(options.DestinationPath))
            {
                DestinationPath = Path.Combine(Environment.CurrentDirectory, DefaultDestinationFolderName);
            }
            else
            {
                DestinationPath = options.DestinationPath.Trim();
            }

            DeleteSourceFile = options.DeleteSourceFile;
            FilenameSchema = options.FilenameSchema;
            CreateLogFiles = options.CreateLogFiles;
        }

    }
}
