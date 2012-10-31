// -----------------------------------------------------------------------
// <copyright file="MP4File.cs" company="Knuckleball Project">
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
//
// Portions created by Jim Evans are Copyright © 2012.
// All Rights Reserved.
//
// Contributors:
//     Jim Evans, james.h.evans.jr@@gmail.com
//
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

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

        /// <summary>
        /// Gets or sets the title of the content contained in this file.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the artist of the content contained in this file.
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// Gets or sets the album artist of the content contained in this file.
        /// </summary>
        public string AlbumArtist { get; set; }

        /// <summary>
        /// Gets or sets the album of the content contained in this file.
        /// </summary>
        public string Album { get; set; }

        /// <summary>
        /// Gets or sets the grouping of the content contained in this file.
        /// </summary>
        public string Grouping { get; set; }

        /// <summary>
        /// Gets or sets the composer of the content contained in this file.
        /// </summary>
        public string Composer { get; set; }

        /// <summary>
        /// Gets or sets the comments of the content contained in this file.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the genre of the content contained in this file.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets the genre type of the content contained in this file.
        /// </summary>
        public short GenreType { get; set; }

        /// <summary>
        /// Gets or sets the release date of the content contained in this file.
        /// </summary>
        public string ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the track number of the content contained in this file.
        /// May be <see langword="null"/> if the value is not set in the file.
        /// </summary>
        public int? TrackNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the total number of tracks of the content contained in this file.
        /// May be <see langword="null"/> if the value is not set in the file.
        /// </summary>
        public int? TotalTracks { get; set; }
        
        /// <summary>
        /// Gets or sets the disc number of tracks of the content contained in this file.
        /// /// May be <see langword="null"/> if the value is not set in the file.
        /// </summary>
        public int? DiscNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the total number of discs of the content contained in this file.
        /// May be <see langword="null"/> if the value is not set in the file.
        /// </summary>
        public int? TotalDiscs { get; set; }
        
        /// <summary>
        /// Gets or sets the tempo of the content contained in this file.
        /// May be <see langword="null"/> if the value is not set in the file.
        /// </summary>
        public short? Tempo { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the content contained in this file is part of a compilation.
        /// May be <see langword="null"/> if the value is not set in the file.
        /// </summary>
        public bool? IsCompilation { get; set; }

        /// <summary>
        /// Gets or sets the name of the TV show for the content contained in this file.
        /// </summary>
        public string TVShow { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the TV network for the content contained in this file.
        /// </summary>
        public string TVNetwork { get; set; }

        /// <summary>
        /// Gets or sets the episode ID of the content contained in this file.
        /// </summary>
        public string EpisodeId { get; set; }
        
        /// <summary>
        /// Gets or sets the season number of the content contained in this file.
        /// May be <see langword="null"/> if the value is not set in the file.
        /// </summary>
        public int? SeasonNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the episode number of the content contained in this file.
        /// May be <see langword="null"/> if the value is not set in the file.
        /// </summary>
        public int? EpisodeNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the description of the content contained in this file.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Gets or sets the long description of the content contained in this file.
        /// </summary>
        public string LongDescription { get; set; }
        
        /// <summary>
        /// Gets or sets the lyrics of the content contained in this file.
        /// </summary>
        public string Lyrics { get; set; }
        
        /// <summary>
        /// Gets or sets the sort name for the content contained in this file.
        /// </summary>
        public string SortName { get; set; }
        
        /// <summary>
        /// Gets or sets the sort artist for the content contained in this file.
        /// </summary>
        public string SortArtist { get; set; }
        
        /// <summary>
        /// Gets or sets the sort album artist for the content contained in this file.
        /// </summary>
        public string SortAlbumArtist { get; set; }
        
        /// <summary>
        /// Gets or sets the sort album for the content contained in this file.
        /// </summary>
        public string SortAlbum { get; set; }
        
        /// <summary>
        /// Gets or sets the sort composer for the content contained in this file.
        /// </summary>
        public string SortComposer { get; set; }
        
        /// <summary>
        /// Gets or sets the sort TV show name for the content contained in this file.
        /// </summary>
        public string SortTVShow { get; set; }
        
        /// <summary>
        /// Gets or sets the count of the artwork contained in this file.
        /// </summary>
        public int ArtworkCount { get; set; }
        
        /// <summary>
        /// Gets the format of the artwork contained in this file.
        /// </summary>
        public ImageFormat ArtworkFormat { get; private set; }
        
        /// <summary>
        /// Gets or sets the copyright information for the content contained in this file.
        /// </summary>
        public string Copyright { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the encoding tool used for the content contained in this file.
        /// </summary>
        public string EncodingTool { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the person who encoded the content contained in this file.
        /// </summary>
        public string EncodedBy { get; set; }
        
        /// <summary>
        /// Gets or sets the date this file was purchased from a media store.
        /// </summary>
        public string PurchasedDate { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the content contained in this file is part of a podcast.
        /// May be <see langword="null"/> if the value is not set in the file.
        /// </summary>
        public bool? IsPodcast { get; set; }
        
        /// <summary>
        /// Gets or sets the podcast keywords for the content contained in this file.
        /// </summary>
        public string Keywords { get; set; }
        
        /// <summary>
        /// Gets or sets the podcast category for the content contained in this file.
        /// </summary>
        public string Category { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the content contained in this file is high-definition video.
        /// May be <see langword="null"/> if the value is not set in the file.
        /// </summary>
        public bool? IsHDVideo { get; set; }
        
        /// <summary>
        /// Gets or sets the type of media for the content contained in this file.
        /// </summary>
        public MediaKind MediaType { get; set; }
        
        /// <summary>
        /// Gets or sets the content rating for the content contained in this file.
        /// </summary>
        public ContentRating ContentRating { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the content contained in this file is part of a gapless playback album.
        /// May be <see langword="null"/> if the value is not set in the file.
        /// </summary>
        public bool? IsGapless { get; set; }
        
        /// <summary>
        /// Gets or sets the account used to purchase this file from a media store, such as iTunes.
        /// </summary>
        public string MediaStoreAccount { get; set; }
        
        /// <summary>
        /// Gets or sets the type of account used to purchase this file from a media store, such as iTunes.
        /// </summary>
        public MediaStoreAccountKind MediaStoreAccountType { get; set; }
        
        /// <summary>
        /// Gets or sets the country where this file was purchased from a media store, such as iTunes.
        /// </summary>
        public Country MediaStoreCountry { get; set; }
        
        /// <summary>
        /// Gets or sets the media store ID of the of the content contained in this file.
        /// May be <see langword="null"/> if the value is not set in the file.
        /// </summary>
        public int? ContentId { get; set; }
        
        /// <summary>
        /// Gets or sets the media store ID of the of the artist of the content contained in this file.
        /// May be <see langword="null"/> if the value is not set in the file.
        /// </summary>
        public int? ArtistId { get; set; }
        
        /// <summary>
        /// Gets or sets the playlist ID of this file.
        /// May be <see langword="null"/> if the value is not set in the file.
        /// </summary>
        public long? PlaylistId { get; set; }
        
        /// <summary>
        /// Gets or sets the ID of the of the genre of the content contained in this file.
        /// May be <see langword="null"/> if the value is not set in the file.
        /// </summary>
        public int? GenreId { get; set; }
        
        /// <summary>
        /// Gets or sets the media store ID of the of the composer of the content contained in this file.
        /// May be <see langword="null"/> if the value is not set in the file.
        /// </summary>
        public int? ComposerId { get; set; }
        
        /// <summary>
        /// Gets or sets the X ID of this file.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Xid is spelled consistently with the external API.")]
        public string Xid { get; set; }
        
        /// <summary>
        /// Gets or sets the ratings information for the content contained in this file, including source
        /// of the rating and the rating value.
        /// </summary>
        public RatingInfo RatingInfo { get; set; }
        
        /// <summary>
        /// Gets or sets the movie information for the content contained in this file, including cast,
        /// directors, producers, and writers.
        /// </summary>
        public MovieInfo MovieInfo { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Image"/> used for the artwork in this file.
        /// </summary>
        public Image Artwork 
        {
            get 
            {
                return this.artwork; 
            }

            set 
            { 
                this.artwork = value;
                this.ArtworkFormat = value.RawFormat;
            }
        }

        /// <summary>
        /// Reads the tags from the specified file.
        /// </summary>
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
            this.EpisodeId = tags.tvEpisodeID;
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
            this.MediaStoreCountry = tags.iTunesCountry.ReadEnumValue<Country, int>(Country.None);
            this.MediaStoreAccountType = tags.iTunesAccountType.ReadEnumValue<MediaStoreAccountKind, byte>(MediaStoreAccountKind.NotSet);
            this.ContentId = tags.contentID.ReadInt();
            this.ArtistId = tags.artistID.ReadInt();
            this.PlaylistId = tags.playlistID.ReadInt();
            this.GenreId = tags.genreID.ReadInt();
            this.ComposerId = tags.composerID.ReadInt();
            this.Xid = tags.xid;

            NativeMethods.MP4TagsFree(tagPtr);

            this.RatingInfo = ReadRawAtom<RatingInfo>(fileHandle, "com.apple.iTunes", "iTunEXTC");
            this.MovieInfo = ReadRawAtom<MovieInfo>(fileHandle, "com.apple.iTunes", "iTunMOVI");

            NativeMethods.MP4Close(fileHandle);
        }

        /// <summary>
        /// Writes the tags to the specified file.
        /// </summary>
        public void WriteTags()
        {
            IntPtr fileHandle = NativeMethods.MP4Read(this.fileName);
            NativeMethods.MP4Close(fileHandle);
        }

        private static T ReadRawAtom<T>(IntPtr fileHandle, string atomMeaning, string atomName) where T : Atom, new()
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
            this.DiscNumber = diskInfo.index;
            this.TotalDiscs = diskInfo.total;
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
    }
}
