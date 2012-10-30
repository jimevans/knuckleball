// -----------------------------------------------------------------------
// <copyright file="MP4File.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;

namespace Knuckleball
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MP4File
    {
        private string fileName;
        private Image artwork;

        /// <summary>
        /// Initializes a new instance of the <see cref="MP4File"/> class for the specified file.
        /// </summary>
        /// <param name="fileName">The full path and file name of the file to use.</param>
        public MP4File(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
            {
                throw new ArgumentException("Must specify a valid file name", "fileName");
            }

            this.fileName = fileName;
        }

        public string Title { get; set; }
        public string Artist { get; set; }
        public string AlbumArtist { get; set; }
        public string Album { get; set; }
        public string Grouping { get; set; }
        public string Composer { get; set; }
        public string Comment { get; set; }
        public string Genre { get; set; }
        public short GenreType { get; set; }
        public string ReleaseDate { get; set; }
        public int? TrackNumber { get; set; }
        public int? TotalTracks { get; set; }
        public int? DiskNumber { get; set; }
        public int? TotalDisks { get; set; }
        public short? Tempo { get; set; }
        public bool? IsCompilation { get; set; }
        public string TVShow { get; set; }
        public string TVNetwork { get; set; }
        public string EpisodeID { get; set; }
        public int? SeasonNumber { get; set; }
        public int? EpisodeNumber { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }
        public string Lyrics { get; set; }
        public string SortName { get; set; }
        public string SortArtist { get; set; }
        public string SortAlbumArtist { get; set; }
        public string SortAlbum { get; set; }
        public string SortComposer { get; set; }
        public string SortTVShow { get; set; }
        public int ArtworkCount { get; set; }
        public ImageFormat ArtworkFormat { get; private set; }
        public string Copyright { get; set; }
        public string EncodingTool { get; set; }
        public string EncodedBy { get; set; }
        public string PurchasedDate { get; set; }
        public bool? IsPodcast { get; set; }
        public string Keywords { get; set; }
        public string Category { get; set; }
        public bool? IsHDVideo { get; set; }
        public MediaKind MediaType { get; set; }
        public ContentRating ContentRating { get; set; }
        public bool? IsGapless { get; set; }
        public string MediaStoreAccount { get; set; }
        public MediaStoreAccountKind MediaStoreAccountType { get; set; }
        public Country MediaStoreCountry { get; set; }
        public int? ContentID { get; set; }
        public int? ArtistID { get; set; }
        public long? PlaylistID { get; set; }
        public int? GenreID { get; set; }
        public int? ComposerID { get; set; }
        public string Xid { get; set; }
        public RatingInfo RatingInfo { get; set; }
        public MovieInfo MovieInfo { get; set; }

        public Image Artwork 
        {
            get { return this.artwork; }
            set 
            { 
                this.artwork = value;
                this.ArtworkFormat = value.RawFormat;
            }
        }

        public void ReadTags()
        {
            IntPtr fileHandle = NativeMethods.MP4Read(this.fileName);
            IntPtr tagPtr = NativeMethods.MP4TagsAlloc();
            NativeMethods.MP4TagsFetch(tagPtr, fileHandle);
            NativeMethods.MP4Tags tags = tagPtr.ReadStructure<NativeMethods.MP4Tags>();
            this.Title = tags.name;
            this.Artist = tags.artist;
            this.AlbumArtist = tags.albumArtist;
            this.Album = tags.album;
            this.Grouping = tags.grouping;
            this.Composer = tags.composer;
            this.Comment = tags.comment;
            this.Genre = tags.genre;
            this.ReadTrackInfo(tags.track);
            this.ReadDiskInfo(tags.disk);
            this.Tempo = tags.tempo.ReadShort();
            this.IsCompilation = tags.compilation.ReadBoolean();
            this.Copyright = tags.copyright;
            this.EncodingTool = tags.encodingTool;
            this.EncodedBy = tags.encodedBy;

            // Tags specific to TV Episodes.
            this.EpisodeNumber = tags.tvEpisode.ReadInt();
            this.SeasonNumber = tags.tvSeason.ReadInt();
            this.EpisodeID = tags.tvEpisodeID;
            this.TVNetwork = tags.tvNetwork;
            this.TVShow = tags.tvShow;

            this.Description = tags.description;
            this.LongDescription = tags.longDescription;
            this.Lyrics = tags.lyrics;

            this.SortName = tags.sortName;
            this.SortArtist = tags.sortArtist;
            this.SortAlbumArtist = tags.sortAlbumArtist;
            this.SortAlbum = tags.sortAlbum;
            this.SortComposer = tags.sortComposer;
            this.SortTVShow = tags.sortTVShow;

            this.ArtworkCount = tags.artworkCount;
            this.ReadArtwork(tags.artwork);

            this.IsPodcast = tags.podcast.ReadBoolean();
            this.Keywords = tags.keywords;
            this.Category = tags.category;

            this.IsHDVideo = tags.hdVideo.ReadBoolean();
            this.MediaType = tags.mediaType.ReadEnumValue<MediaKind, byte>(MediaKind.NotSet);
            this.ContentRating = tags.contentRating.ReadEnumValue<ContentRating, byte>(ContentRating.NotSet);
            this.IsGapless = tags.gapless.ReadBoolean();

            this.MediaStoreAccount = tags.itunesAccount;
            this.MediaStoreCountry = tags.iTunesCountry.ReadEnumValue<Country, int>(Country.NotSet);
            this.MediaStoreAccountType = tags.iTunesAccountType.ReadEnumValue<MediaStoreAccountKind, byte>(MediaStoreAccountKind.NotSet);
            this.ContentID = tags.contentID.ReadInt();
            this.ArtistID = tags.artistID.ReadInt();
            this.PlaylistID = tags.playlistID.ReadInt();
            this.GenreID = tags.genreID.ReadInt();
            this.ComposerID = tags.composerID.ReadInt();
            this.Xid = tags.xid;

            NativeMethods.MP4TagsFree(tagPtr);

            this.RatingInfo = ReadRawAtom<RatingInfo>(fileHandle, "com.apple.iTunes", "iTunEXTC");
            this.MovieInfo = ReadRawAtom<MovieInfo>(fileHandle, "com.apple.iTunes", "iTunMOVI");

            NativeMethods.MP4Close(fileHandle);
        }

        public void WriteTags()
        {
        }

        private void ReadArtwork(IntPtr artworkStructurePointer)
        {
            if (artworkStructurePointer == IntPtr.Zero)
            {
                return;
            }

            NativeMethods.MP4TagArtwork artwork = artworkStructurePointer.ReadStructure<NativeMethods.MP4TagArtwork>();
            byte[] artworkBuffer = new byte[artwork.size];
            Marshal.Copy(artwork.data, artworkBuffer, 0, artwork.size);
            Image artworkImage;
            using (MemoryStream imageStream = new MemoryStream(artworkBuffer))
            {
                artworkImage = Image.FromStream(imageStream);
            }

            switch (artwork.type)
            {
                case NativeMethods.ArtworkType.Bmp:
                    this.ArtworkFormat = ImageFormat.Bmp;
                    break;

                case NativeMethods.ArtworkType.Gif:
                    this.ArtworkFormat = ImageFormat.Gif;
                    break;

                case NativeMethods.ArtworkType.Jpeg:
                    this.ArtworkFormat = ImageFormat.Jpeg;
                    break;

                case NativeMethods.ArtworkType.Png:
                    this.ArtworkFormat = ImageFormat.Png;
                    break;

                default:
                    this.ArtworkFormat = ImageFormat.MemoryBmp;
                    break;
            }

            this.artwork = artworkImage;
        }

        private void ReadDiskInfo(IntPtr diskInfoPointer)
        {
            if (diskInfoPointer == IntPtr.Zero)
            {
                return;
            }

            NativeMethods.MP4TagDisk diskInfo = diskInfoPointer.ReadStructure<NativeMethods.MP4TagDisk>();
            this.DiskNumber = diskInfo.index;
            this.TotalDisks = diskInfo.total;
        }

        private void ReadTrackInfo(IntPtr trackInfoPointer)
        {
            if (trackInfoPointer == IntPtr.Zero)
            {
                return;
            }

            NativeMethods.MP4TagTrack trackInfo = trackInfoPointer.ReadStructure<NativeMethods.MP4TagTrack>();
            this.TrackNumber = trackInfo.index;
            this.TotalTracks = trackInfo.total;
        }

        private T ReadRawAtom<T>(IntPtr fileHandle, string atomMeaning, string atomName) where T : Atom, new()
        {
            T atom = null;
            IntPtr rawAtomPointer = NativeMethods.MP4ItmfGetItemsByMeaning(fileHandle, atomMeaning, atomName);
            if (rawAtomPointer != IntPtr.Zero)
            {   
                // Must use this construct, as generics don't allow constructors with parameters.
                atom = new T();
                atom.Initialize(rawAtomPointer);
            }

            return atom;
        }
    }
}
