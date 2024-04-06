using TagLib;

namespace MusicSegregator.Proxies
{
    internal class TagsProxy
    {
        private readonly Tag tags;

        public TagsProxy(Tag tag)
        {
            this.tags = tag;
        }

        public string AlbumArtists => string.Join("; ", tags.AlbumArtists).Trim();
        public string Performers => string.Join("; ", tags.Performers).Trim();
        public string Composers => string.Join("; ", tags.Composers).Trim();
        public string Genres => string.Join("; ", tags.Genres).Trim();
        public string Title => tags.Title;
        public string Album => tags.Album;
        public uint Year => tags.Year;
        public uint Track => tags.Track;
        public uint TrackCount => tags.TrackCount;
        public uint Disc => tags.Disc;
        public uint DiscCount => tags.DiscCount;
        public string InitialKey => tags.InitialKey;
        public uint BeatsPerMinute => tags.BeatsPerMinute;

    }
}
