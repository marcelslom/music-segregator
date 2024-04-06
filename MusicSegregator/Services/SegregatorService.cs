using MusicSegregator.Proxies;
using NLog;
using SmartFormat;
using TagLib;

namespace MusicSegregator.Services
{
    internal class SegregatorService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly Context context;

        public SegregatorService(Context context)
        {
            this.context = context;
        }

        internal void Start()
        {
            var searchMode = context.SearchSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            logger.Info(context.SearchSubdirectories ? $"Scanning '{context.SourcePath}' and subdirectories..." : $"Scanning '{context.SourcePath}'...");
            var files = Directory.GetFiles(context.SourcePath, "*.mp3", searchMode);
            logger.Info($"Found {files.Length} files to process.");
            var successful = 0;
            var errors = 0;
            foreach (var sourceFile in files)
            {
                var result = ProcessFile(sourceFile);
                if (result)
                {
                    successful++;
                } 
                else
                {
                    errors++;
                }
            }
            logger.Info(errors > 0 ? $"Processed {successful} files successfully. {errors} files failed. Check logs for more information." : $"Processed {successful} files successfully.");
        }

        private bool ProcessFile(string sourceFile)
        {
            try
            {
                using var mp3 = TagLib.File.Create(sourceFile) ?? throw new Exception($"Unable to process the file due to loading failure.");
                var tags = mp3.Tag ?? throw new Exception($"Tags not found for processing.");
                var tagsProxy = new TagsProxy(tags);
                var filename = Path.GetFileName(sourceFile);
                if (context.RenameFile)
                {
                    filename = $"{Smart.Format(context.FilenameFormat, tagsProxy)}{Path.GetExtension(filename)}";
                }
                var destinationDir = GetDestinationDir(tagsProxy);
                var destinationFilename = GetUniqueFilename(destinationDir, filename);
                destinationDir = Sanitize(destinationDir, Path.GetInvalidPathChars());
                destinationFilename = Sanitize(destinationFilename, Path.GetInvalidFileNameChars());
                Directory.CreateDirectory(destinationDir);
                var destinationFile = Path.Combine(destinationDir, destinationFilename);
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
                return true;
            }
            catch (Exception e)
            {
                logger.Error(e, $"An error occured while processing file {sourceFile}");
                return false;
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

        private string GetDestinationDir(TagsProxy tags)
        {
            var artist = string.IsNullOrEmpty(tags.AlbumArtists) ? context.NoArtistFolderName: tags.AlbumArtists;
            if (string.IsNullOrEmpty(tags.Album))
            {
                return Path.Combine(context.DestinationPath, artist);
            }
            return Path.Combine(context.DestinationPath, artist, tags.Album);
        }

        private static string Sanitize(string s, char[] invalid, char replacement = '_')
        {
            return string.Join(replacement, s.Split(invalid));
        }
    }
}
