using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib;

namespace MusicSegregator
{
    internal class Segregator
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly Context context;

        public Segregator(Context context)
        {
            this.context = context;
        }

        internal void Start()
        {
            var searchMode = context.SearchSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            logger.Info(context.SearchSubdirectories ? $"Scanning '{context.SourcePath} and subdirectories...'" : $"Scanning '{context.SourcePath}...'");
            var files = Directory.GetFiles(context.SourcePath, "*.mp3", searchMode);
            logger.Info($"Found {files.Length} files to process.");
            foreach (var sourceFile in files)
            {
                ProcessFile(sourceFile);
            }
        }

        private void ProcessFile(string sourceFile)
        {
            try
            {
                using var mp3 = TagLib.File.Create(sourceFile);
                var tags = mp3.Tag;
                var filename = Path.GetFileName(sourceFile);
                var destinationDir = GetDestinationDir(tags);
                var uniqueFilename = GetUniqueFilename(destinationDir, filename);
                Directory.CreateDirectory(destinationDir);
                var destinationFile = Path.Combine(destinationDir, uniqueFilename);
                if (context.DeleteSourceFile)
                {
                    System.IO.File.Move(sourceFile, destinationFile);
                    logger.Info($"Moved file from '{sourceFile}' to '{destinationFile}'");
                }
                else
                {
                    System.IO.File.Copy(sourceFile, destinationFile);
                    logger.Info($"Copied file from '{sourceFile}' to '{destinationFile}'");
                }
            } 
            catch (Exception e)
            {
                logger.Error($"An error occured while processing file {sourceFile}", e);
            }
        }

        private string GetUniqueFilename(string dir, string originalFilename)
        {
            var originalPath = Path.Combine(dir, originalFilename);
            if (!System.IO.File.Exists(originalPath))
            {
                return originalFilename;
            }

            int counter = 0;
            var filenameWithoutExtension = Path.GetFileNameWithoutExtension(originalFilename);
            var extension = Path.GetExtension(originalFilename);

            string uniqueFilename;
            do
            {
                counter++;
                uniqueFilename = $"{filenameWithoutExtension}_({counter}){extension}";
            } while (System.IO.File.Exists(Path.Combine(dir, uniqueFilename)));

            logger.Info($"'{originalPath}' already exists, this file will be saved as '{uniqueFilename}'.");

            return uniqueFilename;
        }

        private string GetDestinationDir(Tag tags)
        {
            var artist = string.Join(", ", tags.AlbumArtists).Trim();
            if (string.IsNullOrEmpty(artist))
            {
                artist = context.NoArtistFolderName;
            }
            if (string.IsNullOrEmpty(tags.Album))
            {
                return Path.Combine(context.DestinationPath, artist);
            }
            return Path.Combine(context.DestinationPath, artist, tags.Album);
        }
    }
}
