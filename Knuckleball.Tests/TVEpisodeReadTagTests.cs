// -----------------------------------------------------------------------
// <copyright file="TVEpisodeTests.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NUnit.Framework;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Imaging;

namespace Knuckleball.Tests
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class TVEpisodeReadTagTests
    {
        MP4File file = null;

        [TestFixtureSetUp]
        public void SetUp()
        {
            string directory = TestFileUtilities.GetTestFileDirectory();
            string fileName = Path.Combine(directory, "TVEpisode.m4v");
            this.file = new MP4File(fileName);
            this.file.ReadTags();
        }

        [Test]
        public void ShouldReadTitle()
        {
            Assert.AreEqual("The PuzzleMaster Strikes Back", file.Title);
        }

        [Test]
        public void ShouldReadArtist()
        {
            Assert.AreEqual("PuzzleQuest", file.Artist);
        }

        [Test]
        public void ShouldReadAlbum()
        {
            // TODO: Fix the tags in the TV Episode file
            // Assert.AreEqual("PuzzleQuest, Season 2", file.Album);
            Assert.AreEqual("PuzzleQuest", file.Album);
        }

        [Test]
        public void ShouldReadAlbumArtist()
        {
            Assert.AreEqual("PuzzleQuest", file.AlbumArtist);
        }

        [Test]
        public void ShouldReadGrouping()
        {
            Assert.IsNull(file.Grouping);
        }

        [Test]
        public void ShouldReadComposer()
        {
            Assert.IsNull(file.Composer);
        }

        [Test]
        public void ShouldReadComment()
        {
            Assert.IsNull(file.Comment);
        }

        [Test]
        public void ShouldReadGenre()
        {
            Assert.AreEqual("Drama", file.Genre);
        }

        [Test]
        public void ShouldReadTrackNumber()
        {
            Assert.AreEqual(3, file.TrackNumber);
        }

        [Test]
        public void ShouldReadTotalTracks()
        {
            Assert.AreEqual(0, file.TotalTracks);
        }

        [Test]
        public void ShouldReadDiskNumber()
        {
            Assert.AreEqual(1, file.DiskNumber);
        }

        [Test]
        public void ShouldReadTotalDisks()
        {
            Assert.AreEqual(1, file.TotalDisks);
        }

        [Test]
        public void ShouldReadTempo()
        {
            Assert.IsNull(file.Tempo);
        }

        [Test]
        public void ShouldReadIsCompilation()
        {
            Assert.IsFalse(file.IsCompilation.Value);
        }

        [Test]
        public void ShouldReadDescription()
        {
            Assert.AreEqual("The PuzzleMaster returns to show the world he was not defeated.", file.Description);
        }

        [Test]
        public void ShouldReadLongDescription()
        {
            Assert.AreEqual("The PuzzleMaster returns to show the world he was not defeated. Planning new diabolical puzzles for the user to find, He has kidnapped the beloved Jimmy Bear.", file.LongDescription);
        }

        [Test]
        public void ShouldReadLyrics()
        {
            Assert.IsNull(file.Lyrics);
        }

        [Test]
        public void ShouldReadSortName()
        {
            Assert.AreEqual("PuzzleMaster Strikes Back", file.SortName);
        }

        [Test]
        public void ShouldReadSortArtist()
        {
            Assert.IsNull(file.SortArtist);
        }

        [Test]
        public void ShouldReadSortAlbum()
        {
            Assert.AreEqual("PuzzleQuest, Season 2", file.SortAlbum);
        }

        [Test]
        public void ShouldReadSortAlbumArtist()
        {
            Assert.IsNull(file.SortAlbumArtist);
        }

        [Test]
        public void ShouldReadSortComposer()
        {
            Assert.IsNull(file.SortComposer);
        }

        [Test]
        public void ShouldReadSortTVShow()
        {
            Assert.IsNull(file.SortTVShow);
        }

        [Test]
        public void ShouldReadArtworkCount()
        {
            Assert.AreEqual(1, file.ArtworkCount);
        }

        [Test]
        public void ShouldReadArtwork()
        {
            Assert.AreEqual(file.ArtworkFormat, ImageFormat.Png);
            Assert.AreEqual("c1b2758c370db440aa48ff3a7519ff24", ComputeHash(file.Artwork, file.ArtworkFormat));
        }

        [Test]
        public void ShouldReadCopyright()
        {
            Assert.IsNull(file.Copyright);
        }

        [Test]
        public void ShouldReadEncodingTool()
        {
            Assert.AreEqual("HandBrake 0.9.3 2008112300", file.EncodingTool);
        }

        [Test]
        public void ShouldReadEncodedBy()
        {
            Assert.IsNull(file.EncodedBy);
        }

        [Test]
        public void ShouldReadIsPodcast()
        {
            Assert.IsNull(file.IsPodcast);
        }

        [Test]
        public void ShouldReadKeywords()
        {
            Assert.IsNull(file.Keywords);
        }

        [Test]
        public void ShouldReadCategory()
        {
            Assert.IsNull(file.Category);
        }

        [Test]
        public void ShouldReadIsHDVideo()
        {
            Assert.IsNull(file.IsHDVideo);
        }

        [Test]
        public void ShouldReadMediaType()
        {
            Assert.AreEqual(MediaKind.TVShow, file.MediaType);
        }

        [Test]
        public void ShouldReadContentRating()
        {
            Assert.AreEqual(ContentRating.NotSet, file.ContentRating);
        }

        [Test]
        public void ShouldReadIsGapless()
        {
            Assert.False(file.IsGapless.Value);
        }
        
        [Test]
        public void ShouldReadMediaStoreAccount()
        {
            Assert.IsNull(file.MediaStoreAccount);
        }
        
        [Test]
        public void ShouldReadMediaStoreAccountType()
        {
            Assert.AreEqual(MediaStoreAccountKind.NotSet, file.MediaStoreAccountType);
        }
        
        [Test]
        public void ShouldReadMediaStoreCountry()
        {
            Assert.AreEqual(Country.NotSet, file.MediaStoreCountry);
        }
        
        [Test]
        public void ShouldReadContentID()
        {
            Assert.IsNull(file.ContentID);
        }
        
        [Test]
        public void ShouldReadArtistID()
        {
            Assert.IsNull(file.ArtistID);
        }
        
        [Test]
        public void ShouldReadPlaylistID()
        {
            Assert.IsNull(file.PlaylistID);
        }
        
        [Test]
        public void ShouldReadGenreID()
        {
            Assert.IsNull(file.GenreID);
        }
        
        [Test]
        public void ShouldReadComposerID()
        {
            Assert.IsNull(file.ComposerID);
        }
        
        [Test]
        public void ShouldReadXid()
        {
            Assert.IsNull(file.Xid);
        }

        [Test]
        public void ShouldReadRatingInfo()
        {
            Assert.AreEqual("us-tv", file.RatingInfo.RatingSource);
            Assert.AreEqual("TV-14", file.RatingInfo.Rating);
            Assert.AreEqual(500, file.RatingInfo.SortValue);
            Assert.IsNullOrEmpty(file.RatingInfo.RatingAnnotation);
            Assert.AreEqual("us-tv|TV-14|500|", file.RatingInfo.ToString());
        }

        [Test]
        public void ShouldReadMovieInfo()
        {
            Assert.IsNull(file.MovieInfo.Cast);
            Assert.IsNull(file.MovieInfo.Directors);
            Assert.IsNull(file.MovieInfo.Producers);
            Assert.IsNull(file.MovieInfo.Screenwriters);
            Assert.IsNull(file.MovieInfo.Studio);
        }

        // The following are tags unique to TV Episodes

        [Test]
        public void ShouldReadEpisodeNumber()
        {
            Assert.AreEqual(3, file.EpisodeNumber);
        }

        [Test]
        public void ShouldReadSeasonNumber()
        {
            Assert.AreEqual(2, file.SeasonNumber);
        }

        [Test]
        public void ShouldReadEpisodeID()
        {
            Assert.AreEqual("PQS2E3", file.EpisodeID);
        }

        [Test]
        public void ShouldReadTVShow()
        {
            Assert.AreEqual("PuzzleQuest", file.TVShow);
        }

        [Test]
        public void ShouldReadTVNetwork()
        {
            Assert.IsNull(file.TVNetwork);
        }

        private string ComputeHash(Image image, ImageFormat format)
        {
            byte[] buffer;
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, format);
                buffer = stream.ToArray();
            }

            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(buffer);
            string hashString = BitConverter.ToString(hash).ToLower().Replace("-", string.Empty);
            return hashString;
        }
    }
}
