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

namespace MP4V2.NET
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MP4File
    {
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
        public int TrackNumber { get; set; }
        public int TotalTracks { get; set; }
        public int DiskNumber { get; set; }
        public int TotalDisks { get; set; }
        public short Tempo { get; set; }
        public byte Compilation { get; set; }
        public string TVShow { get; set; }
        public string TVNetwork { get; set; }
        public string EpisodeID { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }
        public string Lyrics { get; set; }
        public string SortName { get; set; }
        public string SortArtist { get; set; }
        public string SortAlbumArtist { get; set; }
        public string SortAlbum { get; set; }
        public string SortComposer { get; set; }
        public string SortTVShow { get; set; }
        public Image Artwork { get; set; }
        public int ArtworkCount { get; set; }
        public string Copyright { get; set; }
        public string EncodingTool { get; set; }
        public string EncodedBy { get; set; }
        public string PurchasedDate { get; set; }
        public byte Podcast { get; set; }
        public string Keywords { get; set; }
        public string Category { get; set; }
        public byte HDVideo { get; set; }
        public byte MediaType { get; set; }
        public byte ContentRating { get; set; }
        public byte Gapless { get; set; }
        public string MediaStoreAccount { get; set; }
        public byte MediaStoreAccountType { get; set; }
        public int MediaStoreCountry { get; set; }
        public int ContentID { get; set; }
        public int ArtistID { get; set; }
        public long PlaylistID { get; set; }
        public int GenreID { get; set; }
        public int ComposerID { get; set; }
        public string Xid { get; set; }

        public void ReadTags()
        {
        }

        public void WriteTags()
        {
        }

        private T ConvertStructure<T>(IntPtr structPtr)
        {
            return (T)Marshal.PtrToStructure(structPtr, typeof(T));
        }
    }
}
