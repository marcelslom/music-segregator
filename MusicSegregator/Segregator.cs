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
        private readonly Context context;

        public Segregator(Context context)
        {
            this.context = context;
        }

        internal void Start()
        {
            var searchMode = context.SearchSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            var files = Directory.GetFiles(context.SourcePath, "*.mp3", searchMode);
            foreach (var sourceFile in files)
            {
                using (var mp3 = TagLib.File.Create(sourceFile))
                {
                    var tags = mp3.Tag;
                    var filename = Path.GetFileName(sourceFile);
                    var destinationDir = GetDestinationDir(tags);
                    Directory.CreateDirectory(destinationDir);
                    var destinationPath = Path.Combine(destinationDir, filename);
                    if (context.DeleteSourceFile)
                    {
                        System.IO.File.Move(sourceFile, destinationPath);
                    } 
                    else
                    {
                        System.IO.File.Copy(sourceFile, destinationPath);
                    }
                }
            }
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
