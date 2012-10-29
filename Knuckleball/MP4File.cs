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
        public string releaseDate { get; set; }
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
            NativeMethods.MP4Tags tags = ConvertStructure<NativeMethods.MP4Tags>(tagPtr);
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
            this.Tempo = ReadShort(tags.tempo);
            this.IsCompilation = ReadBoolean(tags.compilation);
            this.Copyright = tags.copyright;
            this.EncodingTool = tags.encodingTool;
            this.EncodedBy = tags.encodedBy;

            // Tags specific to TV Episodes.
            this.EpisodeNumber = ReadInt(tags.tvEpisode);
            this.SeasonNumber = ReadInt(tags.tvSeason);
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

            this.IsPodcast = ReadBoolean(tags.podcast);
            this.Keywords = tags.keywords;
            this.Category = tags.category;

            this.IsHDVideo = ReadBoolean(tags.hdVideo);
            this.MediaType = ReadEnumValue<MediaKind, byte>(tags.mediaType, MediaKind.NotSet);
            this.ContentRating = ReadEnumValue<ContentRating, byte>(tags.contentRating, ContentRating.NotSet);
            this.IsGapless = ReadBoolean(tags.gapless);

            this.MediaStoreAccount = tags.itunesAccount;
            this.MediaStoreCountry = ReadEnumValue<Country, int>(tags.iTunesCountry, Country.NotSet);
            this.MediaStoreAccountType = ReadEnumValue<MediaStoreAccountKind, byte>(tags.iTunesAccountType, MediaStoreAccountKind.NotSet);
            this.ContentID = ReadInt(tags.contentID);
            this.ArtistID = ReadInt(tags.artistID);
            this.PlaylistID = ReadLong(tags.playlistID);
            this.GenreID = ReadInt(tags.genreID);
            this.ComposerID = ReadInt(tags.composerID);
            this.Xid = tags.xid;

            NativeMethods.MP4TagsFree(tagPtr);
            NativeMethods.MP4Close(fileHandle);
        }

        public void WriteTags()
        {
        }

        private int? ReadInt(IntPtr pointer)
        {
            if (pointer == IntPtr.Zero)
            {
                return null;
            }

            return Marshal.ReadInt32(pointer);
        }

        private long? ReadLong(IntPtr pointer)
        {
            if (pointer == IntPtr.Zero)
            {
                return null;
            }

            return Marshal.ReadInt64(pointer);
        }

        private short? ReadShort(IntPtr pointer)
        {
            if (pointer == IntPtr.Zero)
            {
                return null;
            }

            return Marshal.ReadInt16(pointer);
        }

        private bool? ReadBoolean(IntPtr pointer)
        {
            if (pointer == IntPtr.Zero)
            {
                return null;
            }
            
            return Marshal.ReadByte(pointer) != 0;
        }

        private byte? ReadByte(IntPtr pointer)
        {
            if (pointer == IntPtr.Zero)
            {
                return null;
            }
            
            return Marshal.ReadByte(pointer);
        }

        private T ConvertStructure<T>(IntPtr structPtr)
        {
            return (T)Marshal.PtrToStructure(structPtr, typeof(T));
        }

        private T ReadEnumValue<T, U>(IntPtr pointer, T defaultValue) where T : struct where U: struct
        {
            if (pointer == IntPtr.Zero)
            {
                return defaultValue;
            }

            object rawValue;
            if (typeof(U) == typeof(byte))
            {
                rawValue = ReadByte(pointer).Value;
            } 
            else if (typeof(U) == typeof(long))
            {
                rawValue = ReadLong(pointer).Value;
            }
            else if (typeof(U) == typeof(short))
            {
                rawValue = ReadShort(pointer).Value;
            }
            else
            {
                rawValue = ReadInt(pointer).Value;
            }

            return (T)Enum.ToObject(typeof(T), rawValue);
        }

        private void ReadArtwork(IntPtr artworkStructurePointer)
        {
            if (artworkStructurePointer == IntPtr.Zero)
            {
                return;
            }

            NativeMethods.MP4TagArtwork artwork = ConvertStructure<NativeMethods.MP4TagArtwork>(artworkStructurePointer);
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

            NativeMethods.MP4TagDisk diskInfo = ConvertStructure<NativeMethods.MP4TagDisk>(diskInfoPointer);
            this.DiskNumber = diskInfo.index;
            this.TotalDisks = diskInfo.total;
        }

        private void ReadTrackInfo(IntPtr trackInfoPointer)
        {
            if (trackInfoPointer == IntPtr.Zero)
            {
                return;
            }

            NativeMethods.MP4TagTrack trackInfo = ConvertStructure<NativeMethods.MP4TagTrack>(trackInfoPointer);
            this.TrackNumber = trackInfo.index;
            this.TotalTracks = trackInfo.total;
        }
    }
}
