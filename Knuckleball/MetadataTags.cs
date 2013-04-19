// -----------------------------------------------------------------------
// <copyright file="MetadataTags.cs" company="Knuckleball Project">
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
    /// Represents the metadata tags of an MP4 file.
    /// </summary>
    public class MetadataTags : IDisposable
    {
        private MemoryStream artworkStream;
        private Image artwork;
        private bool isArtworkEdited;

        /// <summary>
        /// Prevents a default instance of the <see cref="MetadataTags"/> class from being created.
        /// </summary>
        private MetadataTags()
        {
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
        public short? GenreType { get; set; }

        /// <summary>
        /// Gets or sets the release date of the content contained in this file.
        /// </summary>
        public string ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the track number of the content contained in this file.
        /// May be <see langword="null"/> if the value is not set in the file.
        /// </summary>
        public short? TrackNumber { get; set; }

        /// <summary>
        /// Gets or sets the total number of tracks of the content contained in this file.
        /// May be <see langword="null"/> if the value is not set in the file.
        /// </summary>
        public short? TotalTracks { get; set; }

        /// <summary>
        /// Gets or sets the disc number of tracks of the content contained in this file.
        /// /// May be <see langword="null"/> if the value is not set in the file.
        /// </summary>
        public short? DiscNumber { get; set; }

        /// <summary>
        /// Gets or sets the total number of discs of the content contained in this file.
        /// May be <see langword="null"/> if the value is not set in the file.
        /// </summary>
        public short? TotalDiscs { get; set; }

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
        /// Gets the count of the artwork contained in this file.
        /// </summary>
        public int ArtworkCount { get; private set; }

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
                if (value != null)
                {
                    this.ArtworkFormat = value.RawFormat;
                }
                else
                {
                    this.ArtworkFormat = null;
                }

                this.isArtworkEdited = true;
            }
        }

        /// <summary>
        /// Releases all managed and unmanaged resources referenced by this instance.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Reads the tags from the specified file.
        /// </summary>
        /// <param name="fileHandle">The handle to the file from which to read the tags.</param>
        /// <returns>A new instance of a <see cref="MetadataTags"/> object containing the values
        /// in the metadata tags for the file.</returns>
        internal static MetadataTags ReadFromFile(IntPtr fileHandle)
        {
            IntPtr tagPtr = NativeMethods.MP4TagsAlloc();
            NativeMethods.MP4TagsFetch(tagPtr, fileHandle);
            NativeMethods.MP4Tags tags = tagPtr.ReadStructure<NativeMethods.MP4Tags>();
            MetadataTags managedTags = new MetadataTags();
            managedTags.Title = tags.name;
            managedTags.Artist = tags.artist;
            managedTags.AlbumArtist = tags.albumArtist;
            managedTags.Album = tags.album;
            managedTags.Grouping = tags.grouping;
            managedTags.Composer = tags.composer;
            managedTags.Comment = tags.comment;
            managedTags.Genre = tags.genre;
            managedTags.ReadTrackInfo(tags.track);
            managedTags.ReadDiskInfo(tags.disk);
            managedTags.Tempo = tags.tempo.ReadShort();
            managedTags.IsCompilation = tags.compilation.ReadBoolean();
            managedTags.Copyright = tags.copyright;
            managedTags.EncodingTool = tags.encodingTool;
            managedTags.EncodedBy = tags.encodedBy;

            // Tags specific to TV Episodes.
            managedTags.EpisodeNumber = tags.tvEpisode.ReadInt();
            managedTags.SeasonNumber = tags.tvSeason.ReadInt();
            managedTags.EpisodeId = tags.tvEpisodeID;
            managedTags.TVNetwork = tags.tvNetwork;
            managedTags.TVShow = tags.tvShow;

            managedTags.Description = tags.description;
            managedTags.LongDescription = tags.longDescription;
            managedTags.Lyrics = tags.lyrics;

            managedTags.SortName = tags.sortName;
            managedTags.SortArtist = tags.sortArtist;
            managedTags.SortAlbumArtist = tags.sortAlbumArtist;
            managedTags.SortAlbum = tags.sortAlbum;
            managedTags.SortComposer = tags.sortComposer;
            managedTags.SortTVShow = tags.sortTVShow;

            managedTags.ArtworkCount = tags.artworkCount;
            managedTags.ReadArtwork(tags.artwork);

            managedTags.IsPodcast = tags.podcast.ReadBoolean();
            managedTags.Keywords = tags.keywords;
            managedTags.Category = tags.category;

            managedTags.IsHDVideo = tags.hdVideo.ReadBoolean();
            managedTags.MediaType = tags.mediaType.ReadEnumValue<MediaKind>(MediaKind.NotSet);
            managedTags.ContentRating = tags.contentRating.ReadEnumValue<ContentRating>(ContentRating.NotSet);
            managedTags.IsGapless = tags.gapless.ReadBoolean();

            managedTags.MediaStoreAccount = tags.itunesAccount;
            managedTags.MediaStoreCountry = tags.iTunesCountry.ReadEnumValue<Country>(Country.None);
            managedTags.MediaStoreAccountType = tags.iTunesAccountType.ReadEnumValue<MediaStoreAccountKind>(MediaStoreAccountKind.NotSet);
            managedTags.ContentId = tags.contentID.ReadInt();
            managedTags.ArtistId = tags.artistID.ReadInt();
            managedTags.PlaylistId = tags.playlistID.ReadInt();
            managedTags.GenreId = tags.genreID.ReadInt();
            managedTags.ComposerId = tags.composerID.ReadInt();
            managedTags.Xid = tags.xid;

            NativeMethods.MP4TagsFree(tagPtr);

            managedTags.RatingInfo = ReadRawAtom<RatingInfo>(fileHandle);
            managedTags.MovieInfo = ReadRawAtom<MovieInfo>(fileHandle);
            return managedTags;
        }

        /// <summary>
        /// Writes the tags to the specified file.
        /// </summary>
        /// <param name="fileHandle">The handle to the file to which to write the tags.</param>
        internal void WriteToFile(IntPtr fileHandle)
        {
            IntPtr tagsPtr = NativeMethods.MP4TagsAlloc();
            NativeMethods.MP4TagsFetch(tagsPtr, fileHandle);
            NativeMethods.MP4Tags tags = tagsPtr.ReadStructure<NativeMethods.MP4Tags>();
            if (this.Title != tags.name)
            {
                NativeMethods.MP4TagsSetName(tagsPtr, this.Title);
            }

            if (this.Artist != tags.artist)
            {
                NativeMethods.MP4TagsSetArtist(tagsPtr, this.Artist);
            }

            if (this.Album != tags.album)
            {
                NativeMethods.MP4TagsSetAlbum(tagsPtr, this.Album);
            }

            if (this.AlbumArtist != tags.albumArtist)
            {
                NativeMethods.MP4TagsSetAlbumArtist(tagsPtr, this.AlbumArtist);
            }

            if (this.Grouping != tags.grouping)
            {
                NativeMethods.MP4TagsSetGrouping(tagsPtr, this.Grouping);
            }

            if (this.Composer != tags.composer)
            {
                NativeMethods.MP4TagsSetComposer(tagsPtr, this.Composer);
            }

            if (this.Comment != tags.comment)
            {
                NativeMethods.MP4TagsSetComments(tagsPtr, this.Comment);
            }

            if (this.Genre != tags.genre)
            {
                NativeMethods.MP4TagsSetGenre(tagsPtr, this.Genre);
            }

            if (this.GenreType != tags.genreType.ReadShort())
            {
                tagsPtr.WriteShort(this.GenreType, NativeMethods.MP4TagsSetGenreType);
            }

            if (this.ReleaseDate != tags.releaseDate)
            {
                NativeMethods.MP4TagsSetReleaseDate(tagsPtr, this.ReleaseDate);
            }

            if (this.Tempo != tags.tempo.ReadShort())
            {
                tagsPtr.WriteShort(this.Tempo, NativeMethods.MP4TagsSetTempo);
            }

            if (this.IsCompilation != tags.compilation.ReadBoolean())
            {
                tagsPtr.WriteBoolean(this.IsCompilation, NativeMethods.MP4TagsSetCompilation);
            }

            if (this.TVShow != tags.tvShow)
            {
                NativeMethods.MP4TagsSetTVShow(tagsPtr, this.TVShow);
            }

            if (this.TVNetwork != tags.tvNetwork)
            {
                NativeMethods.MP4TagsSetTVNetwork(tagsPtr, this.TVNetwork);
            }

            if (this.EpisodeId != tags.tvEpisodeID)
            {
                NativeMethods.MP4TagsSetTVEpisodeID(tagsPtr, this.EpisodeId);
            }

            if (this.SeasonNumber != tags.tvSeason.ReadInt())
            {
                tagsPtr.WriteInt(this.SeasonNumber, NativeMethods.MP4TagsSetTVSeason);
            }

            if (this.EpisodeNumber != tags.tvEpisode.ReadInt())
            {
                tagsPtr.WriteInt(this.EpisodeNumber, NativeMethods.MP4TagsSetTVEpisode);
            }

            if (this.Description != tags.description)
            {
                NativeMethods.MP4TagsSetDescription(tagsPtr, this.Description);
            }

            if (this.LongDescription != tags.longDescription)
            {
                NativeMethods.MP4TagsSetLongDescription(tagsPtr, this.LongDescription);
            }

            if (this.Lyrics != tags.lyrics)
            {
                NativeMethods.MP4TagsSetLyrics(tagsPtr, this.Lyrics);
            }

            if (this.SortName != tags.sortName)
            {
                NativeMethods.MP4TagsSetSortName(tagsPtr, this.SortName);
            }

            if (this.SortArtist != tags.sortArtist)
            {
                NativeMethods.MP4TagsSetSortArtist(tagsPtr, this.SortArtist);
            }

            if (this.SortAlbum != tags.sortAlbum)
            {
                NativeMethods.MP4TagsSetSortAlbum(tagsPtr, this.SortAlbum);
            }

            if (this.SortAlbumArtist != tags.sortAlbumArtist)
            {
                NativeMethods.MP4TagsSetSortAlbumArtist(tagsPtr, this.SortAlbumArtist);
            }

            if (this.SortComposer != tags.sortComposer)
            {
                NativeMethods.MP4TagsSetSortComposer(tagsPtr, this.SortComposer);
            }

            if (this.SortTVShow != tags.sortTVShow)
            {
                NativeMethods.MP4TagsSetSortTVShow(tagsPtr, this.SortTVShow);
            }

            if (this.Copyright != tags.copyright)
            {
                NativeMethods.MP4TagsSetCopyright(tagsPtr, this.Copyright);
            }

            if (this.EncodingTool != tags.encodingTool)
            {
                NativeMethods.MP4TagsSetEncodingTool(tagsPtr, this.EncodingTool);
            }

            if (this.EncodedBy != tags.encodedBy)
            {
                NativeMethods.MP4TagsSetEncodedBy(tagsPtr, this.EncodedBy);
            }

            if (this.PurchasedDate != tags.purchasedDate)
            {
                NativeMethods.MP4TagsSetPurchaseDate(tagsPtr, this.PurchasedDate);
            }

            if (this.IsPodcast != tags.podcast.ReadBoolean())
            {
                tagsPtr.WriteBoolean(this.IsPodcast, NativeMethods.MP4TagsSetPodcast);
            }

            if (this.Keywords != tags.keywords)
            {
                NativeMethods.MP4TagsSetKeywords(tagsPtr, this.Keywords);
            }

            if (this.Category != tags.category)
            {
                NativeMethods.MP4TagsSetCategory(tagsPtr, this.Category);
            }

            if (this.IsHDVideo != tags.hdVideo.ReadBoolean())
            {
                tagsPtr.WriteBoolean(this.IsHDVideo, NativeMethods.MP4TagsSetHDVideo);
            }

            if (this.MediaType != tags.mediaType.ReadEnumValue<MediaKind>(MediaKind.NotSet))
            {
                byte? mediaTypeValue = this.MediaType == MediaKind.NotSet ? null : (byte?)this.MediaType;
                tagsPtr.WriteByte(mediaTypeValue, NativeMethods.MP4TagsSetMediaType);
            }

            if (this.ContentRating != tags.mediaType.ReadEnumValue<ContentRating>(ContentRating.NotSet))
            {
                byte? contentRatingValue = this.ContentRating == ContentRating.NotSet ? null : (byte?)this.ContentRating;
                tagsPtr.WriteByte(contentRatingValue, NativeMethods.MP4TagsSetContentRating);
            }

            if (this.IsGapless != tags.gapless.ReadBoolean())
            {
                tagsPtr.WriteBoolean(this.IsGapless, NativeMethods.MP4TagsSetGapless);
            }

            if (this.MediaStoreAccount != tags.itunesAccount)
            {
                NativeMethods.MP4TagsSetITunesAccount(tagsPtr, this.MediaStoreAccount);
            }

            if (this.MediaStoreAccountType != tags.iTunesAccountType.ReadEnumValue<MediaStoreAccountKind>(MediaStoreAccountKind.NotSet))
            {
                byte? accountTypeValue = this.MediaStoreAccountType == MediaStoreAccountKind.NotSet ? null : (byte?)this.MediaStoreAccountType;
                tagsPtr.WriteByte(accountTypeValue, NativeMethods.MP4TagsSetITunesAccountType);
            }

            if (this.MediaStoreCountry != tags.iTunesCountry.ReadEnumValue<Country>(Country.None))
            {
                int? countryValue = this.MediaStoreCountry == Country.None ? null : (int?)this.MediaStoreCountry;
                tagsPtr.WriteInt(countryValue, NativeMethods.MP4TagsSetITunesCountry);
            }

            if (this.ContentId != tags.contentID.ReadInt())
            {
                tagsPtr.WriteInt(this.ContentId, NativeMethods.MP4TagsSetContentID);
            }

            if (this.ArtistId != tags.artistID.ReadInt())
            {
                tagsPtr.WriteInt(this.ArtistId, NativeMethods.MP4TagsSetArtistID);
            }

            if (this.PlaylistId != tags.playlistID.ReadLong())
            {
                tagsPtr.WriteLong(this.PlaylistId, NativeMethods.MP4TagsSetPlaylistID);
            }

            if (this.GenreId != tags.genreID.ReadInt())
            {
                tagsPtr.WriteInt(this.GenreId, NativeMethods.MP4TagsSetGenreID);
            }

            if (this.ComposerId != tags.composerID.ReadInt())
            {
                tagsPtr.WriteInt(this.ComposerId, NativeMethods.MP4TagsSetComposerID);
            }

            if (this.Xid != tags.xid)
            {
                NativeMethods.MP4TagsSetXID(tagsPtr, this.Xid);
            }

            this.WriteTrackInfo(tagsPtr, tags.track);
            this.WriteDiscInfo(tagsPtr, tags.disk);

            // If the artwork has been edited, there are two possibilities:
            // First we are replacing an existing piece of artwork with another; or
            // second, we are deleting the artwork that already existed.
            if (this.isArtworkEdited)
            {
                if (this.artwork != null)
                {
                    this.WriteArtwork(tagsPtr);
                }
                else if (this.ArtworkCount != 0)
                {
                    NativeMethods.MP4TagsRemoveArtwork(tagsPtr, 0);
                }
            }

            NativeMethods.MP4TagsStore(tagsPtr, fileHandle);
            NativeMethods.MP4TagsFree(tagsPtr);

            RatingInfo info = ReadRawAtom<RatingInfo>(fileHandle);
            if (this.RatingInfo != info)
            {
                WriteRawAtom<RatingInfo>(fileHandle, this.RatingInfo);
            }

            // TODO: Implement an equality comparison for MovieInfo, so
            // as to only write the atom to the file if it's been modified.
            // MovieInfo movieInfo = ReadRawAtom<MovieInfo>(fileHandle);
            // if (this.MovieInfo != movieInfo)
            // {
            //    WriteRawAtom<MovieInfo>(fileHandle, this.MovieInfo);
            // }
            WriteRawAtom<MovieInfo>(fileHandle, this.MovieInfo);
        }

        /// <summary>
        /// Releases all managed and unmanaged resources referenced by this instance.
        /// </summary>
        /// <param name="disposing"><see langword="true"/> to dispose of managed and unmanaged resources;
        /// <see langword="false"/> to dispose of only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.artwork != null)
                {
                    this.artwork.Dispose();
                }

                if (this.artworkStream != null)
                {
                    this.artworkStream.Dispose();
                }
            }
        }

        private static T ReadRawAtom<T>(IntPtr fileHandle) where T : Atom, new()
        {
            // Must use this construct, as generics don't allow constructors with parameters.
            T atom = new T();
            if (!atom.Initialize(fileHandle))
            {
                return null;
            }

            return atom;
        }

        private static void WriteRawAtom<T>(IntPtr fileHandle, T atom) where T : Atom, new()
        {
            // Because Generics don't allow inheritable static members, and we
            // really don't want to resort to reflection, we can create an instance
            // of the appropriate Atom type, only to get the Meaning and Name properties.
            // Passing in the parameters leads to potentially getting the strings wrong.
            // This solution is hacky in the worst possible way; let's think of a better
            // approach.
            T templateAtom = new T();
            string atomMeaning = templateAtom.Meaning;
            string atomName = templateAtom.Name;

            IntPtr listPtr = NativeMethods.MP4ItmfGetItemsByMeaning(fileHandle, atomMeaning, atomName);
            if (listPtr != IntPtr.Zero)
            {
                NativeMethods.MP4ItmfItemList list = listPtr.ReadStructure<NativeMethods.MP4ItmfItemList>();
                for (int i = 0; i < list.size; i++)
                {
                    IntPtr item = list.elements[i];
                    NativeMethods.MP4ItmfRemoveItem(fileHandle, item);
                }

                NativeMethods.MP4ItmfItemListFree(listPtr);

                if (atom != null)
                {
                    IntPtr newItemPtr = NativeMethods.MP4ItmfItemAlloc("----", 1);
                    NativeMethods.MP4ItmfItem newItem = newItemPtr.ReadStructure<NativeMethods.MP4ItmfItem>();
                    newItem.mean = atom.Meaning;
                    newItem.name = atom.Name;

                    NativeMethods.MP4ItmfData data = new NativeMethods.MP4ItmfData();
                    data.typeCode = atom.DataType;
                    byte[] dataBuffer = atom.ToByteArray();
                    data.valueSize = dataBuffer.Length;

                    IntPtr dataValuePointer = Marshal.AllocHGlobal(dataBuffer.Length);
                    Marshal.Copy(dataBuffer, 0, dataValuePointer, dataBuffer.Length);
                    data.value = dataValuePointer;

                    IntPtr dataPointer = Marshal.AllocHGlobal(Marshal.SizeOf(data));
                    Marshal.StructureToPtr(data, dataPointer, false);
                    newItem.dataList.elements[0] = dataPointer;

                    Marshal.StructureToPtr(newItem, newItemPtr, false);
                    NativeMethods.MP4ItmfAddItem(fileHandle, newItemPtr);

                    Marshal.FreeHGlobal(dataPointer);
                    Marshal.FreeHGlobal(dataValuePointer);
                }
            }
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
            this.artworkStream = new MemoryStream(artworkBuffer);
            this.artwork = Image.FromStream(this.artworkStream);

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
        }

        private void WriteArtwork(IntPtr tagsPtr)
        {
            NativeMethods.MP4TagArtwork newArtwork = new NativeMethods.MP4TagArtwork();

            MemoryStream stream = new MemoryStream();
            this.artwork.Save(stream, this.ArtworkFormat);
            byte[] artworkBytes = stream.ToArray();

            newArtwork.data = Marshal.AllocHGlobal(artworkBytes.Length);
            newArtwork.size = artworkBytes.Length;
            Marshal.Copy(artworkBytes, 0, newArtwork.data, artworkBytes.Length);

            if (this.ArtworkFormat.Equals(ImageFormat.Bmp))
            {
                newArtwork.type = NativeMethods.ArtworkType.Bmp;
            }
            else if (this.ArtworkFormat.Equals(ImageFormat.Jpeg))
            {
                newArtwork.type = NativeMethods.ArtworkType.Jpeg;
            }
            else if (this.ArtworkFormat.Equals(ImageFormat.Gif))
            {
                newArtwork.type = NativeMethods.ArtworkType.Gif;
            }
            else if (this.ArtworkFormat.Equals(ImageFormat.Png))
            {
                newArtwork.type = NativeMethods.ArtworkType.Png;
            }
            else
            {
                newArtwork.type = NativeMethods.ArtworkType.Undefined;
            }

            IntPtr newArtworkPtr = Marshal.AllocHGlobal(Marshal.SizeOf(newArtwork));
            Marshal.StructureToPtr(newArtwork, newArtworkPtr, false);
            if (this.ArtworkCount == 0)
            {
                NativeMethods.MP4TagsAddArtwork(tagsPtr, newArtworkPtr);
            }
            else
            {
                NativeMethods.MP4TagsSetArtwork(tagsPtr, 0, newArtworkPtr);
            }

            Marshal.FreeHGlobal(newArtwork.data);
            Marshal.FreeHGlobal(newArtworkPtr);
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

        private void WriteDiscInfo(IntPtr tagsPtr, IntPtr discInfoPtr)
        {
            if (this.DiscNumber == null || this.TotalDiscs == null)
            {
                NativeMethods.MP4TagsSetDisk(tagsPtr, IntPtr.Zero);
            }
            else
            {
                NativeMethods.MP4TagDisk discInfo = new NativeMethods.MP4TagDisk();
                if (discInfoPtr != IntPtr.Zero)
                {
                    discInfoPtr.ReadStructure<NativeMethods.MP4TagDisk>();
                }

                if (this.DiscNumber.Value != discInfo.index || this.TotalDiscs != discInfo.total)
                {
                    discInfo.index = this.DiscNumber.Value;
                    discInfo.total = this.TotalDiscs.Value;
                    IntPtr discPtr = Marshal.AllocHGlobal(Marshal.SizeOf(discInfo));
                    Marshal.StructureToPtr(discInfo, discPtr, false);
                    NativeMethods.MP4TagsSetDisk(tagsPtr, discPtr);
                    Marshal.FreeHGlobal(discPtr);
                }
            }
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

        private void WriteTrackInfo(IntPtr tagsPtr, IntPtr trackInfoPtr)
        {
            if (this.TrackNumber == null || this.TotalTracks == null)
            {
                NativeMethods.MP4TagsSetTrack(tagsPtr, IntPtr.Zero);
            }
            else
            {
                NativeMethods.MP4TagTrack trackInfo = new NativeMethods.MP4TagTrack();
                if (trackInfoPtr != IntPtr.Zero)
                {
                    trackInfo = trackInfoPtr.ReadStructure<NativeMethods.MP4TagTrack>();
                }

                if (this.TrackNumber.Value != trackInfo.index || this.TotalTracks != trackInfo.total)
                {
                    trackInfo.index = this.TrackNumber.Value;
                    trackInfo.total = this.TotalTracks.Value;
                    IntPtr trackPtr = Marshal.AllocHGlobal(Marshal.SizeOf(trackInfo));
                    Marshal.StructureToPtr(trackInfo, trackPtr, false);
                    NativeMethods.MP4TagsSetTrack(tagsPtr, trackPtr);
                    Marshal.FreeHGlobal(trackPtr);
                }
            }
        }
    }
}
