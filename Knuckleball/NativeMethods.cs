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
using System.Diagnostics.CodeAnalysis;
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
        /// Invalid track ID
        /// </summary>
        internal const int MP4InvalidTrackId = 0;

        /// <summary>
        /// Od track type
        /// </summary>
        internal const string MP4OdTrackType = "odsm";

        /// <summary>
        /// Scene track type
        /// </summary>
        internal const string MP4SceneTrackType = "sdsm";

        /// <summary>
        /// Audio track type
        /// </summary>
        internal const string MP4AudioTrackType = "soun";

        /// <summary>
        /// Video track type
        /// </summary>
        internal const string MP4VideoTrackType = "vide";

        /// <summary>
        /// Hint track type
        /// </summary>
        internal const string MP4HintTrackType = "hint";

        /// <summary>
        /// Control track type
        /// </summary>
        internal const string MP4ControlTrackType = "cntl";

        /// <summary>
        /// Text track type
        /// </summary>
        internal const string MP4TextTrackType = "text";

        /// <summary>
        /// Subtitle track type
        /// </summary>
        internal const string MP4SubtitleTrackType = "sbtl";

        /// <summary>
        /// Sub-picture track type
        /// </summary>
        internal const string MP4SubpictureTrackType = "subp";

        /// <summary>
        /// Prevents a default instance of the <see cref="NativeMethods"/> class from being created.
        /// </summary>
        private NativeMethods()
        {
        }

        /// <summary>
        /// Represents the iTunes Metadata Format basic types.
        /// </summary>
        /// <remarks>
        /// These values are taken from the MP4V2 header files, documented thus:
        /// <para>
        /// <code>
        /// Basic types of value data as enumerated in spec. */
        /// typedef enum MP4ItmfBasicType_e
        /// {
        ///     MP4_ITMF_BT_IMPLICIT  = 0,   /** for use with tags for which no type needs to be indicated */
        ///     MP4_ITMF_BT_UTF8      = 1,   /** without any count or null terminator */
        ///     MP4_ITMF_BT_UTF16     = 2,   /** also known as UTF-16BE */
        ///     MP4_ITMF_BT_SJIS      = 3,   /** deprecated unless it is needed for special Japanese characters */
        ///     MP4_ITMF_BT_HTML      = 6,   /** the HTML file header specifies which HTML version */
        ///     MP4_ITMF_BT_XML       = 7,   /** the XML header must identify the DTD or schemas */
        ///     MP4_ITMF_BT_UUID      = 8,   /** also known as GUID; stored as 16 bytes in binary (valid as an ID) */
        ///     MP4_ITMF_BT_ISRC      = 9,   /** stored as UTF-8 text (valid as an ID) */
        ///     MP4_ITMF_BT_MI3P      = 10,  /** stored as UTF-8 text (valid as an ID) */
        ///     MP4_ITMF_BT_GIF       = 12,  /** (deprecated) a GIF image */
        ///     MP4_ITMF_BT_JPEG      = 13,  /** a JPEG image */
        ///     MP4_ITMF_BT_PNG       = 14,  /** a PNG image */
        ///     MP4_ITMF_BT_URL       = 15,  /** absolute, in UTF-8 characters */
        ///     MP4_ITMF_BT_DURATION  = 16,  /** in milliseconds, 32-bit integer */
        ///     MP4_ITMF_BT_DATETIME  = 17,  /** in UTC, counting seconds since midnight, January 1, 1904; 32 or 64-bits */
        ///     MP4_ITMF_BT_GENRES    = 18,  /** a list of enumerated values */
        ///     MP4_ITMF_BT_INTEGER   = 21,  /** a signed big-endian integer with length one of { 1,2,3,4,8 } bytes */
        ///     MP4_ITMF_BT_RIAA_PA   = 24,  /** RIAA parental advisory; { -1=no, 1=yes, 0=unspecified }, 8-bit ingteger */
        ///     MP4_ITMF_BT_UPC       = 25,  /** Universal Product Code, in text UTF-8 format (valid as an ID) */
        ///     MP4_ITMF_BT_BMP       = 27,  /** Windows bitmap image */
        ///     MP4_ITMF_BT_UNDEFINED = 255  /** undefined */
        /// } MP4ItmfBasicType;
        /// </code>
        /// </para>
        /// </remarks>
        internal enum MP4ItmfBasicType
        {
            /// <summary>
            /// For use with tags for which no type needs to be indicated.
            /// </summary>
            Implicit = 0,

            /// <summary>
            /// Without any count or null terminator
            /// </summary>
            Utf8 = 1,

            /// <summary>
            /// Also known as UTF-16BE 
            /// </summary>
            Utf16 = 2,

            /// <summary>
            /// Deprecated unless it is needed for special Japanese characters
            /// </summary>
            Sjis = 3,

            /// <summary>
            /// The HTML file header specifies which HTML version
            /// </summary>
            Html = 6,

            /// <summary>
            /// The XML header must identify the DTD or schemas
            /// </summary>
            Xml = 7,

            /// <summary>
            /// Also known as GUID; stored as 16 bytes in binary (valid as an ID) 
            /// </summary>
            Uuid = 8,

            /// <summary>
            /// stored as UTF-8 text (valid as an ID)
            /// </summary>
            Isrc = 9,

            /// <summary>
            /// stored as UTF-8 text (valid as an ID)
            /// </summary>
            Mi3p = 10,

            /// <summary>
            /// (deprecated) a GIF image
            /// </summary>
            Gif = 12,

            /// <summary>
            /// a JPEG image
            /// </summary>
            Jpeg = 13,

            /// <summary>
            /// A PNG image
            /// </summary>
            Png = 14,

            /// <summary>
            /// absolute, in UTF-8 characters 
            /// </summary>
            Url = 15,

            /// <summary>
            /// in milliseconds, 32-bit integer 
            /// </summary>
            Duration = 16,

            /// <summary>
            /// in UTC, counting seconds since midnight, January 1, 1904; 32 or 64-bits
            /// </summary>
            DateTime = 17,

            /// <summary>
            /// a list of enumerated values
            /// </summary>
            Genres = 18,

            /// <summary>
            /// a signed big-endian integer with length one of { 1,2,3,4,8 } bytes
            /// </summary>
            Integer = 21,

            /// <summary>
            /// RIAA parental advisory; { -1=no, 1=yes, 0=unspecified }, 8-bit integer
            /// </summary>
            Riaa_pa = 24,

            /// <summary>
            /// Universal Product Code, in text UTF-8 format (valid as an ID) 
            /// </summary>
            Upc = 25,

            /// <summary>
            /// A Windows bitmap image
            /// </summary>
            Bmp = 27,

            /// <summary>
            /// An undefined value
            /// </summary>
            Undefined = 255
        }

        /// <summary>
        /// Represents the type of image used for artwork.
        /// </summary>
        /// <remarks>
        /// These values are taken from the MP4V2 header files, documented thus:
        /// <para>
        /// <code>
        /// typedef enum MP4TagArtworkType_e
        /// {
        ///      MP4_ART_UNDEFINED = 0,
        ///      MP4_ART_BMP       = 1,
        ///      MP4_ART_GIF       = 2,
        ///      MP4_ART_JPEG      = 3,
        ///      MP4_ART_PNG       = 4
        ///  } MP4TagArtworkType;
        /// </code>
        /// </para>
        /// </remarks>
        internal enum ArtworkType
        {
            /// <summary>
            /// Undefined image type
            /// </summary>
            Undefined = 0,

            /// <summary>
            /// A bitmap image
            /// </summary>
            Bmp = 1,

            /// <summary>
            /// A GIF image
            /// </summary>
            Gif = 2,

            /// <summary>
            /// A JPEG image
            /// </summary>
            Jpeg = 3,

            /// <summary>
            /// A PNG image
            /// </summary>
            Png = 4
        }

        /// <summary>
        /// Represents the known types used for chapters.
        /// </summary>
        /// <remarks>
        /// These values are taken from the MP4V2 header files, documented thus:
        /// <para>
        /// <code>
        /// typedef enum {
        ///     MP4ChapterTypeNone = 0,
        ///     MP4ChapterTypeAny  = 1,
        ///     MP4ChapterTypeQt   = 2,
        ///     MP4ChapterTypeNero = 4 
        /// } MP4ChapterType;
        /// </code>
        /// </para>
        /// </remarks>
        internal enum MP4ChapterType
        {
            /// <summary>
            /// No chapters found return value
            /// </summary>
            None = 0,

            /// <summary>
            /// Any or all known chapter types
            /// </summary>
            Any = 1,

            /// <summary>
            /// QuickTime chapter type
            /// </summary>
            Qt = 2,

            /// <summary>
            /// Nero chapter type
            /// </summary>
            Nero = 4
        }

        /// <summary>
        /// Values representing the time scale for a track
        /// </summary>
        internal enum MP4TimeScale
        {
            /// <summary>
            /// Track duration is measured in seconds.
            /// </summary>
            Seconds = 1,

            /// <summary>
            /// Track duration is measured in milliseconds.
            /// </summary>
            Milliseconds = 1000,

            /// <summary>
            /// Track duration is measured in microseconds.
            /// </summary>
            Microseconds = 1000000,

            /// <summary>
            /// Track duration is measured in nanoseconds.
            /// </summary>
            Nanoseconds = 100000000
        }

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr MP4TagsAlloc();

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsFetch(IntPtr tags, IntPtr file);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsStore(IntPtr tags, IntPtr file);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void MP4TagsFree(IntPtr tags);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr MP4Read([MarshalAs(UnmanagedType.LPStr)]string fileName);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr MP4Modify([MarshalAs(UnmanagedType.LPStr)]string fileName, int flags);

        //// Commenting this API declaration. It isn't called yet, but may be in the future.
        //// [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        //// [return: MarshalAs(UnmanagedType.U1)]
        //// internal static extern bool MP4Optimize([MarshalAs(UnmanagedType.LPStr)]string fileName, [MarshalAs(UnmanagedType.LPStr)]string newName);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void MP4Close(IntPtr file);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void MP4Free(IntPtr pointer);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetName(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string name);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetArtist(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string artist);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetAlbumArtist(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string albumArtist);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetAlbum(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string album);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetGrouping(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string grouping);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetComposer(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string composer);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetComments(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string comments);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetGenre(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string genre);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetGenreType(IntPtr tags, IntPtr genreType);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetReleaseDate(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string releaseDate);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetTrack(IntPtr tags, IntPtr trackInfo);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetDisk(IntPtr tags, IntPtr discInfo);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetTempo(IntPtr tags, IntPtr tempo);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetCompilation(IntPtr tags, IntPtr isCompilation);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetTVShow(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string tvShow);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetTVNetwork(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string tvNetwork);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetTVEpisodeID(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string tvEpisodeId);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetTVSeason(IntPtr tags, IntPtr seasonNumber);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetTVEpisode(IntPtr tags, IntPtr episodeNumber);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetDescription(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string description);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetLongDescription(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string longDescription);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetLyrics(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string lyrics);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetSortName(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string sortName);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetSortArtist(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string sortArtist);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetSortAlbumArtist(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string sortAlbumArtist);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetSortAlbum(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string sortAlbum);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetSortComposer(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string sortComposer);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetSortTVShow(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string sortTVShow);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsAddArtwork(IntPtr tags, IntPtr artwork);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetArtwork(IntPtr tags, int index, IntPtr artwork);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsRemoveArtwork(IntPtr tags, int index);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetCopyright(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string copyright);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetEncodingTool(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)] string encodingTool);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetEncodedBy(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string encodedBy);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetPurchaseDate(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string purchaseDate);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetPodcast(IntPtr tags, IntPtr isPodcast);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetKeywords(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string podcastKeywords);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetCategory(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string podcastCategory);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetHDVideo(IntPtr tags, IntPtr isHDVideo);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetMediaType(IntPtr tags, IntPtr mediaType);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetContentRating(IntPtr tags, IntPtr contentRating);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetGapless(IntPtr tags, IntPtr isGapless);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetITunesAccount(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string iTunesAccount);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetITunesAccountType(IntPtr tags, IntPtr iTunesAccountType);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetITunesCountry(IntPtr tags, IntPtr iTunesAccountCountry);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetContentID(IntPtr tags, IntPtr contentId);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetArtistID(IntPtr tags, IntPtr artistId);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetPlaylistID(IntPtr tags, IntPtr playlistId);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetGenreID(IntPtr tags, IntPtr genreId);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetComposerID(IntPtr tags, IntPtr composerId);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4TagsSetXID(IntPtr tags, [MarshalAs(UnmanagedType.LPStr)]string xid);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr MP4ItmfGetItemsByMeaning(IntPtr file, [MarshalAs(UnmanagedType.LPStr)]string meaning, [MarshalAs(UnmanagedType.LPStr)]string name);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void MP4ItmfItemListFree(IntPtr itemList);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr MP4ItmfItemAlloc([MarshalAs(UnmanagedType.LPStr)]string code, int numData);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4ItmfAddItem(IntPtr hFile, IntPtr item);

        //// Commenting this API declaration. It isn't called yet, but may be in the future.
        //// [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        //// [return: MarshalAs(UnmanagedType.U1)]
        //// internal static extern bool MP4ItmfSetItem(IntPtr hFile, IntPtr item);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static extern bool MP4ItmfRemoveItem(IntPtr hFile, IntPtr item);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int MP4GetNumberOfTracks(IntPtr hFile, [MarshalAs(UnmanagedType.LPStr)]string type, byte subType);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, BestFitMapping = false, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int MP4FindTrackId(IntPtr hFile, short index, [MarshalAs(UnmanagedType.LPStr)]string type, byte subType);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern string MP4GetTrackType(IntPtr hFile, int trackId);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern long MP4ConvertFromTrackDuration(IntPtr hFile, int trackId, long duration, MP4TimeScale timeScale);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern long MP4GetTrackDuration(IntPtr hFile, int trackId);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I4)]
        internal static extern MP4ChapterType MP4GetChapters(IntPtr hFile, ref IntPtr chapterList, ref int chapterCount, MP4ChapterType chapterType);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I4)]
        internal static extern MP4ChapterType MP4SetChapters(IntPtr hFile, [In, Out]MP4Chapter[] chapterList, int chapterCount, MP4ChapterType chapterType);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern long MP4GetDuration(IntPtr hFile);

        [DllImport("libMP4V2.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int MP4GetTimeScale(IntPtr hFile);

        /// <summary>
        /// Models an iTunes Metadata Format data atom contained in an iTMF metadata item atom.
        /// </summary>
        /// <remarks>
        /// This structure definition is taken from the MP4V2 header files, documented thus:
        /// <para>
        /// <code>
        /// typedef struct MP4ItmfData_s
        /// {
        ///     uint8_t          typeSetIdentifier; /** always zero. */
        ///     MP4ItmfBasicType typeCode;          /** iTMF basic type. */
        ///     uint32_t         locale;            /** always zero. */
        ///     uint8_t*         value;             /** may be NULL. */
        ///     uint32_t         valueSize;         /** value size in bytes. */
        /// } MP4ItmfData;
        /// </code>
        /// </para>
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MP4ItmfData
        {
            /// <summary>
            /// Always zero
            /// </summary>
            internal byte typeSetIdentifier;

            /// <summary>
            /// Basic type of data
            /// </summary>
            internal MP4ItmfBasicType typeCode;

            /// <summary>
            /// Always zero
            /// </summary>
            internal int locale;

            /// <summary>
            /// Value of the data, may be NULL (<see cref="IntPtr.Zero"/>)
            /// </summary>
            internal IntPtr value;

            /// <summary>
            /// Value size in bytes.
            /// </summary>
            internal int valueSize;
        }

        /// <summary>
        /// Represents a list of data in an atom.
        /// </summary>
        /// <remarks>
        /// This structure definition is taken from the MP4V2 header files, documented thus:
        /// <para>
        /// <code>
        /// List of data. */
        /// typedef struct MP4ItmfDataList_s
        /// {
        ///     MP4ItmfData* elements; /** flat array. NULL when size is zero. */
        ///     uint32_t     size;     /** number of elements. */
        /// } MP4ItmfDataList;
        /// </code>
        /// </para>
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MP4ItmfDataList
        {
            /// <summary>
            /// flat array. NULL when size is zero.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray)]
            internal IntPtr[] elements;

            /// <summary>
            /// number of elements.
            /// </summary>
            internal int size;
        }

        /// <summary>
        /// Models an iTMF metadata item atom contained in an iTunes atom.
        /// </summary>
        /// <remarks>
        /// This structure definition is taken from the MP4V2 header files, documented thus:
        /// <para>
        /// <code>
        /// typedef struct MP4ItmfItem_s
        /// {
        ///     void* __handle; /** internal use only. */
        ///
        ///     char*           code;     /** four-char code identifing atom type. NULL-terminated. */
        ///     char*           mean;     /** may be NULL. UTF-8 meaning. NULL-terminated. */
        ///     char*           name;     /** may be NULL. UTF-8 name. NULL-terminated. */
        ///     MP4ItmfDataList dataList; /** list of data. can be zero length. */
        /// } MP4ItmfItem;
        /// </code>
        /// </para>
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MP4ItmfItem
        {
            /// <summary>
            /// internal use only.
            /// </summary>
            internal IntPtr handle;

            /// <summary>
            /// four-char code identifying atom type. NULL-terminated.
            /// </summary>
            internal string code;

            /// <summary>
            /// may be NULL. UTF-8 meaning. NULL-terminated.
            /// </summary>
            internal string mean;
            
            /// <summary>
            /// may be NULL. UTF-8 name. NULL-terminated.
            /// </summary>
            internal string name;
            
            /// <summary>
            /// list of data. can be zero length.
            /// </summary>
            internal MP4ItmfDataList dataList;
        }

        /// <summary>
        /// List of items.
        /// </summary>
        /// <remarks>
        /// This structure definition is taken from the MP4V2 header files, documented thus:
        /// <para>
        /// <code>
        /// typedef struct MP4ItmfItemList_s
        /// {
        ///     MP4ItmfItem* elements; /** flat array. NULL when size is zero. */
        ///     uint32_t     size;     /** number of elements. */
        /// } MP4ItmfItemList;
        /// </code>
        /// </para>
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MP4ItmfItemList
        {
            /// <summary>
            /// flat array. NULL when size is zero.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray)]
            internal IntPtr[] elements;
            
            /// <summary>
            /// number of elements.
            /// </summary>
            internal int size;
        }

        /// <summary>
        /// Data object representing a single piece of artwork.
        /// </summary>
        /// <remarks>
        /// This structure definition is taken from the MP4V2 header files, documented thus:
        /// <para>
        /// <code>
        /// typedef struct MP4TagArtwork_s
        /// {   void*             data; /** raw picture data */
        ///     uint32_t          size; /** data size in bytes */
        ///     MP4TagArtworkType type; /** data type */
        /// } MP4TagArtwork; 
        /// </code>
        /// </para>
        /// </remarks>
        [SuppressMessage("Microsoft.Design", "CA1049:TypesThatOwnNativeResourcesShouldBeDisposable", Justification = "The MP4TagArtwork struct is used only with Marshal.AllocHGlobal/FreeHGlobal pairs.")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct MP4TagArtwork
        {
            /// <summary>
            /// raw picture data
            /// </summary>
            internal IntPtr data;

            /// <summary>
            /// data size in bytes
            /// </summary>
            internal int size;

            /// <summary>
            /// data type
            /// </summary>
            internal ArtworkType type;
        }

        /// <summary>
        /// Represents information about the tracks for this file
        /// </summary>
        /// <remarks>
        /// This structure definition is taken from the MP4V2 header files, documented thus:
        /// <para>
        /// <code>
        /// typedef struct MP4TagTrack_s
        /// {
        ///     uint16_t index;
        ///     uint16_t total;
        /// } MP4TagTrack;
        /// </code>
        /// </para>
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MP4TagTrack
        {
            /// <summary>
            /// Track number
            /// </summary>
            internal short index;

            /// <summary>
            /// Total number of tracks.
            /// </summary>
            internal short total;
        }

        /// <summary>
        /// Represents information about the discs for this file
        /// </summary>
        /// <remarks>
        /// This structure definition is taken from the MP4V2 header files, documented thus:
        /// <para>
        /// <code>
        /// typedef struct MP4TagDisk_s
        /// {
        ///     uint16_t index;
        ///     uint16_t total;
        /// } MP4TagDisk;
        /// </code>
        /// </para>
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MP4TagDisk
        {
            /// <summary>
            /// Disc number
            /// </summary>
            internal short index;

            /// <summary>
            /// Total number of discs
            /// </summary>
            internal short total;
        }

        /// <summary>
        /// Represents information for a chapter in this file.
        /// </summary>
        /// <remarks>
        /// This structure definition is taken from the MP4V2 header files, documented thus:
        /// <para>
        /// <code>
        /// #define MP4V2_CHAPTER_TITLE_MAX 1023
        ///
        /// typedef struct MP4Chapter_s {
        ///     MP4Duration duration;
        ///     char title[MP4V2_CHAPTER_TITLE_MAX+1];
        /// } MP4Chapter_t;
        /// </code>
        /// </para>
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MP4Chapter
        {
            /// <summary>
            /// Duration of chapter in milliseconds
            /// </summary>
            internal long duration;

            /// <summary>
            /// Title of chapter
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            internal byte[] title;
        }

        /// <summary>
        /// The main structure containing all of the tags for the file.
        /// </summary>
        /// <remarks>
        /// This structure definition is taken from the MP4V2 header files, documented thus:
        /// <para>
        /// <code>
        /// typedef struct MP4Tags_s
        /// {
        ///     void* __handle; /* internal use only */
        ///
        ///     const char*        name;
        ///     const char*        artist;
        ///     const char*        albumArtist; 
        ///     const char*        album;
        ///     const char*        grouping;
        ///     const char*        composer;
        ///     const char*        comments;
        ///     const char*        genre;
        ///     const uint16_t*    genreType;
        ///     const char*        releaseDate;
        ///     const MP4TagTrack* track;
        ///     const MP4TagDisk*  disk;
        ///     const uint16_t*    tempo;
        ///     const uint8_t*     compilation;
        ///
        ///     const char*     tvShow;
        ///     const char*     tvNetwork;
        ///     const char*     tvEpisodeID;
        ///     const uint32_t* tvSeason;
        ///     const uint32_t* tvEpisode;
        ///
        ///     const char* description;
        ///     const char* longDescription;
        ///     const char* lyrics;
        ///
        ///     const char* sortName;
        ///     const char* sortArtist;
        ///     const char* sortAlbumArtist;
        ///     const char* sortAlbum;
        ///     const char* sortComposer;
        ///     const char* sortTVShow;
        ///
        ///     const MP4TagArtwork* artwork;
        ///     uint32_t             artworkCount;
        ///
        ///     const char* copyright;
        ///     const char* encodingTool;
        ///     const char* encodedBy;
        ///     const char* purchaseDate;
        ///
        ///     const uint8_t* podcast;
        ///     const char*    keywords;  /* TODO: Needs testing */
        ///     const char*    category;    
        ///
        ///     const uint8_t* hdVideo;
        ///     const uint8_t* mediaType;
        ///     const uint8_t* contentRating;
        ///     const uint8_t* gapless;
        ///
        ///     const char*     iTunesAccount;
        ///     const uint8_t*  iTunesAccountType;
        ///     const uint32_t* iTunesCountry;
        ///     const uint32_t* contentID;
        ///     const uint32_t* artistID;
        ///     const uint64_t* playlistID;
        ///     const uint32_t* genreID;
        ///     const uint32_t* composerID;
        ///     const char*     xid;
        /// } MP4Tags;
        /// </code>
        /// </para>
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MP4Tags
        {
            /// <summary>
            /// Internal handle.
            /// </summary>
            internal IntPtr handle;

            /// <summary>
            /// Name of the file.
            /// </summary>
            internal string name;

            /// <summary>
            /// Artist for the file.
            /// </summary>
            internal string artist;

            /// <summary>
            /// Album artist for the file.
            /// </summary>
            internal string albumArtist;

            /// <summary>
            /// Album for the file.
            /// </summary>
            internal string album;

            /// <summary>
            /// Grouping for the file.
            /// </summary>
            internal string grouping;

            /// <summary>
            /// Composer for the file.
            /// </summary>
            internal string composer;

            /// <summary>
            /// Comment for the file.
            /// </summary>
            internal string comment;

            /// <summary>
            /// Genre for the file.
            /// </summary>
            internal string genre;

            /// <summary>
            /// Pointer to the genre type for the file.
            /// </summary>
            internal IntPtr genreType;

            /// <summary>
            /// Release data for the file.
            /// </summary>
            internal string releaseDate;

            /// <summary>
            /// Pointer to the track information about the file.
            /// </summary>
            internal IntPtr track;

            /// <summary>
            /// Pointer to the disc information about the file.
            /// </summary>
            internal IntPtr disk;

            /// <summary>
            /// Pointer to the tempo.
            /// </summary>
            internal IntPtr tempo;

            /// <summary>
            /// Pointer to the "isCompilation" value.
            /// </summary>
            internal IntPtr compilation;

            /// <summary>
            /// Pointer to the TV show name.
            /// </summary>
            internal string tvShow;

            /// <summary>
            /// Pointer to the TV network.
            /// </summary>
            internal string tvNetwork;

            /// <summary>
            /// Pointer to the TV episode ID.
            /// </summary>
            internal string tvEpisodeID;

            /// <summary>
            /// Pointer to the season number
            /// </summary>
            internal IntPtr tvSeason;

            /// <summary>
            /// Pointer to the episode number.
            /// </summary>
            internal IntPtr tvEpisode;

            /// <summary>
            /// Description of the file
            /// </summary>
            internal string description;

            /// <summary>
            /// Long description of the file
            /// </summary>
            internal string longDescription;

            /// <summary>
            /// Lyrics of the file
            /// </summary>
            internal string lyrics;

            /// <summary>
            /// Sort name of the file
            /// </summary>
            internal string sortName;

            /// <summary>
            /// Sort artist of the file 
            /// </summary>
            internal string sortArtist;
            
            /// <summary>
            /// Sort album artist of the file
            /// </summary>
            internal string sortAlbumArtist;

            /// <summary>
            /// Sort album of the file
            /// </summary>
            internal string sortAlbum;

            /// <summary>
            /// Sort composer of the file
            /// </summary>
            internal string sortComposer;

            /// <summary>
            /// Sort TV show of the file.
            /// </summary>
            internal string sortTVShow;

            /// <summary>
            /// Pointer to the artwork in the file.
            /// </summary>
            internal IntPtr artwork;

            /// <summary>
            /// The artwork count in the file.
            /// </summary>
            internal int artworkCount;

            /// <summary>
            /// Copyright in the file.
            /// </summary>
            internal string copyright;

            /// <summary>
            /// Encoding tool used for the file.
            /// </summary>
            internal string encodingTool;

            /// <summary>
            /// Encoded by information for the file
            /// </summary>
            internal string encodedBy;

            /// <summary>
            /// Purchase date for the file.
            /// </summary>
            internal string purchasedDate;

            /// <summary>
            /// Pointer to the "isPodcast" value for the file.
            /// </summary>
            internal IntPtr podcast;

            /// <summary>
            /// Podcast keywords for the file
            /// </summary>
            internal string keywords;

            /// <summary>
            /// Podcast category for the file.
            /// </summary>
            internal string category;

            /// <summary>
            /// Pointer to the "isHDVideo" value for the file.
            /// </summary>
            internal IntPtr hdVideo;

            /// <summary>
            /// Pointer to the media type for the file
            /// </summary>
            internal IntPtr mediaType;

            /// <summary>
            /// Pointer to the content rating for the file
            /// </summary>
            internal IntPtr contentRating;

            /// <summary>
            /// Pointer to the "isGapless" value for the file.
            /// </summary>
            internal IntPtr gapless;

            /// <summary>
            /// iTunes account used to purchase the file.
            /// </summary>
            internal string itunesAccount;

            /// <summary>
            /// Pointer to the type of iTunes account used to purchase the file.
            /// </summary>
            internal IntPtr iTunesAccountType;

            /// <summary>
            /// Pointer to the country for the iTunes account used to purchase the file.
            /// </summary>
            internal IntPtr iTunesCountry;

            /// <summary>
            /// Pointer to the content ID of the file.
            /// </summary>
            internal IntPtr contentID;

            /// <summary>
            /// Pointer to the artist ID of the file.
            /// </summary>
            internal IntPtr artistID;

            /// <summary>
            /// Pointer to the playlist ID of the file.
            /// </summary>
            internal IntPtr playlistID;

            /// <summary>
            /// Pointer to the genre ID of the file.
            /// </summary>
            internal IntPtr genreID;

            /// <summary>
            /// Pointer to the composer ID of the file.
            /// </summary>
            internal IntPtr composerID;

            /// <summary>
            /// Auxiliary ID of the file.
            /// </summary>
            internal string xid;
        }
    }
}
