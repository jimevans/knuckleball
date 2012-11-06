// -----------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Knuckleball Project">
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
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Knuckleball
{
    /// <summary>
    /// Contains methods used for interfacing with the native code MP4V2 library.
    /// </summary>
    internal class NativeMethods
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="NativeMethods"/> class from being created.
        /// </summary>
        private NativeMethods()
        {
        }

        // Basic types of value data as enumerated in spec. */
        // typedef enum MP4ItmfBasicType_e
        // {
        //     MP4_ITMF_BT_IMPLICIT  = 0,   /**< for use with tags for which no type needs to be indicated */
        //     MP4_ITMF_BT_UTF8      = 1,   /**< without any count or null terminator */
        //     MP4_ITMF_BT_UTF16     = 2,   /**< also known as UTF-16BE */
        //     MP4_ITMF_BT_SJIS      = 3,   /**< deprecated unless it is needed for special Japanese characters */
        //     MP4_ITMF_BT_HTML      = 6,   /**< the HTML file header specifies which HTML version */
        //     MP4_ITMF_BT_XML       = 7,   /**< the XML header must identify the DTD or schemas */
        //     MP4_ITMF_BT_UUID      = 8,   /**< also known as GUID; stored as 16 bytes in binary (valid as an ID) */
        //     MP4_ITMF_BT_ISRC      = 9,   /**< stored as UTF-8 text (valid as an ID) */
        //     MP4_ITMF_BT_MI3P      = 10,  /**< stored as UTF-8 text (valid as an ID) */
        //     MP4_ITMF_BT_GIF       = 12,  /**< (deprecated) a GIF image */
        //     MP4_ITMF_BT_JPEG      = 13,  /**< a JPEG image */
        //     MP4_ITMF_BT_PNG       = 14,  /**< a PNG image */
        //     MP4_ITMF_BT_URL       = 15,  /**< absolute, in UTF-8 characters */
        //     MP4_ITMF_BT_DURATION  = 16,  /**< in milliseconds, 32-bit integer */
        //     MP4_ITMF_BT_DATETIME  = 17,  /**< in UTC, counting seconds since midnight, January 1, 1904; 32 or 64-bits */
        //     MP4_ITMF_BT_GENRES    = 18,  /**< a list of enumerated values */
        //     MP4_ITMF_BT_INTEGER   = 21,  /**< a signed big-endian integer with length one of { 1,2,3,4,8 } bytes */
        //     MP4_ITMF_BT_RIAA_PA   = 24,  /**< RIAA parental advisory; { -1=no, 1=yes, 0=unspecified }, 8-bit ingteger */
        //     MP4_ITMF_BT_UPC       = 25,  /**< Universal Product Code, in text UTF-8 format (valid as an ID) */
        //     MP4_ITMF_BT_BMP       = 27,  /**< Windows bitmap image */
        //     MP4_ITMF_BT_UNDEFINED = 255  /**< undefined */
        // } MP4ItmfBasicType;
        public enum MP4ItmfBasicType
        {
            Implicit = 0,
            Utf8 = 1,
            Utf16 = 2,
            Sjis = 3,
            Html = 6,
            Xml = 7,
            Uuid = 8,
            Isrc = 9,
            Mi3p = 10,
            Gif = 12,
            Jpeg = 13,
            Png = 14,
            Url = 15,
            Duration = 16,
            DateTime = 17,
            Genres = 18,
            Integer = 21,
            Riaa_pa = 24,
            Upc = 25,
            Bmp = 27,
            Undefined = 255
        }

        // typedef enum MP4TagArtworkType_e
        // {
        //      MP4_ART_UNDEFINED = 0,
        //      MP4_ART_BMP       = 1,
        //      MP4_ART_GIF       = 2,
        //      MP4_ART_JPEG      = 3,
        //      MP4_ART_PNG       = 4
        //  } MP4TagArtworkType;
        public enum ArtworkType
        {
            Undefined = 0,
            Bmp = 1,
            Gif = 2,
            Jpeg = 3,
            Png = 4
        }

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MP4TagsAlloc();

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsFetch(IntPtr tags, IntPtr file);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsStore(IntPtr tags, IntPtr file);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MP4TagsFree(IntPtr tags);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MP4Read([MarshalAs(UnmanagedType.LPStr)]string fileName);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MP4Modify([MarshalAs(UnmanagedType.LPStr)]string fileName, int flags);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4Optimize([MarshalAs(UnmanagedType.LPStr)]string fileName, [MarshalAs(UnmanagedType.LPStr)]string newName);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MP4Close(IntPtr file);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetName(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string name);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetArtist(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string artist);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetAlbumArtist(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string albumArtist);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetAlbum(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string album);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetGrouping(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string grouping);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetComposer(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string composer);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetComments(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string comments);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetGenre(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string genre);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetGenreType(IntPtr tags, IntPtr genreType);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetReleaseDate(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string releaseDate);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetTrack(IntPtr tags, IntPtr trackInfo);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetDisk(IntPtr tags, IntPtr discInfo);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetTempo(IntPtr tags, IntPtr tempo);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetCompilation(IntPtr tags, IntPtr isCompilation);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetTVShow(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string tvShow);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetTVNetwork(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string tvNetwork);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetTVEpisodeID(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string tvEpisodeId);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetTVSeason(IntPtr tags, IntPtr seasonNumber);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetTVEpisode(IntPtr tags, IntPtr episodeNumber);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetDescription(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string description);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetLongDescription(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string longDescription);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetLyrics(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string lyrics);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetSortName(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string sortName);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetSortArtist(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string sortArtist);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetSortAlbumArtist(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string sortAlbumArtist);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetSortAlbum(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string sortAlbum);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetSortComposer(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string sortComposer);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetSortTVShow(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string sortTVShow);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsAddArtwork(IntPtr tags, IntPtr artwork);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetArtwork(IntPtr tags, int index, IntPtr artwork);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsRemoveArtwork(IntPtr tags, int index);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetCopyright(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string copyright);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetEncodingTool(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string encodingTool);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetEncodedBy(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string encodedBy);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetPurchaseDate(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string purchaseDate);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetPodcast(IntPtr tags, IntPtr isPodcast);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetKeywords(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string podcastKeywords);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetCategory(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string podcastCategory);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetHDVideo(IntPtr tags, IntPtr isHDVideo);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetMediaType(IntPtr tags, IntPtr mediaType);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetContentRating(IntPtr tags, IntPtr contentRating);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetGapless(IntPtr tags, IntPtr isGapless);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetITunesAccount(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string iTunesAccount);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetITunesAccountType(IntPtr tags, IntPtr iTunesAccountType);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetITunesCountry(IntPtr tags, IntPtr iTunesAccountCountry);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetContentID(IntPtr tags, IntPtr contentId);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetArtistID(IntPtr tags, IntPtr artistId);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetPlaylistID(IntPtr tags, IntPtr playlistId);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetGenreID(IntPtr tags, IntPtr genreId);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetComposerID(IntPtr tags, IntPtr composerId);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4TagsSetXID(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string xid);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MP4ItmfGetItemsByMeaning(IntPtr file, [MarshalAs(UnmanagedType.LPStr)]string meaning, [MarshalAs(UnmanagedType.LPStr)]string name);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MP4ItmfItemListFree(IntPtr itemList);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr MP4ItmfItemAlloc(string code, int numData);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4ItmfAddItem(IntPtr hFile, IntPtr item);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4ItmfSetItem(IntPtr hFile, IntPtr item);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool MP4ItmfRemoveItem(IntPtr hFile, IntPtr item);

        // Models an iTMF data atom contained in an iTMF metadata item atom.
        // typedef struct MP4ItmfData_s
        // {
        //     uint8_t          typeSetIdentifier; /**< always zero. */
        //     MP4ItmfBasicType typeCode;          /**< iTMF basic type. */
        //     uint32_t         locale;            /**< always zero. */
        //     uint8_t*         value;             /**< may be NULL. */
        //     uint32_t         valueSize;         /**< value size in bytes. */
        // } MP4ItmfData;
        [StructLayout(LayoutKind.Sequential)]
        public struct MP4ItmfData
        {
            public byte typeSetIdentifier;
            public MP4ItmfBasicType typeCode;
            public int locale;
            public IntPtr value;
            public int valueSize;
        }

        // List of data. */
        // typedef struct MP4ItmfDataList_s
        // {
        //     MP4ItmfData* elements; /**< flat array. NULL when size is zero. */
        //     uint32_t     size;     /**< number of elements. */
        // } MP4ItmfDataList;
        [StructLayout(LayoutKind.Sequential)]
        public struct MP4ItmfDataList
        {
            [MarshalAs(UnmanagedType.ByValArray)]
            public IntPtr[] elements;
            public int size;
        }

        // Models an iTMF metadata item atom contained in an ilst atom.
        // typedef struct MP4ItmfItem_s
        // {
        //     void* __handle; /**< internal use only. */
        //
        //     char*           code;     /**< four-char code identifing atom type. NULL-terminated. */
        //     char*           mean;     /**< may be NULL. UTF-8 meaning. NULL-terminated. */
        //     char*           name;     /**< may be NULL. UTF-8 name. NULL-terminated. */
        //     MP4ItmfDataList dataList; /**< list of data. can be zero length. */
        // } MP4ItmfItem;
        [StructLayout(LayoutKind.Sequential)]
        public struct MP4ItmfItem
        {
            public IntPtr handle;
            public string code;
            public string mean;
            public string name;
            public MP4ItmfDataList dataList;
        }

        // List of items. */
        // typedef struct MP4ItmfItemList_s
        // {
        //     MP4ItmfItem* elements; /**< flat array. NULL when size is zero. */
        //     uint32_t     size;     /**< number of elements. */
        // } MP4ItmfItemList;
        [StructLayout(LayoutKind.Sequential)]
        public struct MP4ItmfItemList
        {
            [MarshalAs(UnmanagedType.ByValArray)]
            public IntPtr[] elements;
            public int size;
        }

        // Data object representing a single piece of artwork.
        // typedef struct MP4TagArtwork_s
        // {   void*             data; /**< raw picture data */
        //     uint32_t          size; /**< data size in bytes */
        //     MP4TagArtworkType type; /**< data type */
        // } MP4TagArtwork; 
        [StructLayout(LayoutKind.Sequential)]
        public struct MP4TagArtwork
        {
            public IntPtr data;
            public int size;
            public ArtworkType type;
        }

        // typedef struct MP4TagTrack_s
        // {
        //     uint16_t index;
        //     uint16_t total;
        // } MP4TagTrack;
        [StructLayout(LayoutKind.Sequential)]
        public struct MP4TagTrack
        {
            public short index;
            public short total;
        }

        // typedef struct MP4TagDisk_s
        // {
        //     uint16_t index;
        //     uint16_t total;
        // } MP4TagDisk;
        [StructLayout(LayoutKind.Sequential)]
        public struct MP4TagDisk
        {
            public short index;
            public short total;
        }

        // typedef struct MP4Tags_s
        // {
        //     void* __handle; /* internal use only */
        //
        //     const char*        name;
        //     const char*        artist;
        //     const char*        albumArtist; 
        //     const char*        album;
        //     const char*        grouping;
        //     const char*        composer;
        //     const char*        comments;
        //     const char*        genre;
        //     const uint16_t*    genreType;
        //     const char*        releaseDate;
        //     const MP4TagTrack* track;
        //     const MP4TagDisk*  disk;
        //     const uint16_t*    tempo;
        //     const uint8_t*     compilation;
        //
        //     const char*     tvShow;
        //     const char*     tvNetwork;
        //     const char*     tvEpisodeID;
        //     const uint32_t* tvSeason;
        //     const uint32_t* tvEpisode;
        //
        //     const char* description;
        //     const char* longDescription;
        //     const char* lyrics;
        //
        //     const char* sortName;
        //     const char* sortArtist;
        //     const char* sortAlbumArtist;
        //     const char* sortAlbum;
        //     const char* sortComposer;
        //     const char* sortTVShow;
        //
        //     const MP4TagArtwork* artwork;
        //     uint32_t             artworkCount;
        //
        //     const char* copyright;
        //     const char* encodingTool;
        //     const char* encodedBy;
        //     const char* purchaseDate;
        //
        //     const uint8_t* podcast;
        //     const char*    keywords;  /* TODO: Needs testing */
        //     const char*    category;    
        //
        //     const uint8_t* hdVideo;
        //     const uint8_t* mediaType;
        //     const uint8_t* contentRating;
        //     const uint8_t* gapless;
        //
        //     const char*     iTunesAccount;
        //     const uint8_t*  iTunesAccountType;
        //     const uint32_t* iTunesCountry;
        //     const uint32_t* contentID;
        //     const uint32_t* artistID;
        //     const uint64_t* playlistID;
        //     const uint32_t* genreID;
        //     const uint32_t* composerID;
        //     const char*     xid;
        // } MP4Tags;
        [StructLayout(LayoutKind.Sequential)]
        public struct MP4Tags
        {
            public IntPtr handle;
            public string name;
            public string artist;
            public string albumArtist;
            public string album;
            public string grouping;
            public string composer;
            public string comment;
            public string genre;
            public IntPtr genreType;
            public string releaseDate;
            public IntPtr track;
            public IntPtr disk;
            public IntPtr tempo;
            public IntPtr compilation;
            public string tvShow;
            public string tvNetwork;
            public string tvEpisodeID;
            public IntPtr tvSeason;
            public IntPtr tvEpisode;
            public string description;
            public string longDescription;
            public string lyrics;
            public string sortName;
            public string sortArtist;
            public string sortAlbumArtist;
            public string sortAlbum;
            public string sortComposer;
            public string sortTVShow;
            public IntPtr artwork;
            public int artworkCount;
            public string copyright;
            public string encodingTool;
            public string encodedBy;
            public string purchasedDate;
            public IntPtr podcast;
            public string keywords;
            public string category;
            public IntPtr hdVideo;
            public IntPtr mediaType;
            public IntPtr contentRating;
            public IntPtr gapless;
            public string itunesAccount;
            public IntPtr iTunesAccountType;
            public IntPtr iTunesCountry;
            public IntPtr contentID;
            public IntPtr artistID;
            public IntPtr playlistID;
            public IntPtr genreID;
            public IntPtr composerID;
            public string xid;
        }
    }
}
